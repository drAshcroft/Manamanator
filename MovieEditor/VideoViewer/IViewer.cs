using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using MovieEditor.CoreAV;

namespace MovieEditor.VideoViewer
{
    public delegate void PlayMovieRequest(IViewer sender, PlayPlan PlayPlan );
    public delegate void StopMovieRequest(IViewer sender);
    public delegate void MoveFrameRequest(IViewer sender,int newFrame, bool Quietly);
    public interface IViewer
    {
        void SetAVCore(CoreAV.MainAVHandler MainAV);
        void ShowFrame(Bitmap frame, double Second, int FrameN,int TotalFrames);
        event PlayMovieRequest PlayMovie;
        event MoveFrameRequest MoveFrame;
        //need to add interface to allow playing to be run by the viewer.  PlayPlan should be requested and then
        //the frames should be served from this same with audio
    }
}
