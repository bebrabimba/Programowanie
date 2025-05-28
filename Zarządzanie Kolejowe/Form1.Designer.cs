using System.Windows.Input;

namespace MediaPlayer_Forms
{
    partial class MediaPlayer
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MediaPlayer));
            fon = new Panel();
            comboBox1 = new ComboBox();
            file = new PictureBox();
            axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
            volumeValue = new Label();
            volumeBar = new TrackBar();
            volume = new PictureBox();
            alltime = new Label();
            time = new Label();
            listBox1 = new ListBox();
            panel1 = new Panel();
            navigacion_panel = new Panel();
            close = new PictureBox();
            collapse = new PictureBox();
            panel2 = new Panel();
            next = new PictureBox();
            play = new PictureBox();
            pause = new PictureBox();
            stop = new PictureBox();
            previous = new PictureBox();
            progressBar = new CustomProgressBar();
            timer = new System.Windows.Forms.Timer(components);
            fon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)file).BeginInit();
            ((System.ComponentModel.ISupportInitialize)axWindowsMediaPlayer1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)volumeBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)volume).BeginInit();
            panel1.SuspendLayout();
            navigacion_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)close).BeginInit();
            ((System.ComponentModel.ISupportInitialize)collapse).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)next).BeginInit();
            ((System.ComponentModel.ISupportInitialize)play).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pause).BeginInit();
            ((System.ComponentModel.ISupportInitialize)stop).BeginInit();
            ((System.ComponentModel.ISupportInitialize)previous).BeginInit();
            SuspendLayout();
            // 
            // fon
            // 
            fon.BackColor = Color.FromArgb(30, 30, 30);
            fon.Controls.Add(comboBox1);
            fon.Controls.Add(file);
            fon.Controls.Add(axWindowsMediaPlayer1);
            fon.Controls.Add(volumeValue);
            fon.Controls.Add(volumeBar);
            fon.Controls.Add(volume);
            fon.Controls.Add(alltime);
            fon.Controls.Add(time);
            fon.Controls.Add(listBox1);
            fon.Controls.Add(panel1);
            fon.Controls.Add(panel2);
            fon.Dock = DockStyle.Fill;
            fon.Location = new Point(0, 0);
            fon.Name = "fon";
            fon.Size = new Size(782, 453);
            fon.TabIndex = 0;
            fon.Paint += panel1_Paint;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(760, 355);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(151, 28);
            comboBox1.TabIndex = 16;
            // 
            // file
            // 
            file.Cursor = System.Windows.Forms.Cursors.Hand;
            file.Image = Properties.Resources.folder;
            file.Location = new Point(692, 363);
            file.Name = "file";
            file.Size = new Size(70, 70);
            file.SizeMode = PictureBoxSizeMode.Zoom;
            file.TabIndex = 15;
            file.TabStop = false;
            file.Click += open;
            // 
            // axWindowsMediaPlayer1
            // 
            axWindowsMediaPlayer1.Enabled = true;
            axWindowsMediaPlayer1.Location = new Point(20, 110);
            axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
            axWindowsMediaPlayer1.OcxState = (AxHost.State)resources.GetObject("axWindowsMediaPlayer1.OcxState");
            axWindowsMediaPlayer1.Size = new Size(430, 168);
            axWindowsMediaPlayer1.TabIndex = 14;
            axWindowsMediaPlayer1.Visible = false;
            axWindowsMediaPlayer1.PlayStateChange += PlayStateChange;
            axWindowsMediaPlayer1.Enter += axWindowsMediaPlayer1_Enter_2;
            // 
            // volumeValue
            // 
            volumeValue.AutoSize = true;
            volumeValue.Font = new Font("Tahoma", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            volumeValue.ForeColor = Color.White;
            volumeValue.Location = new Point(635, 392);
            volumeValue.Name = "volumeValue";
            volumeValue.Size = new Size(38, 28);
            volumeValue.TabIndex = 13;
            volumeValue.Text = "75";
            // 
            // volumeBar
            // 
            volumeBar.AccessibleRole = AccessibleRole.Sound;
            volumeBar.AllowDrop = true;
            volumeBar.AutoSize = false;
            volumeBar.Cursor = System.Windows.Forms.Cursors.Hand;
            volumeBar.LargeChange = 10;
            volumeBar.Location = new Point(530, 395);
            volumeBar.Maximum = 100;
            volumeBar.Name = "volumeBar";
            volumeBar.Size = new Size(100, 30);
            volumeBar.TabIndex = 12;
            volumeBar.TickStyle = TickStyle.None;
            volumeBar.Value = 75;
            volumeBar.Scroll += volumeBarScroll;
            // 
            // volume
            // 
            volume.Image = Properties.Resources.volume;
            volume.Location = new Point(470, 383);
            volume.Name = "volume";
            volume.Size = new Size(50, 50);
            volume.SizeMode = PictureBoxSizeMode.Zoom;
            volume.TabIndex = 11;
            volume.TabStop = false;
            // 
            // alltime
            // 
            alltime.AutoSize = true;
            alltime.Font = new Font("Tahoma", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            alltime.ForeColor = Color.White;
            alltime.Location = new Point(400, 298);
            alltime.Name = "alltime";
            alltime.Size = new Size(56, 22);
            alltime.TabIndex = 10;
            alltime.Text = "00:00";
            // 
            // time
            // 
            time.AutoSize = true;
            time.Font = new Font("Tahoma", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            time.ForeColor = Color.White;
            time.Location = new Point(15, 298);
            time.Margin = new Padding(0);
            time.Name = "time";
            time.Size = new Size(56, 22);
            time.TabIndex = 9;
            time.Text = "00:00";
            // 
            // listBox1
            // 
            listBox1.BackColor = Color.FromArgb(35, 35, 35);
            listBox1.BorderStyle = BorderStyle.None;
            listBox1.Font = new Font("Tahoma", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            listBox1.ForeColor = Color.White;
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 18;
            listBox1.Location = new Point(470, 110);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(292, 216);
            listBox1.TabIndex = 6;
            listBox1.SelectedIndexChanged += list;
            listBox1.DragDrop += DragDrop;
            listBox1.DragEnter += DragEnter;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(35, 35, 35);
            panel1.Controls.Add(navigacion_panel);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(782, 90);
            panel1.TabIndex = 5;
            panel1.Paint += panel1_Paint;
            panel1.MouseMove += move;
            // 
            // navigacion_panel
            // 
            navigacion_panel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            navigacion_panel.AutoScrollMargin = new Size(20, 20);
            navigacion_panel.Controls.Add(close);
            navigacion_panel.Controls.Add(collapse);
            navigacion_panel.Location = new Point(642, 20);
            navigacion_panel.Margin = new Padding(20, 20, 3, 3);
            navigacion_panel.Name = "navigacion_panel";
            navigacion_panel.Size = new Size(120, 50);
            navigacion_panel.TabIndex = 4;
            // 
            // close
            // 
            close.Cursor = System.Windows.Forms.Cursors.Hand;
            close.Image = Properties.Resources.close;
            close.Location = new Point(70, 0);
            close.Name = "close";
            close.Size = new Size(50, 50);
            close.SizeMode = PictureBoxSizeMode.Zoom;
            close.TabIndex = 5;
            close.TabStop = false;
            close.Click += close_click;
            close.DoubleClick += close_click;
            // 
            // collapse
            // 
            collapse.Cursor = System.Windows.Forms.Cursors.Hand;
            collapse.Image = Properties.Resources.minimize;
            collapse.Location = new Point(0, 0);
            collapse.Name = "collapse";
            collapse.Size = new Size(50, 50);
            collapse.SizeMode = PictureBoxSizeMode.Zoom;
            collapse.TabIndex = 7;
            collapse.TabStop = false;
            collapse.Click += collapse_click;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            panel2.Controls.Add(next);
            panel2.Controls.Add(play);
            panel2.Controls.Add(pause);
            panel2.Controls.Add(stop);
            panel2.Controls.Add(previous);
            panel2.Location = new Point(20, 363);
            panel2.Name = "panel2";
            panel2.Size = new Size(430, 70);
            panel2.TabIndex = 3;
            panel2.Paint += panel2_Paint;
            // 
            // next
            // 
            next.Cursor = System.Windows.Forms.Cursors.Hand;
            next.Image = Properties.Resources.next;
            next.Location = new Point(360, 0);
            next.Name = "next";
            next.Size = new Size(70, 70);
            next.SizeMode = PictureBoxSizeMode.Zoom;
            next.TabIndex = 4;
            next.TabStop = false;
            next.Click += nextClick;
            // 
            // play
            // 
            play.Cursor = System.Windows.Forms.Cursors.Hand;
            play.Image = Properties.Resources.play;
            play.Location = new Point(270, 0);
            play.Name = "play";
            play.Size = new Size(70, 70);
            play.SizeMode = PictureBoxSizeMode.Zoom;
            play.TabIndex = 3;
            play.TabStop = false;
            play.Click += playClick;
            // 
            // pause
            // 
            pause.BackgroundImageLayout = ImageLayout.Zoom;
            pause.Cursor = System.Windows.Forms.Cursors.Hand;
            pause.Image = Properties.Resources.pause;
            pause.Location = new Point(180, 0);
            pause.Name = "pause";
            pause.Size = new Size(70, 70);
            pause.SizeMode = PictureBoxSizeMode.Zoom;
            pause.TabIndex = 2;
            pause.TabStop = false;
            pause.Click += pauseClick;
            // 
            // stop
            // 
            stop.Cursor = System.Windows.Forms.Cursors.Hand;
            stop.Image = Properties.Resources.stop;
            stop.Location = new Point(90, 0);
            stop.Name = "stop";
            stop.Size = new Size(70, 70);
            stop.SizeMode = PictureBoxSizeMode.Zoom;
            stop.TabIndex = 1;
            stop.TabStop = false;
            stop.Click += stopClick;
            // 
            // previous
            // 
            previous.Cursor = System.Windows.Forms.Cursors.Hand;
            previous.Image = Properties.Resources.last;
            previous.Location = new Point(0, 0);
            previous.Name = "previous";
            previous.Size = new Size(70, 70);
            previous.SizeMode = PictureBoxSizeMode.Zoom;
            previous.TabIndex = 0;
            previous.TabStop = false;
            previous.Click += previousClick;
            // 
            // progressBar
            // 
            progressBar.Cursor = System.Windows.Forms.Cursors.Hand;
            progressBar.ForeColor = Color.Blue;
            progressBar.Location = new Point(20, 338);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(430, 5);
            progressBar.TabIndex = 8;
            progressBar.MouseDown += progressBar_MouseDown;
            progressBar.MouseMove += progressBar_MouseMove;
            progressBar.MouseUp += progressBar_MouseUp;
            // 
            // timer
            // 
            timer.Tick += timer_Tick;
            // 
            // MediaPlayer
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(31, 31, 31);
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(782, 453);
            Controls.Add(progressBar);
            Controls.Add(fon);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(0);
            Name = "MediaPlayer";
            RightToLeft = RightToLeft.No;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MediaPlayer";
            Load += Form1_Load;
            fon.ResumeLayout(false);
            fon.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)file).EndInit();
            ((System.ComponentModel.ISupportInitialize)axWindowsMediaPlayer1).EndInit();
            ((System.ComponentModel.ISupportInitialize)volumeBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)volume).EndInit();
            panel1.ResumeLayout(false);
            navigacion_panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)close).EndInit();
            ((System.ComponentModel.ISupportInitialize)collapse).EndInit();
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)next).EndInit();
            ((System.ComponentModel.ISupportInitialize)play).EndInit();
            ((System.ComponentModel.ISupportInitialize)pause).EndInit();
            ((System.ComponentModel.ISupportInitialize)stop).EndInit();
            ((System.ComponentModel.ISupportInitialize)previous).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private CustomProgressBar progressBar;
        private Panel fon;
        private Panel panel2;
        private PictureBox next;
        private PictureBox play;
        private PictureBox pause;
        private PictureBox stop;
        private PictureBox previous;
        private Panel navigacion_panel;
        private PictureBox close;
        private PictureBox collapse;
        private Panel panel1;
        private ListBox listBox1;        //private ProgressBar progressBar;
        private Label time;
        private Label alltime;
        private PictureBox volume;
        private TrackBar volumeBar;
        private Label volumeValue;
        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;
        private PictureBox file;
        private System.Windows.Forms.Timer timer;
        private ComboBox comboBox1;
    }

}
