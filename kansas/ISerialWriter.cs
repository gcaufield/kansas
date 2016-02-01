using System;

namespace kansas
{
    public interface ISerialWriter
    {
        /// <summary>
        /// Writes to the Serial device from a buffer.
        /// </summary>
        /// <param name="buffer">Buffer to read from</param>
        /// <param name="count">Number of bytes to write from the buffer</param>
        void Write(byte[] buffer, int count);
    }
}

