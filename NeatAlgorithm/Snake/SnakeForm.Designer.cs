﻿namespace NeatAlgorithm.Snake
{
    partial class SnakeForm
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
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.SnakeBox = new System.Windows.Forms.PictureBox();
            this.DataFile = new System.Windows.Forms.OpenFileDialog();
            this.BtnLoad = new System.Windows.Forms.Button();
            this.Filename = new System.Windows.Forms.TextBox();
            this.LblGenInput = new System.Windows.Forms.Label();
            this.InputGen = new System.Windows.Forms.NumericUpDown();
            this.LblScore = new System.Windows.Forms.Label();
            this.LblScoreValue = new System.Windows.Forms.Label();
            this.LblFitness = new System.Windows.Forms.Label();
            this.LblFitnessValue = new System.Windows.Forms.Label();
            this.LblGenomeId = new System.Windows.Forms.Label();
            this.LblGenomeIdValue = new System.Windows.Forms.Label();
            this.LblFromValue = new System.Windows.Forms.Label();
            this.LblFrom = new System.Windows.Forms.Label();
            this.LblGenValue = new System.Windows.Forms.Label();
            this.LblGen = new System.Windows.Forms.Label();
            this.LblHunger = new System.Windows.Forms.Label();
            this.LblHungerValue = new System.Windows.Forms.Label();
            this.BtnPlay = new System.Windows.Forms.Button();
            this.TopScore = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.LblSpeed = new System.Windows.Forms.Label();
            this.InputSpeed = new System.Windows.Forms.NumericUpDown();
            this.BtnStop = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.SnakeBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InputGen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TopScore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InputSpeed)).BeginInit();
            this.SuspendLayout();
            // 
            // SnakeBox
            // 
            this.SnakeBox.BackColor = System.Drawing.SystemColors.ControlDark;
            this.SnakeBox.Location = new System.Drawing.Point(12, 12);
            this.SnakeBox.Name = "SnakeBox";
            this.SnakeBox.Size = new System.Drawing.Size(320, 320);
            this.SnakeBox.TabIndex = 0;
            this.SnakeBox.TabStop = false;
            this.SnakeBox.Paint += new System.Windows.Forms.PaintEventHandler(this.UpdateGraphics);
            // 
            // DataFile
            // 
            this.DataFile.FileName = "openFileDialog1";
            this.DataFile.Filter = "데이터 파일 (*.txt)|*.txt|모든 파일 (*.*)|*.*";
            this.DataFile.Title = "데이터 로";
            // 
            // BtnLoad
            // 
            this.BtnLoad.Font = new System.Drawing.Font("바탕", 9F);
            this.BtnLoad.Location = new System.Drawing.Point(338, 12);
            this.BtnLoad.Name = "BtnLoad";
            this.BtnLoad.Size = new System.Drawing.Size(67, 28);
            this.BtnLoad.TabIndex = 1;
            this.BtnLoad.Text = "Load";
            this.BtnLoad.UseVisualStyleBackColor = true;
            this.BtnLoad.Click += new System.EventHandler(this.BtnLoad_Click);
            // 
            // Filename
            // 
            this.Filename.Font = new System.Drawing.Font("바탕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Filename.Location = new System.Drawing.Point(411, 17);
            this.Filename.Name = "Filename";
            this.Filename.Size = new System.Drawing.Size(301, 21);
            this.Filename.TabIndex = 2;
            // 
            // LblGenInput
            // 
            this.LblGenInput.AutoSize = true;
            this.LblGenInput.Font = new System.Drawing.Font("바탕", 9F);
            this.LblGenInput.Location = new System.Drawing.Point(341, 50);
            this.LblGenInput.Name = "LblGenInput";
            this.LblGenInput.Size = new System.Drawing.Size(33, 12);
            this.LblGenInput.TabIndex = 4;
            this.LblGenInput.Text = "Gen:";
            // 
            // InputGen
            // 
            this.InputGen.Enabled = false;
            this.InputGen.Location = new System.Drawing.Point(393, 48);
            this.InputGen.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.InputGen.Name = "InputGen";
            this.InputGen.Size = new System.Drawing.Size(120, 21);
            this.InputGen.TabIndex = 5;
            // 
            // LblScore
            // 
            this.LblScore.AutoSize = true;
            this.LblScore.Font = new System.Drawing.Font("바탕", 9F);
            this.LblScore.Location = new System.Drawing.Point(55, 404);
            this.LblScore.Name = "LblScore";
            this.LblScore.Size = new System.Drawing.Size(51, 12);
            this.LblScore.TabIndex = 6;
            this.LblScore.Text = "Score : ";
            // 
            // LblScoreValue
            // 
            this.LblScoreValue.AutoSize = true;
            this.LblScoreValue.Font = new System.Drawing.Font("바탕", 9F);
            this.LblScoreValue.Location = new System.Drawing.Point(115, 404);
            this.LblScoreValue.Name = "LblScoreValue";
            this.LblScoreValue.Size = new System.Drawing.Size(11, 12);
            this.LblScoreValue.TabIndex = 7;
            this.LblScoreValue.Text = "0";
            // 
            // LblFitness
            // 
            this.LblFitness.AutoSize = true;
            this.LblFitness.Location = new System.Drawing.Point(55, 377);
            this.LblFitness.Name = "LblFitness";
            this.LblFitness.Size = new System.Drawing.Size(58, 12);
            this.LblFitness.TabIndex = 8;
            this.LblFitness.Text = "Fitness : ";
            // 
            // LblFitnessValue
            // 
            this.LblFitnessValue.AutoSize = true;
            this.LblFitnessValue.Font = new System.Drawing.Font("바탕", 9F);
            this.LblFitnessValue.Location = new System.Drawing.Point(115, 377);
            this.LblFitnessValue.Name = "LblFitnessValue";
            this.LblFitnessValue.Size = new System.Drawing.Size(11, 12);
            this.LblFitnessValue.TabIndex = 6;
            this.LblFitnessValue.Text = "0";
            // 
            // LblGenomeId
            // 
            this.LblGenomeId.AutoSize = true;
            this.LblGenomeId.Font = new System.Drawing.Font("바탕", 9F);
            this.LblGenomeId.Location = new System.Drawing.Point(188, 351);
            this.LblGenomeId.Name = "LblGenomeId";
            this.LblGenomeId.Size = new System.Drawing.Size(77, 12);
            this.LblGenomeId.TabIndex = 9;
            this.LblGenomeId.Text = "Genome Id :";
            // 
            // LblGenomeIdValue
            // 
            this.LblGenomeIdValue.AutoSize = true;
            this.LblGenomeIdValue.Font = new System.Drawing.Font("바탕", 9F);
            this.LblGenomeIdValue.Location = new System.Drawing.Point(271, 351);
            this.LblGenomeIdValue.Name = "LblGenomeIdValue";
            this.LblGenomeIdValue.Size = new System.Drawing.Size(11, 12);
            this.LblGenomeIdValue.TabIndex = 7;
            this.LblGenomeIdValue.Text = "0";
            // 
            // LblFromValue
            // 
            this.LblFromValue.AutoSize = true;
            this.LblFromValue.Font = new System.Drawing.Font("바탕", 9F);
            this.LblFromValue.Location = new System.Drawing.Point(271, 377);
            this.LblFromValue.Name = "LblFromValue";
            this.LblFromValue.Size = new System.Drawing.Size(11, 12);
            this.LblFromValue.TabIndex = 6;
            this.LblFromValue.Text = "0";
            // 
            // LblFrom
            // 
            this.LblFrom.AutoSize = true;
            this.LblFrom.Location = new System.Drawing.Point(188, 377);
            this.LblFrom.Name = "LblFrom";
            this.LblFrom.Size = new System.Drawing.Size(81, 12);
            this.LblFrom.TabIndex = 8;
            this.LblFrom.Text = "From :    Gen";
            // 
            // LblGenValue
            // 
            this.LblGenValue.AutoSize = true;
            this.LblGenValue.Font = new System.Drawing.Font("바탕", 9F);
            this.LblGenValue.Location = new System.Drawing.Point(115, 351);
            this.LblGenValue.Name = "LblGenValue";
            this.LblGenValue.Size = new System.Drawing.Size(11, 12);
            this.LblGenValue.TabIndex = 7;
            this.LblGenValue.Text = "0";
            // 
            // LblGen
            // 
            this.LblGen.AutoSize = true;
            this.LblGen.Font = new System.Drawing.Font("바탕", 9F);
            this.LblGen.Location = new System.Drawing.Point(55, 351);
            this.LblGen.Name = "LblGen";
            this.LblGen.Size = new System.Drawing.Size(37, 12);
            this.LblGen.TabIndex = 9;
            this.LblGen.Text = "Gen :";
            // 
            // LblHunger
            // 
            this.LblHunger.AutoSize = true;
            this.LblHunger.Font = new System.Drawing.Font("바탕", 9F);
            this.LblHunger.Location = new System.Drawing.Point(188, 404);
            this.LblHunger.Name = "LblHunger";
            this.LblHunger.Size = new System.Drawing.Size(57, 12);
            this.LblHunger.TabIndex = 6;
            this.LblHunger.Text = "Hunger :";
            // 
            // LblHungerValue
            // 
            this.LblHungerValue.AutoSize = true;
            this.LblHungerValue.Font = new System.Drawing.Font("바탕", 9F);
            this.LblHungerValue.Location = new System.Drawing.Point(271, 404);
            this.LblHungerValue.Name = "LblHungerValue";
            this.LblHungerValue.Size = new System.Drawing.Size(11, 12);
            this.LblHungerValue.TabIndex = 7;
            this.LblHungerValue.Text = "0";
            // 
            // BtnPlay
            // 
            this.BtnPlay.Enabled = false;
            this.BtnPlay.Location = new System.Drawing.Point(647, 47);
            this.BtnPlay.Name = "BtnPlay";
            this.BtnPlay.Size = new System.Drawing.Size(65, 22);
            this.BtnPlay.TabIndex = 10;
            this.BtnPlay.Text = "Play";
            this.BtnPlay.UseVisualStyleBackColor = true;
            this.BtnPlay.Click += new System.EventHandler(this.BtnPlay_Click);
            // 
            // TopScore
            // 
            chartArea1.Name = "ChartArea1";
            this.TopScore.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.TopScore.Legends.Add(legend1);
            this.TopScore.Location = new System.Drawing.Point(393, 191);
            this.TopScore.Name = "TopScore";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.TopScore.Series.Add(series1);
            this.TopScore.Size = new System.Drawing.Size(300, 198);
            this.TopScore.TabIndex = 11;
            this.TopScore.Text = "Top Score";
            // 
            // LblSpeed
            // 
            this.LblSpeed.AutoSize = true;
            this.LblSpeed.Font = new System.Drawing.Font("바탕", 9F);
            this.LblSpeed.Location = new System.Drawing.Point(341, 85);
            this.LblSpeed.Name = "LblSpeed";
            this.LblSpeed.Size = new System.Drawing.Size(45, 12);
            this.LblSpeed.TabIndex = 4;
            this.LblSpeed.Text = "Speed:";
            // 
            // InputSpeed
            // 
            this.InputSpeed.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.InputSpeed.Location = new System.Drawing.Point(393, 83);
            this.InputSpeed.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.InputSpeed.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.InputSpeed.Name = "InputSpeed";
            this.InputSpeed.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.InputSpeed.Size = new System.Drawing.Size(120, 21);
            this.InputSpeed.TabIndex = 5;
            this.InputSpeed.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // BtnStop
            // 
            this.BtnStop.Location = new System.Drawing.Point(647, 75);
            this.BtnStop.Name = "BtnStop";
            this.BtnStop.Size = new System.Drawing.Size(65, 22);
            this.BtnStop.TabIndex = 10;
            this.BtnStop.Text = "Stop";
            this.BtnStop.UseVisualStyleBackColor = true;
            this.BtnStop.Click += new System.EventHandler(this.BtnStop_Click);
            // 
            // SnakeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 496);
            this.Controls.Add(this.TopScore);
            this.Controls.Add(this.BtnStop);
            this.Controls.Add(this.BtnPlay);
            this.Controls.Add(this.LblGen);
            this.Controls.Add(this.LblGenomeId);
            this.Controls.Add(this.LblFrom);
            this.Controls.Add(this.LblFitness);
            this.Controls.Add(this.LblGenValue);
            this.Controls.Add(this.LblGenomeIdValue);
            this.Controls.Add(this.LblFromValue);
            this.Controls.Add(this.LblHungerValue);
            this.Controls.Add(this.LblScoreValue);
            this.Controls.Add(this.LblFitnessValue);
            this.Controls.Add(this.LblHunger);
            this.Controls.Add(this.LblScore);
            this.Controls.Add(this.InputSpeed);
            this.Controls.Add(this.InputGen);
            this.Controls.Add(this.LblSpeed);
            this.Controls.Add(this.LblGenInput);
            this.Controls.Add(this.Filename);
            this.Controls.Add(this.BtnLoad);
            this.Controls.Add(this.SnakeBox);
            this.Name = "SnakeForm";
            this.Text = "SnakeForm";
            this.Load += new System.EventHandler(this.SnakeForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.SnakeBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InputGen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TopScore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InputSpeed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox SnakeBox;
        private System.Windows.Forms.OpenFileDialog DataFile;
        private System.Windows.Forms.Button BtnLoad;
        private System.Windows.Forms.TextBox Filename;
        private System.Windows.Forms.Label LblGenInput;
        private System.Windows.Forms.NumericUpDown InputGen;
        private System.Windows.Forms.Label LblScore;
        private System.Windows.Forms.Label LblScoreValue;
        private System.Windows.Forms.Label LblFitness;
        private System.Windows.Forms.Label LblFitnessValue;
        private System.Windows.Forms.Label LblGenomeId;
        private System.Windows.Forms.Label LblGenomeIdValue;
        private System.Windows.Forms.Label LblFromValue;
        private System.Windows.Forms.Label LblFrom;
        private System.Windows.Forms.Label LblGenValue;
        private System.Windows.Forms.Label LblGen;
        private System.Windows.Forms.Label LblHunger;
        private System.Windows.Forms.Label LblHungerValue;
        private System.Windows.Forms.Button BtnPlay;
        private System.Windows.Forms.DataVisualization.Charting.Chart TopScore;
        private System.Windows.Forms.Label LblSpeed;
        private System.Windows.Forms.NumericUpDown InputSpeed;
        private System.Windows.Forms.Button BtnStop;
    }
}