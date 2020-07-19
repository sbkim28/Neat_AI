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

        private bool hasData;
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
                series.Points.AddXY(++gen, best);
            }
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
            SnakeBox.Invalidate();
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

        private void SnakeForm_Load(object sender, EventArgs e)
        {

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

        private void BtnStop_Click(object sender, EventArgs e)
        {
            if (isPlaying)
            {
                isPlaying = false;
                cts.Cancel();
                agent.Gameover = true;
            }
        }
    }
}
