using Synthesizer.Models;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Synthesizer
{
    public partial class MainLayout : Form
    {
        private Track track = new Track(Configuration.Application.Defaults.TrackLength);
        private WaveType waveType = WaveType.Sin;

        private string projectFile;
        private bool modified;

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
                SetModified(true);
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

        private void MenuItemSave_Click(object sender, EventArgs e)
        {
            if (projectFile != null)
            {
                var data = track.Save();
                var file = File.Open(projectFile, FileMode.Truncate);
                file.Write(data, 0, data.Length);
                file.Close();

                SetModified(false);
            } else
            {
                MenuItemSaveAs_Click(sender, e);
            }
        }

        private void MenuItemSaveAs_Click(object sender, EventArgs e)
        {
            var dialog = new SaveFileDialog
            {
                Filter = "Synthesizer project files|*.syn",
                RestoreDirectory = true,
                OverwritePrompt = true
            };

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            projectFile = dialog.FileName;
            var file = dialog.OpenFile();
            file.SetLength(0);

            var data = track.Save();
            file.Write(data, 0, data.Length);
            file.Close();

            SetModified(false);
        }

        private void MenuItemLoad_Click(object sender, EventArgs e)
        {
            if (modified)
            {
                var msg = "Projekt nie został zapisany od ostatniej modyfikacji. Czy na pewno chcesz kontynuować?";
                if (MessageBox.Show(msg, "Niezapisane zmiany", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
            }

            var dialog = new OpenFileDialog()
            {
                Filter = "Synthesizer project files|*.syn",
                RestoreDirectory = true,
            };

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                byte[] content = File.ReadAllBytes(dialog.FileName);
                track.Load(content);
                TrackLength.Value = track.GetSize();
                Tempo.Value = track.GetTempo();
                projectFile = dialog.FileName;
                SetModified(false);
                SynchronizeKeyboardWithModel();
            } catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                MessageBox.Show("Wystąpił błąd podczas ładowania projektu", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SynchronizeKeyboardWithModel()
        {
            for (int row = 0; row < keyboardGrid.GridHeight; row++)
            {
                for (int col = 0; col < keyboardGrid.GridWidth; col++)
                {
                    var state = track.GetState(row, col);
                    var type = track.GetWaveType(row, col);
                    keyboardGrid.SetPanelState(row, col, state, type);
                }
            }
        }

        private void SetModified(bool state)
        {
            if (state == modified)
                return;
            modified = state;

            Text = state ? "Synthesizer - niezapisane zmiany" : "Synthesizer";
        }

        private void MenuItemExport_Click(object sender, EventArgs e)
        {
            var dialog = new SaveFileDialog()
            {
                Filter = "WAV files|*.wav",
                RestoreDirectory = true,
                OverwritePrompt = true
            };

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            var rawData = track.Export();
            var stream = dialog.OpenFile();
            stream.Write(rawData, 0, rawData.Length);
            stream.Close();

            MessageBox.Show("Plik został zapisany", "Eksport zakończony powodzeniem", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
