using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace System_obsługi_osobowej_komunikacji_kolejowej
{
    public partial class fon : Form
    {
        // Kolory używane do podświetlania:
        private readonly Color defaultButtonColor = Color.FromArgb(45, 45, 45);
        private readonly Color selectedButtonColor = Color.FromArgb(70, 70, 70);

        // Lista wszystkich przycisków menu, aby łatwo je resetować
        private List<Button> menuButtons;

        public fon()
        {
            InitializeComponent();

            // Utworzenie listy wszystkich przycisków na potrzeby resetowania kolorów:
            menuButtons = new List<Button>()
            {
                btnRezerwacja,
                btnLista,
                btnPrzystanki,
                btnPociagi,
                btnMaszynisci,
                btnKonduktorzy
            };

            // Domyślnie uruchamiamy aplikację z zakładką "Rezerwacja biletów" aktywną:
            ActivateButton(btnRezerwacja);
            OpenRezerwacjaView();
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void minimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void move(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                panel1.Capture = false;
                Message m = Message.Create(Handle, 0xA1, new IntPtr(2), IntPtr.Zero);
                WndProc(ref m);
            }
        }

        /// <summary>
        /// Ustawia dany przycisk jako "aktywny" (zmienia jego BackColor) i resetuje pozostałe.
        /// </summary>
        private void ActivateButton(Button btn)
        {
            // Reset kolorów wszystkich przycisków:
            foreach (var b in menuButtons)
            {
                b.BackColor = defaultButtonColor;
            }
            // Ustaw wybrany przycisk na kolor "selected"
            btn.BackColor = selectedButtonColor;
        }

        #region Event Handlery przycisków menu

        private void btnRezerwacja_Click(object sender, EventArgs e)
        {
            ActivateButton(btnRezerwacja);
            OpenRezerwacjaView();
        }

        private void btnLista_Click(object sender, EventArgs e)
        {
            ActivateButton(btnLista);
            OpenListaBiletowView();
        }

        private void btnPrzystanki_Click(object sender, EventArgs e)
        {
            ActivateButton(btnPrzystanki);
            OpenPrzystankiView();
        }

        private void btnPociagi_Click(object sender, EventArgs e)
        {
            ActivateButton(btnPociagi);
            OpenPociagiView();
        }

        private void btnMaszynisci_Click(object sender, EventArgs e)
        {
            ActivateButton(btnMaszynisci);
            OpenMaszynisciView();
        }

        private void btnKonduktorzy_Click(object sender, EventArgs e)
        {
            ActivateButton(btnKonduktorzy);
            OpenKonduktorzyView();
        }

        #endregion

        #region Metody “otwierające” podwidoki (na razie tylko zwierciadła)

        // Każda z poniższych metod czyści panelContent i może w przyszłości wczytać
        // odpowiedni UserControl / DataGridView / cokolwiek chcesz.

        private void OpenRezerwacjaView()
        {
            panelContent.Controls.Clear();
            // Tutaj możesz wczytać np. UserControlRezerwacja lub formularz z formularzem rezerwacji.
            // Na razie można dodać proste Label, żeby było widać różnicę:
            var lbl = new Label()
            {
                Text = "Tutaj będzie widok: REZERWACJA BILETÓW",
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 14, FontStyle.Bold)
            };
            panelContent.Controls.Add(lbl);
        }

        private void OpenListaBiletowView()
        {
            panelContent.Controls.Clear();
            var lbl = new Label()
            {
                Text = "Tutaj będzie widok: LISTA BILETÓW",
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 14, FontStyle.Bold)
            };
            panelContent.Controls.Add(lbl);
        }

        private void OpenPrzystankiView()
        {
            panelContent.Controls.Clear();
            var lbl = new Label()
            {
                Text = "Tutaj będzie widok: PRZYSTANKI",
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 14, FontStyle.Bold)
            };
            panelContent.Controls.Add(lbl);
        }

        private void OpenPociagiView()
        {
            panelContent.Controls.Clear();
            var lbl = new Label()
            {
                Text = "Tutaj będzie widok: POCIĄGI",
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 14, FontStyle.Bold)
            };
            panelContent.Controls.Add(lbl);
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

        #endregion
    }
}
