namespace System_obsługi_osobowej_komunikacji_kolejowej
{
    partial class fon
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fon));
            panel1 = new Panel();
            navigacion_panel = new Panel();
            minimize = new PictureBox();
            close = new PictureBox();
            panelMenu = new Panel();
            btnKonduktorzy = new Button();
            btnMaszynisci = new Button();
            btnPociagi = new Button();
            btnPrzystanki = new Button();
            btnLista = new Button();
            btnRezerwacja = new Button();
            panelContent = new Panel();
            panel1.SuspendLayout();
            navigacion_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)minimize).BeginInit();
            ((System.ComponentModel.ISupportInitialize)close).BeginInit();
            panelMenu.SuspendLayout();
            SuspendLayout();
            // 
            // panel1 (górny pasek – przenoszenie okna + minimalizuj/zamknij)
            // 
            panel1.BackColor = Color.FromArgb(35, 35, 35);
            panel1.Controls.Add(navigacion_panel);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1200, 90);
            panel1.TabIndex = 0;
            panel1.MouseMove += move;
            // 
            // navigacion_panel (przyciski minimalizuj/zamknij)
            // 
            navigacion_panel.AutoScrollMargin = new Size(20, 20);
            navigacion_panel.Controls.Add(minimize);
            navigacion_panel.Controls.Add(close);
            navigacion_panel.Location = new Point(1060, 20);
            navigacion_panel.Margin = new Padding(20, 20, 3, 3);
            navigacion_panel.Name = "navigacion_panel";
            navigacion_panel.Size = new Size(120, 50);
            navigacion_panel.TabIndex = 1;
            // 
            // minimize (ikonka minimalizacji)
            // 
            minimize.Cursor = Cursors.Hand;
            minimize.Image = (Image)resources.GetObject("minimize.Image");
            minimize.Location = new Point(0, 0);
            minimize.Name = "minimize";
            minimize.Size = new Size(50, 50);
            minimize.SizeMode = PictureBoxSizeMode.Zoom;
            minimize.TabIndex = 2;
            minimize.TabStop = false;
            minimize.Click += minimize_Click;
            // 
            // close (ikonka zamknięcia)
            // 
            close.Cursor = Cursors.Hand;
            close.Image = (Image)resources.GetObject("close.Image");
            close.Location = new Point(70, 0);
            close.Name = "close";
            close.Size = new Size(50, 50);
            close.SizeMode = PictureBoxSizeMode.Zoom;
            close.TabIndex = 3;
            close.TabStop = false;
            close.Click += close_Click;
            // 
            // panelMenu (lewy panel nawigacyjny)
            // 
            panelMenu.BackColor = Color.FromArgb(45, 45, 45);
            panelMenu.Controls.Add(btnKonduktorzy);
            panelMenu.Controls.Add(btnMaszynisci);
            panelMenu.Controls.Add(btnPociagi);
            panelMenu.Controls.Add(btnPrzystanki);
            panelMenu.Controls.Add(btnLista);
            panelMenu.Controls.Add(btnRezerwacja);
            panelMenu.Dock = DockStyle.Left;
            panelMenu.Location = new Point(0, 90);
            panelMenu.Name = "panelMenu";
            panelMenu.Size = new Size(200, 560);
            panelMenu.TabIndex = 4;
            // 
            // btnRezerwacja
            // 
            btnRezerwacja.Cursor = Cursors.Hand;
            btnRezerwacja.Dock = DockStyle.Top;
            btnRezerwacja.FlatAppearance.BorderSize = 0;
            btnRezerwacja.FlatStyle = FlatStyle.Flat;
            btnRezerwacja.Font = new Font("Tahoma", 10.2F, FontStyle.Bold);
            btnRezerwacja.ForeColor = Color.White;
            btnRezerwacja.BackColor = Color.FromArgb(45, 45, 45); // kolor domyślny
            btnRezerwacja.Location = new Point(0, 0);
            btnRezerwacja.Name = "btnRezerwacja";
            btnRezerwacja.Padding = new Padding(10, 0, 0, 0);
            btnRezerwacja.Size = new Size(200, 50);
            btnRezerwacja.TabIndex = 0;
            btnRezerwacja.Text = "Rezerwacja biletów";
            btnRezerwacja.TextAlign = ContentAlignment.MiddleLeft;
            btnRezerwacja.UseVisualStyleBackColor = true;
            btnRezerwacja.Click += btnRezerwacja_Click;
            // 
            // btnLista
            // 
            btnLista.Cursor = Cursors.Hand;
            btnLista.Dock = DockStyle.Top;
            btnLista.FlatAppearance.BorderSize = 0;
            btnLista.FlatStyle = FlatStyle.Flat;
            btnLista.Font = new Font("Tahoma", 10.2F, FontStyle.Bold);
            btnLista.ForeColor = Color.White;
            btnLista.BackColor = Color.FromArgb(45, 45, 45);
            btnLista.Location = new Point(0, 50);
            btnLista.Name = "btnLista";
            btnLista.Padding = new Padding(10, 0, 0, 0);
            btnLista.Size = new Size(200, 50);
            btnLista.TabIndex = 1;
            btnLista.Text = "Lista biletów";
            btnLista.TextAlign = ContentAlignment.MiddleLeft;
            btnLista.UseVisualStyleBackColor = true;
            btnLista.Click += btnLista_Click;
            // 
            // btnPrzystanki
            // 
            btnPrzystanki.Cursor = Cursors.Hand;
            btnPrzystanki.Dock = DockStyle.Top;
            btnPrzystanki.FlatAppearance.BorderSize = 0;
            btnPrzystanki.FlatStyle = FlatStyle.Flat;
            btnPrzystanki.Font = new Font("Tahoma", 10.2F, FontStyle.Bold);
            btnPrzystanki.ForeColor = Color.White;
            btnPrzystanki.BackColor = Color.FromArgb(45, 45, 45);
            btnPrzystanki.Location = new Point(0, 100);
            btnPrzystanki.Name = "btnPrzystanki";
            btnPrzystanki.Padding = new Padding(10, 0, 0, 0);
            btnPrzystanki.Size = new Size(200, 50);
            btnPrzystanki.TabIndex = 2;
            btnPrzystanki.Text = "Przystanki";
            btnPrzystanki.TextAlign = ContentAlignment.MiddleLeft;
            btnPrzystanki.UseVisualStyleBackColor = true;
            btnPrzystanki.Click += btnPrzystanki_Click;
            // 
            // btnPociagi
            // 
            btnPociagi.Cursor = Cursors.Hand;
            btnPociagi.Dock = DockStyle.Top;
            btnPociagi.FlatAppearance.BorderSize = 0;
            btnPociagi.FlatStyle = FlatStyle.Flat;
            btnPociagi.Font = new Font("Tahoma", 10.2F, FontStyle.Bold);
            btnPociagi.ForeColor = Color.White;
            btnPociagi.BackColor = Color.FromArgb(45, 45, 45);
            btnPociagi.Location = new Point(0, 150);
            btnPociagi.Name = "btnPociagi";
            btnPociagi.Padding = new Padding(10, 0, 0, 0);
            btnPociagi.Size = new Size(200, 50);
            btnPociagi.TabIndex = 3;
            btnPociagi.Text = "Pociągi";
            btnPociagi.TextAlign = ContentAlignment.MiddleLeft;
            btnPociagi.UseVisualStyleBackColor = true;
            btnPociagi.Click += btnPociagi_Click;
            // 
            // btnMaszynisci
            // 
            btnMaszynisci.Cursor = Cursors.Hand;
            btnMaszynisci.Dock = DockStyle.Top;
            btnMaszynisci.FlatAppearance.BorderSize = 0;
            btnMaszynisci.FlatStyle = FlatStyle.Flat;
            btnMaszynisci.Font = new Font("Tahoma", 10.2F, FontStyle.Bold);
            btnMaszynisci.ForeColor = Color.White;
            btnMaszynisci.BackColor = Color.FromArgb(45, 45, 45);
            btnMaszynisci.Location = new Point(0, 200);
            btnMaszynisci.Name = "btnMaszynisci";
            btnMaszynisci.Padding = new Padding(10, 0, 0, 0);
            btnMaszynisci.Size = new Size(200, 50);
            btnMaszynisci.TabIndex = 4;
            btnMaszynisci.Text = "Maszyniści";
            btnMaszynisci.TextAlign = ContentAlignment.MiddleLeft;
            btnMaszynisci.UseVisualStyleBackColor = true;
            btnMaszynisci.Click += btnMaszynisci_Click;
            // 
            // btnKonduktorzy
            // 
            btnKonduktorzy.Cursor = Cursors.Hand;
            btnKonduktorzy.Dock = DockStyle.Top;
            btnKonduktorzy.FlatAppearance.BorderSize = 0;
            btnKonduktorzy.FlatStyle = FlatStyle.Flat;
            btnKonduktorzy.Font = new Font("Tahoma", 10.2F, FontStyle.Bold);
            btnKonduktorzy.ForeColor = Color.White;
            btnKonduktorzy.BackColor = Color.FromArgb(45, 45, 45);
            btnKonduktorzy.Location = new Point(0, 250);
            btnKonduktorzy.Name = "btnKonduktorzy";
            btnKonduktorzy.Padding = new Padding(10, 0, 0, 0);
            btnKonduktorzy.Size = new Size(200, 50);
            btnKonduktorzy.TabIndex = 5;
            btnKonduktorzy.Text = "Konduktorzy";
            btnKonduktorzy.TextAlign = ContentAlignment.MiddleLeft;
            btnKonduktorzy.UseVisualStyleBackColor = true;
            btnKonduktorzy.Click += btnKonduktorzy_Click;
            // 
            // panelContent (główny obszar na dane)
            // 
            panelContent.BackColor = Color.FromArgb(30, 30, 30);
            panelContent.Dock = DockStyle.Fill;
            panelContent.Location = new Point(200, 90);
            panelContent.Name = "panelContent";
            panelContent.Size = new Size(1000, 560);
            panelContent.TabIndex = 5;
            // 
            // fon (główny formularz)
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 30, 30);
            ClientSize = new Size(1200, 650);
            Controls.Add(panelContent);
            Controls.Add(panelMenu);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "fon";
            Text = "System Obsługi OOKK";
            panel1.ResumeLayout(false);
            navigacion_panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)minimize).EndInit();
            ((System.ComponentModel.ISupportInitialize)close).EndInit();
            panelMenu.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        // Istniejące kontrolki:
        private Panel panel1;
        private Panel navigacion_panel;
        private PictureBox close;
        private PictureBox minimize;

        // Nowe kontrolki:
        private Panel panelMenu;
        private Button btnRezerwacja;
        private Button btnLista;
        private Button btnPrzystanki;
        private Button btnPociagi;
        private Button btnMaszynisci;
        private Button btnKonduktorzy;

        private Panel panelContent;
    }
}
