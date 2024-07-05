using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.IO;


namespace MovieEditor.CoreAV
{
    public class RawVideoFile
    {
        AviSynthClip avs=null;
        string _Filename="";
        IntPtr OwnerHandle=IntPtr.Zero ;
        
        public bool HasVideo
        {
            get {
                if (avs != null)
                    return avs.HasVideo;
                else if (MyStream != null)
                    return false;
                return false;
            }
        }
        MovieEditor.WaveWriter.WaveStream MyStream=null;
        private bool _HasAudio = false;
        public bool HasAudio
        {
            get { return _HasAudio ; }
        }
        private bool _HasClip = false; 
        public bool HasClip
        {
            get { return _HasClip ; }
        }
        private MovieEditor.WaveWriter.WaveStream ConvertTo1Channel(MovieEditor.WaveWriter.WaveStream OriginalStream)
        {
            string TempFileName = Generals.TempFilename() + ".wav";
            FileStream fs = new FileStream(TempFileName, FileMode.CreateNew);
            WaveWriter.WaveFormat wf = 
                new MovieEditor.WaveWriter.WaveFormat(OriginalStream.Format.nSamplesPerSec , OriginalStream.Format.wBitsPerSample /OriginalStream.Format.nChannels ,1);
            WaveWriter.WaveWriter ww =
                new MovieEditor.WaveWriter.WaveWriter(fs, wf);
            byte[]Inbuffer=new byte[OriginalStream.Length ];
            byte[] OutBuffer=new byte[OriginalStream.Length/OriginalStream.Format.nChannels ];

            OriginalStream.Read(Inbuffer,0,(int)OriginalStream.Length );
            for (int i=0;i<OutBuffer.Length/2-1 ;i++)
            {
                OutBuffer[i] = Inbuffer[i * OriginalStream.Format.nChannels*2];
                OutBuffer[i+1] = Inbuffer[i * OriginalStream.Format.nChannels*2+1];
            }
            ww.Write(OutBuffer);

            ww.Close();
            fs.Close();
            return  new MovieEditor.WaveWriter.WaveStream(TempFileName);
        }
        private bool  LoadWavFile(string Filename)
        {
            try
            {
                MyStream = new MovieEditor.WaveWriter.WaveStream(Filename);
                if (MyStream.Format.nChannels > 1)
                {

                    // MyStream = ConvertTo1Channel(MyStream);
                }
                
                return true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("There was an error loading this file: " + Filename +
                       " Please check to make sure that the file is still in the correct place and then check if the correct codex is loaded on your system.  This can usually be done by playing the file in Windows Media Player. \n\n\n" + ex.Message);

            }
            return false;
        }
        private bool LoadAVI(string Filename, AviSynthScriptEnvironment ASSE)
        {
            //todo: it would be better to convert everything to 5.1 channels and the highest 
            //bit resolution.  Maybe do that later.
            string script =
                "source= AviSource(\"" + Filename + "\") \n " +
                "ResampleAudio(source, 48000) \n" +
                "ConvertAudioTo16bit(source) \n" +
                "ConvertToMono(source) \n" +
                "ConvertToRGB32(Source) " +
                "";
            try
            {
                avs = ASSE.ParseScript(script);
                //  System.Diagnostics.Debug.Print(((double)avs.raten / (double)avs.rated).ToString());
            }
            catch (Exception ex)
            {
                string script2 = /*"DirectShowSource(\"" + Filename + "\") \n ";*/
                "source = DirectShowSource(\"" + Filename + "\") \n " +
                     "ResampleAudio(source, 48000) \n" +
                     "ConvertAudioTo16bit(source) \n" +
                     "ConvertToMono(source) \n" +
                     "ConvertToRGB32(Source) " +
               "";
                try
                {
                    avs = ASSE.ParseScript(script2);
                }
                catch
                {
                    System.Windows.Forms.MessageBox.Show("There was an error loading this file: " + Filename +
                        " Please check to make sure that the file is still in the correct place and then check if the correct codex is loaded on your system.  This can usually be done by playing the file in Windows Media Player. \n\n\n" + ex.Message);
                    return false;
                }
            }
            return (avs !=null);
        }
        public RawVideoFile(string Filename, AviSynthScriptEnvironment ASSE, IntPtr MainOwnerHandle)
        {

            _Filename=Filename ;


            if (Path.GetExtension(Filename).ToLower() == ".wav")
            {
                
                _HasClip = LoadWavFile(Filename );
                _HasAudio = _HasClip;
            }
            else if (Path.GetExtension(Filename).Trim() == "")
            {
                _HasClip= LoadAVI(Filename + ".avi", ASSE);
                _HasAudio= LoadWavFile(Filename + ".wav");
            }
            else 
            {

                _HasClip = LoadAVI(Filename, ASSE);
                _HasAudio = (avs.ChannelsCount != 0);
            }
            OwnerHandle = MainOwnerHandle;
            //AudioChunk ac = GetAudioChunk();
        }
        #region VideoProperties
        public int NumChannels
        {
            get {
                if (MyStream == null)
                    return avs.ChannelsCount;
                else
                    return MyStream.Format.nChannels;
            
            }
        }
        public AudioSampleType AudioSampleType
        {
            get {
                if (MyStream == null)
                    return avs.SampleType;
                else
                    return AudioSampleType.INT16;
            }
        }
        public int SamplesPerSecond
        {
            get {
                if (MyStream == null)
                    return avs.AudioSampleRate;
                else
                    return MyStream.Format.nSamplesPerSec;
            }
        }
        public int BytesPerSample
        {
            get {
                if (MyStream == null)
                    return avs.BytesPerSample ;
                else
                    return MyStream.Format.wBitsPerSample /8;
            }

        }
        public int VideoWidth
        {
            get
            {
                return avs.VideoWidth;
            }
        }
        public int VideoHeight
        {
            get { return avs.VideoHeight; }
        }
        public double FrameRate
        {
            get
            {
                return (double)avs.FrameRate ;
            }
        }
        public Bitmap FileIcon
        {
            get {
                if (HasClip == true)
                {
                   Bitmap v= VideoChunk.ReadFrameBitmap((int)(avs.num_frames *.05),VideoWidth,VideoHeight,avs);
                   Graphics g =Graphics.FromImage(v);
                   Bitmap svm=new Bitmap(16,16);
                   g.DrawImage(svm,0,0,16,16);
                   return svm;
                }
                else
                    return (new Bitmap(16, 16));
                }

        }
        public string Filename
        {
            get {return _Filename;}
        }
        #endregion
        public VideoChunk GetVideoChunk()
        {
            if (avs !=null && HasVideo==true)
            {
                VideoChunk VC= new VideoChunk(avs);
                VC.RawFileProps.Filename  = _Filename;
                return VC;
            }
            else
                return null;

        }
        public AudioChunk GetAudioChunk(double StartTime, double Duration)
        {
            AudioChunk AuS = new AudioChunk( );
            AuS.RawFileProps.Filename = _Filename;
            
           

            if (  MyStream!=null)
            {

                              //avs.ReadAudio(address, 0,(int) avs.SamplesCount);
                AuS.SetBuffer(MyStream);/*,MyStream.Format.nChannels ,
                    MyStream.Format.wBitsPerSample,
                    MyStream.Format.wFormatTag ,
                    MyStream.Format.nSamplesPerSec );*/
                
            }
            else
            {
                long EndCount = (long)((StartTime + Duration) * (double)avs.AudioSampleRate * (double)avs.BytesPerSample);
                if (EndCount > avs.AudioSizeInBytes)
                {
                    Duration = avs.AudioSampleRate * avs.SamplesCount - StartTime;
                    if (Duration < 0)
                        Duration = 0;
                }


                long buffersize = (long)(Duration * (double)avs.AudioSampleRate * (double)avs.BytesPerSample);
                if (buffersize > avs.AudioSizeInBytes)
                    buffersize = avs.AudioSizeInBytes;

                long BytesPerSamp = avs.BytesPerSample;
                double SecPerSamp = 1 / (double)avs.AudioSampleRate;

                byte[] frameBuffer = new byte[buffersize];
                GCHandle h = GCHandle.Alloc(frameBuffer, GCHandleType.Pinned);
                IntPtr address = h.AddrOfPinnedObject();
                int Offset = (int)(StartTime / SecPerSamp);
                int Count = (int)(Duration / SecPerSamp);
                avs.ReadAudio(address, Offset, Count);
                h.Free();

                string TempFileName = Generals.TempFilename() + ".wav";
                FileStream fs = new FileStream(TempFileName, FileMode.CreateNew);
                WaveWriter.WaveFormat wf = new MovieEditor.WaveWriter.WaveFormat((int)SamplesPerSecond, 8 * BytesPerSample, avs.ChannelsCount);
                WaveWriter.WaveWriter ww = new MovieEditor.WaveWriter.WaveWriter(fs, wf);
              
                ww.Write(frameBuffer);
              
                ww.Close();
                fs.Close();



                //avs.ReadAudio(address, 0,(int) avs.SamplesCount);
                AuS.SetBuffer(TempFileName);//, avs.ChannelsCount, avs.BytesPerSample, avs.SampleType, avs.AudioSampleRate);
                
            }
            return  AuS;
        }
       
        public AudioChunk GetAudioChunk()
        {
            if (MyStream !=null)
                return GetAudioChunk(0, MyStream.Length / MyStream.Format.nSamplesPerSec );
            else if (avs != null )
                return GetAudioChunk(0, (double)avs.SamplesCount / (double)avs.AudioSampleRate);
            else
                return null;
        }

        
        
    }
}
