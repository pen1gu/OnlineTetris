using Common;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TetrisServer
{
    public static class PacketHandler
    {
        public static async Task HandlePacketAsync(this IServerApi api, JObject packetObj)
        {
            var packetType = Enum.Parse<PacketType>(packetObj.Value<string>("Type"));

            if (packetType == PacketType.CS_Login)
            {
                var packet = packetObj.ToObject<CS_Login>();
                await api.Handle_CS_Login(packet);
            }
            else if (packetType == PacketType.CS_Start)
            {
                var packet = packetObj.ToObject<CS_Start>();
                await api.Handle_CS_Start(packet);
            }
        }
    }
}
