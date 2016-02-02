using System;

namespace kansas
{
    public class AntMessageFramer
    {
        private const byte TxSync = 0xA4;
        public AntMessageFramer()
        {
        }

        public byte[] GetFramedMessage(AntMessage message)
        {
            byte[] framedMessage = new byte[message.Length + 3];

            framedMessage[0] = TxSync;
            framedMessage[1] = message.Length;
            framedMessage[2] = message.MessageId;

            message.GetMessageContent(framedMessage, 3);

            byte checksum = 0;
            for (int i = 0; i < framedMessage.Length - 2; i++)
            {
                checksum ^= framedMessage[i];
            }

            framedMessage[framedMessage.Length - 1] = checksum;
            return framedMessage;
        }
    }
}

