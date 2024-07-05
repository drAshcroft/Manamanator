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
using OpenALDotNet;
using OpenALDotNet.Streams;
using AdvanceMath;

namespace MovieEditor.SoundEngines
{
    public class SoundEngine
    {
        #region SaveWaves
        public void SaveWaveToFile(string Filename, ref  byte[] Buffer, short BitsPerSample, short numChannels, int SamplesPerSecond)
        {
            clsWaveProcessor wp = new clsWaveProcessor();
            wp.BitsPerSample = BitsPerSample;
            wp.Channels = numChannels;
            wp.DataLength = Buffer.Length;
            wp.SampleRate = SamplesPerSecond;
            wp.SaveWaveFile(Filename, ref Buffer);


        }

        public void SaveWaveToFile(Stream outStream, ref  byte[] Buffer, short BitsPerSample, short numChannels, int SamplesPerSecond)
        {
            MovieEditor.WaveWriter.WaveWriter ww = new MovieEditor.WaveWriter.WaveWriter(outStream, new MovieEditor.WaveWriter.WaveFormat(SamplesPerSecond, BitsPerSample, numChannels));
            ww.Write(Buffer);
            ww.Close();

        }
        MovieEditor.WaveWriter.WaveWriter ww = null;
        public void CreateWaveForSave(Stream outStream, short BitsPerSample, short numChannels, int SamplesPerSecond)
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
        #endregion

        private long _SamplesPerSecond;
        private int _BytesPerSample = 0;
        private AudioSampleType _AudioSampleType;
        private bool MuteSound = false ;
        
        private int _AverageBytesPerSecond;

        public int SamplesPerSecond
        {
            get { 
                
                return (int)_SamplesPerSecond; }

        }
        public int BytesPerSample
        {
            get { return _BytesPerSample; }
        }
        public int AverageBytesPerSecond
        {
            get { return _AverageBytesPerSecond; }
        }
       
        
        AudioSource source;
        AudioStreamPlayer player;
        AudioMemoryStream  audioStream;

        public void WriteNextBuffer(int mode, byte[] Buffer)
        {
            audioStream.Write(Buffer, 0, Buffer.Length);
            //System.Diagnostics.Debug.Print("Writing " + Buffer.Length.ToString() + " Bytes to audio buffer");
        }
        public void ClearAudio()
        {
          
            audioStream.Flush();
        }
      
        public void SetupBuffer(int BufferSize, int numChannels, int BytesPerSample, AudioSampleType SampleType, long SamplesPerSecond)
        {

            _BytesPerSample = BytesPerSample;

            _AudioSampleType = SampleType;
            _SamplesPerSecond = SamplesPerSecond;

            short  BlockAlign = (short)(numChannels * BytesPerSample );
            _AverageBytesPerSecond = (int)(SamplesPerSecond * BlockAlign);

            audioStream = new AudioMemoryStream(BytesPerSample, numChannels,(int) SamplesPerSecond);
            audioStream.PrimaryBufferSize = BufferSize;
            audioStream.BufferLength = 1024;
            //AudioStreamPlayer player = new AudioStreamPlayer(audioStream, source, 1024, 64);
           // player = new AudioStreamPlayer(audioStream, source, 1024, 64);
        }
        public void SetupBuffer(double BufferTime, int numChannels, int BytesPerSample, AudioSampleType SampleType, long SamplesPerSecond)
        {

            _BytesPerSample = BytesPerSample;

            _AudioSampleType = SampleType;
            _SamplesPerSecond = SamplesPerSecond;
            short BlockAlign = (short)(numChannels * BytesPerSample);
            _AverageBytesPerSecond = (int)(SamplesPerSecond * BlockAlign);
            if (audioStream != null) audioStream.Flush();
            audioStream = new AudioMemoryStream(BytesPerSample, numChannels, (int)SamplesPerSecond);
            audioStream.PrimaryBufferSize = ((int)(_SamplesPerSecond * BufferTime)) * _BytesPerSample;
            audioStream.BufferLength = 1024;
           // player = new AudioStreamPlayer(audioStream, source, 1024, 64);
        }

        IntPtr OwnerForm;
        public SoundEngine(Control OwnerForm)
        {
            this.OwnerForm = OwnerForm.Handle;
        }

        public void InitializeSound()
        {
           if (MuteSound!=true  )
            {
                try
                {
                    OpenAudioLibrary.AlutInit();

                    AudioListener.Position = new Vector3D(0, 0, 0);
                    AudioListener.Velocity = new Vector3D(0, 0, 0);
                    AudioListener.Orientation = new OpenALDotNet.Orientation(new Vector3D(0, 0, -1), new Vector3D(0, 1, 0));

                    source = new AudioSource();

                    source.IsRelative = true;
                    source.Position = new Vector3D(1, 0, 0);
                    source.Velocity = new Vector3D(0, 0, 0);
                    source.Gain = .5f;
                }
                catch {
                //alut library is dead now,  just eat the error
                }
            }
        }

        ~SoundEngine()
        {

        }
        
        public void PreInitialBuffer(byte[] Buffer)
        {
            audioStream.Flush();
            audioStream.Write(Buffer, 0, Buffer.Length);
        }
        Thread MakePlay=null;
        private bool KillSound = false;
        public void Play()
        {
            
            if (MuteSound != true)
            {

                player = new AudioStreamPlayer(audioStream, source, 1024, 64);
                player.Play();
                MakePlay = new Thread(delegate()
                    {
                        KillSound = false;
                        int SleepTime = (int)(500 * (double)(1024 * 64) / (double)_AverageBytesPerSecond);
                        while (!KillSound )
                        {
                            Thread.Sleep(SleepTime);
                            player.Update();
                        }
                        System.Diagnostics.Debug.Print("Killing Sound Engine");

                    }
                );
                MakePlay.Start();
            }
        }

        public void Stop()
        {
            if (player != null)
            {

                player.StopAndReset();
               
            }
            KillSound = true;
            if (MakePlay != null) MakePlay.Abort();
            audioStream.Flush();
            
        }

        public void Pause()
        {
            if (MakePlay != null) MakePlay.Abort();
            player.Pause();
        }
    }
}
