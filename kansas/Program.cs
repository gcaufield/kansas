using System;
using System.IO;

namespace kansas
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var framer = new AntMessageFramer();
            using(var device = new UsbDevice("/dev/ttyUSB0"))
            {
                byte[] cmd = new byte[] { 0x4B, 0 };
                byte[] framedCmd = framer.GetFramedMessage(cmd);
                device.Write(framedCmd, framedCmd.Length);
                while (true)
                {
                    byte[] data = new byte[255];
                    int bytes = device.Read(data);
                    Console.Write("Bytes Read{0}\n", bytes);
                }
            }
        }
    }
}
