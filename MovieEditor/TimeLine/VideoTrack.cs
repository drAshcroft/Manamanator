using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace MovieEditor.TimeLine
{
    public class VideoTrack:Track 
    {
        CoreAV.VideoChunk videoChunk;
        public VideoTrack(CoreAV.VideoChunk VC,double SecondsPerPixel)
            : base()
        {
            this.SecondsPerPixel = SecondsPerPixel;
            
            videoChunk = VC;

            _highLightColor = Color.Red;
        }
        public override  CoreAV.Chunk BaseChunk
        {
            get { return (CoreAV.Chunk)videoChunk ; }
        }

        public CoreAV.VideoChunk VideoChunk
        {
            get { return videoChunk; }
        }

        public override void ResetChunkProps()
        {
            base.ResetChunkProps();
            DrawVis();
        }
        public override   void DrawVis()
        {
            double FrameWidth = (double)videoChunk.Width / (double)videoChunk.Height * (double)this.Height;
            int nImages = (int)((double)this.Width / FrameWidth);
            Bitmap[] bs = new Bitmap[nImages];
            for (int i = 0; i < nImages; i++)
            {
                double sec = videoChunk.ChunkProperties.Duration   / nImages * i;
                Bitmap b = videoChunk.GetFrame(sec);
                Bitmap icon = new Bitmap((int)FrameWidth, this.Height);
                Graphics g = Graphics.FromImage(icon);
                g.DrawImage(b, new Rectangle(0, 0, (int)FrameWidth, this.Height));
                bs[i] = icon;
            }
            Bitmap big = new Bitmap(Vis.Width, Vis.Height, Vis.CreateGraphics());
            Graphics gBig = Graphics.FromImage(big);
            for (int i = 0; i < nImages; i++)
            {
                gBig.DrawImage(bs[i], new Point((int)(FrameWidth * i), 0));
            }
            _BackGround = big;
            Vis.Image = big;
            Vis.Refresh();
        }

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this.Vis)).BeginInit();
            this.SuspendLayout();
            // 
            // Vis
            // 
            this.Vis.Size = new System.Drawing.Size(665, 39);
            
            // 
            // VideoTrack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "VideoTrack";
            
            ((System.ComponentModel.ISupportInitialize)(this.Vis)).EndInit();
            this.ResumeLayout(false);

        }

        public override void  SetPriority(int Priority)
        {
            videoChunk.Priority = Priority;
        }
        
        public override void SetStartTime(double GlobalStartTime)
        {
            videoChunk.ChunkProperties.StartTime  = GlobalStartTime ;
            

        }
    }
}
