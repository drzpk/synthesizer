using Synthesizer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Synthesizer
{
    public partial class MainLayout : Form
    {
        private Track track = new Track(30);

        public MainLayout()
        {
            InitializeComponent();
            PostInitialize();
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

            keyboardGrid.OnGridUpdate = (int row, int column, bool state) => track.SetState(row, column, state);
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

        private void stopButton_Click(object sender, EventArgs e)
        {
            track.Stop();
        }
    }
}
