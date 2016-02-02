using System;
using System.IO;

namespace kansas
{
    /// <summary>
    /// Message Interface
    /// </summary>
	public interface IAntMessage
	{
        byte MessageId { get; }
        byte Length { get; }

        /// <summary>
        /// Gets the content of the message.
        /// </summary>
        /// <param name="buffer">Output Buffer.</param>
        /// <param name="offset">Output Offset.</param>
        void GetMessageContent(byte[] buffer, int offset);
	}
}

