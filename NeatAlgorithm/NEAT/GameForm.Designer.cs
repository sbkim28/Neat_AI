namespace NeatAlgorithm.NEAT
{
    partial class GameForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
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
            this.ChartTopScore = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.LblDuration = new System.Windows.Forms.Label();
            this.InputSpeed = new System.Windows.Forms.NumericUpDown();
            this.BtnStop = new System.Windows.Forms.Button();
            this.NetworkBox = new System.Windows.Forms.PictureBox();
            this.LblBest = new System.Windows.Forms.Label();
            this.LblBestWhere = new System.Windows.Forms.Label();
            this.LblScoreChart = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.SnakeBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InputGen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartTopScore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InputSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NetworkBox)).BeginInit();
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
            this.BtnLoad.DialogResult = System.Windows.Forms.DialogResult.Cancel;
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
            this.Filename.Size = new System.Drawing.Size(313, 21);
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
            this.InputGen.Location = new System.Drawing.Point(424, 44);
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
            this.BtnPlay.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnPlay.Enabled = false;
            this.BtnPlay.Location = new System.Drawing.Point(659, 45);
            this.BtnPlay.Name = "BtnPlay";
            this.BtnPlay.Size = new System.Drawing.Size(65, 22);
            this.BtnPlay.TabIndex = 10;
            this.BtnPlay.Text = "Play";
            this.BtnPlay.UseVisualStyleBackColor = true;
            this.BtnPlay.Click += new System.EventHandler(this.BtnPlay_Click);
            // 
            // ChartTopScore
            // 
            chartArea2.AxisX.Minimum = 0D;
            chartArea2.AxisY.MajorGrid.Interval = 10D;
            chartArea2.Name = "ChartArea1";
            chartArea2.Position.Auto = false;
            chartArea2.Position.Height = 90F;
            chartArea2.Position.Width = 100F;
            chartArea2.Position.Y = 5F;
            this.ChartTopScore.ChartAreas.Add(chartArea2);
            this.ChartTopScore.Location = new System.Drawing.Point(355, 116);
            this.ChartTopScore.Name = "ChartTopScore";
            this.ChartTopScore.Size = new System.Drawing.Size(370, 209);
            this.ChartTopScore.TabIndex = 11;
            this.ChartTopScore.Text = "Top Score";
            this.ChartTopScore.MouseEnter += new System.EventHandler(this.ChartTopScore_MouseEnter);
            this.ChartTopScore.MouseLeave += new System.EventHandler(this.ChartTopScore_MouseLeave);
            // 
            // LblDuration
            // 
            this.LblDuration.AutoSize = true;
            this.LblDuration.Font = new System.Drawing.Font("바탕", 9F);
            this.LblDuration.Location = new System.Drawing.Point(341, 77);
            this.LblDuration.Name = "LblDuration";
            this.LblDuration.Size = new System.Drawing.Size(61, 12);
            this.LblDuration.TabIndex = 4;
            this.LblDuration.Text = "Duration:";
            // 
            // InputSpeed
            // 
            this.InputSpeed.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.InputSpeed.Location = new System.Drawing.Point(424, 71);
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
            this.BtnStop.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnStop.Location = new System.Drawing.Point(659, 73);
            this.BtnStop.Name = "BtnStop";
            this.BtnStop.Size = new System.Drawing.Size(65, 22);
            this.BtnStop.TabIndex = 10;
            this.BtnStop.Text = "Stop";
            this.BtnStop.UseVisualStyleBackColor = true;
            this.BtnStop.Click += new System.EventHandler(this.BtnStop_Click);
            // 
            // NetworkBox
            // 
            this.NetworkBox.BackColor = System.Drawing.SystemColors.ControlDark;
            this.NetworkBox.Location = new System.Drawing.Point(354, 331);
            this.NetworkBox.Name = "NetworkBox";
            this.NetworkBox.Size = new System.Drawing.Size(370, 200);
            this.NetworkBox.TabIndex = 12;
            this.NetworkBox.TabStop = false;
            this.NetworkBox.Paint += new System.Windows.Forms.PaintEventHandler(this.UpdateTopology);
            // 
            // LblBest
            // 
            this.LblBest.AutoSize = true;
            this.LblBest.Font = new System.Drawing.Font("바탕", 9F);
            this.LblBest.Location = new System.Drawing.Point(550, 50);
            this.LblBest.Name = "LblBest";
            this.LblBest.Size = new System.Drawing.Size(63, 12);
            this.LblBest.TabIndex = 4;
            this.LblBest.Text = "Best: Gen";
            // 
            // LblBestWhere
            // 
            this.LblBestWhere.AutoSize = true;
            this.LblBestWhere.Font = new System.Drawing.Font("바탕", 9F);
            this.LblBestWhere.Location = new System.Drawing.Point(619, 50);
            this.LblBestWhere.Name = "LblBestWhere";
            this.LblBestWhere.Size = new System.Drawing.Size(11, 12);
            this.LblBestWhere.TabIndex = 4;
            this.LblBestWhere.Text = "0";
            // 
            // LblScoreChart
            // 
            this.LblScoreChart.AutoSize = true;
            this.LblScoreChart.Font = new System.Drawing.Font("바탕", 9F);
            this.LblScoreChart.Location = new System.Drawing.Point(352, 101);
            this.LblScoreChart.Name = "LblScoreChart";
            this.LblScoreChart.Size = new System.Drawing.Size(64, 12);
            this.LblScoreChart.TabIndex = 4;
            this.LblScoreChart.Text = "Top Score";
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 543);
            this.Controls.Add(this.NetworkBox);
            this.Controls.Add(this.ChartTopScore);
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
            this.Controls.Add(this.LblDuration);
            this.Controls.Add(this.LblBestWhere);
            this.Controls.Add(this.LblBest);
            this.Controls.Add(this.LblScoreChart);
            this.Controls.Add(this.LblGenInput);
            this.Controls.Add(this.Filename);
            this.Controls.Add(this.BtnLoad);
            this.Controls.Add(this.SnakeBox);
            this.Name = "GameForm";
            this.Text = "SnakeForm";
            ((System.ComponentModel.ISupportInitialize)(this.SnakeBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InputGen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartTopScore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InputSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NetworkBox)).EndInit();
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
        private System.Windows.Forms.DataVisualization.Charting.Chart ChartTopScore;
        private System.Windows.Forms.Label LblDuration;
        private System.Windows.Forms.NumericUpDown InputSpeed;
        private System.Windows.Forms.Button BtnStop;
        private System.Windows.Forms.PictureBox NetworkBox;
        private System.Windows.Forms.Label LblBest;
        private System.Windows.Forms.Label LblBestWhere;
        private System.Windows.Forms.Label LblScoreChart;
    }
}