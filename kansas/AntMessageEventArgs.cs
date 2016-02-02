using System;
using System.ComponentModel;
using System.Threading;

namespace kansas
{
	public class AntMessageEventArgs
        : EventArgs
	{
        public IRxMessage Message
        {
            get;
            private set;
        }

        public AntMessageEventArgs(IRxMessage message)
        {
            Message = message;
        }
	}
}

