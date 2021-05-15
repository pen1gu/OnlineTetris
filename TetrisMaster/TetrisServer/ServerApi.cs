using Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TetrisServer
{
    public interface IServerApi
    {
        Task ConnectionClosed();
        Task Handle_CS_Login(CS_Login pakcet);
        Task Handle_CS_Start(CS_Start packet);
    }
    public class ServerApi : IServerApi
    {
        private Lobby _lobby;
        private SocketEx _clientSocket;
        private User _user;

        public ServerApi(Lobby lobby, SocketEx socket)
        {
            _lobby = lobby;
            _clientSocket = socket;
        }
        public async Task ConnectionClosed()
        {
            await _lobby.LeaveUserAsync(_user);
        }
        public async Task Handle_CS_Login(CS_Login packet)
        {
            _user = new User(packet.UserName, _clientSocket);
            await _lobby.EnterUserAsync(_user);
        }
        public async Task Handle_CS_Start(CS_Start packet)
        {
            var game = _lobby.FindGame(_user);
            await game?.StartAsync();
        }
    }
}
