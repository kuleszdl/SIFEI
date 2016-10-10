using System;
using System.Net.Sockets;
using System.Text;
using SIF.Visualization.Excel.Helper;
using SIF.Visualization.Excel.Properties;

namespace SIF.Visualization.Excel.Networking
{
    public static class SocketExtensions
    {
        /// <summary>
        /// Sends a String over the socket
        /// </summary>
        /// <param name="socket">Socket to send data over</param>
        /// <param name="value"> The string to send over the socket</param>
        public static void SendString(this Socket socket, string value)
        {
            var buffer = Encoding.UTF8.GetBytes(value);
            socket.Send(BitConverter.GetBytes((long)buffer.Length));
            socket.Send(buffer);
        }

        /// <summary>
        /// Sends ´some bytes over the socket
        /// </summary>
        /// <param name="socket">Socket to send data over</param>
        /// <param name="value"> The bytes to send over the socket</param>
        public static void SendBytes(this Socket socket, byte[] value)
        {
            socket.Send(BitConverter.GetBytes((long)value.Length));
            socket.Send(value);
        }

        /// <summary>
        /// Reads a String from the socket
        /// </summary>
        /// <param name="socket">Socket to recieve data from</param>
        /// <returns string> The string read from the socket </returns>
        public static string ReadString(this Socket socket)
        {
            try
            {
                byte[] buffer = new byte[8];

                socket.Receive(buffer, 0, 8, SocketFlags.None);

                int stringLength = (int) BitConverter.ToInt64(buffer, 0);

                buffer = new byte[stringLength];
                socket.Receive(buffer, 0, stringLength, SocketFlags.None);

                return Encoding.UTF8.GetString(buffer).Trim();
            }
            catch (OutOfMemoryException ex)
            {
                ScanHelper.ScanUnsuccessful(Resources.tl_OutOfMemory);
                return string.Empty;
            }
        }
    }
}
