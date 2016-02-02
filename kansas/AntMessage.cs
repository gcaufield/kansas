using System;
using System.IO;

namespace kansas
{
    /// <summary>
    /// Represents an AntMessage to send to a
    /// </summary>
    public abstract class AntMessage
        : IDisposable, IAntMessage
    {
        private MemoryStream _stream;
        private byte[] _message;
        private byte _messageId;

        /// <summary>
        /// Gets the message identifier.
        /// </summary>
        /// <value>The message identifier.</value>
        public byte MessageId
        {
            get
            {
                return _messageId;
            }
        }

        /// <summary>
        /// Gets the size of Message Content
        /// </summary>
        public byte Length
        {
            get
            {
                return (byte)_message.Length;
            }
        }

        /// <summary>
        /// Gets the stream associated with the message content.
        /// </summary>
        /// <value>The stream.</value>
        protected Stream MessageContent
        {
            get
            {
                return _stream;
            }
        }

        private AntMessage(byte messageId)
        {
            _messageId = messageId;
        }

        protected AntMessage(byte messageId, byte[] buffer, int offset, byte length)
            : this(messageId, length)
        {
            _stream.Seek(0, SeekOrigin.Begin);
            _stream.Write(buffer, offset, length);
        }

        protected AntMessage(byte messageId, byte size)
            : this(messageId)
        {
            _message = new byte[size];
            _stream = new MemoryStream(_message);
        }

        public void GetMessageContent(byte[] buffer, int offset)
        {
            if ((buffer.Length - offset) < _message.Length)
            {
                throw new InvalidOperationException("Buffer is not large enough to hold message content");
            }

            Array.Copy(_message, 0, buffer, offset, _message.Length);
        }

        #region IDisposable implementation

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            _stream.Dispose();
        }

        #endregion
    }
}

