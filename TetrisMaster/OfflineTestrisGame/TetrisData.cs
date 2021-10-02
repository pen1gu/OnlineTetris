using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OfflineTestrisGame
{
    class TetrisData
    {
        // 여기서 데이터만 바꿔줌, 데이터만 이라는거 명심.

        public CellType[,] _board = new CellType[20, 12];
        public CellType[,] subBoard = new CellType[20, 12]; // 도입 생각 중
        public int _CurrentX = 0;
        public int _CurrentY = 0;
        public int _CurrentBlockSize = 0;
        int WIDTH = 12;
        int HEIGHT = 20;
        


        int _dirIndex = 1;
        PieceType _pieceType;

        Direction[] _directions = { Direction.Up, Direction.Right, Direction.Down, Direction.Left };
        Direction _currentDirection = Direction.Up;
        public TetrisData()
        {
            _CurrentY = 0;
            _CurrentX = WIDTH / 2;

            for (int i = 0; i < HEIGHT; i++)
            {
                for (int j = 0; i < WIDTH; i++)
                {
                    _board[i, j] = CellType.Empty;
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
            return _board;
        }

        public void MakeNewBlock()
        {
            _dirIndex = 1;
            _currentDirection = Direction.Up;
            _CurrentX = WIDTH / 2 - 2;
            _CurrentY = 1;
            _pieceType = SetRandomBlock();
            MergeBlockToBoard();
            // 데이터에 블록 추가
        }
        public void MergeBlockToBoard()
        {

            CellType[,] block = GetBlockObject(_pieceType, _currentDirection);
            int arrayLength = block.Length;

            switch (arrayLength)
            {
                case 4:
                    _CurrentBlockSize = 2;
                    break;
                case 9:
                    _CurrentBlockSize = 3;
                    break;
                case 16:
                    _CurrentBlockSize = 4;
                    break;

            }

            for (int i = 0; i < _CurrentBlockSize; i++)
            {
                for (int j = 0; j < _CurrentBlockSize; j++)
                {
                    if (block[i, j] == CellType.Active)
                    {
                        _board[_CurrentY + i, _CurrentX + j] = CellType.Active;
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
                        case Direction.Down:
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
                        case Direction.Down:
                            block = new CellType[3, 3]{
                                { CellType.Empty, CellType.Empty, CellType.Empty},
                                { CellType.Active, CellType.Active, CellType.Active},
                                {CellType.Empty, CellType.Active,CellType.Empty }
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
                        case Direction.Down:
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
                        case Direction.Down:
                            block = new CellType[3, 3]
                            {
                                    { CellType.Empty, CellType.Empty, CellType.Empty},
                                    { CellType.Active, CellType.Active, CellType.Active},
                                    {CellType.Active, CellType.Empty,CellType.Empty }
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
                        case Direction.Down:
                            block = new CellType[3, 3]
                            {
                                { CellType.Empty, CellType.Empty, CellType.Empty},
                                { CellType.Active, CellType.Active, CellType.Active},
                                {CellType.Empty, CellType.Empty,CellType.Active }
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
                        case Direction.Down:
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
                        case Direction.Down:
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
            if (CanMoveDown()) {   
                for (int i = HEIGHT - 2; i >= 0 ; i--)
                {
                    for (int j = WIDTH - 1; j >= 0; j--)
                    {
                        if (_board[i, j] == CellType.Active)
                        {
                            _board[i, j] = CellType.Empty;
                            _board[i + 1, j] = CellType.Active;
                        }

                        
                    }
                }
                _CurrentY++;
            }

            if (CheckEndedActiveStatus())
            {
                ChangeActiveToAnyStatus(CellType.Fill);
                CheckLineFilled();
                MakeNewBlock();
            }
        }

        public void MoveRight()
        {
            if (CanMoveRight())
            {
                for (int i = 0; i < HEIGHT; i++)
                {
                    for (int j = WIDTH - 2; j >= 0; j--)
                    {
                        if (_board[i, j] == CellType.Active)
                        {
                            _board[i, j] = CellType.Empty;
                            _board[i, j + 1] = CellType.Active;
                        }
                    }
                }
            _CurrentX++;
            }
        }

        public void MoveLeft()
        {
            if (CanMoveLeft())
            {
                for (int i = 0; i < HEIGHT; i++)
                {
                    for (int j = 0; j < WIDTH; j++)
                    {
                        if (_board[i, j] == CellType.Active)
                        {
                            _board[i, j] = CellType.Empty;
                            _board[i, j - 1] = CellType.Active;
                        }
                    }
                }
            _CurrentX--;
            }
        }


        public void ChangeToward()
        {
            int overIdx = 0; // 방향을 돌릴 때 화면 밖으로 나가는 현상 배제

            _dirIndex = _dirIndex > 3 ? 0 : _dirIndex;
            _currentDirection = _directions[_dirIndex];
            _dirIndex++;

            if(_CurrentX + _CurrentBlockSize > WIDTH)
            {
                overIdx = (_CurrentBlockSize + _CurrentX) - WIDTH;
                _CurrentX -= overIdx;
            }

            if (_CurrentX - _CurrentBlockSize < 0)
            {
                _CurrentX = 0;
            }

            

            ChangeActiveToAnyStatus(CellType.Empty);
            MergeBlockToBoard();
        }

        /*private void CheckChangeFillState()
        {
            for (int i = 0; i < HEIGHT; i++)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    if (board[i, j] == CellType.Active)
                    {
                        if (board[i, j - 1] == CellType.Fill)
                        {

                        }
                        board[i, j] = CellType.Empty;
                        board[i, j - 1] = CellType.Active;
                    }
                }
            }
        }*/

        private void CheckLineFilled()
        {
            for (int row = HEIGHT - 1; row >= 0; row--)
            {
                int fillCount = 0;
                for (int col = WIDTH - 1; col >= 0; col--)
                {
                    if (_board[row, col] == CellType.Fill)
                    {
                        fillCount++;
                    }
                }

                if (fillCount == WIDTH)
                {
                    DestroyBlock(row);
                }
            }
        }

        private void DestroyBlock(int removedRow)
        {
            for (int row = removedRow; row > 0; row--)
            {
                for (int col = 0; col < WIDTH; col++)
                {
                    _board[row, col] = _board[row - 1, col];
                }
            }
            
        }

        public bool CheckEndedActiveStatus()
        {
            int endHeight = HEIGHT - 1;

            for (int row = 0; row < HEIGHT; row++)
            {
                for (int col = 0; col < WIDTH; col++)
                {
                    if (_board[row, col] == CellType.Active && row + 1 == HEIGHT) // 마지막 라인에 갔을 때
                    {
                        return true;
                    }else if (_board[row, col] == CellType.Active && _board[row + 1, col] == CellType.Fill) // Active 블록 아래에 Fill 되어있을 때
                    {
                        // 어떤 상황에 끝나는지 테트리스에 대한 이해도가 필요하다.
                        return true;
                    }
                }
            }
            return false;
        }

        public void ChangeActiveToAnyStatus(CellType type)
        {
            for (int i = 0; i < HEIGHT; i++)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    if (_board[i, j] == CellType.Active)
                    {

                        _board[i, j] = type;
                        
                    }
                }
            }
        }

        private bool CanMoveLeft()
        {
            for (var row = 0; row < HEIGHT; row++)
            {
                if (_board[row, 0] == CellType.Active)
                {
                    return false;
                }
            }

            for (var row = 0; row < HEIGHT; row++)
            {
                for (var col = 1; col < WIDTH; col++)
                {
                    if (_board[row, col] == CellType.Active && _board[row, col - 1] == CellType.Fill)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private bool CanMoveRight()
        {
            for (var row = 0; row < HEIGHT; row++)
            {
                if (_board[row, WIDTH - 1] == CellType.Active)
                {
                    return false;
                }
            }

            for (var row = 0; row < HEIGHT; row++)
            {
                for (var col = 0; col < WIDTH - 1; col++)
                {
                    if (_board[row, col] == CellType.Active && _board[row, col + 1] == CellType.Fill)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        private bool CanMoveDown()
        {
            for (var col = 0; col < WIDTH; col++)
            {
                if (_board[HEIGHT - 1, col] == CellType.Active)
                {
                    return false;
                }
            }
            return true;
        }
        // 동작마다 움직임에 대한 예외 처리 할 것
    }
}
