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

namespace MovieEditor.CoreAV
{
   
    /*public class SoundEngine
    {
        public void SaveWaveToFile(string Filename,ref  byte[] Buffer,short BitsPerSample, short numChannels, int SamplesPerSecond)
        {
            clsWaveProcessor wp = new clsWaveProcessor();
            wp.BitsPerSample =BitsPerSample ;
            wp.Channels=numChannels;
            wp.DataLength=Buffer.Length ;
            wp.SampleRate=SamplesPerSecond;
            wp.SaveWaveFile(Filename, ref Buffer);

           
        }

        public void SaveWaveToFile(Stream outStream, ref  byte[] Buffer, short BitsPerSample, short numChannels, int SamplesPerSecond)
        {
            MovieEditor.WaveWriter.WaveWriter ww=new MovieEditor.WaveWriter.WaveWriter(outStream,new MovieEditor.WaveWriter.WaveFormat(SamplesPerSecond,BitsPerSample,numChannels ));
            ww.Write(Buffer);
            ww.Close();

        }
        MovieEditor.WaveWriter.WaveWriter ww = null;
        public void CreateWaveForSave(Stream outStream,  short BitsPerSample, short numChannels, int SamplesPerSecond)
        {
            ww = new MovieEditor.WaveWriter.WaveWriter(outStream, new MovieEditor.WaveWriter.WaveFormat(SamplesPerSecond, BitsPerSample, numChannels));
            
            

        }
        public void WriteWaveChunktoSave(ref  byte[] Buffer)
        {
            ww.Write(Buffer);

        }
        public void CloseWaveFile()
        {
            ww.Close();
        }
        private long _SamplesPerSecond;
        private int _BytesPerSample = 1;
        private _AudioSampleType _AudioSampleType;
        private DevicesCollection devList;
        private Device devSound;
        private BufferDescription buffDescription;
        private SecondaryBuffer sound;
       
        private bool playing = false;
        private int len;
        private int WritePosition = 0;
        private int _AverageBytesPerSecond;

        public int SamplesPerSecond
        {
            get { return (int)_SamplesPerSecond; }

        }
        public int BytesPerSample
        {
            get { return _BytesPerSample; }
        }
        public int AverageBytesPerSecond
        {
            get { return _AverageBytesPerSecond; }
        }
        private List<string > AudioCards=new List<string>();

        private int SBWritePosition = 0;
        private Mutex SecondaryBufferMut = new Mutex();
        private int Try = 0;
        
        private void FillSecondaryBuffer()
        {
            
            if (sound.Status.BufferLost)
                sound.Restore();
            if (WriteBufferHalf == 0)
            {
                sound.Write(0,new MemoryStream( SecondBuffer),SecondBuffer.Length ,  LockFlag.None  );
                WriteBufferHalf = 1;
                SBWritePosition = 0;
                if (Try == 1)
                    System.Diagnostics.Debug.Print("hello");
                Try +=1;

            }
            else
            {
                
                //sound.Write((int)(WholeBufferSize / 2), SecondBuffer, LockFlag.None  );
                WriteBufferHalf = 0;
                SBWritePosition = 0;
            }

        }

        public void WriteNextBuffer(int mode, byte[] Buffer)
        {
            SecondaryBufferMut.WaitOne();
            if (mode == 1)
                FillSecondaryBuffer();
            else
            {
                int nextWritePosition = SBWritePosition + Buffer.Length;

                if (nextWritePosition > buffDescription.BufferBytes)
                {
                    /*   byte[] cBuffer=new byte[buffDescription.BufferBytes-WritePosition  ];
                       byte[] nBuffer=new byte[nextWritePosition-buffDescription.BufferBytes];
                       if (cBuffer.Length > 0)
                       {
                           for (int i = 0; i < cBuffer.Length; i++)
                               cBuffer[i] = Buffer[i];
                        //   sound.Write(WritePosition, cBuffer, LockFlag.None);
                       }
                       int readPos=cBuffer.Length ;
                       for (int i=0;i<nBuffer.Length;i++)
                           nBuffer[i]=Buffer[readPos+i];
                     //  sound.Write(0,nBuffer ,LockFlag.None  );
                     
                       SBWritePosition = nBuffer.Length;
                }
                else
                {
                    for (int i = 0; i < Buffer.Length; i++)
                    {
                        //SecondBuffer[i + SBWritePosition] =Buffer[i];
                        //  sound.Write(WritePosition, Buffer, LockFlag.None  );
                    }
                    SBWritePosition += Buffer.Length;
                }
            }
            SecondaryBufferMut.ReleaseMutex();
        }

        private byte[] SecondBuffer;
        private int WholeBufferSize = 0;
        private int WriteBufferHalf = 0;
        public void SetupBuffer(double nSeconds, int numChannels, int _BytesPerSample, _AudioSampleType SampleType, long _SamplesPerSecond)
        {
           
            this._BytesPerSample = _BytesPerSample;
            
            _AudioSampleType = SampleType;
            this._SamplesPerSecond = _SamplesPerSecond;
            
            
            
            buffDescription = new BufferDescription();
            WaveFormat wf=buffDescription.Format ;
            wf.SamplesPerSecond = (int)_SamplesPerSecond;
            wf.Channels =(short) numChannels ;
            wf.BitsPerSample =(short)( 8*_BytesPerSample );
            wf.BlockAlign = (short)(wf.Channels * (wf.BitsPerSample / 8));
            wf.AverageBytesPerSecond = wf.SamplesPerSecond * wf.BlockAlign;
            _AverageBytesPerSecond = wf.AverageBytesPerSecond;
            //wf.BlockAlign = (short)(_BytesPerSample * numChannels );
            //wf.AverageBytesPerSecond = (int)(_BytesPerSample * numChannels   * _SamplesPerSecond);
            wf.FormatTag = WaveFormatTag.Pcm;
            buffDescription.Format = wf;

            int length = (int)(nSeconds * _AverageBytesPerSecond );
            buffDescription.BufferBytes = length;
            WholeBufferSize = length;
            SecondBuffer = new byte[(int)length/2];
            sound = new SecondaryBuffer(buffDescription, devSound);
            
            len = sound.Caps.BufferBytes;
            //sound.Play(0, BufferPlayFlags.Looping );
        }
        public void SetupBuffer(int  BufferSize, int numChannels, int _BytesPerSample, _AudioSampleType SampleType, long _SamplesPerSecond)
        {

            this._BytesPerSample = _BytesPerSample;

            _AudioSampleType = SampleType;
            this._SamplesPerSecond = _SamplesPerSecond;



            buffDescription = new BufferDescription();
            WaveFormat wf = buffDescription.Format;
            wf.SamplesPerSecond = (int)_SamplesPerSecond;
            wf.Channels = (short)numChannels;
            wf.BitsPerSample = (short)(8 * _BytesPerSample);
            wf.BlockAlign = (short)(wf.Channels * (wf.BitsPerSample / 8));
            wf.AverageBytesPerSecond = wf.SamplesPerSecond * wf.BlockAlign;
            _AverageBytesPerSecond = wf.AverageBytesPerSecond;
            //wf.BlockAlign = (short)(_BytesPerSample * numChannels );
            //wf.AverageBytesPerSecond = (int)(_BytesPerSample * numChannels   * _SamplesPerSecond);
            wf.FormatTag = WaveFormatTag.Pcm;
            buffDescription.Format = wf;

            int length = BufferSize ;
            buffDescription.BufferBytes = length;
            WholeBufferSize = length;
            SecondBuffer = new byte[(int)length/2];
            sound = new SecondaryBuffer(buffDescription, devSound);

            len = sound.Caps.BufferBytes;
            //sound.Play(0, BufferPlayFlags.Looping );
        }
               
        IntPtr OwnerForm;
        public SoundEngine(Control  OwnerForm)
        {
            this.OwnerForm = OwnerForm.Handle ;
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

            // Create Device
            devSound = new Device(devList[0].DriverGuid);
            devSound.SetCooperativeLevel(OwnerForm, CooperativeLevel.Priority);
        }

        ~SoundEngine()
        {
           
        }
        public int BufferSize
        {
            get { return sound.Caps.BufferBytes; }
        }
        public void PreInitialBuffer(byte[] Buffer)
        {
            for (int i = 0; i < SecondBuffer.Length; i++)
                SecondBuffer[i] = Buffer[i];
            sound.Write(0, Buffer, LockFlag.EntireBuffer);
        }
        Thread BufferWatcher=null;
        public void Play()
        {
            
            playing = true;
            
            sound.Play(0, BufferPlayFlags.Looping );
            BufferWatcher = new Thread(delegate()
                {

                    int CurPlayPos = 0;
                    int CurWritePos = 0;
                    while (playing == true)
                    {
                        if (sound.Status.BufferLost)
                            sound.Restore();

                        sound.GetCurrentPosition(out CurPlayPos, out CurWritePos);
                        System.Diagnostics.Debug.Print(CurPlayPos.ToString());
                        if (WriteBufferHalf == 0)
                        {
                            if (CurPlayPos > (int)(WholeBufferSize / 1.8))
                            {
                                WriteNextBuffer(1, null);
                                //FillSecondaryBuffer();
                            }

                        }
                        else
                        {
                            if (CurPlayPos <(int)(WholeBufferSize /2.2))
                            {
                                WriteNextBuffer(1, null); 
                                //FillSecondaryBuffer();
                            }
                        }
                        Thread.Sleep(50);
                    }
                }
            );
            BufferWatcher.Start();
        }

        public void Stop()
        {
            playing = false;
            sound.Stop();
            sound.SetCurrentPosition(0);
        }

        public void Pause()
        {
            playing = false;
            sound.Stop();
        }
    }*/
}

    
