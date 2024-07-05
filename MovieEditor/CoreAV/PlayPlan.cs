using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MovieEditor.CoreAV
{
    public class PlayPlan
    {
        vidFrameIndex[,] vidFrameAssign;
        audFrameIndex[,] audFrameAssign;
        private int _StartFrameOffset = 0;
        public int StartFrameOffset
        {
            get { return _StartFrameOffset; }
            set { _StartFrameOffset = value; }

        }
        double[] TimePerFrame;
        MainAVHandler MainAV;
        //SoundEngine _SoundEngine;

        public PlayPlan(MainAVHandler MainAV, vidFrameIndex[,] vidFrameAssign, audFrameIndex[,] audFrameAssign,double[] TimePerFrame)
        {
            
            this.MainAV = MainAV;
            this.vidFrameAssign = vidFrameAssign;
            this.audFrameAssign = audFrameAssign;
            this.TimePerFrame = TimePerFrame;
        }
      /*  public FrameInfo GetFrame(int FrameN)
        {
            FrameInfo fi = new FrameInfo();
            fi.AudTrackName = audFrameAssign[FrameN].ChunkName;
            fi.audFrameSec = audFrameAssign[FrameN].FrameSec;
            fi.VidTrackName = vidFrameAssign[FrameN].ChunkName;
            fi.vidFrameN = vidFrameAssign[FrameN].FrameN;
            fi.FrameDuration = TimePerFrame[FrameN];
            return fi;
        }
        public FrameInfo GetFrame(double Second)
        {
            double SumTime=0;
            for (int i = 0; i < TimePerFrame.Length; i++)
            {
                
                if (TimePerFrame[i] >= Second)
                    return GetFrame(i);
                SumTime += TimePerFrame[i];
            }
            return null;
        }*/


        public Bitmap GetFrame(int frameNumber, out double NextFrameDeltaT)
        {
            int FrameNumber = frameNumber - _StartFrameOffset;
            if (FrameNumber > 0)
            {
                NextFrameDeltaT = TimePerFrame[FrameNumber];
                Bitmap outFrame = null;
                vidFrameIndex[] frameList = new vidFrameIndex[vidFrameAssign.GetLength(0)];
                for (int kk = 0; kk < vidFrameAssign.GetLength(0); kk++)
                    frameList[kk] = vidFrameAssign[kk, FrameNumber];
                try
                {
                    if (vidFrameAssign[0, FrameNumber] != null)
                        outFrame = vidFrameAssign[0, FrameNumber].vidChunk.GetFrame(FrameNumber, vidFrameAssign[0, FrameNumber].FrameTime, frameList); //MainAV.GetFrame(vidFrameAssign[0,FrameNumber].ChunkName, vidFrameAssign[0,FrameNumber].FrameTime );
                    else
                        outFrame = MainAV.currentProject.BlankFrame;// new Bitmap(MainAV.currentProject.VideoWidth, MainAV.currentProject.VideoHeight);
                    
                    if (vidFrameAssign[0, FrameNumber] != null && vidFrameAssign[0, FrameNumber].FrameFilters != null)
                    {

                        foreach (Filters.IFilter f in vidFrameAssign[0, FrameNumber].FrameFilters)
                        {
                            outFrame = f.TransformFrame(outFrame, vidFrameAssign[0, FrameNumber].FrameTime, frameList);
                        }
                    }
                }
                catch (IndexOutOfRangeException iorEX)
                {
                    outFrame = MainAV.currentProject.BlankFrame;
                }

                
                return outFrame;
            }
            else
            {
                NextFrameDeltaT = TimePerFrame[0];
                return MainAV.currentProject.BlankFrame;
            }
        }
       
        public void PlayFrameAudio(int frameNumber, double NextFrameDeltaT)
        {
            int FrameNumber = frameNumber - _StartFrameOffset;
            if (FrameNumber > 0)
            {
                NextFrameDeltaT = TimePerFrame[FrameNumber];
                if (audFrameAssign[0, FrameNumber] != null)
                    MainAV.PlayFrameAudio(audFrameAssign[0, FrameNumber].ChunkName, audFrameAssign[0, FrameNumber].FrameSec, NextFrameDeltaT);
                else
                    MainAV.PlayFrameAudio(null, 0, NextFrameDeltaT);
            }
            else
                MainAV.PlayFrameAudio(null, 0, NextFrameDeltaT);
            
        }
        public byte[] GetFrameAudioBuffer(int frameNumber, double NextFrameDeltaT)
        {
            int FrameNumber = frameNumber - _StartFrameOffset;
            if (FrameNumber > 0)
            {
                if (audFrameAssign[0, FrameNumber] != null)
                    return MainAV.GetFrameAudio(audFrameAssign[0, FrameNumber].ChunkName, audFrameAssign[0, FrameNumber].FrameSec, NextFrameDeltaT);
                else
                    return MainAV.GetFrameAudio(null, 0, NextFrameDeltaT);
            }
            else
                return MainAV.GetFrameAudio(null, 0, NextFrameDeltaT);
        }
        public int FrameCount()
        {
            return vidFrameAssign.GetLength(1);
        }
        private ConvertProps TheFinalFormat=new ConvertProps();

        public ConvertProps FinalFormat
        {
            get { return TheFinalFormat; }
            set { TheFinalFormat = value; }
        }
       
    }
    
    public class vidFrameIndex
    {
        //public int FrameN;
        public double FrameTime;
        public string ChunkName;
        public VideoChunk vidChunk;
        public Filters.IFilter[] FrameFilters;
        public vidFrameIndex(double FrameTime, VideoChunk  Chunk,Filters.IFilter[] FrameFilters)
        {

            this.FrameTime = FrameTime;
            vidChunk = Chunk;
            this.ChunkName = Chunk.ChunkName ;
            this.FrameFilters = FrameFilters;
        }
        /*public vidFrameIndex(int FrameN, string ChunkName)
        {
            this.FrameN = FrameN;
            this.ChunkName = ChunkName;
        }*/
        
    }

    public class ConvertProps
    {
        public string DestinationFile = "";
       public string outFileExt = ".avi";
       public string VideoCodec = "mpeg4";
       public string AudioCodec = "";
       public string VideoBitrate = "360";
       public string VideoFrameRate = "29.97";
       public string VideoSize = "320x240";
       public string AudioBitrate = "48";
       public string AudioChannels = "2";
       public string AudioSamples = "22050";
       public string AudioVolume = "256";
       public string ForceFormat = "wav";
       public string VideoAspectRatio;
       public string TargetFormat;
       public bool Deinterlace = true;
       public bool SameQuality = true;

    }

    public class audFrameIndex
    {
        public double FrameSec;
        public string ChunkName;
        public Filters.IFilter[] FrameFilters;
        public AudioChunk audChunk;
        public audFrameIndex(double FrameSeconds, AudioChunk  Chunk, Filters.IFilter[] Filters)
        {
            this.FrameSec = FrameSeconds;
            this.ChunkName = Chunk.ChunkName ;
            this.FrameFilters = Filters;
            audChunk = Chunk;
        }
    }
}
