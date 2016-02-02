using System;
using System.IO;

namespace kansas
{
    public interface ITxMessage
        : IAntMessage
    {
        void SetByte(byte value, uint offset);
        void SetUInt16(uint value, uint offset);
    }
}

