using Synthesizer.Models;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Synthesizer
{
    public partial class MainLayout : Form
    {
        private Track track = new Track(Configuration.Application.Defaults.TrackLength);

        public MainLayout()
        {
            InitializeComponent();
            PostInitialize();
            SetDefaults();
        }

        /// <summary>
        /// Dodatkowa inicjalizacja, której nie da się wykonać w designerze
        /// </summary>
        private void PostInitialize()
        {
            meterBox.SuspendLayout();
            foreach (var meter in Configuration.Keyboard.meters)
            {
                meterBox.Items.Add(meter.ToString());
            }
            meterBox.ResumeLayout();
            meterBox.SelectedIndexChanged += UpdatePlayButtonState;

            playPauseButton.Image = Properties.Resources.play;
            playPauseButton.ImageAlign = ContentAlignment.MiddleCenter;
            stopButton.Image = Properties.Resources.stop;
            stopButton.ImageAlign = ContentAlignment.MiddleCenter;

            PostInitializeKeyboardGrid();
        }

        private void PostInitializeKeyboardGrid()
        {
            keyboardGrid.OnGridUpdate = (int row, int column, bool state) => track.SetState(row, column, state);
            keyboardGrid.ResizeGrid(Configuration.Application.Defaults.TrackLength);

            TrackLength.LostFocus += delegate (object sender, EventArgs e)
            {
                keyboardGrid.ResizeGrid(TrackLength.Value);
                track.Resize(TrackLength.Value);
            };
            Tempo.LostFocus += delegate (object sender, EventArgs e)
            {
                track.SetTempo(Tempo.Value);
            };

            track.SetTempo(Configuration.Application.Defaults.Tempo);
            track.onStateChange = OnTrackStateChange;
        }

        /// <summary>
        /// Ustawienie domyślnych wartości ustawień
        /// </summary>
        private void SetDefaults()
        {
            TrackLength.Text = Configuration.Application.Defaults.TrackLength.ToString();
            Tempo.Text = Configuration.Application.Defaults.Tempo.ToString();
        }

        private void UpdatePlayButtonState(object sender, EventArgs e)
        {
            var meterSelected = meterBox.SelectedItem != null;

            playPauseButton.Enabled = meterSelected;
        }

        private void playPauseButton_Click(object sender, EventArgs e)
        {
            if (track.State != Track.States.PLAYING)
                track.Play();
            else
                track.Pause();
        }

        private void OnTrackStateChange(Track.States newState)
        {
            // Ta metoda może zostać wywołana z innego wątku
            playPauseButton.Invoke((MethodInvoker)delegate
            {
                switch (newState)
                {
                    case Track.States.PLAYING:
                        playPauseButton.Image = Properties.Resources.pause;
                        stopButton.Enabled = true;
                        SetControlsEnabledState(false);
                        TrackStatusLabel.Text = "odtwarzanie";
                        break;
                    case Track.States.PAUSED:
                        playPauseButton.Image = Properties.Resources.play;
                        stopButton.Enabled = true;
                        TrackStatusLabel.Text = "pauza";
                        break;
                    case Track.States.STOPPED:
                        playPauseButton.Image = Properties.Resources.play;
                        stopButton.Enabled = false;
                        SetControlsEnabledState(true);
                        TrackStatusLabel.Text = "zatrzymany";
                        break;
                }
            });
        }

        /// <summary>
        /// Włączanie/wyłączanie edycji kontrolek na ekranie głównym
        /// </summary>
        /// <param name="state"></param>
        private void SetControlsEnabledState(bool state)
        {
            TrackLength.Enabled = state;
            Tempo.Enabled = state;
            meterBox.Enabled = state;
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            track.Stop();
        }
    }
}
