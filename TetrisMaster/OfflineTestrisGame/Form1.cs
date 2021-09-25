using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OfflineTestrisGame
{
    public partial class Form1 : Form
    {
        TetrisData _data = new TetrisData();
        GameRule rule = new GameRule();

        SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);

        static readonly int HEIGHT = 800;
        static readonly int WIDTH = 700;

        public Form1()
        {
            InitializeComponent();

            BtnStart.KeyDown += Form1_KeyDown;
            //BtnStart.KeyPress += BtnStart_KeyPress;
            BtnStart.PreviewKeyDown += BtnStart_PreviewKeyDown;
        }

        private void BtnStart_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                case Keys.Up:
                case Keys.Left:
                case Keys.Right:
                    e.IsInputKey = true;
                    break;
            }
        }

        private void BtnStart_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.Text = new string(new[] { e.KeyChar });
        }


        // Paint Cell
        // Move Event
        // Cell Data

        private async void GameStart()
        {

            Graphics g;
            try
            {
                g = this.CreateGraphics();
            }
            catch
            {
                return;
            }

            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    g.DrawRectangle(new Pen(Brushes.White), 10 + j * 20, 10 + i * 20, 20, 20);
                    g.FillRectangle(Brushes.Black, 10 + j * 20, 10 + i * 20, 20, 20);
                }
            }

            await MoveBlockDownLooplyAsync();
            //TODO: 뒷 배경 칠하기
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //MessageBox.Show(e.KeyCode + "");

            if (e.KeyCode == Keys.Left)
            {
                semaphoreSlim.WaitAsync();
                _data.MoveLeft();
                semaphoreSlim.Release();
            }
            else if (e.KeyCode == Keys.Right)
            {
                semaphoreSlim.WaitAsync();
                _data.MoveRight();
                semaphoreSlim.Release();
            }
            else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Space)
            {
                semaphoreSlim.WaitAsync();
                _data.MoveDown();
                semaphoreSlim.Release();
            }

            DrawBoard();
        }

        private async void UpdateBoard()
        {
            // receive
        }


        public async Task<bool> MoveBlockDownLooplyAsync() // 동작 부분
        {
            _data.MakeNewBlock();
            await DrawBoard();

            while (true) // 계속해서 돌면서 delay 150을 넣음
            {
                await semaphoreSlim.WaitAsync();
                _data.MoveDown();
                semaphoreSlim.Release();
                await Task.Delay(1000);

                if (rule.IsGameOver(20, 12, _data.board))
                {
                    break;
                }

               
                await DrawBoard();
            }

            return false;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            MessageBox.Show("Game Over.");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StartPosition = FormStartPosition.CenterScreen;
        }

        private async Task<bool> DrawBoard()
        {
            for (int i = 0; i < 20; i++)
            {
                for(int j = 0; j< 12; j++)
                { 
                    PaintCell(20, 10 + j * 20, 10 + i * 20, i, j);
                }
            }
            return true;
        }

        private void PaintCell(int unitSize, float x1, float y1, int i, int j) // 그릴 폼, 한 블록의 크기, x, y 좌표, i,j 번째의 방향
        {
            Graphics g;

            try
            {
                g = this.CreateGraphics();
            }
            catch
            {
                return;      
            }

            switch (_data.board[i, j])
            {
                case CellType.Fill:
                    g.FillRectangle(Brushes.White, x1, y1, unitSize, unitSize);
                    g.DrawRectangle(new Pen(Brushes.Black), x1, y1, unitSize, unitSize);
                    break;
                case CellType.Active:
                    g.FillRectangle(Brushes.Red, x1, y1, unitSize, unitSize);
                    g.DrawRectangle(new Pen(Brushes.Black), x1, y1, unitSize, unitSize);
                    break;
                default:
                    g.FillRectangle(Brushes.Black, x1, y1, unitSize, unitSize);
                    g.DrawRectangle(new Pen(Brushes.White), x1, y1, unitSize, unitSize);
                    break;
            }
            this.Update();
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            GameStart();
        }
    }
}
