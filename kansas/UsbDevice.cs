using System;
using System.IO;

namespace kansas
{
    public sealed class UsbDevice
        : IDisposable, ISerialReader, ISerialWriter
    {
        private FileStream _stream;

        public UsbDevice(string address)
        {
            _stream = File.Open(address, FileMode.Open, FileAccess.ReadWrite);
        }

        /// <summary>
        /// Reads from the USB device into the buffer.
        /// </summary>
        /// <param name="buf">Buffer to write to</param>
        /// <returns>Total number of bytes read</returns>
        public int Read(byte[] buffer)
        {
            return _stream.Read(buffer, 0, buffer.Length);
        }

        public void Write(byte[] buffer, int count)
        {
            _stream.Write(buffer, 0, count);
        }


        #region IDisposable implementation

        public void Dispose()
        {
            _stream.Dispose();
        }

        #endregion
    }
}

