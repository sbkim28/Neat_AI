using NeatAlgorithm.Data;
using NeatAlgorithm.NEAT;
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

namespace NeatAlgorithm.Snake
{
    public partial class SnakeForm : Form
    {

        private bool hasData;
        private Reader reader;
        private SnakeDataDictionary sdd;
        private SnakeAgent sa;
        private bool isPlaying;
        private CancellationTokenSource cts;

        public SnakeForm()
        {
            InitializeComponent();
            sa = new SnakeAgent(16,16, null);
            isPlaying = false;
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
                    sa.Gameover = true;
                }
                string filename = ofd.FileName;
                InputGen.Enabled = true;
                Filename.ReadOnly = true;
                Filename.Text = ofd.FileName;
                ReadFile(ofd.FileName);
                BtnPlay.Enabled = true;
            }
        }

        
        private void BtnPlay_Click(object sender, EventArgs e)
        {
            if (isPlaying) return;
            sa.Initialize();
            sa.Drawer = new Draw(Draw);
            sa.DisplayDelay = (int) InputSpeed.Value;
            Console.Write(sa.DisplayDelay);
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
                sa.Display(genome, sdd);
            }, cts.Token);


        }

        private void Draw()
        {
            if (sa.Gameover)
            {
                isPlaying = false;
                return;
            }
            SnakeBox.Invalidate();
        }
        
        private void ReadFile(string file)
        {
            reader = new Reader(file, 24, 4);
            sdd = new SnakeDataDictionary();
            reader.Read(sdd);
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

            LblScoreValue.Text = "" + sa.Score;
            LblHungerValue.Text = "" + sa.Hunger;

            Brush color = Brushes.White;
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

        private void BtnStop_Click(object sender, EventArgs e)
        {
            if (isPlaying)
            {
                isPlaying = false;
                cts.Cancel();
                sa.Gameover = true;
            }
        }
    }
}
