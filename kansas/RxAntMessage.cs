using System;
using System.IO;

namespace kansas
{
    public sealed class RxAntMessage
        : AntMessage, IRxMessage
    {
        private readonly BinaryReader _reader;

        public RxAntMessage(byte messageId, byte[] buffer, int offset, byte length)
            : base(messageId, buffer, offset, length)
        {
            _reader = new BinaryReader(MessageContent);
        }

        public byte ReadByte(int offset)
        {
            throw new NotImplementedException();
        }

        public byte[] ReadBytes(int offset)
        {
            throw new NotImplementedException();
        }

        public short ReadShort(int offset)
        {
            throw new NotImplementedException();
        }

        protected override void Dispose(bool disposing)
        {
            _reader.Dispose();
            base.Dispose(disposing);
        }
    }

}
