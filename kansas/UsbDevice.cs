using System;
using System.IO;
using Mono.Unix;
using Mono.Unix.Native;

namespace kansas
{
    public sealed class UsbDevice
        : IDisposable, ISerialReader, ISerialWriter
    {
        private int _fd;
        private Pollfd[] _poll;
        private UnixStream _stream;

        private readonly object _lock;

        public UsbDevice(string address)
        {
            _fd = Syscall.open( address, OpenFlags.O_NONBLOCK | OpenFlags.O_RDWR );

            Pollfd poll = new Pollfd();
            poll.fd = _fd;
            poll.events = PollEvents.POLLIN | PollEvents.POLLRDNORM;
            _poll = new [] { poll };

            _stream = new UnixStream(_fd);
            _lock = new object();
        }

        /// <summary>
        /// Reads from the USB device into the buffer.
        /// </summary>
        /// <param name="buf">Buffer to write to</param>
        /// <returns>Total number of bytes read</returns>
        public int Read(byte[] buffer)
        {
            PollEvents res = (PollEvents)Syscall.poll(_poll, 1000);

            if ((PollEvents.POLLIN & res) != PollEvents.POLLIN)
            {
                return 0;
            }

            lock (_lock)
            {
                return _stream.Read(buffer, 0, buffer.Length);
            }
        }

        public void Write(byte[] buffer, int count)
        {
            lock (_lock)
            {
                _stream.Write(buffer, 0, count);
                _stream.Flush();
            }
        }


        #region IDisposable implementation

        public void Dispose()
        {
            _stream.Close();
            _stream.Dispose();
        }

        #endregion
    }
}

