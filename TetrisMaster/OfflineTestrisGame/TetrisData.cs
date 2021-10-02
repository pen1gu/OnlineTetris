using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OfflineTestrisGame
{
    class TetrisData
    {
        GameRule rule = new GameRule();
        // 여기서 데이터만 바꿔줌, 데이터만 이라는거 명심.

        public CellType[,] board = new CellType[20, 12];
        int WIDTH = 12;
        int HEIGHT = 20;
        int NewBlockX;
        int NewBlockY;
        int CurrentX = 0;
        int CurrentY = 0;
        PieceType _pieceType;
        public TetrisData()
        {
            CurrentY = NewBlockY = 0;
            CurrentX = NewBlockX = WIDTH / 2;

            for (int i = 0; i < HEIGHT; i++)
            {
                for (int j = 0; i < WIDTH; i++)
                {
                    board[i, j] = CellType.Empty;
                }
            }
        }

        // 필요한 것 지속적으로 테트리스가 내려가야함,
        // 하나의 블록이 떨어졌을 때 다음 블록이 생성되어야함
        // 각 블록의 크기 정의
        // Empty 비어있는, Fill 이전에 이미 차있는, Active 현재 움직이는
        // 왼쪽 위를 기준으로 프로그래밍.
        // 움직임만을 담당하는 클래스\
        // 닿았을 때는 상관 없고 그냥 있는지 확인만 하면 된다.

        private PieceType SetRandomBlock()
        {
            Random random = new Random();
            return (PieceType)random.Next(0, 7);
        }

        public CellType[,] GetBoard()
        {
            return board;
        }

        public void MakeNewBlock()
        {
            CurrentX = 4;
            CurrentY = 0;
            _pieceType = SetRandomBlock();
            MergeBlockToBoard();
            // 데이터에 블록 추가
        }

        public void MergeBlockToBoard()
        {

            CellType[,] block = GetBlockObject(_pieceType, Direction.Up);
            int arrayLength = block.Length;
            int size = 0;

            switch (arrayLength)
            {
                case 4:
                    size = 2;
                    break;
                case 9:
                    size = 3;
                    break;
                case 16:
                    size = 4;
                    break;

            }

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (block[i, j] == CellType.Active)
                    {
                        board[CurrentY + i, CurrentX + j] = CellType.Active;
                    }
                }
            }
        }

        public CellType[,] GetBlockObject(PieceType type, Direction direction)
        {
            CellType[,] block = null;
            switch (type)
            {
                case PieceType.A:
                    switch (direction)
                    {
                        case Direction.Up: 
                            block = new CellType[4, 4] {
                                { CellType.Empty, CellType.Empty , CellType.Empty , CellType.Empty },
                                { CellType.Active, CellType.Active, CellType.Active, CellType.Active },
                                { CellType.Empty, CellType.Empty , CellType.Empty , CellType.Empty },
                                { CellType.Empty, CellType.Empty , CellType.Empty , CellType.Empty },
                            };
                            break;
                        case Direction.Left:
                            block = new CellType[4, 4] {
                                { CellType.Empty, CellType.Empty , CellType.Active , CellType.Empty },
                                { CellType.Empty, CellType.Empty, CellType.Active, CellType.Empty },
                                { CellType.Empty, CellType.Empty , CellType.Active , CellType.Empty },
                                { CellType.Empty, CellType.Empty , CellType.Active , CellType.Empty },
                            };
                            break;
                        case Direction.Right:
                            block = new CellType[4, 4] {
                                { CellType.Empty, CellType.Active , CellType.Empty , CellType.Empty },
                                { CellType.Empty, CellType.Active, CellType.Empty, CellType.Empty },
                                { CellType.Empty, CellType.Active , CellType.Empty , CellType.Empty },
                                { CellType.Empty, CellType.Active , CellType.Empty , CellType.Empty },
                            };
                            break;
                    }
                    break;
                case PieceType.B:
                    switch (direction)
                    {
                        case Direction.Up: // normal
                            block = new CellType[3, 3]{
                                { CellType.Empty, CellType.Empty, CellType.Empty},
                                { CellType.Empty, CellType.Active, CellType.Empty},
                                {CellType.Active, CellType.Active,CellType.Active }
                            };
                            break;

                        case Direction.Left:
                            block = new CellType[3, 3]{
                                { CellType.Empty, CellType.Empty, CellType.Active},
                                { CellType.Empty, CellType.Active, CellType.Active},
                                {CellType.Empty, CellType.Empty,CellType.Active }
                            };
                            break;
                        case Direction.Right:
                            block = new CellType[3, 3]{
                                { CellType.Active, CellType.Empty, CellType.Empty},
                                { CellType.Active, CellType.Active, CellType.Empty},
                                {CellType.Active, CellType.Empty,CellType.Empty }
                            };
                            break;
                    }
                    break;
                case PieceType.C:
                    switch (direction)
                    {
                        case Direction.Up:
                        case Direction.Left:
                        case Direction.Right:
                            block = new CellType[2, 2]
                            {
                                { CellType.Active, CellType.Active},
                                {CellType.Active, CellType.Active}
                            };
                            break;
                    }
                    break;
                case PieceType.D:
                    switch (direction)
                    {
                        case Direction.Up: // normal
                            block = new CellType[3, 3]
                            {
                                    { CellType.Empty, CellType.Empty, CellType.Empty},
                                    { CellType.Empty, CellType.Empty, CellType.Active},
                                    {CellType.Active, CellType.Active,CellType.Active }
                            };
                            break;

                        case Direction.Left:
                            block = new CellType[3, 3]
                                {
                                        { CellType.Empty, CellType.Active, CellType.Active},
                                        { CellType.Empty, CellType.Empty, CellType.Active},
                                        {CellType.Empty, CellType.Empty,CellType.Active }
                                };
                            break;
                        case Direction.Right:
                            block = new CellType[3, 3]
                               {
                                        { CellType.Active, CellType.Empty, CellType.Empty},
                                        { CellType.Active, CellType.Empty, CellType.Empty},
                                        {CellType.Active, CellType.Active,CellType.Empty }
                               };
                            break;
                    }
                    break;
                case PieceType.E:
                    switch (direction)
                    {
                        case Direction.Up: // normal
                            block = new CellType[3, 3]
                            {
                                { CellType.Empty, CellType.Empty, CellType.Empty},
                                { CellType.Active, CellType.Empty, CellType.Empty},
                                {CellType.Active, CellType.Active,CellType.Active }
                           };
                            break;
                        case Direction.Left:
                            block = new CellType[3, 3]
                           {
                                { CellType.Active, CellType.Active, CellType.Empty},
                                        { CellType.Active, CellType.Empty, CellType.Empty},
                                        {CellType.Active, CellType.Empty,CellType.Empty }
                           };
                            break;
                        case Direction.Right:
                            block = new CellType[3, 3]
                   {
                                { CellType.Empty, CellType.Empty, CellType.Active},
                                { CellType.Empty, CellType.Empty, CellType.Active},
                                {CellType.Empty, CellType.Active,CellType.Active }
                   };
                            break;
                    }
                    break;
                case PieceType.F:
                    switch (direction)
                    {
                        case Direction.Up: // normal
                            block = new CellType[3, 3]
                    {
                        { CellType.Empty, CellType.Empty, CellType.Empty},
                        { CellType.Empty, CellType.Active, CellType.Active},
                        {CellType.Active, CellType.Active,CellType.Empty }
                    };
                            break;
                        case Direction.Left:
                            block = new CellType[3, 3]
                    {
                        { CellType.Active, CellType.Empty, CellType.Empty},
                        { CellType.Active, CellType.Active, CellType.Empty},
                        {CellType.Empty, CellType.Active,CellType.Empty }
                    };
                            break;
                        case Direction.Right:
                            block = new CellType[3, 3]
                    {
                        { CellType.Empty, CellType.Active, CellType.Empty},
                        { CellType.Empty, CellType.Active, CellType.Active},
                        {CellType.Empty, CellType.Empty,CellType.Active }
                    };
                            break;
                    }

                    break;
                case PieceType.G:
                    switch (direction)
                    {
                        case Direction.Up: // normal
                            block = new CellType[3, 3]
                    {
                        { CellType.Empty, CellType.Empty, CellType.Empty},
                        { CellType.Active, CellType.Active, CellType.Empty},
                        {CellType.Empty, CellType.Active,CellType.Active }
                    };
                            break;
                        case Direction.Left:
                            block = new CellType[3, 3]
                    {
                        { CellType.Empty, CellType.Active, CellType.Empty},
                        { CellType.Active, CellType.Active, CellType.Empty},
                        {CellType.Active, CellType.Empty,CellType.Empty }
                    };
                            break;
                        case Direction.Right:
                            block = new CellType[3, 3]
                    {
                        { CellType.Empty, CellType.Empty, CellType.Active},
                        { CellType.Empty, CellType.Active, CellType.Active},
                        {CellType.Empty, CellType.Active,CellType.Empty }
                    };
                            break;
                    }
                    break;
            }


            return block;
        }

        public void MoveDown()
        {

            for (int i = HEIGHT - 1; i >= 0; i--)
            {
                for (int j = WIDTH - 1; j >= 0; j--)
                {
                    if (board[i, j] == CellType.Active)
                    {
                        board[i, j] = CellType.Empty;
                        board[i + 1, j] = CellType.Active;
                    }
                }
            }
        }

        public void MoveRight()
        {
            for (int i = 0; i < HEIGHT; i++)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    if (board[i, j] == CellType.Active)
                    {
                        board[i, j] = CellType.Empty;
                        board[i, j + 1] = CellType.Active;
                    }
                }
            }
        }

        public void MoveLeft()
        {
            for (int i = 0; i < HEIGHT; i++)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    if (board[i, j] == CellType.Active)
                    {
                        board[i, j] = CellType.Empty;
                        board[i, j - 1] = CellType.Active;
                    }
                }
            }
        }

        private void ChangeTorawd()
        {


        }

        private void CheckLineFilled()
        {
            for (int i = HEIGHT - 1; i >=0; i--)
            {
                int fillCount = 0;
                for (int j = WIDTH - 1; j >= 0; j--)
                {
                    if (board[i, j] == CellType.Fill)
                    {
                        fillCount++;
                    }
                }

                if (fillCount == WIDTH)
                {
                    DestroyBlock();
                }
            }
        }

        private void DestroyBlock()
        {
            for (int i = 0; i < HEIGHT - 1; i++)
            {
                for (int j = 0; j < WIDTH - 1; j++)
                { 
                    board[i+1, j+1] = board[i,j];
                }
            }

            MakeNewBlock();
        }

    }
}
