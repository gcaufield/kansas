using System;

namespace kansas
{
    public interface ISerialDevice
    {
        /// <summary>
        /// Reads from the Serial device into the buffer.
        /// </summary>
        /// <param name="buffer">Buffer to write to</param>
        /// <returns>Total number of bytes read</returns>
        int Read(byte[] buffer);

        /// <summary>
        /// Writes to the Serial device from a buffer.
        /// </summary>
        /// <param name="buffer">Buffer to read from</param>
        /// <param name="count">Number of bytes to write from the buffer</param>
        void Write(byte[] buffer, int count);
    }
}

