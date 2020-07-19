using NeatAlgorithm.NEAT;
using NeatAlgorithm.Snake;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace NeatAlgorithm.Data
{
    public partial class AnalyzeForm : Form
    {
        private Reader reader;

        private DirectoryInfo working;
        private bool isDrawing;
        private CancellationTokenSource cts;

        private string xVal;
        private string yVal;
        private int count;

        public AnalyzeForm()
        {
            InitializeComponent();
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult dr = fbd.ShowDialog();
            if(dr == DialogResult.OK)
            {
                if (isDrawing)
                {
                    BtnCancel_Click(sender, e);
                }


                Filename.ReadOnly = true;
                Filename.Text = fbd.SelectedPath;
                working = new DirectoryInfo(fbd.SelectedPath);
            }
        }

        private void BtnDraw_Click(object sender, EventArgs e)
        {
            if (isDrawing) return;

            isDrawing = true;

            cts = new CancellationTokenSource();
            LblState.Text = "Working";

            count = working.GetFiles().Length;
            Progress.Maximum = count;
            Progress.Step = 1;
            Progress.Value = 0;
            LblAllCount.Text = "" + count;
            xVal = DmnX.Text;
            yVal = DmnY.Text;
            Graph.Series.Clear();
            Series s = Graph.Series.Add(yVal);

                s.ChartType = SeriesChartType.Point;
            s.Color = Color.FromArgb(192, Color.Orange);
                s.MarkerSize = 2;
                s.MarkerStyle = MarkerStyle.Circle;
            Task.Factory.StartNew(() =>
            {
                FileInfo[] fis = working.GetFiles();
                foreach(FileInfo file in fis)
                {
                    if (!isDrawing) return;

                    Reader reader = new Reader(file.FullName, 24,4);
                    DataDictionary dd = reader.Read();
                    BeginInvoke((Action)(() => Proceed(reader, dd)));
                }
                BeginInvoke((Action)(() => Finish()));
            }, cts.Token);

        }


        private void Proceed(Reader reader, DataDictionary dd)
        {


            if (isDrawing)
            {
                Progress.PerformStep();
                LblCurrentValue.Text = "" + Progress.Value ;

                
                int x, y;
                for (int i = 0; i <= reader.Gen; ++i) {
                    
                    switch (xVal)
                    {
                        case "Generation":
                            x = i+1;
                            break;
                        case "Top Score (Best)":
                            Genome g = reader.Best[i];
                            int[] score = dd.GetScore(g.GenomeId);
                            int best = score[0];
                            for(int j = 1; j < score.Length; ++j)
                            {
                                if(score[j] > best)
                                {
                                    best = score[j];
                                }
                            }
                            x = best;
                            break;
                        default:
                            x = 0;
                            break;
                    }

                    switch (yVal)
                    {
                        case "Generation":
                            y = i;
                            break;

                        case "Top Score (Best)":
                            Genome g = reader.Best[i];
                            int[] score = dd.GetScore(g.GenomeId);
                            int best = score[0];
                            for (int j = 1; j < score.Length; ++j)
                            {
                                if (score[j] > best)
                                {
                                    best = score[j];
                                }
                            }
                            y = best;
                            break;

                        default:
                            y = 0;
                            break;

                    }
                    Graph.Series[0].Points.AddXY(x, y);

                }
                Graph.Invalidate();
            }
        }
        
        private void Finish()
        {
            if (isDrawing)
            {
                Series s = Graph.Series.Add("Means");

                s.ChartType = SeriesChartType.Line;
                s.Color = Color.Blue;

                isDrawing = false;
                LblState.Text = "Done";
                double[] means = new double[300];
                double[] stdDevs = new double[300];
                
                for (int i = 0; i < 300; ++i)
                {
                    int index = 0;
                    DataPoint[] dps = new DataPoint[count];
                    foreach (DataPoint dp in Graph.Series[0].Points)
                    {
                        if (dp.XValue == i + 1)
                        {
                            dps[index++] = dp;
                        }
                    }

                    long dpSum = 0;
                    foreach (DataPoint dp in dps)
                    {

                        dpSum += (long)dp.YValues[0];


                    }
                    means[i] = (double)dpSum / count;


                    double variation = 0;
                    foreach (DataPoint dp in dps)
                    {
                        double dev = dp.YValues[0] - means[i];
                        variation += dev * dev;
                    }
                    variation /= count - 1;
                    stdDevs[i] = Math.Sqrt(variation);
                    s.Points.AddXY(i+1, means[i]);
                }

            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            if (isDrawing)
            {
                isDrawing = false;
                cts.Cancel();
                LblState.Text = "Cancelled.";
            }
        }
    }
}
