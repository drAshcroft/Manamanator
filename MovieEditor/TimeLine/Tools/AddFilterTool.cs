using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace MovieEditor.TimeLine.Tools
{
    public class AddFilterTool:ITimeLineTool 
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
            
            ActiveFilter = _TimeLine.MainAVHandler.CurrentActiveFilter.Clone();
            if (ActiveFilter == null) 
                MessageBox.Show("Please Select a filter from the filter toolkit", "Filter Error");

        }
        public AddFilterTool()
        {
          
            _MouseIcon = CursorHandling.LoadDifficultCursor(typeof(Tools.AddFilterTool ), "ApplyFilterCursor.cur");
            
        }
        
        private Point TrackStartOffset;
        private Point OriginalPoint;
        private Track EditTrack = null;
        private Filters.IFilter ActiveFilter;


        public void HandleTrackMouseDown(Track ActiveTrack, Panel TrackPanel, double Time, MouseEventArgs e)
        {
            this.EditTrack = ActiveTrack;
            OriginalPoint = new Point(ActiveTrack.Left, ActiveTrack.Top);
            TrackStartOffset = new Point(e.X + ActiveTrack.Left, e.Y + ActiveTrack.Top);
            _TimeLine.ClearTrackHighLights();
            _TimeLine.ActiveTrack = ActiveTrack;
            ActiveTrack.Highlight(true);
            ActiveTrack.AddFilter(ActiveFilter);
          //  _TimeLine.MainAVHandler.AddFilter(ActiveFilter);
            _TimeLine.ShowFilterGui(ActiveFilter);
            double sTime = _TimeLine.PixelToTime(OriginalPoint.X);
            double cTime = _TimeLine.PixelToTime(OriginalPoint.X +e.X );
            double eTime = _TimeLine.PixelToTime(ActiveTrack.Right);

            if (ActiveFilter.FilterType == Filters.eFilterType.Start)
            {
                ActiveFilter.ClipStartTime = sTime ;
                ActiveFilter.ClipEndTime = cTime ;
            }
            else if (ActiveFilter.FilterType == Filters.eFilterType.End)
            {
                ActiveFilter.ClipEndTime = eTime ;
                ActiveFilter.ClipStartTime = cTime ;
            }
            else
            {
                ActiveFilter.ClipStartTime = cTime ;
                ActiveFilter.ClipEndTime = cTime ;
            }
            ActiveTrack.ResetFilterControls();
        }

        public void HandleTrackMouseMove(Track ActiveTrack, Panel TrackPanel, double Time, MouseEventArgs e)
        {
            if (EditTrack != null)
            {
                int x = e.X;// +EditTrack.Left - TrackStartOffset.X;
                int y = e.Y;// +EditTrack.Top - TrackStartOffset.Y;

                double cTime = _TimeLine.PixelToTime(OriginalPoint.X + x);// -ActiveTrack.BaseChunk.ChunkProperties.StartTime;

                if (ActiveFilter.FilterType == Filters.eFilterType.Start)
                {
                    if (cTime > ActiveFilter.ClipStartTime)
                        ActiveFilter.ClipEndTime = cTime;
                    else
                        ActiveFilter.ClipEndTime = 0;
                }
                else if (ActiveFilter.FilterType == Filters.eFilterType.End)
                {
                    if (cTime < ActiveFilter.ClipEndTime )
                        ActiveFilter.ClipStartTime = cTime;
                    else
                        ActiveFilter.ClipStartTime = ActiveFilter.ClipEndTime ;
                }
                else
                {
                    
                    ActiveFilter.ClipEndTime = cTime ;
                }
                ActiveTrack.ResetFilterControls();
                //EditTrack.SetStartTime();

               
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
