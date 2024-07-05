using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.DirectX.DirectSound;
using System.Threading;
using System.IO;
using Un4seen;

namespace MovieEditor.CoreAV
{
    public class SoundEngine
    {


        SoundEngines.StreamingPlayer Streamer;
        private long SamplesPerSecond;
        private int NumChannels;
        private int BytesPerSample = 1;
        private AudioSampleType AudioSampleType;
        private DevicesCollection devList;
        private Device devSound;
        //private BufferDescription buffDescription;
       // private SecondaryBuffer sound;
        private bool playing = false;
        private int len;
        private int WritePosition = 0;

        private byte[] WriteBuffer1;
        private byte[] WriteBuffer2;
        private int CurrentWriteBuffer;

        private List<string > AudioCards=new List<string>();      

        public void WriteNextBuffer(byte[] Buffer)
        {
            if (CurrentWriteBuffer == 0)
            {
                for (int i=0;i<Buffer.Length ;i++)
                    WriteBuffer2[i] = Buffer[i];
                CurrentWriteBuffer = 1;
            }
            else
            {
                for (int i = 0; i < Buffer.Length; i++)
                    WriteBuffer1[i] = Buffer[i];
                CurrentWriteBuffer = 0;
            }


        }

        public void SetupBuffer(double nSeconds, int numChannels, int BytesPerSample, AudioSampleType SampleType, long SamplesPerSecond)
        {
           
            this.BytesPerSample = BytesPerSample;
            this.AudioSampleType = SampleType;
            this.SamplesPerSecond = SamplesPerSecond;
            this.NumChannels = numChannels;



            int length =(int)( nSeconds*SamplesPerSecond );
            WriteBuffer1 = new byte[length];
            WriteBuffer2 = new byte[length];
            BufferDescription buffDescription = new BufferDescription();
            WaveFormat wf=buffDescription.Format ;
            wf.SamplesPerSecond = (int)SamplesPerSecond;
            wf.Channels =(short) numChannels ;
            wf.BitsPerSample =(short)( 8*BytesPerSample );
            wf.BlockAlign = (short)(BytesPerSample * numChannels );
            wf.AverageBytesPerSecond = (int)(BytesPerSample * numChannels   * SamplesPerSecond);
            wf.FormatTag = WaveFormatTag.Pcm;
            
            Streamer = new MovieEditor.SoundEngines.StreamingPlayer(OwnerForm, new Device(devList[0].DriverGuid), wf,length );

            
        }

        private void WriteFrameAudio(IntPtr dest, int size)
        {
            if (CurrentWriteBuffer ==0)
            {
                System.Runtime.InteropServices.Marshal.Copy(WriteBuffer1 , 0, dest, WriteBuffer1.Length );
            }
            else 
            {
                System.Runtime.InteropServices.Marshal.Copy(WriteBuffer2  , 0, dest, WriteBuffer2.Length  );
            }
        }
       
        Control OwnerForm;
        public SoundEngine2(Control OwnerForm)
        {
            this.OwnerForm = OwnerForm;
        }

        public void InitializeSound()
        {
            // List all Audio Cards
            devList = new DevicesCollection();
            // Populate cmbAudioCards
            AudioCards.Clear();// .Items.Clear();
            for (int i = 0; i < devList.Count; i++)
            {
                AudioCards.Add(devList[i].Description);
            }

            
        }

        ~SoundEngine2()
        {
           
        }

       
        

        public void Play()
        {
            
            playing = true;
            Streamer.Play(new SoundEngines.PullAudioCallback(WriteFrameAudio ));
            //sound.Play(0, BufferPlayFlags.Looping );
           
        }

        public void Stop()
        {
            playing = false;
            Streamer.Stop();
        }

        public void Pause()
        {
            playing = false;
            Streamer.Stop();
        }
    }
}

    
