using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MovieEditor.CoreAV
{
    public interface ITrack
    {
        void ResetChunkProps();
        int Top { get; set; }
        int Height { get; set; }
        int Left { get; set; }
        int Right { get; }
        ITrack LinkedTrack {  set; get; }
        Chunk BaseChunk    {      get;  }
        void SetStartTime(double Second);
        void ResetFilterControls();
        void Highlight(bool HighlightOn);
    }
}
