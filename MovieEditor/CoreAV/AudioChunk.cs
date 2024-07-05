using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using System.Threading;
using System.IO;

namespace MovieEditor.CoreAV
{
    public class AudioChunk : Chunk 
    {
        
        private WaveWriter.WaveStream MyStream;
        //private byte[] WholeBuffer;
        private long _SamplesPerSecond;
        private int _BytesPerSample = 1;
        private AudioSampleType _AudioSampleType;
        private long _AverageBytesPerSecond;
        int _NumChannels;
       

        
        public void AudioProperties(out int NumChannels, out int BytesPerSample, out AudioSampleType SampleType, out long SamplesPerSecond)
        {
            NumChannels = _NumChannels;
            BytesPerSample = _BytesPerSample;
            SampleType = _AudioSampleType;
            SamplesPerSecond = _SamplesPerSecond;
        }
        public void SetBuffer(string TempFileName)//, int numChannels, int BytesPerSample, AudioSampleType SampleType, long SamplesPerSecond,double Duration)
        {
            MyStream = new MovieEditor.WaveWriter.WaveStream(TempFileName);
            SetBuffer(MyStream);
           
            int length =(int) MyStream.Length ;
        }
        public void SetBuffer(byte[] Buffer,int numChannels, int BytesPerSample,AudioSampleType SampleType, long SamplesPerSecond)
        {
            string TempFileName=Generals.TempFilename() + ".wav";
            FileStream fs=new FileStream(TempFileName ,FileMode.CreateNew  );
            WaveWriter.WaveFormat wf = new MovieEditor.WaveWriter.WaveFormat((int)SamplesPerSecond, 8 * BytesPerSample, numChannels);
            WaveWriter.WaveWriter ww =
                new MovieEditor.WaveWriter.WaveWriter(fs , wf);
           
            ww.Write(Buffer);
           
            ww.Close();
            fs.Close();
            MyStream = new MovieEditor.WaveWriter.WaveStream(TempFileName);

            short BlockAlign = (short)(numChannels * (BytesPerSample ));
            _AverageBytesPerSecond = SamplesPerSecond * BlockAlign;
            
            this._BytesPerSample = BytesPerSample;
            _NumChannels = numChannels;
            _AudioSampleType = SampleType;
            this._SamplesPerSecond = SamplesPerSecond;
            _RawFileProps.TotalDuration  = ((double)Buffer.Length / (double)SamplesPerSecond / BytesPerSample);
            _ClipProperties.Duration = _RawFileProps.TotalDuration;

            int length = Buffer.Length;
        }
        public void SetBuffer(WaveWriter.WaveStream WaveStream)//, int numChannels, int BytesPerSample, AudioSampleType SampleType, long SamplesPerSecond)
        {
            MyStream = WaveStream;


            this._BytesPerSample = MyStream.Format.wBitsPerSample/8; //BytesPerSample;
            _NumChannels = MyStream.Format.nChannels;// numChannels;
           // _AudioSampleType = MyStream.Format.wFormatTag;// SampleType;
            this._SamplesPerSecond = MyStream.Format.nSamplesPerSec;
            short BlockAlign = (short)(_NumChannels * (_BytesPerSample));
            _AverageBytesPerSecond =_SamplesPerSecond * BlockAlign;

            int length =(int) MyStream.Length;
            _RawFileProps.TotalDuration = ((double)length / (double)_SamplesPerSecond / _BytesPerSample);
            _ClipProperties.Duration = _RawFileProps.TotalDuration;
            if (_BytesPerSample == 1)
                this._AudioSampleType  = AudioSampleType.INT8;
            else if (_BytesPerSample == 2)
                this._AudioSampleType = AudioSampleType.INT16;
            else
                throw new Exception("Format not implemented yet");
            
        }
        public override Chunk Clone()
        {
            
                AudioChunk NewChunk = new AudioChunk( );

                NewChunk.SetBuffer(MyStream );//, _NumChannels, _BytesPerSample, _AudioSampleType, _SamplesPerSecond);
                NewChunk.ChunkName = ChunkName;
                NewChunk.LinkedTrack = LinkedTrack;
                NewChunk.ChunkName = ChunkName;
                NewChunk.RawFileProps = _RawFileProps.Clone();
                NewChunk.ChunkProperties = _ClipProperties.Clone();
                NewChunk.LinkedTrack = LinkedTrack;
                NewChunk.Priority = mPriority;

                return NewChunk;
            
        }
        public double[] MakeEnvelope(int nPixels)
        {
           

            if (MyStream  == null || _BytesPerSample == 0)
                return null;

            //determine the part of the buffer to show on the screen
            int BufferUseLength =(int)( (double)_SamplesPerSecond *_BytesPerSample  * _ClipProperties.Duration);
            
            double PTime = _RawFileProps.StartOffsetTime;
            int BufferStart =((int)Math.Round ( PTime * _SamplesPerSecond)*_BytesPerSample   );
            if ((BufferStart + BufferUseLength) > MyStream.Length) BufferUseLength =(int) MyStream.Length - BufferStart;
            

            //now determine where the pixel boundries fall
            int pWidth = nPixels ;
            int ChunkSize=( (int)Math.Round ((double)(BufferUseLength /nPixels/_BytesPerSample) ,0))*_BytesPerSample  ;

            
            double [] Envelope = new double[pWidth * 2];
            byte [] TempBuffer = new byte[ChunkSize ];
            int MaxValue = 1;
            int EC=0;
            
            MyStream.Position=BufferStart;

            for (int i=0;i<pWidth ;i++)
            {
                MyStream.Read(TempBuffer, 0, ChunkSize);
                unsafe
                {
                    GCHandle h = GCHandle.Alloc(TempBuffer, GCHandleType.Pinned);
                    IntPtr address = h.AddrOfPinnedObject();
                    double Max=0;
                    double Min=0;
                    if (_AudioSampleType == AudioSampleType.INT8)
                    {
                        byte* src;

                        src = (byte*)(address.ToPointer());
                        for (int j = 0; j < ChunkSize; j++)
                        {
                            byte Data = src[j];
                            if (Data > Max) Max = Data;
                            if (Data < Min) Min = Data;
                        }
                    }
                    else if (_AudioSampleType == AudioSampleType.INT16)
                    {
                        Int16  * src;

                        src = (Int16  *)(address.ToPointer());
                        int convertedChunkSize = ChunkSize / 2;
                        for (int j = 0; j < convertedChunkSize; j++)
                        {
                            Int16  Data = src[j];
                           
                            if (Data > Max) Max = Data;
                            if (Data < Min) Min = Data;
                        }
                    }
                    else if (_AudioSampleType == AudioSampleType.INT24 )
                    {
                        throw new Exception("This is not implemented yet");
                    }
                    else if (_AudioSampleType == AudioSampleType.INT32)
                    {
                        Int32* src;

                        src = (Int32*)(address.ToPointer());
                        int convertedChunkSize = ChunkSize / 4;
                        for (int j = 0; j < convertedChunkSize; j++)
                        {
                            Int32 Data = src[j];
                            if (Data > Max) Max = Data;
                            if (Data < Min) Min = Data;
                        }
                    }
                    Envelope[EC] = Max;
                    Envelope[EC + 1] = Min;
                    EC += 2;
                   
                    h.Free();
                }
            }
            //todo: Would it better to take the maximum intensity or just use the 
            //maximum possible.  Leaving it with max possible for right now
            if (_AudioSampleType == AudioSampleType.INT8)
            {
                MaxValue = 256;
            }
            else if (_AudioSampleType == AudioSampleType.INT16)
            {
                MaxValue =(int)Math.Pow( 2, 16);
            }
            else if (_AudioSampleType == AudioSampleType.INT24)
            {
                MaxValue = (int)Math.Pow(2, 24);
            }
            else if (_AudioSampleType == AudioSampleType.INT32)
            {
                MaxValue = (int)Math.Pow(2, 32);
            }
            for (int i = 0; i < Envelope.Length; i++)
            {
                Envelope[i] = (Envelope[i] / MaxValue + 1) / 2;

            }
            return Envelope;
        }

