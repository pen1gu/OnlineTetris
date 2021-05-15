using JkwExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TetrisServer
{
    public static class BroadcastExtension
    {
        public static async Task Broadcast(this List<User> sharedUserList, Func<User, Task> action)
        {
            List<User> userList;
            lock (sharedUserList)
            {
                userList = sharedUserList.ToList();
            }

            await userList
                .Select(user => action(user))
                .WhenAll();
        }
    }
}
