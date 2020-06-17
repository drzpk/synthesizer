namespace Synthesizer
{
    partial class MainLayout
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.plikToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemSave = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItemExport = new System.Windows.Forms.ToolStripMenuItem();
            this.informacjeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.plikToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.otwórzToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zapiszToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zapiszJakoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.zamknijToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pomocToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oProgramioeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trackLengthLabel = new System.Windows.Forms.Label();
            this.TempoLabel = new System.Windows.Forms.Label();
            this.playPauseButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.StatusBar = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.TrackStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.WaveformChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.WaveTypeGroup = new System.Windows.Forms.GroupBox();
            this.positionMarkerOverlay = new Synthesizer.Views.PositionMarkerOverlay();
            this.Tempo = new Synthesizer.Views.NumberBox();
            this.TrackLength = new Synthesizer.Views.NumberBox();
            this.keyboardGrid = new Synthesizer.Views.KeyboardGrid();
            this.mainMenu.SuspendLayout();
            this.StatusBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WaveformChart)).BeginInit();
            this.WaveTypeGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.plikToolStripMenuItem1,
            this.informacjeToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(802, 24);
            this.mainMenu.TabIndex = 2;
            this.mainMenu.Text = "Menu";
            // 
            // plikToolStripMenuItem1
            // 
            this.plikToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemSave,
            this.MenuItemSaveAs,
            this.MenuItemLoad,
            this.toolStripSeparator2,
            this.MenuItemExport});
            this.plikToolStripMenuItem1.Name = "plikToolStripMenuItem1";
            this.plikToolStripMenuItem1.Size = new System.Drawing.Size(38, 20);
            this.plikToolStripMenuItem1.Text = "Plik";
            // 
            // MenuItemSave
            // 
            this.MenuItemSave.Name = "MenuItemSave";
            this.MenuItemSave.Size = new System.Drawing.Size(180, 22);
            this.MenuItemSave.Text = "Zapisz";
            this.MenuItemSave.Click += new System.EventHandler(this.MenuItemSave_Click);
            // 
            // MenuItemSaveAs
            // 
            this.MenuItemSaveAs.Name = "MenuItemSaveAs";
            this.MenuItemSaveAs.Size = new System.Drawing.Size(180, 22);
            this.MenuItemSaveAs.Text = "Zapisz jako";
            this.MenuItemSaveAs.Click += new System.EventHandler(this.MenuItemSaveAs_Click);
            // 
            // MenuItemLoad
            // 
            this.MenuItemLoad.Name = "MenuItemLoad";
            this.MenuItemLoad.Size = new System.Drawing.Size(180, 22);
            this.MenuItemLoad.Text = "Wczytaj";
            this.MenuItemLoad.Click += new System.EventHandler(this.MenuItemLoad_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(177, 6);
            // 
            // MenuItemExport
            // 
            this.MenuItemExport.Name = "MenuItemExport";
            this.MenuItemExport.Size = new System.Drawing.Size(180, 22);
            this.MenuItemExport.Text = "Eksportuj";
            this.MenuItemExport.Click += new System.EventHandler(this.MenuItemExport_Click);
            // 
            // informacjeToolStripMenuItem
            // 
            this.informacjeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemAbout});
            this.informacjeToolStripMenuItem.Name = "informacjeToolStripMenuItem";
            this.informacjeToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
            this.informacjeToolStripMenuItem.Text = "Informacje";
            // 
            // MenuItemAbout
            // 
            this.MenuItemAbout.Name = "MenuItemAbout";
            this.MenuItemAbout.Size = new System.Drawing.Size(141, 22);
            this.MenuItemAbout.Text = "O programie";
            // 
            // plikToolStripMenuItem
            // 
            this.plikToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.otwórzToolStripMenuItem,
            this.zapiszToolStripMenuItem,
            this.zapiszJakoToolStripMenuItem,
            this.toolStripSeparator1,
            this.zamknijToolStripMenuItem});
            this.plikToolStripMenuItem.Name = "plikToolStripMenuItem";
            this.plikToolStripMenuItem.Size = new System.Drawing.Size(38, 20);
            this.plikToolStripMenuItem.Text = "Plik";
            // 
            // otwórzToolStripMenuItem
            // 
            this.otwórzToolStripMenuItem.Name = "otwórzToolStripMenuItem";
            this.otwórzToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.otwórzToolStripMenuItem.Text = "Otwórz";
            // 
            // zapiszToolStripMenuItem
            // 
            this.zapiszToolStripMenuItem.Name = "zapiszToolStripMenuItem";
            this.zapiszToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.zapiszToolStripMenuItem.Text = "Zapisz";
            // 
            // zapiszJakoToolStripMenuItem
            // 
            this.zapiszJakoToolStripMenuItem.Name = "zapiszJakoToolStripMenuItem";
            this.zapiszJakoToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.zapiszJakoToolStripMenuItem.Text = "Zapisz jako";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(129, 6);
            // 
            // zamknijToolStripMenuItem
            // 
            this.zamknijToolStripMenuItem.Name = "zamknijToolStripMenuItem";
            this.zamknijToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.zamknijToolStripMenuItem.Text = "Zamknij";
            // 
            // pomocToolStripMenuItem
            // 
            this.pomocToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.oProgramioeToolStripMenuItem});
            this.pomocToolStripMenuItem.Name = "pomocToolStripMenuItem";
            this.pomocToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.pomocToolStripMenuItem.Text = "Pomoc";
            // 
            // oProgramioeToolStripMenuItem
            // 
            this.oProgramioeToolStripMenuItem.Name = "oProgramioeToolStripMenuItem";
            this.oProgramioeToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.oProgramioeToolStripMenuItem.Text = "O programioe";
            // 
            // trackLengthLabel
            // 
            this.trackLengthLabel.AutoSize = true;
            this.trackLengthLabel.Location = new System.Drawing.Point(24, 49);
            this.trackLengthLabel.Name = "trackLengthLabel";
            this.trackLengthLabel.Size = new System.Drawing.Size(86, 13);
            this.trackLengthLabel.TabIndex = 3;
            this.trackLengthLabel.Text = "Długość ścieżki:";
            // 
            // TempoLabel
            // 
            this.TempoLabel.AutoSize = true;
            this.TempoLabel.Location = new System.Drawing.Point(24, 73);
            this.TempoLabel.Name = "TempoLabel";
            this.TempoLabel.Size = new System.Drawing.Size(55, 13);
            this.TempoLabel.TabIndex = 4;
            this.TempoLabel.Text = "Prędkość:";
            // 
            // playPauseButton
            // 
            this.playPauseButton.Location = new System.Drawing.Point(293, 97);
            this.playPauseButton.Name = "playPauseButton";
            this.playPauseButton.Size = new System.Drawing.Size(60, 54);
            this.playPauseButton.TabIndex = 7;
            this.playPauseButton.UseVisualStyleBackColor = true;
            this.playPauseButton.Click += new System.EventHandler(this.playPauseButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(378, 97);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(60, 54);
            this.stopButton.TabIndex = 8;
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // StatusBar
            // 
            this.StatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.TrackStatusLabel});
            this.StatusBar.Location = new System.Drawing.Point(0, 411);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(802, 22);
            this.StatusBar.TabIndex = 12;
            this.StatusBar.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(33, 17);
            this.toolStripStatusLabel1.Text = "Stan:";
            // 
            // TrackStatusLabel
            // 
            this.TrackStatusLabel.Name = "TrackStatusLabel";
            this.TrackStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // WaveformChart
            // 
            this.WaveformChart.BackColor = System.Drawing.Color.Transparent;
            chartArea1.AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
            chartArea1.AxisX2.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
            chartArea1.AxisY.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
            chartArea1.AxisY2.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
            chartArea1.BackColor = System.Drawing.Color.Transparent;
            chartArea1.Name = "MainArea";
            this.WaveformChart.ChartAreas.Add(chartArea1);
            this.WaveformChart.Location = new System.Drawing.Point(152, 19);
            this.WaveformChart.Name = "WaveformChart";
            this.WaveformChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
            this.WaveformChart.Size = new System.Drawing.Size(131, 88);
            this.WaveformChart.TabIndex = 14;
            this.WaveformChart.Text = "chart1";
            // 
            // WaveTypeGroup
            // 
            this.WaveTypeGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.WaveTypeGroup.Controls.Add(this.WaveformChart);
            this.WaveTypeGroup.Location = new System.Drawing.Point(499, 36);
            this.WaveTypeGroup.Name = "WaveTypeGroup";
            this.WaveTypeGroup.Size = new System.Drawing.Size(289, 115);
            this.WaveTypeGroup.TabIndex = 15;
            this.WaveTypeGroup.TabStop = false;
            this.WaveTypeGroup.Text = "Typ fali dźwiękowej";
            // 
            // positionMarkerOverlay
            // 
            this.positionMarkerOverlay.BackColor = System.Drawing.Color.Transparent;
            this.positionMarkerOverlay.Location = new System.Drawing.Point(12, 175);
            this.positionMarkerOverlay.Name = "positionMarkerOverlay";
            this.positionMarkerOverlay.Size = new System.Drawing.Size(776, 14);
            this.positionMarkerOverlay.TabIndex = 13;
            // 
            // Tempo
            // 
            this.Tempo.Location = new System.Drawing.Point(128, 70);
            this.Tempo.MaxValue = 200;
            this.Tempo.MinValue = 10;
            this.Tempo.Name = "Tempo";
            this.Tempo.Size = new System.Drawing.Size(72, 20);
            this.Tempo.TabIndex = 11;
            this.Tempo.Text = "0";
            this.Tempo.Value = 0;
            // 
            // TrackLength
            // 
            this.TrackLength.Location = new System.Drawing.Point(128, 46);
            this.TrackLength.MaxValue = 60;
            this.TrackLength.MinValue = 10;
            this.TrackLength.Name = "TrackLength";
            this.TrackLength.Size = new System.Drawing.Size(72, 20);
            this.TrackLength.TabIndex = 10;
            this.TrackLength.Text = "0";
            this.TrackLength.Value = 0;
            // 
            // keyboardGrid
            // 
            this.keyboardGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.keyboardGrid.AutoScroll = true;
            this.keyboardGrid.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.keyboardGrid.Location = new System.Drawing.Point(12, 195);
            this.keyboardGrid.Name = "keyboardGrid";
            this.keyboardGrid.OnGridUpdate = null;
            this.keyboardGrid.Size = new System.Drawing.Size(776, 201);
            this.keyboardGrid.TabIndex = 1;
            // 
            // MainLayout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 433);
            this.Controls.Add(this.WaveTypeGroup);
            this.Controls.Add(this.positionMarkerOverlay);
            this.Controls.Add(this.StatusBar);
            this.Controls.Add(this.Tempo);
            this.Controls.Add(this.TrackLength);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.playPauseButton);
            this.Controls.Add(this.TempoLabel);
            this.Controls.Add(this.trackLengthLabel);
            this.Controls.Add(this.keyboardGrid);
            this.Controls.Add(this.mainMenu);
            this.MainMenuStrip = this.mainMenu;
            this.MinimumSize = new System.Drawing.Size(818, 472);
            this.Name = "MainLayout";
            this.Text = "Synthesizer";
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.StatusBar.ResumeLayout(false);
            this.StatusBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WaveformChart)).EndInit();
            this.WaveTypeGroup.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Views.KeyboardGrid keyboardGrid;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem plikToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem otwórzToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zapiszToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zapiszJakoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem zamknijToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pomocToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oProgramioeToolStripMenuItem;
        private System.Windows.Forms.Label trackLengthLabel;
        private System.Windows.Forms.Label TempoLabel;
        private System.Windows.Forms.Button playPauseButton;
        private System.Windows.Forms.Button stopButton;
        private Views.NumberBox TrackLength;
        private Views.NumberBox Tempo;
        private System.Windows.Forms.StatusStrip StatusBar;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel TrackStatusLabel;
        private Views.PositionMarkerOverlay positionMarkerOverlay;
        private System.Windows.Forms.DataVisualization.Charting.Chart WaveformChart;
        private System.Windows.Forms.GroupBox WaveTypeGroup;
        private System.Windows.Forms.ToolStripMenuItem plikToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem MenuItemSave;
        private System.Windows.Forms.ToolStripMenuItem MenuItemSaveAs;
        private System.Windows.Forms.ToolStripMenuItem MenuItemLoad;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem MenuItemExport;
        private System.Windows.Forms.ToolStripMenuItem informacjeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuItemAbout;
    }
}

