using System;
using System.Collections.Generic;
using OpenALDotNet;
using OpenALDotNet.Streams;
using AdvanceMath;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;

namespace MovieEditor.SoundEngines
{
    public class AudioMemoryStream:OpenALDotNet.Streams.AudioStream 
    {
       // List<byte[]> Chunks = new List<byte[]>();
        int _BytesPerSample;
        int _NumChannels;
        int _SamplesPerSecond;
        int _BufferLength;
        public int PrimaryBufferSize
        {
            set
            {
                AudBuffer = new byte[value];
                ReadIndex = 0;
                WriteIndex = 0;
            }
            get
            {
                return AudBuffer.Length;
            }
        }
        public int BufferLength
        {
            get { return _BufferLength; }
            set { _BufferLength = value; }
        }
        public AudioMemoryStream(int BytesPerSample, int NumChannels, int SamplesPerSecond )
        {
            _BytesPerSample = BytesPerSample;
            _NumChannels = NumChannels;
            _SamplesPerSecond = SamplesPerSecond;
           
        }

        public override AudioFormatEnum Format
        {
            get
            {
                return AudioBuffer.GetFormat(_BytesPerSample*8, _NumChannels );
            }
        }

        public override int Frequency
        {
            get
            {
                return _SamplesPerSecond ;
            }
        }
       // private int cc = 0;
        public override int Read(byte[] buffer, int offset, int count)
        {

            if (ReadIndex + count < AudBuffer.Length)
            {
                for (int i = 0; i < count; i++)
                {
                    buffer[i] = AudBuffer[i + ReadIndex];
                }
                ReadIndex += count;
            }
            else
            {
                if (AudBuffer.Length == 0)
                {
                    count = 0;
                }
                else
                {
                    int j = 0;
                    for (int i = ReadIndex; i < AudBuffer.Length; i++)
                    {
                        buffer[j] = AudBuffer[i];
                        j++;
                    }
                    int jj = 0;
                    for (int i = j; i < count; i++)
                    {
                        buffer[i] = AudBuffer[jj];
                        jj++;
                    }
                    ReadIndex = jj;
                }
            }
            return count;
        }

        #region Unimplemented
        public override bool CanSeekTime
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override TimeSpan TimePosition
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public override TimeSpan TimeSeek(TimeSpan offset, System.IO.SeekOrigin origin)
        {

            throw new Exception("The method or operation is not implemented.");
        }

        public override TimeSpan TimeLength
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override bool CanSeekTrack
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override int TrackPosition
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public override int TrackSeek(int offset, System.IO.SeekOrigin origin)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override int TrackCount
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override bool CanRead
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override bool CanSeek
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override long Length
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override long Position
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }


        public override long Seek(long offset, System.IO.SeekOrigin origin)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        public override void SetLength(long value)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        #endregion
        public override bool CanWrite
        {
            get { return false; }
        }

        public override void Flush()
        {
            WriteIndex = 0;
            for (int i = 0; i < AudBuffer.Length; i++)
            {
                AudBuffer[i] = 0;
            }
        }

        private byte[] AudBuffer = null;
        private int WriteIndex = 0;
        private int ReadIndex = 0;
        public override void Write(byte[] buffer, int offset, int count)
        {
            if (AudBuffer == null)
                AudBuffer = new byte[buffer.Length * 2];
            if (WriteIndex + buffer.Length < AudBuffer.Length)
            {
                buffer.CopyTo(AudBuffer, WriteIndex);
                WriteIndex += buffer.Length;
            }
            else
            {
                int j = 0;
                for (int i = WriteIndex; i < AudBuffer.Length; i++)
                {
                    AudBuffer[i] = buffer[j];
                    j++;
                }
                int jj = 0;
                for (int i = j; i < buffer.Length; i++)
                {
                    AudBuffer[jj] = buffer[i];
                    jj++;
                }
                WriteIndex = jj;
            }
        }
        
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
