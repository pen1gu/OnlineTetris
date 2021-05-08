using Common;
using System.Threading.Tasks;

namespace TetrisServer
{
    public class UserName : StringId
    {
        public string Name => Id;
    }

    public class User
    {
        public UserName Name { get; private set; }

        private SocketEx _socket;

        public User(UserName name, SocketEx socket)
        {
            Name = name;
            _socket = socket;
        }

        public User(string name, SocketEx socket)
            : this(new UserName { Id = name }, socket)
        {
        }

        public bool IsYou(SocketEx socket)
        {
            return _socket == socket;
        }

        public async Task SendMessageAsync(ServerPacketBase packet)
        {
            await _socket?.SendMessageAsync(packet);
        }
    }
}
