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
        private bool isDone;

        private string xVal;
        private string yVal;
        private int count;
        private int bestOfAll;

        private double[] means;
        private double[] stdDevs;

        public AnalyzeForm()
        {
            InitializeComponent();
            Graph.ChartAreas[0].AxisX.IntervalOffset = 0;
            Graph.ChartAreas[0].AxisY.Interval = 10;
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

            isDone = false;
            isDrawing = true;
            bestOfAll = 0;

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
                            x = i;
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
                    if(y > bestOfAll)
                    {
                        bestOfAll = y;
                        FileInfo f = new FileInfo(reader.Filename);
                        LblData.Text = f.Name;
                        LblGen.Text = "" + x;

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
                Series s1 = Graph.Series.Add("95citop");
                Series s2 = Graph.Series.Add("95cibot");
                Series s5 = Graph.Series.Add("99citop");
                Series s6 = Graph.Series.Add("99cibot");
                Series s3 = Graph.Series.Add("999citop");
                Series s4 = Graph.Series.Add("999cibot");
                s1.Enabled = CB95.Checked;
                s2.Enabled = CB95.Checked;
                s3.Enabled = CB999.Checked;
                s4.Enabled = CB999.Checked;
                s5.Enabled = CB99.Checked;
                s6.Enabled = CB99.Checked;

                s.ChartType = SeriesChartType.Line;
                s.Color = Color.Blue;
                s1.ChartType = SeriesChartType.Line;
                s1.Color = Color.Cyan;
                s2.ChartType = SeriesChartType.Line;
                s2.Color = Color.Cyan;
                s5.ChartType = SeriesChartType.Line;
                s5.Color = Color.Green;
                s6.ChartType = SeriesChartType.Line;
                s6.Color = Color.Green;
                s3.ChartType = SeriesChartType.Line;
                s3.Color = Color.Red;
                s4.ChartType = SeriesChartType.Line;
                s4.Color = Color.Red;

                isDrawing = false;
                LblState.Text = "Done";
                means = new double[300];
                stdDevs = new double[300];

                for (int i = 0; i < 300; ++i)
                {
                    int index = 0;
                    DataPoint[] dps = new DataPoint[count];
                    foreach (DataPoint dp in Graph.Series[0].Points)
                    {
                        if (dp.XValue == i )
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
                    s.Points.AddXY(i , means[i]);
                    s1.Points.AddXY(i , means[i] + 1.96 * stdDevs[i] / Math.Sqrt(count));
                    s2.Points.AddXY(i , means[i] - 1.96 * stdDevs[i] / Math.Sqrt(count));
                    s5.Points.AddXY(i, means[i] + 2.58 * stdDevs[i] / Math.Sqrt(count));
                    s6.Points.AddXY(i, means[i] - 2.58 * stdDevs[i] / Math.Sqrt(count));
                    s3.Points.AddXY(i, means[i] + 3.30 * stdDevs[i] / Math.Sqrt(count));
                    s4.Points.AddXY(i, means[i] - 3.30 * stdDevs[i] / Math.Sqrt(count));

                }
                isDone = true;
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

        private void CB95_CheckedChanged(object sender, EventArgs e)
        {
            if (!isDone) return;
            Series s1 = Graph.Series["95citop"];
            Series s2 = Graph.Series["95cibot"];
            s1.Enabled = CB95.Checked;
            s2.Enabled = CB95.Checked;
            Graph.Invalidate();
        }

        private void CB99_CheckedChanged(object sender, EventArgs e)
        {
            if (!isDone) return;
            Series s1 = Graph.Series["99citop"];
            Series s2 = Graph.Series["99cibot"];
            s1.Enabled = CB99.Checked;
            s2.Enabled = CB99.Checked;
            Graph.Invalidate();

        }

        private void CB999_CheckedChanged(object sender, EventArgs e)
        {
            if (!isDone) return;
            Series s1 = Graph.Series["999citop"];
            Series s2 = Graph.Series["999cibot"];
            s1.Enabled = CB999.Checked;
            s2.Enabled = CB999.Checked;
            Graph.Invalidate();
        }
    }
}
