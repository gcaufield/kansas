using System;

namespace kansas
{
    public class AntMessageFramer
    {
        private const byte TxSync = 0xA4;
        public AntMessageFramer()
        {
        }

        public byte GetFramedMessage(AntMessage message, byte[] buffer)
        {
            if (buffer.Length < (message.Length + 4))
            {
                throw new InvalidOperationException("Buffer Not Long enough");
            }

            buffer[0] = TxSync;
            buffer[1] = message.Length;
            buffer[2] = message.MessageId;

            message.GetMessageContent(buffer, 3);

            byte checksum = 0;
            for (int i = 0; i < message.Length + 3; i++)
            {
                checksum ^= buffer[i];
            }

            buffer[message.Length + 3] = checksum;
            return (byte)(message.Length + 4);
        }
    }
}

