using System;

namespace kansas
{
    public interface ISerialReader
    {
        /// <summary>
        /// Reads from the Serial device into the buffer.
        /// </summary>
        /// <param name="buffer">Buffer to write to</param>
        /// <returns>Total number of bytes read</returns>
        int Read(byte[] buffer);
    }
}

