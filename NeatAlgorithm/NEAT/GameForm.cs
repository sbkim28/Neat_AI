using NeatAlgorithm._2048;
using NeatAlgorithm.Data;
using NeatAlgorithm.NEAT;
using NeatAlgorithm.Snake;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace NeatAlgorithm.NEAT
{
    public partial class GameForm : Form
    {
        
        private Reader reader;
        private DataDictionary dd;
        private Agent agent;
        private bool isPlaying;
        private CancellationTokenSource cts;
        private GameFactory gf;

        public GameForm()
        {
            InitializeComponent();
            isPlaying = false;
            ChartTopScore.Series.Clear();
            ChartTopScore.MouseWheel += ChartTopScore_MouseWheel;
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "데이터 파일";
            ofd.FileName = "Data";
            ofd.DefaultExt = "txt";
            ofd.Multiselect = false;
            ofd.Filter = "데이터 파일 (*.txt)|*.txt|모든 파일 (*.*)|*.*";
            DialogResult dr = ofd.ShowDialog();
            if(dr == DialogResult.OK)
            {
                if (isPlaying)
                {
                    isPlaying = false;
                    cts.Cancel();
                    agent.Gameover = true;
                }
                ChartTopScore.ChartAreas[0].AxisX.ScaleView.ZoomReset();
                string filename = ofd.FileName;
                InputGen.Enabled = true;
                Filename.ReadOnly = true;
                Filename.Text = ofd.FileName;
                ReadFile(ofd.FileName);
                BtnPlay.Enabled = true;
                DrawTopScore();
                gf = reader.GameFactory;
                agent = gf.GetAgent(null);
            }
        }

        
        public void DrawTopScore()
        {
            ChartTopScore.Series.Clear();
            Series series = ChartTopScore.Series.Add("Top Score");
            series.ChartType = SeriesChartType.Line;
            int gen = 0;

            int bestGen = 0;
            int bestOfAll = 0;

            foreach(Genome g in reader.Best)
            {
                int[] score = dd.GetScore(g.GenomeId);
                int best = score[0];
                for(int i = 1; i < score.Length; ++i)
                {
                    if(score[i] > best)
                    {
                        best = score[i];
                    }
                }
                if(best > bestOfAll)
                {
                    bestGen = gen;
                    bestOfAll = best;
                }
                series.Points.AddXY(gen++, best);
            }

            LblBestWhere.Text = "" + bestGen;
        }

        private void BtnPlay_Click(object sender, EventArgs e)
        {
            if (isPlaying) return;
            
            agent.Drawer = new Draw(Draw);
            agent.DisplayDelay = (int) InputSpeed.Value;
            isPlaying = true;
            int gen = (int)InputGen.Value;
            Genome genome = reader.Best[gen];
            genome.GenerateNetwork();
            LblGenValue.Text = "" + gen;
            LblGenomeIdValue.Text = "" + genome.GenomeId;
            LblFitnessValue.Text = "" + genome.Fitness;
            LblFromValue.Text = "" + genome.FromGeneration;
            cts = new CancellationTokenSource();

            Task.Factory.StartNew(() =>
            {
                agent.Display(genome, dd);
            }, cts.Token);


        }

        private void Draw()
        {
            if (agent.Gameover)
            {
                isPlaying = false;
                return;
            }
            BeginInvoke((Action)    (()=>{
                SnakeBox.Invalidate();
                NetworkBox.Invalidate();

            }));
        }
        
        private void ReadFile(string file)
        {
            reader = new Reader(file, 24, 4);
            dd = reader.Read();
            Console.Write(reader.Gen);
            InputGen.Maximum = reader.Gen;
            InputGen.Minimum = 0;
            InputGen.Value = 0;

        }

        private void UpdateGraphics(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;
            if (!isPlaying) return;

            LblScoreValue.Text = "" + agent.Score;

            if (agent is SnakeAgent)
            {
                SnakeAgent sa = agent as SnakeAgent;
                Brush color = Brushes.White;

                LblHungerValue.Text = "" + sa.Hunger;

                int x = sa.X;
                int y = sa.Y;
                int[,] cells = new int[y, x];
                foreach (Square s in sa.Snake)
                {
                    cells[s.Y, s.X] = -1;
                }
                cells[sa.Food.Y, sa.Food.X] = 1;
                for (int i = 0; i < y; ++i)
                {
                    for (int j = 0; j < x; ++j)
                    {
                        if (cells[i, j] == 0) color = Brushes.White;
                        else if (cells[i, j] == -1) color = Brushes.Black;
                        else if (cells[i, j] == 1) color = Brushes.Red;
                        canvas.FillRectangle(color, new Rectangle(
                                j * 20 + 1, i * 20 + 1, 18, 18
                            ));
                    }
                }
            }
        }
        private void UpdateTopology(object sender, PaintEventArgs e)
        {
            if (!isPlaying) return;
            Graphics canvas = e.Graphics;
            Genome g = agent.Current;

            int outlength = 0;
            int highOut = 0 ;
            double highVal = double.MinValue;

            Pen border = Pens.Black;
            foreach(int index in g.Network.Keys)
            {
                Node n = g.Network[index];
                double map = Math.Atan(n.value);

                map *= 2.0 / Math.PI;
                
                if(index < g.Pool.Inputs)
                {
                    Brush color = new SolidBrush(Color.FromArgb(64, 64 + (int)( map * 64), 64 + (int)(map * 190) ));
                    int x = 5;
                    int y = 10;

                    y += index * 20;
                    while(y > 180)
                    {
                        x += 20;
                        y -= 180;
                    }

                    canvas.FillEllipse(color, new Rectangle(x, y, 15, 15));
                    canvas.DrawEllipse(border, new Rectangle(x, y, 15, 15));

                } else if(index < g.Pool.MaxNodes)
                {
                    Brush color = new SolidBrush(Color.FromArgb(64, 64 + (int)(map * 64), 64 + (int)(map * 190)));
                    int x = 125;
                    int y = 10;
                    y += (index - g.Pool.Inputs)* 20;
                    while (y > 180)
                    {
                        x += 20;
                        y -= 180;
                    }
                    
                    foreach (Gene con in n.Incomings)
                    {
                        double weight = Math.Atan(con.Weight);
                        weight *= 2.0 / Math.PI;
                        Pen p = new Pen(Color.FromArgb(128, 128 - (int)(weight * 120), 128 + (int)(weight * 120), 0));
                        int cx;
                        int cy = 10;
                        if (con.In < g.Pool.Inputs)
                        {
                            cx = 5;
                            cy += con.In * 20;
                            while (cy > 180)
                            {
                                cx += 20;
                                cy -= 180;
                            }
                        }
                        else
                        {
                            cx = 125;
                            cy += (con.In - g.Pool.Inputs) * 20;
                            while (cy > 180)
                            {
                                cx += 20;
                                cy -= 180;
                            }
                        }
                        canvas.DrawLine(p, cx + 15, cy + 7, x, y + 7);
                    }

                    canvas.FillEllipse(color, new Rectangle(x, y, 15, 15));
                    canvas.DrawEllipse(border, new Rectangle(x, y, 15, 15));

                }
                else
                {
                    if(highVal < n.value)
                    {
                        highOut = outlength;
                        highVal = n.value;
                    }
                    outlength++;
                }
                
            }

            int outx = 350;
            int outy = (200 - (20 * outlength - 5)) / 2;
            for(int i = 0; i < outlength; ++i)
            {
                Node n = g.Network[i + g.Pool.MaxNodes];
                int x = outx;
                int y = outy + i * 20;
                if(i == highOut)
                {
                    canvas.FillEllipse(Brushes.Green, new Rectangle(x, y, 15, 15));
                }
                else
                {
                    canvas.FillEllipse(Brushes.Gray, new Rectangle(x, y, 15, 15));
                }

                foreach (Gene con in n.Incomings)
                {
                    double weight = Math.Atan(con.Weight);
                    weight *= 2.0 / Math.PI;
                    Pen p = new Pen(Color.FromArgb(128, 128 - (int)(weight * 120), 128 + (int)(weight * 120), 0));
                    int cx;
                    int cy = 10;
                    if (con.In < g.Pool.Inputs)
                    {
                        cx = 5;
                        cy += con.In * 20;
                        while (cy > 180)
                        {
                            cx += 20;
                            cy -= 180;
                        }
                    }
                    else
                    {
                        cx = 125;
                        cy += (con.In - g.Pool.Inputs) * 20;
                        while (cy > 180)
                        {
                            cx += 20;
                            cy -= 180;
                        }
                    }
                    canvas.DrawLine(p, cx + 15, cy + 7, x, y + 7);
                }

                canvas.DrawEllipse(border, new Rectangle(x, y, 15, 15));
            }

        }

        

        private void BtnStop_Click(object sender, EventArgs e)
        {
            if (isPlaying)
            {
                isPlaying = false;
                cts.Cancel();
                agent.Gameover = true;
            }
        }


        private void ChartTopScore_MouseWheel(object sender, MouseEventArgs e)
        {
            if (ChartTopScore.Series.Count == 0) return;

            Axis xAxis = ChartTopScore.ChartAreas[0].AxisX;

            if (e.Delta < 0) // Scrolled down.
            {
                xAxis.ScaleView.ZoomReset();
            }
            else if (e.Delta > 0) // Scrolled up.
            {
                double xMin = xAxis.ScaleView.ViewMinimum;
                double xMax = xAxis.ScaleView.ViewMaximum;

                int posXStart = (int)(xAxis.PixelPositionToValue(e.Location.X) - (xMax - xMin) / 1.5);
                int posXFinish = (int)(xAxis.PixelPositionToValue(e.Location.X) + (xMax - xMin) / 1.5);
                
                xAxis.ScaleView.Zoom(posXStart, posXFinish);
            }
        
        }

        private void ChartTopScore_MouseLeave(object sender, EventArgs e)
        {
            if (ChartTopScore.Focused) ChartTopScore.Parent.Focus();
            
        }

        private void ChartTopScore_MouseEnter(object sender, EventArgs e)
        {
            if (!ChartTopScore.Focused) ChartTopScore.Focus();
        }
    }

}
