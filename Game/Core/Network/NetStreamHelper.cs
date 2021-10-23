using System.IO;

namespace Game.Core.Network
{
    public class NetStreamHelper
    {
        /// <summary>
        /// Read one sequence of json in a <see cref="Stream"/> into a data buffer.
        /// </summary>
        /// <param name="dataBuffer">Data buffer to read the sequence of bytes into</param>
        /// <param name="stream">The stream to read from. The stream position will be </param>
        /// <returns>Number of bytes in the JSON sequence</returns>
        /// <exception cref="IOException"></exception>
        public static int ReadOneJsonSequence(byte[] dataBuffer, Stream stream)
        {
            const byte jsonBlockOpenByte = 123; // "{"
            const byte jsonBlockCloseByte = 125; // "}"

            int jsonDepth = 0;
            for (int i = 0; i < dataBuffer.Length; i++)
            {
                int rawByte = stream.ReadByte();
                if (rawByte == -1)
                {
                    throw new IOException("Stream ended before JSON sequence ended");
                }

                byte byteRead = (byte)rawByte;
                if (byteRead == jsonBlockOpenByte) jsonDepth++;
                if (byteRead == jsonBlockCloseByte) jsonDepth--;

                dataBuffer[i] = byteRead;

                if (jsonDepth == 0) return i + 1; // return bytes read
            }
            throw new IOException("Size of databuffer was exceeded");
        }
    }
}
