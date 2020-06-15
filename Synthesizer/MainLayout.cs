using NAudio.Wave.SampleProviders;
using Synthesizer.Models;
using Synthesizer.Views;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Synthesizer
{
    public partial class MainLayout : Form
    {
        private Track track = new Track(Configuration.Application.Defaults.TrackLength);

        private WaveType waveType = WaveType.Sin;

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

            // Szerokość etykiet wysokości dźwięku
            positionMarkerOverlay.SetOffset(Configuration.Keyboard.keyPanelWidth);

            PostInitializeKeyboardGrid();
            InitializeWaveTypeSelector();
            RefreshWaveformChart(WaveType.Sin);
        }

        private void PostInitializeKeyboardGrid()
        {
            keyboardGrid.OnGridUpdate = (int row, int column, bool state, WaveType type) =>
            {
                var newType = type ?? waveType;
                track.SetStateAndType(row, column, state, newType);
            };
            keyboardGrid.ResizeGrid(Configuration.Application.Defaults.TrackLength);

            keyboardGrid.Scroll += delegate (object sender, ScrollEventArgs e)
            {
                positionMarkerOverlay.SetOffset(Configuration.Keyboard.keyPanelWidth - keyboardGrid.HorizontalScroll.Value);
            };

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

        private void InitializeWaveTypeSelector()
        {
            int index = 0;
            foreach (var type in WaveType.AllTypes)
            {
                var radio = new RadioButton
                {
                    Text = type.Name,
                    Checked = index == 0,
                    Location = new Point(7, 19 + (23 * index++))
                };
                radio.CheckedChanged += (object sender, EventArgs e) =>
                {
                    if (((RadioButton)sender).Checked)
                        RefreshWaveformChart(type);
                };

                WaveTypeGroup.Controls.Add(radio);
            }
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

        private void OnTrackStateChange(Track.States newState, Track.States oldState)
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
                        positionMarkerOverlay.Start(Configuration.Keyboard.keyPanelWidth);
                        if (oldState == Track.States.PAUSED)
                        {
                            var pw = Configuration.Keyboard.keyPanelWidth;
                            var newPos = (Math.Ceiling((float)(track.CurrentNoteIndex) / pw) + 1) * pw;
                            positionMarkerOverlay.SetPosition((float)newPos - keyboardGrid.HorizontalScroll.Value);
                        }
                        break;
                    case Track.States.PAUSED:
                        playPauseButton.Image = Properties.Resources.play;
                        stopButton.Enabled = true;
                        TrackStatusLabel.Text = "pauza";
                        positionMarkerOverlay.Stop();
                        break;
                    case Track.States.STOPPED:
                        playPauseButton.Image = Properties.Resources.play;
                        stopButton.Enabled = false;
                        SetControlsEnabledState(true);
                        TrackStatusLabel.Text = "zatrzymany";
                        positionMarkerOverlay.Reset();
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

        private void RefreshWaveformChart(WaveType type)
        {
            waveType = type;
            keyboardGrid.WaveType = type;

            var series = new Series()
            {
                ChartType = SeriesChartType.Line,
                IsVisibleInLegend = false,
                Color = type.Color,
                Legend = null
            };

            WaveVisualizer.Plot(type).ForEach(t =>
            {
                series.Points.AddXY(t.Item1, t.Item2);
            });

            WaveformChart.Series.Clear();
            WaveformChart.Series.Add(series);
            WaveformChart.Invalidate();
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            track.Stop();
        }
    }
}
