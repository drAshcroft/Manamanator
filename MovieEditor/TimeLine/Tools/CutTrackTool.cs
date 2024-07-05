using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Resources;
using System.Reflection;


namespace MovieEditor.TimeLine.Tools
{
    public class CutTrackTool:ITimeLineTool 
    {
        
        
        private TimelineMaster _TimeLine = null;
        public void SetTimeLine(TimelineMaster TimeLine)
        {
            _TimeLine = TimeLine;
        }
        public CutTrackTool()
        {
           /* Assembly executingAssembly = Assembly.GetExecutingAssembly();

           
            System.IO.Stream stream = executingAssembly.GetManifestResourceStream(
            "MovieEditor.TimeLine.Tools.CutCursor.cur");*/

            _MouseIcon = CursorHandling.LoadDifficultCursor(typeof(Tools.CutAllTracksTool), "CutCursor.cur");
            

        }

        private Cursor _MouseIcon = null;
        public Cursor  MouseIcon()
        {
            return _MouseIcon;

        }

        private int MouseState = 0;
        public void HandleTrackMouseMove(Track ActiveTrack,Panel TrackPanel, double Time, MouseEventArgs e)
        {
            if (MouseState == 1)
            {


            }
        }
        public void HandleTrackMouseDown(Track ActiveTrack, Panel TrackPanel, double Time, MouseEventArgs e)
        {
            MouseState = 1;
        }
        public void HandleTrackMouseUp(Track ActiveTrack, Panel TrackPanel, double Time, MouseEventArgs e)
        {
            MouseState = 0;
            _TimeLine.MainAVHandler.CutChunk(ActiveTrack.BaseChunk,Time );
        }

        private int NoTrackMouseState = 0;
        public void HandleMouseDown(double Time, MouseEventArgs e)
        {
            NoTrackMouseState = 1;
            _TimeLine.ShowSelectTimeIndicator(Time);
        }

        public void HandleMouseMove(double Time, MouseEventArgs e)
        {
            if (NoTrackMouseState == 1)
            {
                _TimeLine.MoveSelectTimeIndicator(Time);

            }
        }

        public void HandleMouseUp(double Time, MouseEventArgs e)
        {
            NoTrackMouseState = 0;
            _TimeLine.HideSelectTimeIndicator();
            Track[] HitTracks = _TimeLine.TracksAtTime(Time);
            Dictionary<string, string> LinkHints = new Dictionary<string, string>();
            foreach (Track t in HitTracks)
            {
                string[] Names = _TimeLine.MainAVHandler.CutChunk(t.BaseChunk, Time);
                LinkHints.Add(Names[0], Names[1]);
            }
            foreach (Track t in HitTracks)
            {
                if (t.LinkedTrack != null)
                {
                    string bTrackName = t.BaseChunk.ChunkName;
                    Track BaseTrack = _TimeLine.GetTrack(LinkHints[bTrackName]);
                    if (BaseTrack != null)
                    {
                        string bbTrackName = t.BaseChunk.GuiTrack.LinkedTrack.BaseChunk.ChunkName;
                        try
                        {
                            Track LinkTrack = _TimeLine.GetTrack(LinkHints[bbTrackName]);
                            BaseTrack.LinkedTrack = LinkTrack;
                        }
                        catch
                        {
                            BaseTrack.LinkedTrack = null;
                        }
                    }
                }
            }
        }


    }
}
