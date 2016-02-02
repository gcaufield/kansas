using System;
using System.IO;

namespace kansas
{
    public sealed class TxAntMessage
        : AntMessage, ITxMessage
    {
        private readonly BinaryWriter _writer;

        public TxAntMessage(byte messageId, byte size)
            : base(messageId, size)
        {
            _writer = new BinaryWriter(MessageContent);
        }

        protected override void Dispose(bool disposing)
        {
            _writer.Dispose();
            base.Dispose(disposing);
        }

        public void SetByte(byte value, uint offset)
        {
            throw new NotImplementedException();
        }

        public void SetUInt16(uint value, uint offset)
        {
            throw new NotImplementedException();
        }
    }
}
