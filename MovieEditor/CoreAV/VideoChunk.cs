using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace MovieEditor.CoreAV
{
    public  class VideoChunk : Chunk 
    {
        AviSynthClip avs;

        public override Chunk Clone()
        {

            VideoChunk NewChunk = new VideoChunk(avs);

            NewChunk.ChunkName = ChunkName;
            NewChunk.RawFileProps=   _RawFileProps.Clone() ;
            NewChunk.ChunkProperties = _ClipProperties.Clone();
            NewChunk.LinkedTrack = LinkedTrack;
            NewChunk.Priority = mPriority;

            return NewChunk;

        }
        public override  double GetCutTime(double Time)
        {
            return Time;
        }

        public static  Bitmap ReadFrameBitmap(int position, int width, int height, AviSynthClip clip)
        {
            Bitmap bmp = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            try
            {
                // Lock the bitmap's bits.  
                Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                System.Drawing.Imaging.BitmapData bmpData =
                    bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                    bmp.PixelFormat);
                try
                {
                    // Get the address of the first line.
                    IntPtr ptr = bmpData.Scan0;
                    // Read data
                    clip.ReadFrame(ptr, bmpData.Stride, position);
                }
                finally
                {
                    // Unlock the bits.
                    bmp.UnlockBits(bmpData);
                }
                bmp.RotateFlip(RotateFlipType.Rotate180FlipX);
                return bmp;
            }
            catch (Exception)
            {
                bmp.Dispose();
                throw;
            }
        }
        public Bitmap GetFrame(int FrameNumber, double FrameTime, vidFrameIndex[] FrameList)
        {
            int InternalFrameNumber = GetFrameNumber(FrameTime);
            Bitmap outFrame = ReadFrameBitmap(InternalFrameNumber, avs.VideoWidth, avs.VideoHeight, avs);
            
            foreach (Filters.IFilter fs in _FilterList)
            {
                if (fs.ClipStartTime <= FrameTime && fs.ClipEndTime >= FrameTime)
                    fs.TransformFrame(outFrame, FrameTime, FrameList );
            }
            return outFrame;

        }
        public Bitmap GetFrame(int FrameNumber)
        {
            Bitmap outFrame=ReadFrameBitmap(FrameNumber, avs.VideoWidth, avs.VideoHeight, avs);
            double GlobalTime = (double)FrameNumber / (double)avs.FrameRate  + _ClipProperties.StartTime - _RawFileProps.StartOffsetTime;
            foreach (Filters.IFilter fs in _FilterList)
            {
                if (fs.ClipStartTime <= GlobalTime && fs.ClipEndTime >= GlobalTime)
                {
                   
                    fs.TransformFrame(outFrame, GlobalTime,null );
                }
            }
            return outFrame;
        }
        public Bitmap GetFrame(double GlobalTime)
        {
            return GetFrame(GetFrameNumber (GlobalTime ));
        }
        public int GetFrameNumber(double Seconds)
        {
            return (int)(GetInternalTime(Seconds) * (double)avs.FrameRate );
           
        }
        public VideoChunk(AviSynthClip AVIClip)
        {
            avs = AVIClip;
            _RawFileProps = new RawFileProperties("",0, (double)avs.num_frames / (double)avs.FrameRate );
            _ClipProperties = new ClipProperties(0, _RawFileProps.TotalDuration, 1);
            
        }
       /* public double TotalSeconds()
        {
            return ( (double)avs.num_frames / (double)avs.raten);
        }*/

        public int Width
        {
            get { return avs.VideoWidth; }
        }
        public int Height
        {
            get { return avs.VideoHeight; }
        }
    }
}
