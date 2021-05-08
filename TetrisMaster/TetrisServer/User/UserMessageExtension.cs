using Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TetrisServer
{
    public static class UserMessageExtension
    {
        public static async Task SendLoginAllowAsync(this User user, bool allow, string deniedReason = null)
        {
            await user.SendMessageAsync(new SC_LoginAllow
            {
                Allow = allow,
                DeniedReason = deniedReason,
            });
        }

        public static async Task SendMemberUpdatedAsync(this User user, List<User> userList)
        {
            await user.SendMessageAsync(new SC_MemberUpdated
            {
                UserList = userList.Select(x => x.Name.Name).ToList(),
            });
        }
    }
}
