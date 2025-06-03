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

        #region Widoki


        private DataTable CreateHarmonogramTable(DataTable dtStops, int kierunek)
        {
            var stationRows = dtStops.AsEnumerable()
                .Select(r =>
                {
                    short rawId = r.Field<short>("IDPrzystanku");
                    string sId = rawId.ToString();      // np. "111", "234" itp.

                    // 1.b) Druga cyfra: (np. "111"[1] = '1' → 1):
                    int trainNum = (int)char.GetNumericValue(sId[1]);

                    // 1.c) Numer stacji (ostatnia cyfra):
                    int stopNum = rawId % 10;

                    // 1.d) Nazwa stacji:
                    string name = r.Field<string>("NazwaPrzystanku");

                    return new { RawId = rawId, TrainNum = trainNum, StopNum = stopNum, Name = name };
                })
                .Where(x => (x.TrainNum % 2) == kierunek)
                .Select(x => new { StopNum = x.StopNum, Name = x.Name })
                .Distinct()
                .OrderBy(x => kierunek == 1 ? x.StopNum : -x.StopNum)
                .ToList();

            // 2) Przygotowujemy schemat DataTable:
            var dt = new DataTable();
            dt.Columns.Add("Cykl", typeof(string));

            // 2.a) Dodajemy kolumny z nazwami stacji w odpowiedniej kolejności:
            var stationNames = stationRows.Select(x => x.Name).ToList();
            foreach (string name in stationNames)
            {
                dt.Columns.Add(name, typeof(string));
            }

            // 3) Wyciągamy unikalne cykle (pierwsza cyfra IDPrzystanku):
            var cycles = dtStops.AsEnumerable()
                .Select(r =>
                {
                    short rawId = r.Field<short>("IDPrzystanku");
                    string sId = rawId.ToString();     // np. "111", "234"
                                                       // Pierwsza cyfra:
                    int cycle = (int)char.GetNumericValue(sId[0]);
                    return cycle;
                })
                .Distinct()
                .OrderBy(c => c)
                .ToList();

            foreach (int c in cycles)
            {
                var rowArrivals = dt.NewRow();
                var rowDepartures = dt.NewRow();

                rowArrivals["Cykl"] = c.ToString();
                rowDepartures["Cykl"] = "";

                foreach (string station in stationNames)
                {

                    var matchedRows = dtStops.AsEnumerable()
                        .Where(r =>
                        {
                            short rawId = r.Field<short>("IDPrzystanku");
                            string sId = rawId.ToString();

                            // a) Pierwsza cyfra:
                            int cycleR = (int)char.GetNumericValue(sId[0]);
                            if (cycleR != c) return false;

                            // b) Druga cyfra (trainNum):
                            int trainNum = (int)char.GetNumericValue(sId[1]);
                            if ((trainNum % 2) != kierunek) return false;

                            // c) Nazwa stacji:
                            string nameR = r.Field<string>("NazwaPrzystanku");
                            return nameR == station;
                        })
                        .ToList();

                    if (matchedRows.Count == 1)
                    {
                        DataRow mr = matchedRows[0];

                        string przyjazd = mr.Field<TimeSpan>("Przyjazdy").ToString();
                        string odjazd = mr.Field<TimeSpan>("Odjazdy").ToString();

                        // Jeżeli jest "00:00:00", to wstaw "-":
                        if (przyjazd == "00:00:00") przyjazd = "-";
                        if (odjazd == "00:00:00") odjazd = "-";

                        rowArrivals[station] = przyjazd;
                        rowDepartures[station] = odjazd;
                    }
                    else
                    {
                        rowArrivals[station] = "-";
                        rowDepartures[station] = "-";
                    }
                }

                dt.Rows.Add(rowArrivals);
                dt.Rows.Add(rowDepartures);
            }

            return dt;
        }

        public void OpenKasowanieView()
        {
            panelContent.Controls.Clear();

            // 1) Tworzymy wrapper z 20px marginesem:
            var wrapper = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                BackColor = Color.FromArgb(30, 30, 30)
            };
            panelContent.Controls.Add(wrapper);

            int y = 20; // aktualna wysokość dla ustawiania kontrolek

            // 2) Label "Podaj numer biletu"
            var lblPrompt = new Label
            {
                Text = "Podaj numer biletu:",
                ForeColor = Color.White,
                Location = new Point(20, y),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            wrapper.Controls.Add(lblPrompt);

            // 3) TextBox z placeholderem "Podaj ID"
            txtDeleteID = new TextBox
            {
                PlaceholderText = "Podaj ID",
                Location = new Point(20, lblPrompt.Bottom + 5),
                Width = 200
            };
            wrapper.Controls.Add(txtDeleteID);

            // 4) Label błędu (na boku TextBoxa)
            lblErrorDelete = new Label
            {
                Text = "",
                ForeColor = Color.OrangeRed,
                Location = new Point(txtDeleteID.Right + 10, txtDeleteID.Top),
                AutoSize = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Italic)
            };
            wrapper.Controls.Add(lblErrorDelete);

            // 5) Przycisk "Skasuj bilet"
            btnSkasuj = new Button
            {
                Text = "Skasuj bilet",
                Location = new Point(20, txtDeleteID.Bottom + 20),
                Width = 120,
                Height = 35,
                BackColor = Color.FromArgb(45, 45, 45),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnSkasuj.Click += BtnSkasuj_Click;
            wrapper.Controls.Add(btnSkasuj);

            // 6) Label "Usuń bilet" (klikalny, szary, mniejsza czcionka)
            lblUsun = new Label
            {
                Text = "Usuń bilet",
                ForeColor = Color.Gray,
                Location = new Point(20, btnSkasuj.Bottom + 10),
                AutoSize = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                Cursor = Cursors.Hand
            };
            lblUsun.Click += LblUsun_Click;
            wrapper.Controls.Add(lblUsun);
        }

        private void OpenListaBiletowView()
        {
            panelContent.Controls.Clear();

            var dgv = dataGridViewBilety;
            dgv.DataSource = null;
            dgv.Columns.Clear();
            dgv.AllowUserToAddRows = false;

            // Konfiguracja wyglądu:
            dgv.BackgroundColor = Color.FromArgb(30, 30, 30);
            dgv.GridColor = Color.White;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(45, 45, 45);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.RowsDefaultCellStyle.BackColor = Color.FromArgb(35, 35, 35);
            dgv.RowsDefaultCellStyle.ForeColor = Color.White;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(30, 30, 30);
            dgv.AlternatingRowsDefaultCellStyle.ForeColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;

            panelContent.Controls.Add(dgv);

            LoadBiletyData(dgv);
        }

        private void OpenHarmonogramView()
        {
            panelContent.Controls.Clear();


            var wrapper = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20)
            };


            var tbl = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2
            };
            tbl.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tbl.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));

            // 2) Pobieramy wszystkie wiersze z tabeli Przystanki:
            DataTable dtStops = new DataTable();
            string connString = ConfigurationManager
                .ConnectionStrings["SystemOOKK_ConnectionString"]
                .ConnectionString;
            string query = "SELECT IDPrzystanku, NazwaPrzystanku, Przyjazdy, Odjazdy FROM Przystanki";
            using (SqlConnection conn = new SqlConnection(connString))
            using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
            {
                adapter.Fill(dtStops);
            }

            // 3) Budujemy pivotowane tabele:
            DataTable dtDir1 = CreateHarmonogramTable(dtStops, kierunek: 1);
            DataTable dtDir0 = CreateHarmonogramTable(dtStops, kierunek: 0);

            // 4A) Kierunek 1: Andrzejów → Emilianów
            var lbl1 = new Label
            {
                Text = "Kierunek: Andrzejów → Emilianów",
                ForeColor = Color.White,
                Dock = DockStyle.Top,
                Height = 30,
                Font = new Font("Segoe UI", 11, FontStyle.Bold)
            };
            var dgv1 = CreateStyledDataGridView();
            dgv1.DataSource = dtDir1;
            // Wyłączamy sortowanie i czyśćmy zaznaczenie:
            foreach (DataGridViewColumn col in dgv1.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            dgv1.ClearSelection();


            var panel1_kierunek = new Panel { Dock = DockStyle.Fill };
            panel1_kierunek.Controls.Add(dgv1);
            panel1_kierunek.Controls.Add(lbl1);


            var lbl0 = new Label
            {
                Text = "Kierunek: Emilianów → Andrzejów",
                ForeColor = Color.White,
                Dock = DockStyle.Top,
                Height = 30,
                Font = new Font("Segoe UI", 11, FontStyle.Bold)
            };
            var dgv0 = CreateStyledDataGridView();
            dgv0.DataSource = dtDir0;

            foreach (DataGridViewColumn col in dgv0.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            dgv0.ClearSelection();

            var panel0_kierunek = new Panel { Dock = DockStyle.Fill };
            panel0_kierunek.Controls.Add(dgv0);
            panel0_kierunek.Controls.Add(lbl0);


            tbl.Controls.Add(panel1_kierunek, 0, 0);
            tbl.Controls.Add(panel0_kierunek, 0, 1);


            wrapper.Controls.Add(tbl);


            panelContent.Controls.Add(wrapper);
        }

        private void OpenPociagiView()
        {
            panelContent.Controls.Clear();

            // Wrapper z 20px marginesu:
            var wrapper = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20)
            };

            // Tworzymy DataGridView w stylu „Listy biletów”:
            var dgv = CreateStyledDataGridView();

            // Ładujemy dane pociągów do dgv (z szerokościami i checkboxem):
            LoadPociagiData(dgv);

            // Dodajemy dgv do wrappera (z marginesem 20px):
            wrapper.Controls.Add(dgv);

            // Dodajemy wrapper do głównego panelContent:
            panelContent.Controls.Add(wrapper);
        }


        private void OpenMaszynisciView()
        {
            panelContent.Controls.Clear();
            var lbl = new Label()
            {
                Text = "Tutaj będzie widok: MASZYNISCI",
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 14, FontStyle.Bold)
            };
            panelContent.Controls.Add(lbl);
        }

        private void OpenKonduktorzyView()
        {
            panelContent.Controls.Clear();
            var lbl = new Label()
            {
                Text = "Tutaj będzie widok: KONDUKTORZY",
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 14, FontStyle.Bold)
            };
            panelContent.Controls.Add(lbl);
        }

        private void OpenKupicBiletView()
        {
            panelContent.Controls.Clear();

            // 1) Wrapper z 20px marginesu:
            var wrapper = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                BackColor = Color.FromArgb(30, 30, 30)
            };
            panelContent.Controls.Add(wrapper);

            // 2) Ładujemy tabelę Przystanki do dtPrzystanki_All (jeśli nie została jeszcze wczytana):
            if (dtPrzystanki_All == null)
            {
                dtPrzystanki_All = new DataTable();
                string connString = ConfigurationManager
                    .ConnectionStrings["SystemOOKK_ConnectionString"]
                    .ConnectionString;
                string query = "SELECT IDPrzystanku, NazwaPrzystanku, Przyjazdy, Odjazdy FROM Przystanki";
                using (var conn = new SqlConnection(connString))
                using (var adapter = new SqlDataAdapter(query, conn))
                {
                    adapter.Fill(dtPrzystanki_All);
                }
            }

            // 3) Etykieta i TextBox „Imię”
            var lblImie = new Label
            {
                Text = "Imię:",
                ForeColor = Color.White,
                Location = new Point(20, 20),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            wrapper.Controls.Add(lblImie);

            txtImie = new TextBox
            {
                PlaceholderText = "Podaj imię",
                Location = new Point(20, lblImie.Bottom + 5),
                Width = 200
            };
            wrapper.Controls.Add(txtImie);

            lblErrorImie = new Label
            {
                Text = "",
                ForeColor = Color.OrangeRed,
                Location = new Point(txtImie.Right + 10, txtImie.Top),
                AutoSize = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Italic)
            };
            wrapper.Controls.Add(lblErrorImie);

            // 4) Etykieta i TextBox „Nazwisko”
            var lblNazwisko = new Label
            {
                Text = "Nazwisko:",
                ForeColor = Color.White,
                Location = new Point(txtImie.Right + 150, lblImie.Top),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            wrapper.Controls.Add(lblNazwisko);

            txtNazwisko = new TextBox
            {
                PlaceholderText = "Podaj nazwisko",
                Location = new Point(lblNazwisko.Left, lblNazwisko.Bottom + 5),
                Width = 200
            };
            wrapper.Controls.Add(txtNazwisko);

            lblErrorNazwisko = new Label
            {
                Text = "",
                ForeColor = Color.OrangeRed,
                Location = new Point(txtNazwisko.Right + 10, txtNazwisko.Top),
                AutoSize = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Italic)
            };
            wrapper.Controls.Add(lblErrorNazwisko);

            // 5) Przystanek startowy
            var lblStart = new Label
            {
                Text = "Przystanek startowy:",
                ForeColor = Color.White,
                Location = new Point(20, txtImie.Bottom + 40),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            wrapper.Controls.Add(lblStart);

            cmbStartPrzystanek = new ComboBox
            {
                Location = new Point(20, lblStart.Bottom + 5),
                Width = 200,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            wrapper.Controls.Add(cmbStartPrzystanek);

            // 6) Etykieta „Wybierz godzinę odjazdu” pod cmbStartPrzystanek
            var lblStartGodz = new Label
            {
                Text = "Wybierz godzinę odjazdu:",
                ForeColor = Color.White,
                Location = new Point(20, cmbStartPrzystanek.Bottom + 10),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            wrapper.Controls.Add(lblStartGodz);

            cmbStartGodzina = new ComboBox
            {
                Location = new Point(20, lblStartGodz.Bottom + 5),
                Width = 120,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Enabled = false // do odblokowania, gdy będzie wybór stacji finalnej
            };
            wrapper.Controls.Add(cmbStartGodzina);

            // 7) Przystanek końcowy
            var lblStop = new Label
            {
                Text = "Przystanek końcowy:",
                ForeColor = Color.White,
                Location = new Point(cmbStartPrzystanek.Right + 300, lblStart.Top),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            wrapper.Controls.Add(lblStop);

            cmbStopPrzystanek = new ComboBox
            {
                Location = new Point(lblStop.Left, lblStop.Bottom + 5),
                Width = 200,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Enabled = false // do odblokowania, gdy będzie wybór startu
            };
            wrapper.Controls.Add(cmbStopPrzystanek);

            // 8) Etykieta „Godzina przyjazdu” (będzie uzupełniana automatycznie, zamiast ComboBox’a)
            var lblArrivalStatic = new Label
            {
                Text = "Godzina przyjazdu:",
                ForeColor = Color.White,
                Location = new Point(cmbStopPrzystanek.Left, cmbStopPrzystanek.Bottom + 10),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            wrapper.Controls.Add(lblArrivalStatic);

            lblArrivalTime = new Label
            {
                Text = "-", // domyślnie puste
                ForeColor = Color.White,
                Location = new Point(cmbStopPrzystanek.Left, lblArrivalStatic.Bottom + 5),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular)
            };
            wrapper.Controls.Add(lblArrivalTime);

            // 9) „Ulga”
            var lblUlga = new Label
            {
                Text = "Ulga:",
                ForeColor = Color.White,
                Location = new Point(20, cmbStartGodzina.Bottom + 40),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            wrapper.Controls.Add(lblUlga);

            chkUlga = new CheckBox
            {
                Location = new Point(lblUlga.Right + 10, lblUlga.Top),
                AutoSize = true
            };
            wrapper.Controls.Add(chkUlga);

            // 10) „Cena biletu: 0 zł”
            lblCena = new Label
            {
                Text = "Cena biletu: 0 zł",
                ForeColor = Color.White,
                Location = new Point(20, lblUlga.Bottom + 20),
                AutoSize = true,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold)
            };
            wrapper.Controls.Add(lblCena);

            // 11) Przycisk „Kupić bilet”
            btnKupBilet = new Button
            {
                Text = "Kupić bilet",
                Location = new Point(20, lblCena.Bottom + 20),
                Width = 150,
                Height = 35,
                BackColor = Color.FromArgb(45, 45, 45),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnKupBilet.Click += BtnKupBilet_Click;
            wrapper.Controls.Add(btnKupBilet);

            // ——— Wypełnienie danych do cmbStartPrzystanek z bazy ———
            var stations = dtPrzystanki_All.AsEnumerable()
                .Select(r => new
                {
                    Name = r.Field<string>("NazwaPrzystanku"),
                    Id = r.Field<short>("IDPrzystanku")
                })
                .GroupBy(x => x.Name)
                .Select(g => g.First())
                .OrderBy(x => x.Id % 10)
                .ToList();

            cmbStartPrzystanek.Items.AddRange(stations.Select(s => s.Name).ToArray());
            cmbStartPrzystanek.SelectedIndexChanged += CmbStartPrzystanek_SelectedIndexChanged;
            cmbStopPrzystanek.SelectedIndexChanged += CmbStopPrzystanek_SelectedIndexChanged;
            cmbStartGodzina.SelectedIndexChanged += CmbStartGodzina_SelectedIndexChanged;
            chkUlga.CheckedChanged += ChkUlga_CheckedChanged;
        }

        private void OpenMapView()
        {
            panelContent.Controls.Clear();

            // 1) Panel wrapper z 20px marginesu:
            var wrapper = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                BackColor = Color.FromArgb(30, 30, 30)
            };

            // 2) Tworzymy PictureBox, który wypełni całą dostępną przestrzeń w wrapperze
            var picture = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom  // automatycznie dostosuje rozmiar, zachowując proporcje
            };

            // 3) Budujemy ścieżkę do folderu projektu (idziemy 3 poziomy „w górę” z bin/Debug/...):
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string projectDir = Path.GetFullPath(Path.Combine(baseDir, "..", "..", ".."));
            string imagePath = Path.Combine(projectDir, "images", "map.jpg");

            if (File.Exists(imagePath))
            {
                // 4) Jeśli plik istnieje, wczytujemy go do PictureBox
                picture.Image = Image.FromFile(imagePath);
            }
            else
            {
                // 5) Gdyby pliku nie było, wyświetlamy stosowny komunikat
                var lblError = new Label
                {
                    Text = "Nie znaleziono pliku mapy:\n" + imagePath,
                    ForeColor = Color.Red,
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 11, FontStyle.Bold)
                };
                wrapper.Controls.Add(lblError);
                panelContent.Controls.Add(wrapper);
                return;
            }

            // 6) Dodajemy PictureBox do wrappera
            wrapper.Controls.Add(picture);

            // 7) Dodajemy wrapper do panelContent
            panelContent.Controls.Add(wrapper);
        }

        #endregion

    }
}
