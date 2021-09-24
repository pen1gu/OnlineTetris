using System;
using System.Collections.Generic;
using System.Text;

namespace OfflineTestrisGame
{
    class GameRule
    {
        // 게임 룰 ( 라인 체크 등 )

        /*internal bool IsFilledLine(int width, int height, CellType[,] board)
        {
            int cnt = 0;

            for (int i = 0; i < height; i++)
            {
                cnt = 0;
                for (int j = 0; j < width; j++)
                {
                    if (board[i, j] == CellType.Fill)
                        cnt++;
                }
            }

            if (cnt == width)
            {
                return true;
            }

            return false;
        }*/ // 안써서 잠궈둠

        internal bool IsGameOver(int width, int height, CellType[,] board)
        {
            bool isBlockFIlled = true;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (board[i, j] != CellType.Fill)
                    {
                        isBlockFIlled = false;
                    }
                }
            }

            return isBlockFIlled;
        }
    }
}
