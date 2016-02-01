using System;
using System.IO;
using System.Threading;

namespace kansas
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var framer = new AntMessageFramer();
            using(var device = new UsbDevice("/dev/ttyUSB0"))
            using(var reader = new AntReader(device))
            {
                reader.Start();

                byte[] cmd = new byte[] { 0x4B, 0 };
                byte[] framedCmd = framer.GetFramedMessage(cmd);

                device.Write(framedCmd, framedCmd.Length);
                while (true)
                {
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
