using System;
using System.IO;
using System.Threading;

namespace kansas
{
    class MainClass
    {
        private static void OnMessageRecieved(object sender, AntMessageEventArgs e)
        {
            byte[] data = new byte[e.Message.Length];
            Console.WriteLine("Recieved {0}", e.Message.MessageId);

            e.Message.GetMessageContent(data, 0);
            foreach (byte b in data)
            {
                Console.Write("[{0:X2}]", b);
            }
            Console.WriteLine();
        }

        public static void Main(string[] args)
        {
            byte[] buffer = new byte[256];
            var framer = new AntMessageFramer();
            using(var device = new UsbDevice("/dev/ttyUSB0"))
            using(var reader = new AntReader(device))
            {
                reader.MessageRecieved += OnMessageRecieved;
                reader.Start();

                Console.ReadKey();

                var message = new TxAntMessage(0x4A, 1);
                message.SetByte(0, 0);

                byte size = framer.GetFramedMessage(message, buffer);
                device.Write(buffer, size);
                Console.ReadKey();

                message = new TxAntMessage(0x42, 3);
                message.SetByte(0, 0);
                message.SetByte(0x10, 1);
                message.SetByte(0, 2);

                size = framer.GetFramedMessage(message, buffer);
                device.Write(buffer, size);

                message = new TxAntMessage(0x51, 5);
                message.SetByte(0, 0);
                message.SetUInt16(0x10, 1);
                message.SetByte(3, 3);
                message.SetByte(3, 4);

                size = framer.GetFramedMessage(message, buffer);
                device.Write(buffer, size);

                message = new TxAntMessage(0x43, 3);
                message.SetByte(0, 0);
                message.SetUInt16(4096, 1);

                size = framer.GetFramedMessage(message, buffer);
                device.Write(buffer, size);

                message = new TxAntMessage(0x4B, 1);
                message.SetByte(0, 0);

                size = framer.GetFramedMessage(message, buffer);
                device.Write(buffer, size);
                Console.ReadKey();
            }
        }
    }
}
