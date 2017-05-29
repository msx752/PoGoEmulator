using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PoGoEmulator.Models
{
    public class HttpNetworkStream : IDisposable
    {
        private byte[] _sendBuffer = new byte[0];
        public byte[] SendBuffer { get { return _sendBuffer; } }
        public int Position { get; private set; }

        public Encoding Encode { get; } = Encoding.UTF8;
        private NetworkStream NStream { get; }

        /// <summary>
        /// </summary>
        /// <param name="enc">
        /// default: utf8 
        /// </param>
        public HttpNetworkStream(Encoding enc = null)
        {
            if (enc != null)
                Encode = enc;
        }

        /// <summary>
        /// </summary>
        /// <param name="ns">
        /// network connection 
        /// </param>
        /// <param name="enc">
        /// default: utf8 
        /// </param>
        public HttpNetworkStream(NetworkStream ns, Encoding enc = null) : this(enc)
        {
            NStream = ns;
        }

        public int Read(byte[] buffer, int offset, int size)
        {
            if (NStream == null) return -1;
            return NStream.Read(buffer, offset, size);
        }

        public byte[] ReadBuffer(int maxLenght = 1024 * 1024)
        {
            byte[] b = new byte[maxLenght];
            int len = Read(b, 0, b.Length);
            Array.Resize(ref b, len);
            return b;
        }

        public void Write(StringBuilder sb)
        {
            Write(sb.ToString());
        }

        public void Write(string textData)
        {
            if (string.IsNullOrWhiteSpace(textData)) return;
            Write(Encode.GetBytes(textData.ToCharArray()));
        }

        public void Write(byte[] byteData)
        {
            if (byteData.Length == 0) return;
            Array.Resize(ref _sendBuffer, _sendBuffer.Length + byteData.Length);
            Array.Copy(byteData, 0, _sendBuffer, Position, byteData.Length);
            Position = _sendBuffer.Length;
        }

        public void Flush()
        {
            NStream?.Write(SendBuffer, 0, Position);
            NStream?.Flush();
        }

        public byte[] SelectBuffer(int startIndex)
        {
            int lenght = Math.Abs(startIndex - Position);
            byte[] selected = new byte[lenght];
            Array.Copy(_sendBuffer, startIndex, selected, 0, selected.Length);
            return selected;
        }

        public void Dispose()
        {
            _sendBuffer = null;
        }

        public void Close()
        {
            NStream.Close();
        }
    }
}