using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace TetrisServer
{
    public interface IGame
    {
        Task StartAsync();
        bool IsIn(User user);
        Task EnterUserAsync(User user);
        Task LeaveUserAsync(User user);
    }

    public class Game : IGame
    {
        private readonly string Id = Guid.NewGuid().ToString();
        public List<User> UserList { get; private set; } = new List<User>();

        public async Task StartAsync()
        {
            await UserList.Broadcast(user => user.SendGameStartAsync());
            await UserList.Broadcast(user => user.SendRandomPieceAsync());
        }

        public async Task EnterUserAsync(User user)
        {
            List<User> userList = null;
            lock (UserList)
            {
                UserList.Add(user);
                userList = UserList.ToList();
            }

            await userList
                .Broadcast(user => user.SendMemberUpdatedAsync(Id, userList));
        }

        public async Task LeaveUserAsync(User user)
        {
            List<User> userList = null;
            lock (UserList)
            {
                UserList = UserList.Where(x => x.Name != user.Name).ToList();
                userList = UserList.ToList();
            }

            await userList
                .Broadcast(user => user.SendMemberUpdatedAsync(Id, userList));
        }

        public bool IsIn(User user)
        {
            lock (UserList)
            {
                return UserList.Any(x => x == user);
            }
        }
    }
}
