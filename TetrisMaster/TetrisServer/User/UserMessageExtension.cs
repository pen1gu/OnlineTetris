using Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JkwExtensions;

namespace TetrisServer
{
    public static class UserMessageExtension
    {
        private static List<PieceType> pieceTypes;
        static UserMessageExtension()
        {
            pieceTypes = typeof(PieceType).GetValues<PieceType>().ToList();
        }

        public static async Task SendLoginAllowAsync(this User user, bool allow, string deniedReason = null)
        {
            await user.SendMessageAsync(new SC_LoginAllow
            {
                Allow = allow,
                DeniedReason = deniedReason,
            });
        }

        public static async Task SendMemberUpdatedAsync(this User user, string Id, List<User> userList)
        {
            await user.SendMessageAsync(new SC_MemberUpdated
            {
                Id = Id,
                UserList = userList.Select(x => x.Name.Name).ToList(),
            });
        }

        public static async Task SendGameStartAsync(this User user)
        {
            await user.SendMessageAsync(new SC_Start());
        }

        public static async Task SendRandomPieceAsync(this User user)
        {
            await user.SendMessageAsync(new SC_NextPiece
            {
                PieceType = pieceTypes.GetRandom(),
            });
        }
        public static async Task SendBoardUpdated(this User user, List<(string, BoardBase)> boardList)
        {
            await user.SendMessageAsync(new SC_BoardUpdated
            {
                BoardList = boardList,
            });
        }
    }
}
