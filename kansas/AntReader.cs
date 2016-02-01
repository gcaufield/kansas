using System;
using System.ComponentModel;
using System.Threading;

namespace kansas
{
    internal enum ReadState
    {
        Sync,
        Size,
        Data
    }

    public class AntReader
        : IDisposable
    {
        private const int BufferSize = 256;
        private const byte MesgTxSync = 0xA4;

        private bool _stopped;
        private BackgroundWorker _readThread;
        private readonly ISerialReader _reader;

        public event EventHandler<AntMessageEventArgs> MessageRecieved;

        public AntReader(ISerialReader reader)
        {
            _reader = reader;
            _stopped = true;
        }

        public void Start()
        {
            lock (this)
            {
                if (_readThread != null)
                {
                    throw new InvalidOperationException("Thread is Already Running!");
                }

                _readThread = new BackgroundWorker();
                _readThread.DoWork += Read;
                _readThread.RunWorkerAsync();
                _readThread.WorkerSupportsCancellation = true;
            }
        }

        public void Stop()
        {
            lock (this)
            {
                if (_readThread == null)
                {
                    return;
                }

                _readThread.CancelAsync();

                Monitor.Wait(this);
                _readThread.Dispose();
                _readThread = null;
            }
        }

        #region IDisposable implementation

        public void Dispose()
        {
            Stop();
        }

        #endregion

        private void Read(object sender, DoWorkEventArgs e)
        {
            byte[] mesgBuffer = null;
            byte dataRemaining = 0;
            byte bytesToRead;
            byte mesgOffset = 0;
            ReadState state = ReadState.Sync;
            byte[] buffer = new byte[BufferSize];
            while (!_readThread.CancellationPending)
            {
                int bytesRead = _reader.Read(buffer);

                for (int i = 0; i < bytesRead; i++)
                {
                    switch (state)
                    {
                        case ReadState.Sync:
                            if (buffer[i] == MesgTxSync)
                            {
                                state = ReadState.Size;
                            }
                            break;

                        case ReadState.Size:
                            mesgBuffer = new byte[buffer[i] + 4];
                            dataRemaining = (byte)(buffer[i] + 2);
                            mesgBuffer[0] = MesgTxSync;
                            mesgBuffer[1] = buffer[i];
                            mesgOffset = 2;
                            state = ReadState.Data;
                            break;

                        case ReadState.Data:
                            bytesToRead = (byte)Math.Min(dataRemaining, bytesRead - i);
                            Array.Copy(buffer, i, mesgBuffer, mesgOffset, bytesToRead);
                            dataRemaining -= bytesToRead;
                            mesgOffset += bytesToRead;

                            if (dataRemaining == 0)
                            {
                                byte checksum = 0;

                                for (int j = 0; j < mesgBuffer.Length; j++)
                                {
                                    checksum ^= mesgBuffer[j];
                                }

                                if (checksum == 0)
                                {
                                    OnMessageCompleted(mesgBuffer);
                                }

                                state = ReadState.Sync;
                            }
                            break;
                    }
                }
            }

            lock (this)
            {
                Monitor.PulseAll(_readThread);
            }
        }

        private void OnMessageCompleted(byte[] mesgBuffer)
        {
            Console.Write("Message: ");
            foreach (byte b in mesgBuffer)
            {
                Console.Write("[{0:X2}]", b);
            }
            Console.WriteLine();
        }
    }
}

