using System.Windows.Forms;
using AxWMPLib;

namespace MediaPlayer_Forms
{
    public class MediaControl
    {
        private AxWindowsMediaPlayer _mediaControl;
        private Label _timeLabel;

        public MediaControl(AxWindowsMediaPlayer mediaPlayer, Label timeLabel)
        {
            _mediaControl = mediaPlayer;
            _timeLabel = timeLabel;
        }


        public void Play()
        {
            _mediaControl.Ctlcontrols.play();
        }
        public void Pause()
        {
            _mediaControl.Ctlcontrols.pause();
        }
        public void Stop()
        {
            _mediaControl.Ctlcontrols.stop();
            _timeLabel.Text = "00:00";
        }



    }
}