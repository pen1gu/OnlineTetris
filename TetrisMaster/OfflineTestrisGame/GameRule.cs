using System;
using System.Collections.Generic;
using System.Text;

namespace OfflineTestrisGame
{
    public class GameRule
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

        internal bool IsGameOver(int width, CellType[,] board)
        {
            for (int col = 0; col < width; col++)
            {
                if(board[1, col] == CellType.Fill)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
