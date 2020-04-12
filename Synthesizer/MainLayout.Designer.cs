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
            this.components = new System.ComponentModel.Container();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.plikToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.otwórzToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zapiszToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zapiszJakoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.zamknijToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pomocToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oProgramioeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trackLengthLabel = new System.Windows.Forms.Label();
            this.speedLabel = new System.Windows.Forms.Label();
            this.rythmLabel = new System.Windows.Forms.Label();
            this.meterBox = new System.Windows.Forms.ComboBox();
            this.playPauseButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.positionSlider1 = new Synthesizer.Views.PositionSlider();
            this.keyboardGrid = new Synthesizer.Views.KeyboardGrid();
            this.keyboardBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.keyboardBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.keyboardBindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.keyboardBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.keyboardBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.keyboardBindingSource2)).BeginInit();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(800, 24);
            this.mainMenu.TabIndex = 2;
            this.mainMenu.Text = "Menu";
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
            // speedLabel
            // 
            this.speedLabel.AutoSize = true;
            this.speedLabel.Location = new System.Drawing.Point(24, 73);
            this.speedLabel.Name = "speedLabel";
            this.speedLabel.Size = new System.Drawing.Size(55, 13);
            this.speedLabel.TabIndex = 4;
            this.speedLabel.Text = "Prędkość:";
            // 
            // rythmLabel
            // 
            this.rythmLabel.AutoSize = true;
            this.rythmLabel.Location = new System.Drawing.Point(24, 97);
            this.rythmLabel.Name = "rythmLabel";
            this.rythmLabel.Size = new System.Drawing.Size(45, 13);
            this.rythmLabel.TabIndex = 5;
            this.rythmLabel.Text = "Metrum:";
            // 
            // meterBox
            // 
            this.meterBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.meterBox.FormattingEnabled = true;
            this.meterBox.Location = new System.Drawing.Point(128, 94);
            this.meterBox.Name = "meterBox";
            this.meterBox.Size = new System.Drawing.Size(72, 21);
            this.meterBox.TabIndex = 6;
            // 
            // playPauseButton
            // 
            this.playPauseButton.Enabled = false;
            this.playPauseButton.Location = new System.Drawing.Point(295, 153);
            this.playPauseButton.Name = "playPauseButton";
            this.playPauseButton.Size = new System.Drawing.Size(60, 54);
            this.playPauseButton.TabIndex = 7;
            this.playPauseButton.UseVisualStyleBackColor = true;
            this.playPauseButton.Click += new System.EventHandler(this.playPauseButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(389, 153);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(60, 54);
            this.stopButton.TabIndex = 8;
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // positionSlider1
            // 
            this.positionSlider1.Location = new System.Drawing.Point(48, 234);
            this.positionSlider1.Name = "positionSlider1";
            this.positionSlider1.Size = new System.Drawing.Size(722, 27);
            this.positionSlider1.TabIndex = 9;
            // 
            // keyboardGrid
            // 
            this.keyboardGrid.AutoScroll = true;
            this.keyboardGrid.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.keyboardGrid.Location = new System.Drawing.Point(12, 267);
            this.keyboardGrid.Name = "keyboardGrid";
            this.keyboardGrid.OnGridUpdate = null;
            this.keyboardGrid.Size = new System.Drawing.Size(776, 171);
            this.keyboardGrid.TabIndex = 1;
            // 
            // keyboardBindingSource1
            // 
            this.keyboardBindingSource1.DataSource = typeof(Synthesizer.Configuration.Keyboard);
            // 
            // keyboardBindingSource
            // 
            this.keyboardBindingSource.DataSource = typeof(Synthesizer.Configuration.Keyboard);
            // 
            // keyboardBindingSource2
            // 
            this.keyboardBindingSource2.DataSource = typeof(Synthesizer.Configuration.Keyboard);
            // 
            // MainLayout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.positionSlider1);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.playPauseButton);
            this.Controls.Add(this.meterBox);
            this.Controls.Add(this.rythmLabel);
            this.Controls.Add(this.speedLabel);
            this.Controls.Add(this.trackLengthLabel);
            this.Controls.Add(this.keyboardGrid);
            this.Controls.Add(this.mainMenu);
            this.MainMenuStrip = this.mainMenu;
            this.Name = "MainLayout";
            this.Text = "Synthesizer";
            ((System.ComponentModel.ISupportInitialize)(this.keyboardBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.keyboardBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.keyboardBindingSource2)).EndInit();
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
        private System.Windows.Forms.Label speedLabel;
        private System.Windows.Forms.Label rythmLabel;
        private System.Windows.Forms.ComboBox meterBox;
        private System.Windows.Forms.BindingSource keyboardBindingSource;
        private System.Windows.Forms.BindingSource keyboardBindingSource1;
        private System.Windows.Forms.BindingSource keyboardBindingSource2;
        private System.Windows.Forms.Button playPauseButton;
        private System.Windows.Forms.Button stopButton;
        private Views.PositionSlider positionSlider1;
    }
}

