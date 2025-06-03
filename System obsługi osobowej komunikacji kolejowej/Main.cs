using System;
using System.Collections.Generic;
using System.Configuration;   
using System.Data;
using Microsoft.Data.SqlClient; 
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace System_obsługi_osobowej_komunikacji_kolejowej
{
    public partial class fon : Form
    {

        private readonly Color defaultButtonColor = Color.FromArgb(45, 45, 45);
        private readonly Color selectedButtonColor = Color.FromArgb(70, 70, 70);

        private const int MaxTotalWidth = 940;

        private List<Button> menuButtons;

        public fon()
        {
            InitializeComponent();

            // Utworzenie listy wszystkich przycisków na potrzeby resetowania kolorów:
            menuButtons = new List<Button>()
            {
                btnKasowanie,
                btnLista,
                btnHarmonogram,
                btnPociagi,
            };

            ActivateButton(btnKasowanie);
            OpenKasowanieView();

            // Na dobry początek ustawmy od razu aktualny czas w Clock:
            Clock.Text = DateTime.Now.ToString("HH:mm:ss   dd.MM.yyyy");
        }

        private DataTable dtPrzystanki_All;

        private TextBox txtImie;
        private TextBox txtNazwisko;
        private Label lblErrorImie;
        private Label lblErrorNazwisko;

        private ComboBox cmbStartPrzystanek;
        private ComboBox cmbStopPrzystanek;

        private ComboBox cmbStartGodzina;
        private Label lblArrivalTime; // zamiast ComboBox
        private CheckBox chkUlga;
        private Label lblCena;


        private Button btnKupBilet;

        private DataGridView CreateStyledDataGridView()
        {
            var dgv = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.FromArgb(30, 30, 30),
                GridColor = Color.White,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                EnableHeadersVisualStyles = false,
                BorderStyle = BorderStyle.None // brak zewnętrznej ramki
            };

            // --- Styl nagłówków ---------------------------------------------
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(45, 45, 45);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;

            // Wyłączamy sortowanie w nagłówkach:
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = dgv.ColumnHeadersDefaultCellStyle.BackColor;
            dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = dgv.ColumnHeadersDefaultCellStyle.ForeColor;

            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            // --- Styl wierszy danych -----------------------------------------
            dgv.RowsDefaultCellStyle.BackColor = Color.FromArgb(35, 35, 35);
            dgv.RowsDefaultCellStyle.ForeColor = Color.White;
            dgv.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgv.RowsDefaultCellStyle.SelectionBackColor = dgv.RowsDefaultCellStyle.BackColor;
            dgv.RowsDefaultCellStyle.SelectionForeColor = dgv.RowsDefaultCellStyle.ForeColor;

            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(30, 30, 30);
            dgv.AlternatingRowsDefaultCellStyle.ForeColor = Color.White;
            dgv.AlternatingRowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.AlternatingRowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgv.AlternatingRowsDefaultCellStyle.SelectionBackColor = dgv.AlternatingRowsDefaultCellStyle.BackColor;
            dgv.AlternatingRowsDefaultCellStyle.SelectionForeColor = dgv.AlternatingRowsDefaultCellStyle.ForeColor;


            return dgv;
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

        private void move_on_clock(object sender, MouseEventArgs e)
        {
            Clock.Capture = false;
            Message m = Message.Create(Handle, 0xA1, new IntPtr(2), IntPtr.Zero);
            WndProc(ref m);
        }

        private void ActivateButton(Button btn)
        {
            foreach (var b in menuButtons)
            {
                b.BackColor = defaultButtonColor;
            }
            btn.BackColor = selectedButtonColor;
        }

        private TextBox txtDeleteID;
        private Label lblErrorDelete;
        private Button btnSkasuj;
        private Label lblUsun;



        #region Obsługa timera daty i czasu

        private void timerDateTime_Tick(object sender, EventArgs e)
        {
            Clock.Text = DateTime.Now.ToString("HH:mm:ss   dd.MM.yyyy");
        }

        #endregion


    }
}
