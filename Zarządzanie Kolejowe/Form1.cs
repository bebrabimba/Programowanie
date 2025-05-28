using System.Windows.Forms;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;


namespace MediaPlayer_Forms
{

    public partial class MediaPlayer : Form
    {
        private MediaControl _mediaControl;
        public MediaPlayer()
        {
            InitializeComponent();

            _mediaControl = new MediaControl(axWindowsMediaPlayer1, time);

            listBox1.AllowDrop = true;

            // Przypisanie zdarzeń do metod
            /*
            stop.Click += stopClick;
            play.Click += playClick;
            pause.Click += pauseClick;

            */
            // axWindowsMediaPlayer1.ShowPropertyPages();

        }
        string[] paths, files;
        bool mouse = false;
        double tempPosition = 0;

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void close_click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void collapse_click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void panel1_Paint(object sender, EventArgs e)
        {

        }
        private void axWindowsMediaPlayer1_Enter_2(object sender, EventArgs e)
        {

        }





        private void progressBar_MouseDown(object sender, MouseEventArgs e)
        {
            mouse = true;
            if (mouse)
            {
                // Oblicz pozycję kursora jako część szerokości paska
                double clickPosition = (double)e.X / progressBar.Width;

                if (listBox1.Items.Count > 0)
                {
                    // Oblicz nową pozycję, ale jej jeszcze nie ustawiaj
                    tempPosition = clickPosition * axWindowsMediaPlayer1.currentMedia.duration;

                    // Aktualizuj tylko wizualną reprezentację paska postępu
                    progressBar.Value = (int)tempPosition;
                }
            }
        }

        private void progressBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouse)
            {
                double clickPosition = (double)e.X / progressBar.Width;
                double newTempPosition = clickPosition * axWindowsMediaPlayer1.currentMedia.duration;

                // Jeśli wartość przekroczy zakres, ustaw na 0 (odtwarzanie od nowa)
                if (newTempPosition < 0 || newTempPosition > axWindowsMediaPlayer1.currentMedia.duration)
                {
                    newTempPosition = 0;
                }

                tempPosition = newTempPosition;
                progressBar.Value = (int)(tempPosition * progressBar.Maximum / axWindowsMediaPlayer1.currentMedia.duration);
            }
        }

        private void progressBar_MouseUp(object sender, MouseEventArgs e)
        {
            if (mouse)
            {
                mouse = false;

                // Zastosuj pozycję dopiero w momencie puszczenia przycisku myszy
                if (listBox1.Items.Count > 0)
                {
                    axWindowsMediaPlayer1.Ctlcontrols.currentPosition = tempPosition;
                }
            }
        }

        //private int x, y;

        private void move(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                panel1.Capture = false;
                Message m = Message.Create(Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
                WndProc(ref m);
            }
        }

        private void open(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (files == null) files = new string[0];
                if (paths == null) paths = new string[0];

                // Połączenie starej i nowej listy plików
                files = files.Concat(ofd.SafeFileNames).ToArray();
                paths = paths.Concat(ofd.FileNames).ToArray();

                foreach (var file in ofd.SafeFileNames)
                {
                    listBox1.Items.Add(file);
                }

                // Automatyczne odtwarzanie pierwszego pliku, jeśli żaden nie jest wybrany
                if (listBox1.Items.Count > 0 && listBox1.SelectedIndex == -1)
                {
                    listBox1.SelectedIndex = 0;
                    axWindowsMediaPlayer1.URL = paths[listBox1.SelectedIndex];
                    axWindowsMediaPlayer1.Ctlcontrols.play();
                }
            }

        }

        private void list(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.URL = paths[listBox1.SelectedIndex];
        }





        private void nextClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < listBox1.Items.Count - 1)
                listBox1.SelectedIndex = listBox1.SelectedIndex + 1;
        }
        private void playClick(object sender, EventArgs e)
        {
            _mediaControl.Play();
        }
        private void pauseClick(object sender, EventArgs e)
        {
            _mediaControl.Pause();
        }
        private void stopClick(object sender, EventArgs e)
        {
            _mediaControl.Stop();
        }
        private void previousClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > 0)
                listBox1.SelectedIndex = listBox1.SelectedIndex - 1;
        }





        private void volumeBarScroll(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.settings.volume = volumeBar.Value;
            volumeValue.Text = volumeBar.Value.ToString();
        }

        private void PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                progressBar.Maximum = (int)axWindowsMediaPlayer1.Ctlcontrols.currentItem.duration;
                timer.Start();
            }
            else if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPaused)
            {
                timer.Stop();
            }
            else if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsStopped)
            {
                timer.Stop();
                progressBar.Value = 0;
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            /*time.Text = axWindowsMediaPlayer1.Ctlcontrols.currentPositionString;
            alltime.Text = axWindowsMediaPlayer1.Ctlcontrols.currentItem.durationString.ToString();

            if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                progressBar.Value = (int)axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
            }*/
            if (!mouse) // Aktualizuj pasek tylko, gdy mysz nie jest wciśnięta
            {
                time.Text = axWindowsMediaPlayer1.Ctlcontrols.currentPositionString;
                alltime.Text = axWindowsMediaPlayer1.Ctlcontrols.currentItem.durationString;

                if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPlaying)
                {
                    progressBar.Value = (int)(axWindowsMediaPlayer1.Ctlcontrols.currentPosition * progressBar.Maximum / axWindowsMediaPlayer1.currentMedia.duration);
                }
            }
        }

        private void DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void DragDrop(object sender, DragEventArgs e)
        {
            string[] droppedFiles = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (droppedFiles != null && droppedFiles.Length > 0)
            {
                if (files == null) files = new string[0];
                if (paths == null) paths = new string[0];

                // Połączenie starej i nowej listy plików
                files = files.Concat(droppedFiles.Select(System.IO.Path.GetFileName)).ToArray();
                paths = paths.Concat(droppedFiles).ToArray();

                foreach (var file in droppedFiles)
                {
                    listBox1.Items.Add(System.IO.Path.GetFileName(file));
                }

                // Automatyczne odtwarzanie pierwszego pliku, jeśli żaden nie jest wybrany
                if (listBox1.Items.Count > 0 && listBox1.SelectedIndex == -1)
                {
                    listBox1.SelectedIndex = 0;
                    axWindowsMediaPlayer1.URL = paths[listBox1.SelectedIndex];
                    axWindowsMediaPlayer1.Ctlcontrols.play();
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}



