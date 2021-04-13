using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common
{
    public interface ISocketEx
    {
        Task<(int MessageLength, string Message)> ReceiveMessageAsync(CancellationToken ct = default);
        Task SendMessageAsync<T>(T message, CancellationToken ct = default) where T : PacketBase;
    }
    public class SocketEx : ISocketEx, IDisposable
    {
        private Socket _socket;
        private CircularArray<byte> _recvBuffer = new CircularArray<byte>(10000);
        private byte[] _recvBuffer2 = new byte[10000];
        private byte[] _messageLengthBuffer = new byte[sizeof(int)];

        private byte[] _sendBuffer = new byte[10000];

        public bool Connected => _socket?.Connected ?? false;

        public SocketEx(Socket socket)
        {
            _socket = socket;
        }

        public void Dispose()
        {
            _socket?.Dispose();
        }

        public void Disconnect(bool reuseSocket)
        {
            _socket?.Disconnect(reuseSocket);
        }

        public void Close()
        {
            _socket?.Close();
        }

        public async Task<(int MessageLength, string Message)> ReceiveMessageAsync(CancellationToken ct = default)
        {
            var messageLength = await ReadMessageLengthAsync(ct);
            if (messageLength == 0)
            {
                return (0, string.Empty);
            }

            var message = await ReadMessageAsync(messageLength, ct);
            return (messageLength, message);
        }

        private async Task<int> ReadMessageLengthAsync(CancellationToken ct)
        {
            while (_recvBuffer.Length < sizeof(int))
            {
                await FillRecvBufferAsync(ct);
            }
            _recvBuffer.Read(_messageLengthBuffer);
            var length = BitConverter.ToInt32(_messageLengthBuffer);
            return length;
        }

        private async Task<string> ReadMessageAsync(int messageLength, CancellationToken ct)
        {
            while (_recvBuffer.Length < messageLength)
            {
                await FillRecvBufferAsync(ct);
            }
            _recvBuffer.Read(_recvBuffer2, 0, messageLength);
            var message = Encoding.UTF8.GetString(_recvBuffer2, 0, messageLength);
            return message;
        }

        private async Task<int> FillRecvBufferAsync(CancellationToken ct)
        {
            var memory = _recvBuffer.GetWritableMemory();
            var count = await _socket.ReceiveAsync(memory, SocketFlags.None, ct);
            _recvBuffer.ShiftEndIndex(count);

            return count;
        }

        public async Task SendMessageAsync<T>(T message, CancellationToken ct = default) where T : PacketBase
        {
            var json = JsonConvert.SerializeObject(message);
            await SendTextAsync(json, ct);
        }

        private async Task SendTextAsync(string text, CancellationToken ct)
        {
            int byteLength = Encoding.UTF8.GetBytes(text, 0, text.Length, _sendBuffer, sizeof(int));
            var lengthBuffer = BitConverter.GetBytes(byteLength);
            lengthBuffer.CopyTo(_sendBuffer, 0);
            await _socket.SendAsync(new Memory<byte>(_sendBuffer, 0, byteLength + sizeof(int)), SocketFlags.None, ct);
        }
    }
}
