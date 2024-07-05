using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MovieEditor.TimeLine.Tools
{
    public interface  ITimeLineTool
    {
       
        void SetTimeLine(TimelineMaster TimeLine);
        void HandleTrackMouseMove(Track ActiveTrack, Panel TrackPanel, double Time, MouseEventArgs e);
        void HandleTrackMouseDown(Track ActiveTrack, Panel TrackPanel, double Time, MouseEventArgs e);
        void HandleTrackMouseUp(Track ActiveTrack, Panel TrackPanel, double Time, MouseEventArgs e);

        void HandleMouseMove(double Time, MouseEventArgs e);
        void HandleMouseDown(double Time, MouseEventArgs e);
        void HandleMouseUp(double Time, MouseEventArgs e);

        Cursor  MouseIcon();

    }
}
