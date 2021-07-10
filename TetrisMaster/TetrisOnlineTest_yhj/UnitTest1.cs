using System;
using TetrisMasterClient_yhj;
using Xunit;

namespace TetrisOnlineTest_yhj
{
    public class UnitTest1
    {
        [Fact]
        public void TestSetBoardSize()
        {
            var board = new Board(5, 10);
            Assert.Equal(5, board.Width);
        }

        [Fact]
        public void TestMaximumBoardWidth()
        {
            var board = new Board(25, 10);
            Assert.Equal(20, board.Width);
        }
    }
}
