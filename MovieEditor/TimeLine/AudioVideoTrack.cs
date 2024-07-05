using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MovieEditor.TimeLine
{
    public class AudioVideoTrack:Track 
    {
        CoreAV.VideoChunk videoChunk;
        CoreAV.AudioChunk audioChunk;

        public AudioVideoTrack(CoreAV.VideoChunk VC, CoreAV.AudioChunk AC) :base()
        {
            videoChunk = VC;
            audioChunk = AC;

        }
        public void SplitChunks(out CoreAV.VideoChunk VC, out CoreAV.AudioChunk AC)
        {
            VC = videoChunk;
            AC = audioChunk;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // AudioVideoTrack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "AudioVideoTrack";
            this.Size = new System.Drawing.Size(1110, 49);
            this.ResumeLayout(false);

        }
    }
}
