using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SIF.Visualization.Excel.Networking
{
    public static class SocketExtensions
    {
        public static void SendString(this Socket socket, string value)
        {
            var buffer = UTF8Encoding.UTF8.GetBytes(value);
            socket.Send(BitConverter.GetBytes((long)buffer.Length));
            socket.Send(buffer);
        }

        public static void SendBytes(this Socket socket, byte[] value)
        {
            socket.Send(BitConverter.GetBytes((long)value.Length));
            socket.Send(value);
        }

        public static string ReadString(this Socket socket)
        {
            byte[] buffer = new byte[8];
            socket.Receive(buffer, 0, 8, SocketFlags.None);

            int stringLength = (int)BitConverter.ToInt64(buffer, 0);

            buffer = new byte[stringLength];
            socket.Receive(buffer, 0, stringLength, SocketFlags.None);

            return UTF8Encoding.UTF8.GetString(buffer);
        }
    }
}
