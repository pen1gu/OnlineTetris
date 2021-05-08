using Common;
using JkwExtensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisServer
{
    public class Lobby
    {
        private List<User> Users { get; set; }

        public Lobby()
        {
        }

        public async Task EnterUserAsync(SocketEx socket, string userName)
        {
            var user = new User(userName, socket);

            await user.SendLoginAllowAsync(allow: true);

            List<User> userList = null;
            lock (Users)
            {
                Users.Add(user);
                userList = Users.ToList();
            }

            await userList
                ?.Select(async user => await user.SendMemberUpdatedAsync(userList: userList))
                .WhenAll();
        }

        public async Task LeaveUserAsync(SocketEx socket)
        {
            User user;
            List<User> userList = null;
            lock (Users)
            {
                user = Users.Find(x => x.IsYou(socket));
                if (user != null)
                {
                    Users.Remove(user);
                    userList = Users.ToList();
                }
            }

            if (userList != null)
            {
                await userList
                    .Select(async user => await user.SendMemberUpdatedAsync(userList: userList))
                    .WhenAll();
            }
        }
    }
}
