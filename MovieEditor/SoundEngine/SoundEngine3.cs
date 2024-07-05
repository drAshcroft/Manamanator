using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Un4seen;
using System.Threading;
using System.IO;
using Un4seen.Bass;

namespace MovieEditor.CoreAV
{
    
    public class SoundEngine3
    {

        private int  _SamplesPerSecond;
        private int _BytesPerSample = 1;
        private AudioSampleType AudioSampleType;
       
        private bool playing = false;
        
        private int _AverageBytesPerSecond;
        public int AverageBytesPerSecond
        {
            get { return _AverageBytesPerSecond; }
        }
        public int SamplesPerSecond
        {
            get { return _SamplesPerSecond; }
        }
        public int BytesPerSample
        {
            get { return _BytesPerSample; }
        }
        private int SBWritePosition = 0;
        private Mutex SecondaryBufferMut = new Mutex();
        private int Try = 0;

        
        /*public void OWriteNextBuffer(int mode, byte[] Buffer)
        {
           // int ret2 = Bass.BASS_StreamPutData(StreamPTR, Buffer, Buffer.Length);
            
        }*/
        public void WriteNextBuffer(int mode, byte[] Buffer)
        {
             int nextWritePosition = SBWritePosition + Buffer.Length;

                if (nextWritePosition > SecondBuffer.Length  )
                {
                    int ret2 = Bass.BASS_StreamPutData(StreamPTR, SecondBuffer, SecondBuffer.Length);

                    Bass.BASS_ChannelPlay(StreamPTR, false); // play clone
                    SBWritePosition = 0;
                }
                else
                {
                    for (int i = 0; i < Buffer.Length; i++)
                    {
                        SecondBuffer[i + SBWritePosition] =Buffer[i];
                        //  sound.Write(WritePosition, Buffer, LockFlag.None  );
                    }
                    SBWritePosition += Buffer.Length;
                }

        }
        private byte[] SecondBuffer;
        private int WholeBufferSize = 0;
        private int WriteBufferHalf = 0;
        public void SetupBuffer(double nSeconds, int numChannels, int BytesPerSample, AudioSampleType SampleType, long SamplesPerSecond)
        {
            short BlockAlign = (short)(numChannels * (BytesPerSample));
            _AverageBytesPerSecond = (int)(SamplesPerSecond * BlockAlign);
            _SamplesPerSecond =(int) SamplesPerSecond;
            _BytesPerSample = BytesPerSample;
            int length = (int)(nSeconds * _AverageBytesPerSecond);
            SetupBuffer(length, numChannels, BytesPerSample, SampleType, SamplesPerSecond);
        }
        int StreamPTR=0;
        public void SetupBuffer(int BufferSize, int numChannels, int BytesPerSample, AudioSampleType SampleType, long SamplesPerSecond)
        {
            bool initialized= Bass.BASS_Init(-1,(int)SamplesPerSecond, BASSInit.BASS_DEVICE_DEFAULT, OwnerForm );
            if (BytesPerSample == 2)
            {
                StreamPTR  = Bass.BASS_StreamCreatePush((int)SamplesPerSecond, numChannels
                    , BASSFlag.BASS_DEFAULT, IntPtr.Zero);
                BASSError bes = Bass.BASS_ErrorGetCode();
            }
            else
            {
                StreamPTR  = Bass.BASS_StreamCreatePush((int)SamplesPerSecond, numChannels
                    , BASSFlag.BASS_SAMPLE_8BITS , IntPtr.Zero);
            }
            
            short BlockAlign = (short)(numChannels * (BytesPerSample ));
            _AverageBytesPerSecond = (int)(SamplesPerSecond * BlockAlign);
            _SamplesPerSecond =(int) SamplesPerSecond;
            _BytesPerSample = BytesPerSample;

            WholeBufferSize = BufferSize ;
            SecondBuffer = new byte[BufferSize];
         
        }

        IntPtr OwnerForm;
        public SoundEngine3(Control OwnerForm)
        {
            this.OwnerForm = OwnerForm.Handle;
        }

        private DSPPROC _dupCallback;

        private void DupDSP(int handle, int channel, IntPtr buffer, int length, IntPtr user)
        {
            //Bass.BASS_StreamPutData(0, buffer, length);
           // System.Diagnostics.Debug.Print(length.ToString());
        }

        public void InitializeSound()
        {
            

            // create stream on device 1
            Bass.BASS_SetDevice(1);
          
        }

        ~SoundEngine3()
        {

        }
        public int BufferSize
        {
            get { return WholeBufferSize ; }
        }
        public void PreInitialBuffer(byte[] Buffer)
        {

            
            //int ret2 = Bass.BASS_StreamPutData(StreamPTR , Buffer,Buffer.Length );
           
            
           
        }
        
        public void Play()
        {

            playing = true;
            // set DSP to copy new data from source stream
            _dupCallback = new DSPPROC(DupDSP);
            int ret1 = Bass.BASS_ChannelSetDSP(StreamPTR, _dupCallback, new IntPtr(StreamPTR), 0);

           // Bass.BASS_ChannelPlay(StreamPTR , false); // play clone

            
           
        }

        public void Stop()
        {
            playing = false;
            Bass.BASS_Stop();
        }

        public void Pause()
        {
            playing = false;
            Bass.BASS_Pause();
        }
    }
}


