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
        Task UpdateBoardAsync(User user, BoardBase board);
    }

    public class Game : IGame
    {
        private readonly string Id = Guid.NewGuid().ToString();
        public List<User> UserList { get; private set; } = new List<User>();
        public Dictionary<User, BoardBase> UserBoardState = new Dictionary<User, BoardBase>();

        public async Task StartAsync()
        {
            await UserList.Broadcast(user => user.SendGameStartAsync());
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

        public async Task UpdateBoardAsync(User user, BoardBase board)
        {
            if (!IsIn(user))
                return;

            List<(string UserName, BoardBase Board)> boardList = null;
            lock (UserBoardState)
            {
                UserBoardState[user] = board;
                boardList = UserBoardState.Select(x => (x.Key.Name.Name, x.Value)).ToList();
            }

            await UserList.Broadcast(u => u.SendBoardUpdated(boardList));
        }
    }
}
