using System;
using System.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace System_obsługi_osobowej_komunikacji_kolejowej
{
    public partial class fon
    {

        #region Przyciski

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void minimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnKasowanie_Click(object sender, EventArgs e)
        {
            ActivateButton(btnKasowanie);
            OpenKasowanieView();
        }

        private void btnLista_Click(object sender, EventArgs e)
        {
            ActivateButton(btnLista);
            OpenListaBiletowView();
        }

        private void btnHarmonogram_Click(object sender, EventArgs e)
        {
            ActivateButton(btnHarmonogram);
            OpenHarmonogramView();
        }

        private void btnPociagi_Click(object sender, EventArgs e)
        {
            ActivateButton(btnPociagi);
            OpenPociagiView();
        }


        private void btnKupicBilet_Click(object sender, EventArgs e)
        {
            ActivateButton(btnKupicBilet);
            OpenKupicBiletView();
        }

        private void icomap_Click(object sender, EventArgs e)
        {
            OpenMapView();
        }

        private void BtnSkasuj_Click(object sender, EventArgs e)
        {
            lblErrorDelete.Text = "";
            string rawId = txtDeleteID.Text.Trim();

            // 1) Walidacja: sprawdź, czy pole to wyłącznie cyfry
            if (!Regex.IsMatch(rawId, @"^\d+$"))
            {
                lblErrorDelete.Text = "Wprowadź poprawny ID";
                return;
            }

            int idBiletu = int.Parse(rawId);

            try
            {
                string connString = ConfigurationManager
                    .ConnectionStrings["SystemOOKK_ConnectionString"]
                    .ConnectionString;

                using (var conn = new SqlConnection(connString))
                {
                    conn.Open();

                    // 2) Pobierz aktualny stan pola Wykorzystany dla tego biletu:
                    string selectQuery = @"
                        SELECT Wykorzystany 
                        FROM Bilety 
                        WHERE IDBiletu = @IDBiletu;
                    ";
                    int wykorzystanyValue;
                    using (var cmdSelect = new SqlCommand(selectQuery, conn))
                    {
                        cmdSelect.Parameters.AddWithValue("@IDBiletu", idBiletu);
                        var result = cmdSelect.ExecuteScalar();
                        if (result == null)
                        {
                            MessageBox.Show(
                                $"Nie znaleziono biletu o ID = {idBiletu}.",
                                "Brak biletu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        wykorzystanyValue = Convert.ToInt32(result);
                    }

                    // 3) Jeśli już wykorzystany → wyświetl komunikat i przerwij:
                    if (wykorzystanyValue == 1)
                    {
                        MessageBox.Show(
                            "Ten bilet był już oznaczony jako wykorzystany.",
                            "Bilet wykorzystany", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    // 4) W przeciwnym razie zaktualizuj Wykorzystany = 1:
                    string updateQuery = @"
                        UPDATE Bilety 
                        SET Wykorzystany = 1 
                        WHERE IDBiletu = @IDBiletu;
                    ";
                    using (var cmdUpdate = new SqlCommand(updateQuery, conn))
                    {
                        cmdUpdate.Parameters.AddWithValue("@IDBiletu", idBiletu);
                        cmdUpdate.ExecuteNonQuery();
                    }

                    MessageBox.Show(
                        "Bilet został oznaczony jako wykorzystany.",
                        "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtDeleteID.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Wystąpił błąd podczas kasowania biletu:\n{ex.Message}",
                    "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LblUsun_Click(object sender, EventArgs e)
        {
            lblErrorDelete.Text = "";
            string textId = txtDeleteID.Text.Trim();

            // 1) Walidacja: pole musi być tylko cyframi
            if (!Regex.IsMatch(textId, @"^\d+$"))
            {
                lblErrorDelete.Text = "Wprowadź poprawny ID";
                return;
            }

            int idBiletu = int.Parse(textId);

            try
            {
                string connString = ConfigurationManager
                    .ConnectionStrings["SystemOOKK_ConnectionString"]
                    .ConnectionString;

                using (var conn = new SqlConnection(connString))
                {
                    conn.Open();

                    // 2) Najpierw USUWAMY wiersz z tabeli Bilety (dziecko):
                    string deleteBilet = @"
                        DELETE FROM Bilety
                        WHERE IDBiletu = @IDBiletu;
                    ";
                    using (var cmdBilet = new SqlCommand(deleteBilet, conn))
                    {
                        cmdBilet.Parameters.AddWithValue("@IDBiletu", idBiletu);
                        int rowsB = cmdBilet.ExecuteNonQuery();

                        if (rowsB == 0)
                        {
                            MessageBox.Show(
                                $"Nie znaleziono biletu o ID = {idBiletu}.",
                                "Brak biletu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        // (jeśli rowsB > 0, usunął przynajmniej 1 wiersz)
                    }

                    // 3) Dopiero teraz usuwamy powiązany rekord z tabeli Pasażerowie (rodzic):
                    string deletePasazer = @"
                        DELETE FROM Pasażerowie
                        WHERE IDBiletu = @IDBiletu;
                    ";
                    using (var cmdPasazer = new SqlCommand(deletePasazer, conn))
                    {
                        cmdPasazer.Parameters.AddWithValue("@IDBiletu", idBiletu);
                        cmdPasazer.ExecuteNonQuery();
                    }

                    MessageBox.Show(
                        "Bilet wraz z danymi pasażera został usunięty.",
                        "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtDeleteID.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Wystąpił błąd podczas usuwania biletu:\n{ex.Message}",
                    "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        #endregion

    }
}