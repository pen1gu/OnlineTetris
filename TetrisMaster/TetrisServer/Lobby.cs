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
        private readonly string LobbyId = "Lobby";
        private List<User> Users { get; set; } = new List<User>();
        private List<IGame> Games { get; } = new List<IGame>();

        public Lobby()
        {
        }

        public async Task EnterUserAsync(User user)
        {
            await user.SendLoginAllowAsync(allow: true);

            List<User> userList = null;
            lock (Users)
            {
                Users.Add(user);
                userList = Users.ToList();
            }

            await Users.Broadcast(user => user.SendMemberUpdatedAsync(LobbyId, userList));

            IGame game;
            lock (Games)
            {
                game = FindGame(user);
                if (game == null)
                {
                    game = new Game();
                    Games.Add(game);
                }
            }
            await game?.EnterUserAsync(user);
        }

        public async Task LeaveUserAsync(User user)
        {
            List<User> userList = null;
            lock (Users)
            {
                user = Users.Find(x => x == user);
                if (user != null)
                {
                    Users.Remove(user);
                    userList = Users.ToList();
                }
            }

            if (userList != null)
            {
                await Users.Broadcast(user => user.SendMemberUpdatedAsync(LobbyId, userList));
            }

            var game = FindGame(user);
            await game?.LeaveUserAsync(user);
        }

        public IGame FindGame(User user)
        {
            lock (Games)
            {
                return Games.FirstOrDefault(x => x.IsIn(user));
            }
        }
    }
}
