using System;

namespace kansas
{
    public class AntMessageFramer
    {
        private const byte TxSync = 0xA4;
        public AntMessageFramer()
        {
        }

        public byte[] GetFramedMessage(byte[] message)
        {
            byte[] framedMessage = new byte[message.Length + 3];

            framedMessage[0] = TxSync;
            framedMessage[1] = (byte)(message.Length - 1);

            Array.Copy(message, 0, framedMessage, 2, message.Length);

            byte crc = 0;
            for (int i = 0; i < framedMessage.Length - 2; i++)
            {
                crc ^= framedMessage[i];
            }

            framedMessage[framedMessage.Length - 1] = crc;
            return framedMessage;
        }
    }
}

