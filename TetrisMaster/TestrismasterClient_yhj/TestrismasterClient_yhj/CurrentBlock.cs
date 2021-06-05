using System;
using Common;
using System.Collections.Generic;
using System.Text;

namespace TetrisMasterClient_yhj
{
    class CurrentBlock
    {
        int width, height;
        public PieceType pieceType { get; set; }
        
        bool landed = false;

        CurrentBlock(string type) {
            pieceType = ParseType(type);
        }

        private PieceType ParseType(string type)
        {
            int typeNum = -1;

            switch (type) {
                case "A":
                    typeNum = 0;
                    width = 4;
                    height = 1;
                    break;
                case "B":
                    typeNum = 1;
                    width = 3;
                    height = 2;
                    break;
                case "C":
                    typeNum = 2;
                    width = 2;
                    height = 2;
                    break;
                case "D":
                    typeNum = 3;
                    width = 3;
                    height = 2;
                    break;
                case "E":
                    typeNum = 4;
                    width = 3;
                    height = 2;
                    break;
                case "F":
                    typeNum = 5;
                    width = 3;
                    height = 2;
                    break;
                case "G":
                    typeNum = 6;
                    width = 3;
                    height = 2;
                    break;
                default:
                    typeNum = -1;
                    throw new NullReferenceException("존재하지 않는 블록입니다.");
            }


            return (PieceType) typeNum; // enum 사용법 질문

           
        }
    }

    class BlockType
    {
        public BlockType(PieceType type, int width, int height)
        {

        }
    }
}