        public override  double GetCutTime(double Time)
        {
            return ((int)Math.Round(Time * _SamplesPerSecond) * _BytesPerSample)/_AverageBytesPerSecond ;
        }
        private int LastFrameIndex=0;
        public byte[] GetFrame(double FrameTime, double FrameDuration)
        {

            double PTime = GetInternalTime(FrameTime);// (FrameTime - ChunkProperties.StartTime + _RawFileProps.StartOffsetTime);
            int SIndex =((int)Math.Round ( PTime * _SamplesPerSecond)*_BytesPerSample   );
            int eIndex = ((int)Math.Round ((PTime + FrameDuration) * _SamplesPerSecond) * _BytesPerSample);
            if (Math.Abs (SIndex - LastFrameIndex) < 200) SIndex = LastFrameIndex;

            byte[] OutBuf = new byte[eIndex - SIndex];
            try
            {
                MyStream.Position = SIndex;
                MyStream.Read(OutBuf, 0, OutBuf.Length);
            }
            catch { }

            LastFrameIndex = eIndex;
            return OutBuf;
        }
        public AudioChunk()
        {
            
            _RawFileProps = new RawFileProperties("", 0, 0);
            _ClipProperties = new ClipProperties(0, 0, 1);

        }
             
        ~AudioChunk()
        {
            if (MyStream == null)
                MyStream.Close();
        }

       
    }
}
