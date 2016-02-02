using System;
using System.IO;

namespace kansas
{
    public interface IRxMessage
        : IAntMessage
    {
        byte ReadByte(int offset);
        byte[] ReadBytes(int offset);
        short ReadShort(int offset);
    }
}
