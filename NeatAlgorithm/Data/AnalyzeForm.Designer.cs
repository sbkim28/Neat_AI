﻿namespace NeatAlgorithm.Data
{
    partial class AnalyzeForm
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
            this.Graph = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.BtnLoad = new System.Windows.Forms.Button();
            this.Filename = new System.Windows.Forms.TextBox();
            this.DmnX = new System.Windows.Forms.DomainUpDown();
            this.DmnY = new System.Windows.Forms.DomainUpDown();
            this.LblX = new System.Windows.Forms.Label();
            this.LblY = new System.Windows.Forms.Label();
            this.Progress = new System.Windows.Forms.ProgressBar();
            this.LblCurrentValue = new System.Windows.Forms.Label();
            this.LblOf = new System.Windows.Forms.Label();
            this.LblAllCount = new System.Windows.Forms.Label();
            this.LblState = new System.Windows.Forms.Label();
            this.BtnDraw = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Graph)).BeginInit();
            this.SuspendLayout();
            // 
            // Graph
            // 
            chartArea1.Name = "CharArea";
            this.Graph.ChartAreas.Add(chartArea1);
            this.Graph.Location = new System.Drawing.Point(13, 13);
            this.Graph.Name = "Graph";
            this.Graph.Size = new System.Drawing.Size(837, 636);
            this.Graph.TabIndex = 0;
            this.Graph.Text = "chart1";
            // 
            // BtnLoad
            // 
            this.BtnLoad.Font = new System.Drawing.Font("바탕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnLoad.Location = new System.Drawing.Point(856, 13);
            this.BtnLoad.Name = "BtnLoad";
            this.BtnLoad.Size = new System.Drawing.Size(79, 25);
            this.BtnLoad.TabIndex = 1;
            this.BtnLoad.Text = "Load";
            this.BtnLoad.UseVisualStyleBackColor = true;
            this.BtnLoad.Click += new System.EventHandler(this.BtnLoad_Click);
            // 
            // Filename
            // 
            this.Filename.Font = new System.Drawing.Font("바탕", 9F);
            this.Filename.Location = new System.Drawing.Point(941, 16);
            this.Filename.Name = "Filename";
            this.Filename.Size = new System.Drawing.Size(231, 21);
            this.Filename.TabIndex = 2;
            // 
            // DmnX
            // 
            this.DmnX.Items.Add("Generation");
            this.DmnX.Items.Add("Top Score (Best)");
            this.DmnX.Items.Add("Average Score (Best)");
            this.DmnX.Items.Add("Fitness (Best)");
            this.DmnX.Items.Add("Execution Time");
            this.DmnX.Items.Add("Top Score (All)");
            this.DmnX.Items.Add("Average Score (All)");
            this.DmnX.Items.Add("Fitness (All)");
            this.DmnX.Location = new System.Drawing.Point(856, 63);
            this.DmnX.Name = "DmnX";
            this.DmnX.Size = new System.Drawing.Size(146, 21);
            this.DmnX.TabIndex = 3;
            this.DmnX.Text = "Generation";
            // 
            // DmnY
            // 
            this.DmnY.Items.Add("Generation");
            this.DmnY.Items.Add("Top Score (Best)");
            this.DmnY.Items.Add("Average Score (Best)");
            this.DmnY.Items.Add("Fitness (Best)");
            this.DmnY.Items.Add("Execution Time");
            this.DmnY.Items.Add("Top Score (All)");
            this.DmnY.Items.Add("Average Score (All)");
            this.DmnY.Items.Add("Fitness (All)");
            this.DmnY.Location = new System.Drawing.Point(1014, 63);
            this.DmnY.Name = "DmnY";
            this.DmnY.Size = new System.Drawing.Size(158, 21);
            this.DmnY.TabIndex = 4;
            this.DmnY.Text = "Generation";
            // 
            // LblX
            // 
            this.LblX.AutoSize = true;
            this.LblX.Font = new System.Drawing.Font("바탕", 9F);
            this.LblX.Location = new System.Drawing.Point(858, 45);
            this.LblX.Name = "LblX";
            this.LblX.Size = new System.Drawing.Size(16, 12);
            this.LblX.TabIndex = 5;
            this.LblX.Text = "X:";
            // 
            // LblY
            // 
            this.LblY.AutoSize = true;
            this.LblY.Font = new System.Drawing.Font("바탕", 9F);
            this.LblY.Location = new System.Drawing.Point(1012, 45);
            this.LblY.Name = "LblY";
            this.LblY.Size = new System.Drawing.Size(16, 12);
            this.LblY.TabIndex = 5;
            this.LblY.Text = "Y:";
            // 
            // Progress
            // 
            this.Progress.Location = new System.Drawing.Point(860, 626);
            this.Progress.Name = "Progress";
            this.Progress.Size = new System.Drawing.Size(230, 23);
            this.Progress.TabIndex = 6;
            // 
            // LblCurrentValue
            // 
            this.LblCurrentValue.AutoSize = true;
            this.LblCurrentValue.Font = new System.Drawing.Font("바탕", 9F);
            this.LblCurrentValue.Location = new System.Drawing.Point(1100, 623);
            this.LblCurrentValue.Name = "LblCurrentValue";
            this.LblCurrentValue.Size = new System.Drawing.Size(11, 12);
            this.LblCurrentValue.TabIndex = 7;
            this.LblCurrentValue.Text = "0";
            // 
            // LblOf
            // 
            this.LblOf.AutoSize = true;
            this.LblOf.Font = new System.Drawing.Font("바탕", 9F);
            this.LblOf.Location = new System.Drawing.Point(1117, 623);
            this.LblOf.Name = "LblOf";
            this.LblOf.Size = new System.Drawing.Size(19, 12);
            this.LblOf.TabIndex = 7;
            this.LblOf.Text = "Of";
            // 
            // LblAllCount
            // 
            this.LblAllCount.AutoSize = true;
            this.LblAllCount.Font = new System.Drawing.Font("바탕", 9F);
            this.LblAllCount.Location = new System.Drawing.Point(1142, 623);
            this.LblAllCount.Name = "LblAllCount";
            this.LblAllCount.Size = new System.Drawing.Size(11, 12);
            this.LblAllCount.TabIndex = 7;
            this.LblAllCount.Text = "0";
            // 
            // LblState
            // 
            this.LblState.AutoSize = true;
            this.LblState.Font = new System.Drawing.Font("바탕", 9F);
            this.LblState.Location = new System.Drawing.Point(1097, 641);
            this.LblState.Name = "LblState";
            this.LblState.Size = new System.Drawing.Size(35, 12);
            this.LblState.TabIndex = 7;
            this.LblState.Text = "Done";
            // 
            // BtnDraw
            // 
            this.BtnDraw.Font = new System.Drawing.Font("바탕", 9F);
            this.BtnDraw.Location = new System.Drawing.Point(856, 99);
            this.BtnDraw.Name = "BtnDraw";
            this.BtnDraw.Size = new System.Drawing.Size(75, 23);
            this.BtnDraw.TabIndex = 8;
            this.BtnDraw.Text = "Draw";
            this.BtnDraw.UseVisualStyleBackColor = true;
            this.BtnDraw.Click += new System.EventHandler(this.BtnDraw_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Font = new System.Drawing.Font("바탕", 9F);
            this.BtnCancel.Location = new System.Drawing.Point(941, 99);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(75, 23);
            this.BtnCancel.TabIndex = 8;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // AnalyzeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnDraw);
            this.Controls.Add(this.LblOf);
            this.Controls.Add(this.LblAllCount);
            this.Controls.Add(this.LblState);
            this.Controls.Add(this.LblCurrentValue);
            this.Controls.Add(this.Progress);
            this.Controls.Add(this.LblY);
            this.Controls.Add(this.LblX);
            this.Controls.Add(this.DmnY);
            this.Controls.Add(this.DmnX);
            this.Controls.Add(this.Filename);
            this.Controls.Add(this.BtnLoad);
            this.Controls.Add(this.Graph);
            this.Name = "AnalyzeForm";
            this.Text = "AnalyzeForm";
            ((System.ComponentModel.ISupportInitialize)(this.Graph)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart Graph;
        private System.Windows.Forms.Button BtnLoad;
        private System.Windows.Forms.TextBox Filename;
        private System.Windows.Forms.DomainUpDown DmnX;
        private System.Windows.Forms.DomainUpDown DmnY;
        private System.Windows.Forms.Label LblX;
        private System.Windows.Forms.Label LblY;
        private System.Windows.Forms.ProgressBar Progress;
        private System.Windows.Forms.Label LblCurrentValue;
        private System.Windows.Forms.Label LblOf;
        private System.Windows.Forms.Label LblAllCount;
        private System.Windows.Forms.Label LblState;
        private System.Windows.Forms.Button BtnDraw;
        private System.Windows.Forms.Button BtnCancel;
    }
}