using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MovieEditor.CoreAV
{
    public interface ITimeLine
    {
        void ShowSelectTimeIndicator(double Showtime);
        void MoveSelectTimeIndicator(double Time);
        void MoveTimeIndicator(double Time);
        void MoveTimeIndicatorQuietly(double Time);
        void HideSelectTimeIndicator();

        ITrack AddTrack(CoreAV.Chunk NewChunk);
        ITrack[] AddAudioVideoTrack(CoreAV.VideoChunk VC, CoreAV.AudioChunk AC);
        void ClearEveryThing();
        void StartFilterTool();
    }
}
