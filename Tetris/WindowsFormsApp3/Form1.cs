using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        public Tetris Tetris = new Tetris();

        SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1); // 풀에 존재할 수 있는 최대 최소 스레드 개수 설정

        public Form1()
        {
            InitializeComponent();
        }

        //Events
        private void Form1_Load(object sender, EventArgs e)
        {
            Width = 700;
            Height = 800;
            StartPosition = FormStartPosition.CenterScreen;
        }

        private async void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }

            if (e.KeyCode == Keys.Left) // 왼쪽 움직임
            {
                await semaphoreSlim.WaitAsync();
                Tetris.MoveLeft();
                Tetris.DrawBoard((Form)this);
                semaphoreSlim.Release();
            }

            if (e.KeyCode == Keys.Right) // 오른쪽 움직임
            {
                await semaphoreSlim.WaitAsync();
                Tetris.MoveRight();
                Tetris.DrawBoard((Form)this);
                semaphoreSlim.Release();
            }

            if (e.KeyCode == Keys.Up) // 회전
            {
                await semaphoreSlim.WaitAsync();
                Tetris.NextDirection();
                Tetris.DrawBoard((Form)this);
                semaphoreSlim.Release();
            }

            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Down) // 강제로 내리기
            {
                await semaphoreSlim.WaitAsync();
                Tetris.MoveDown();
                Tetris.DrawBoard((Form)this);
                semaphoreSlim.Release();
            }

        }

        private async void Form1_Paint(object sender, PaintEventArgs e)
        {
            Tetris.NewBlock();
            await MoveBlockDownLooplyAsync();
            Tetris.DrawBoard((Form)this);
            MessageBox.Show("Game Over.");
        }

        private async Task<bool> MoveBlockDownLooplyAsync()
        {
            while (true) // 계속해서 돌면서 delay 150을 넣음
            {
                await semaphoreSlim.WaitAsync();
                Tetris.MoveDown();
                semaphoreSlim.Release();
                Tetris.DrawBoard((Form)this);

                if (Tetris.IsGameOver()) // 게임이 끝났을 때
                {
                    break;
                }

                Text = Tetris.CurrentBlock.ToString();
                await Task.Delay(150);
            }
            return true;
        }
    }
}
