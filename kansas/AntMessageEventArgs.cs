using System;
using System.ComponentModel;
using System.Threading;

namespace kansas
{
	public class AntMessageEventArgs
        : EventArgs
	{
        public AntMessage Message
        {
            get;
            private set;
        }

        public AntMessageEventArgs(AntMessage message)
        {
            Message = message;
        }
	}
}

