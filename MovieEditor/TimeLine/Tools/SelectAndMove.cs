using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace MovieEditor.TimeLine.Tools
{
    public class SelectAndMove:ITimeLineTool 
    {

        private Cursor _MouseIcon = null;
        public Cursor MouseIcon()
        {
            return _MouseIcon;

        }

        private TimelineMaster _TimeLine = null;


        public void SetTimeLine(TimelineMaster TimeLine)
        {
            _TimeLine = TimeLine;
        }
        public SelectAndMove()
        {
            _MouseIcon = CursorHandling.LoadDifficultCursor(typeof(Tools.SelectAndMove ), "SelectAndMoveTool.cur");
            
        }
        
        private Point TrackStartOffset;
        private Point OriginalPoint;
        private Track EditTrack = null;

        public void HandleTrackMouseMove(Track ActiveTrack, Panel TrackPanel, double Time, MouseEventArgs e)
        {
            if (EditTrack != null)
            {
                int x = e.X + EditTrack.Left - TrackStartOffset.X;
                int y = e.Y + EditTrack.Top - TrackStartOffset.Y;
                
                
                EditTrack.Left = OriginalPoint.X +x ;
                EditTrack.Top = OriginalPoint.Y + y;

                EditTrack.SetStartTime(_TimeLine.PixelToTime(OriginalPoint.X + x));

                if (EditTrack.LinkedTrack != null)
                {
                    EditTrack.LinkedTrack.Left = EditTrack.Left;
                    EditTrack.LinkedTrack.SetStartTime(_TimeLine.PixelToTime(OriginalPoint.X + x));
                    EditTrack.LinkedTrack.ResetFilterControls();
                }

                if (EditTrack.Width + EditTrack.Left > TrackPanel.Width)
                    TrackPanel.Width = (int)(1.5 * (EditTrack.Width + EditTrack.Left));

                Point ScreenPoint= EditTrack.PointToScreen(new Point(e.X, e.Y)) ;
                //Point TrackPoint = TrackPanel.PointToClient ( ScreenPoint  );
                x = _TimeLine.panel2.PointToClient(ScreenPoint).X;//_TimeLine.PointToClient(ScreenPoint  ).X;
                if (x + 15> _TimeLine.panel2 .Width)
                {
                    _TimeLine.ScrollBarPosition += 1;
                   // TrackPanel.Left = (int)((double)_TimeLine.ScrollBarPosition / -100 * (double)TrackPanel.Width);
                    System.Threading.Thread.Sleep(100);
                }
                if (x < 0 && _TimeLine.ScrollBarPosition > 0)
                {

                    _TimeLine.ScrollBarPosition -= 1;
                   // TrackPanel.Left = (int)((double)_TimeLine.ScrollBarPosition / -100 * (double)TrackPanel.Width);
                    System.Threading.Thread.Sleep(100);
                }
                EditTrack.ResetFilterControls();

                //this.ScrollControlIntoView(EditTrack);
                _TimeLine.RefreshFrameViewer();
            }
        }
        public void HandleTrackMouseDown(Track ActiveTrack, Panel TrackPanel, double Time, MouseEventArgs e)
        {
            this.EditTrack = ActiveTrack;
            OriginalPoint = new Point(ActiveTrack.Left, ActiveTrack.Top);
            TrackStartOffset = new Point(e.X + ActiveTrack.Left, e.Y + ActiveTrack.Top);
            _TimeLine.ClearTrackHighLights();
            _TimeLine.ActiveTrack = ActiveTrack;
            ActiveTrack.Highlight(true);
            if (ActiveTrack.LinkedTrack != null)
            {
                ActiveTrack.LinkedTrack.Highlight(true);
            }
        }
        public void HandleTrackMouseUp(Track ActiveTrack, Panel TrackPanel, double Time, MouseEventArgs e)
        {
            EditTrack = null;

            _TimeLine.SetPriorities();
           
        }

        public void HandleMouseMove(double Time, MouseEventArgs e)
        {
        }
        public void HandleMouseDown(double Time, MouseEventArgs e)
        { }

        public void HandleMouseUp(double Time, MouseEventArgs e)
        { }


    }
}
