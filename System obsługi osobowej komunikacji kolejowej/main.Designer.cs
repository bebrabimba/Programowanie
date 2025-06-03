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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fon));
            panel1 = new Panel();
            icomap = new PictureBox();
            Clock = new Label();
            navigacion_panel = new Panel();
            minimize = new PictureBox();
            close = new PictureBox();
            panelMenu = new Panel();
            btnKupicBilet = new Button();
            btnPociagi = new Button();
            btnHarmonogram = new Button();
            btnLista = new Button();
            btnKasowanie = new Button();
            panelContent = new Panel();
            dataGridViewBilety = new DataGridView();
            timerDateTime = new System.Windows.Forms.Timer(components);
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)icomap).BeginInit();
            navigacion_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)minimize).BeginInit();
            ((System.ComponentModel.ISupportInitialize)close).BeginInit();
            panelMenu.SuspendLayout();
            panelContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewBilety).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(35, 35, 35);
            panel1.Controls.Add(icomap);
            panel1.Controls.Add(Clock);
            panel1.Controls.Add(navigacion_panel);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1200, 90);
            panel1.TabIndex = 0;
            panel1.MouseMove += move;
            // 
            // icomap
            // 
            icomap.Cursor = Cursors.Hand;
            icomap.Image = (Image)resources.GetObject("icomap.Image");
            icomap.Location = new Point(240, 20);
            icomap.Name = "icomap";
            icomap.Size = new Size(50, 50);
            icomap.SizeMode = PictureBoxSizeMode.Zoom;
            icomap.TabIndex = 100;
            icomap.TabStop = false;
            icomap.Click += icomap_Click;
            // 
            // Clock
            // 
            Clock.AutoSize = true;
            Clock.Font = new Font("Tahoma", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 204);
            Clock.ForeColor = Color.White;
            Clock.Location = new Point(20, 35);
            Clock.Name = "Clock";
            Clock.Size = new Size(201, 21);
            Clock.TabIndex = 99;
            Clock.Text = "00:00:00   00.00.0000";
            Clock.MouseMove += move_on_clock;
            // 
            // navigacion_panel
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
            // minimize
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
            // close
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
            // panelMenu
            // 
            panelMenu.BackColor = Color.FromArgb(45, 45, 45);
            panelMenu.Controls.Add(btnKupicBilet);
            panelMenu.Controls.Add(btnPociagi);
            panelMenu.Controls.Add(btnHarmonogram);
            panelMenu.Controls.Add(btnLista);
            panelMenu.Controls.Add(btnKasowanie);
            panelMenu.Dock = DockStyle.Left;
            panelMenu.Location = new Point(0, 90);
            panelMenu.Name = "panelMenu";
            panelMenu.Size = new Size(220, 560);
            panelMenu.TabIndex = 4;
            // 
            // btnKupicBilet
            // 
            btnKupicBilet.BackColor = Color.FromArgb(75, 75, 75);
            btnKupicBilet.Cursor = Cursors.Hand;
            btnKupicBilet.FlatAppearance.BorderSize = 0;
            btnKupicBilet.FlatStyle = FlatStyle.Flat;
            btnKupicBilet.Font = new Font("Tahoma", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnKupicBilet.ForeColor = Color.White;
            btnKupicBilet.Location = new Point(0, 480);
            btnKupicBilet.Name = "btnKupicBilet";
            btnKupicBilet.Padding = new Padding(20, 0, 20, 0);
            btnKupicBilet.Size = new Size(220, 60);
            btnKupicBilet.TabIndex = 6;
            btnKupicBilet.Text = "Kupić bilet";
            btnKupicBilet.UseVisualStyleBackColor = false;
            btnKupicBilet.Click += btnKupicBilet_Click;
            // 
            // btnPociagi
            // 
            btnPociagi.BackColor = Color.FromArgb(45, 45, 45);
            btnPociagi.Cursor = Cursors.Hand;
            btnPociagi.FlatAppearance.BorderSize = 0;
            btnPociagi.FlatStyle = FlatStyle.Flat;
            btnPociagi.Font = new Font("Tahoma", 10.2F, FontStyle.Bold);
            btnPociagi.ForeColor = Color.White;
            btnPociagi.Location = new Point(0, 180);
            btnPociagi.Name = "btnPociagi";
            btnPociagi.Padding = new Padding(20, 0, 0, 0);
            btnPociagi.Size = new Size(220, 60);
            btnPociagi.TabIndex = 3;
            btnPociagi.Text = "Pociągi";
            btnPociagi.TextAlign = ContentAlignment.MiddleLeft;
            btnPociagi.UseVisualStyleBackColor = true;
            btnPociagi.Click += btnPociagi_Click;
            // 
            // btnHarmonogram
            // 
            btnHarmonogram.BackColor = Color.FromArgb(45, 45, 45);
            btnHarmonogram.Cursor = Cursors.Hand;
            btnHarmonogram.FlatAppearance.BorderSize = 0;
            btnHarmonogram.FlatStyle = FlatStyle.Flat;
            btnHarmonogram.Font = new Font("Tahoma", 10.2F, FontStyle.Bold);
            btnHarmonogram.ForeColor = Color.White;
            btnHarmonogram.Location = new Point(0, 120);
            btnHarmonogram.Name = "btnHarmonogram";
            btnHarmonogram.Padding = new Padding(20, 0, 0, 0);
            btnHarmonogram.Size = new Size(220, 60);
            btnHarmonogram.TabIndex = 2;
            btnHarmonogram.Text = "Harmonogram";
            btnHarmonogram.TextAlign = ContentAlignment.MiddleLeft;
            btnHarmonogram.UseVisualStyleBackColor = true;
            btnHarmonogram.Click += btnHarmonogram_Click;
            // 
            // btnLista
            // 
            btnLista.BackColor = Color.FromArgb(45, 45, 45);
            btnLista.Cursor = Cursors.Hand;
            btnLista.FlatAppearance.BorderSize = 0;
            btnLista.FlatStyle = FlatStyle.Flat;
            btnLista.Font = new Font("Tahoma", 10.2F, FontStyle.Bold);
            btnLista.ForeColor = Color.White;
            btnLista.Location = new Point(0, 60);
            btnLista.Name = "btnLista";
            btnLista.Padding = new Padding(20, 0, 0, 0);
            btnLista.Size = new Size(220, 60);
            btnLista.TabIndex = 1;
            btnLista.Text = "Lista biletów";
            btnLista.TextAlign = ContentAlignment.MiddleLeft;
            btnLista.UseVisualStyleBackColor = true;
            btnLista.Click += btnLista_Click;
            // 
            // btnKasowanie
            // 
            btnKasowanie.BackColor = Color.FromArgb(45, 45, 45);
            btnKasowanie.Cursor = Cursors.Hand;
            btnKasowanie.FlatAppearance.BorderSize = 0;
            btnKasowanie.FlatStyle = FlatStyle.Flat;
            btnKasowanie.Font = new Font("Tahoma", 10.2F, FontStyle.Bold);
            btnKasowanie.ForeColor = Color.White;
            btnKasowanie.Location = new Point(0, 0);
            btnKasowanie.Name = "btnKasowanie";
            btnKasowanie.Padding = new Padding(20, 0, 0, 0);
            btnKasowanie.Size = new Size(220, 60);
            btnKasowanie.TabIndex = 0;
            btnKasowanie.Text = "Kasowanie biletów";
            btnKasowanie.TextAlign = ContentAlignment.MiddleLeft;
            btnKasowanie.UseVisualStyleBackColor = true;
            btnKasowanie.Click += btnKasowanie_Click;
            // 
            // panelContent
            // 
            panelContent.BackColor = Color.FromArgb(30, 30, 30);
            panelContent.Controls.Add(dataGridViewBilety);
            panelContent.Dock = DockStyle.Fill;
            panelContent.Location = new Point(220, 90);
            panelContent.Name = "panelContent";
            panelContent.Size = new Size(980, 560);
            panelContent.TabIndex = 5;
            // 
            // dataGridViewBilety
            // 
            dataGridViewBilety.BorderStyle = BorderStyle.None;
            dataGridViewBilety.ColumnHeadersHeight = 29;
            dataGridViewBilety.Location = new Point(20, 20);
            dataGridViewBilety.Margin = new Padding(20);
            dataGridViewBilety.Name = "dataGridViewBilety";
            dataGridViewBilety.ReadOnly = true;
            dataGridViewBilety.RowHeadersVisible = false;
            dataGridViewBilety.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            dataGridViewBilety.Size = new Size(940, 520);
            dataGridViewBilety.TabIndex = 0;
            // 
            // timerDateTime
            // 
            timerDateTime.Enabled = true;
            timerDateTime.Interval = 1000;
            timerDateTime.Tick += timerDateTime_Tick;
            // 
            // fon
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
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)icomap).EndInit();
            navigacion_panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)minimize).EndInit();
            ((System.ComponentModel.ISupportInitialize)close).EndInit();
            panelMenu.ResumeLayout(false);
            panelContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewBilety).EndInit();
            ResumeLayout(false);
        }

        #endregion

        // Istniejące kontrolki:
        private Panel panel1;
        private Panel navigacion_panel;
        private PictureBox close;
        private PictureBox minimize;

        // Nowe kontrolki:
        private Label Clock;
        private System.Windows.Forms.Timer timerDateTime;

        // Lewy panel menu i przyciski:
        private Panel panelMenu;
        private Button btnKasowanie;
        private Button btnLista;
        private Button btnHarmonogram;
        private Button btnPociagi;

        // Panel na zawartość + DataGridView do listy biletów
        private Panel panelContent;
        private DataGridView dataGridViewBilety;
        private Button btnKupicBilet;
        private PictureBox icomap;
    }
}
