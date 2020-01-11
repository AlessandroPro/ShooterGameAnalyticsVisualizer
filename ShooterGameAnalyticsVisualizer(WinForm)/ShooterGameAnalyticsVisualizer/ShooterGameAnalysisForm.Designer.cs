namespace ShooterGameAnalyticsVisualizer
{
    partial class ShooterGameAnalysisForm
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
            this.playerDeathHeatmap = new System.Windows.Forms.PictureBox();
            this.databaseTableView = new System.Windows.Forms.DataGridView();
            this.playerRadioButton = new System.Windows.Forms.RadioButton();
            this.GameSessionList = new System.Windows.Forms.ComboBox();
            this.OpenDatabase = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.enemiesRadioButton = new System.Windows.Forms.RadioButton();
            this.statsGroupBox = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tableNameLabel = new System.Windows.Forms.Label();
            this.databaseLabel = new System.Windows.Forms.Label();
            this.playerRoundsLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.playerDeathHeatmap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.databaseTableView)).BeginInit();
            this.statsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // playerDeathHeatmap
            // 
            this.playerDeathHeatmap.Location = new System.Drawing.Point(538, 115);
            this.playerDeathHeatmap.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.playerDeathHeatmap.Name = "playerDeathHeatmap";
            this.playerDeathHeatmap.Size = new System.Drawing.Size(510, 490);
            this.playerDeathHeatmap.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.playerDeathHeatmap.TabIndex = 1;
            this.playerDeathHeatmap.TabStop = false;
            // 
            // databaseTableView
            // 
            this.databaseTableView.AllowUserToAddRows = false;
            this.databaseTableView.AllowUserToDeleteRows = false;
            this.databaseTableView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.databaseTableView.Location = new System.Drawing.Point(60, 671);
            this.databaseTableView.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.databaseTableView.Name = "databaseTableView";
            this.databaseTableView.ReadOnly = true;
            this.databaseTableView.Size = new System.Drawing.Size(988, 333);
            this.databaseTableView.TabIndex = 2;
            // 
            // playerRadioButton
            // 
            this.playerRadioButton.AutoSize = true;
            this.playerRadioButton.Checked = true;
            this.playerRadioButton.Location = new System.Drawing.Point(12, 60);
            this.playerRadioButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.playerRadioButton.Name = "playerRadioButton";
            this.playerRadioButton.Size = new System.Drawing.Size(104, 29);
            this.playerRadioButton.TabIndex = 3;
            this.playerRadioButton.TabStop = true;
            this.playerRadioButton.Text = "Player";
            this.playerRadioButton.UseVisualStyleBackColor = true;
            this.playerRadioButton.CheckedChanged += new System.EventHandler(this.PlayerRadioButton_CheckedChanged);
            // 
            // GameSessionList
            // 
            this.GameSessionList.FormattingEnabled = true;
            this.GameSessionList.Location = new System.Drawing.Point(66, 237);
            this.GameSessionList.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.GameSessionList.Name = "GameSessionList";
            this.GameSessionList.Size = new System.Drawing.Size(432, 33);
            this.GameSessionList.TabIndex = 4;
            this.GameSessionList.SelectedIndexChanged += new System.EventHandler(this.GameSessionList_SelectedIndexChanged);
            // 
            // OpenDatabase
            // 
            this.OpenDatabase.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.OpenDatabase.Location = new System.Drawing.Point(72, 62);
            this.OpenDatabase.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.OpenDatabase.Name = "OpenDatabase";
            this.OpenDatabase.Size = new System.Drawing.Size(430, 44);
            this.OpenDatabase.TabIndex = 5;
            this.OpenDatabase.Text = "Open Database";
            this.OpenDatabase.UseVisualStyleBackColor = true;
            this.OpenDatabase.Click += new System.EventHandler(this.OpenDatabase_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Database Files (*.db) | *.db";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(66, 206);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(249, 25);
            this.label1.TabIndex = 6;
            this.label1.Text = "Choose a game session:";
            // 
            // enemiesRadioButton
            // 
            this.enemiesRadioButton.AutoSize = true;
            this.enemiesRadioButton.Location = new System.Drawing.Point(12, 104);
            this.enemiesRadioButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.enemiesRadioButton.Name = "enemiesRadioButton";
            this.enemiesRadioButton.Size = new System.Drawing.Size(126, 29);
            this.enemiesRadioButton.TabIndex = 7;
            this.enemiesRadioButton.Text = "Enemies";
            this.enemiesRadioButton.UseVisualStyleBackColor = true;
            // 
            // statsGroupBox
            // 
            this.statsGroupBox.Controls.Add(this.playerRadioButton);
            this.statsGroupBox.Controls.Add(this.enemiesRadioButton);
            this.statsGroupBox.Location = new System.Drawing.Point(54, 438);
            this.statsGroupBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.statsGroupBox.Name = "statsGroupBox";
            this.statsGroupBox.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.statsGroupBox.Size = new System.Drawing.Size(448, 167);
            this.statsGroupBox.TabIndex = 8;
            this.statsGroupBox.TabStop = false;
            this.statsGroupBox.Text = "Show statistics for:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(532, 62);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(352, 25);
            this.label2.TabIndex = 9;
            this.label2.Text = "Heat Map of player death positions:";
            // 
            // tableNameLabel
            // 
            this.tableNameLabel.AutoSize = true;
            this.tableNameLabel.Location = new System.Drawing.Point(54, 623);
            this.tableNameLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.tableNameLabel.Name = "tableNameLabel";
            this.tableNameLabel.Size = new System.Drawing.Size(127, 25);
            this.tableNameLabel.TabIndex = 10;
            this.tableNameLabel.Text = "Table Stats:";
            // 
            // databaseLabel
            // 
            this.databaseLabel.AutoSize = true;
            this.databaseLabel.Location = new System.Drawing.Point(72, 137);
            this.databaseLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.databaseLabel.Name = "databaseLabel";
            this.databaseLabel.Size = new System.Drawing.Size(110, 25);
            this.databaseLabel.TabIndex = 11;
            this.databaseLabel.Text = "Database:";
            // 
            // playerRoundsLabel
            // 
            this.playerRoundsLabel.AutoSize = true;
            this.playerRoundsLabel.Location = new System.Drawing.Point(66, 333);
            this.playerRoundsLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.playerRoundsLabel.Name = "playerRoundsLabel";
            this.playerRoundsLabel.Size = new System.Drawing.Size(345, 25);
            this.playerRoundsLabel.TabIndex = 12;
            this.playerRoundsLabel.Text = "Number of rounds (player deaths): ";
            // 
            // ShooterGameAnalysisForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Menu;
            this.ClientSize = new System.Drawing.Size(1104, 1063);
            this.Controls.Add(this.playerRoundsLabel);
            this.Controls.Add(this.databaseLabel);
            this.Controls.Add(this.tableNameLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.statsGroupBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.OpenDatabase);
            this.Controls.Add(this.GameSessionList);
            this.Controls.Add(this.databaseTableView);
            this.Controls.Add(this.playerDeathHeatmap);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "ShooterGameAnalysisForm";
            this.Text = "Shooter Game Analysis";
            ((System.ComponentModel.ISupportInitialize)(this.playerDeathHeatmap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.databaseTableView)).EndInit();
            this.statsGroupBox.ResumeLayout(false);
            this.statsGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox playerDeathHeatmap;
        private System.Windows.Forms.DataGridView databaseTableView;
        private System.Windows.Forms.RadioButton playerRadioButton;
        private System.Windows.Forms.ComboBox GameSessionList;
        private System.Windows.Forms.Button OpenDatabase;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton enemiesRadioButton;
        private System.Windows.Forms.GroupBox statsGroupBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label tableNameLabel;
        private System.Windows.Forms.Label databaseLabel;
        private System.Windows.Forms.Label playerRoundsLabel;
    }
}

