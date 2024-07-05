using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.IO;

namespace MovieEditor.TimeLine
{
    public partial class TimelineMaster :  DockContent ,CoreAV.ITimeLine 
    {
        private List<Track> Tracks=new List<Track>();
        private Track _ActiveTrack;
        private CoreAV.MainAVHandler MainAV;
        //private VideoViewer.Viewer defaultViewer;
        private double  SecondsPerPixel=1f/25f;
        private Tools.ITimeLineTool  CurrentTool = null;
        private double  SelectedFrameTime=0;

        public void ClearEveryThing()
        {
            foreach (Track t in Tracks)
            {
                Controls.Remove(t);
            }
            Tracks.Clear();
        }
        public void StartFilterTool()
        {
            UpdateCurrentTool(new Tools.AddFilterTool());
        }
        public void ShowFilterGui(Filters.IFilter filter)
        {
            this.TrackPanel.Controls.Add(filter.GUI );
            filter.GUI.BringToFront();
            this.Refresh();
        }
        public CoreAV.MainAVHandler MainAVHandler
        {
            get { return MainAV; }
        }

        public Track ActiveTrack
        {
            get { return _ActiveTrack; }
            set { _ActiveTrack = value; }
        }

        public double PixelToTime(int PixelNumber)
        {
            int x;
            double Time= MainAV.GetClosestFrameTime(((double)PixelNumber  - frameSelector1.Labelsize) * SecondsPerPixel, out x);

            return Time;
        }
        public int TimeToPixel(double Time)
        {
             return (int)( Time / SecondsPerPixel + frameSelector1.Labelsize);

        }
        public Track GetTrack(string ChunkName)
        {
            foreach (Track t in Tracks)
                if (t.BaseChunk.ChunkName == ChunkName)
                    return t;
            return null;
        }
        public Track[] TracksAtTime(double Time)
        {
            int Pixel = TimeToPixel(Time);
            List<Track> HitTracks = new List<Track>();
            foreach (Track t in Tracks)
            {
                if ((t.Left < Pixel) && ((t.Left + t.Width) > Pixel))
                {
                    HitTracks.Add(t);
                }
            }
            return HitTracks.ToArray();
        }
        public TimelineMaster()
        {
            InitializeComponent();
            frameSelector1.Dpu = 1 / SecondsPerPixel;
            UpdateCurrentTool( new Tools.SelectAndMove() );
            TimeLocation.Left = 1;
        }

        public void LinkTracks(Track Track1, Track Track2)
        {
            if (Track1  != null && Track2  != null)
            {
                Track1.LinkedTrack = Track2 ;
                Track2.LinkedTrack = Track1 ;
            }
        }
        public CoreAV.ITrack AddTrack(CoreAV.Chunk NewChunk)
        {
            Track newTrack = null;
            
            if (NewChunk == null)
                return null;
            if (NewChunk.GetType ()  == typeof(CoreAV.VideoChunk ))
            {
                newTrack = new VideoTrack((CoreAV.VideoChunk) NewChunk , SecondsPerPixel);
            }
            else  if (NewChunk.GetType() == typeof( CoreAV.AudioChunk))
            {
                newTrack = new AudioTrack((CoreAV.AudioChunk)NewChunk  , SecondsPerPixel);
            }
            NewChunk.GuiTrack = newTrack;
            //locate the new track as one down from the rest 
            int TrackTop = 0;
            for (int i = 0; i < Tracks.Count; i++)
            {
                int h = Tracks[i].Top + Tracks[i].Height;
                if (h > TrackTop) TrackTop = h;
            }
            //put it on the screen
            Tracks.Add(newTrack);
            newTrack.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            //put the track in the correct location for its timing
            newTrack.Location = new System.Drawing.Point(frameSelector1.Labelsize +(int)(NewChunk.ChunkProperties.StartTime /SecondsPerPixel) , TrackTop + 10);
            newTrack.Name = "track1";
            //make its size dependant on the duration
            double Secs = NewChunk.ChunkProperties.Duration  / SecondsPerPixel;
            newTrack.Size = new System.Drawing.Size((int)Secs, 37);

            newTrack.TabIndex = 3;
            newTrack.MouseMove += new System.Windows.Forms.MouseEventHandler(this.track1_MouseMove);
            newTrack.MouseDown += new System.Windows.Forms.MouseEventHandler(this.track1_MouseDown);
            newTrack.MouseUp += new System.Windows.Forms.MouseEventHandler(this.track1_MouseUp);
            newTrack.KeyUp += new KeyEventHandler(newTrack_KeyUp);
            this.TrackPanel.Controls.Add(newTrack);
            newTrack.Visible = true;
            try
            {
                newTrack.DrawVis();
            }
            catch { }
            
            
            if (newTrack.Width + newTrack.Left > TrackPanel.Width)
                TrackPanel.Width = (int)(1.5 * (newTrack.Width + newTrack.Left));

            SetPriorities();
            foreach(Filters.IFilter filter in NewChunk.FilterList)
            {
                ShowFilterGui(filter);
            }
            newTrack.ResetFilterControls();
            this.Invalidate();
            this.Refresh();
            return newTrack;
        }

        

        public CoreAV.ITrack[] AddAudioVideoTrack(CoreAV.VideoChunk VC, CoreAV.AudioChunk AC)
        {
            Track t1 =(Track) AddTrack(VC);
            Track t2 =(Track) AddTrack(AC);
            LinkTracks(t1, t2);
            Track[] tracks = { t1, t2 };
            return tracks;
        }
      
        public void SetMainAV(CoreAV.MainAVHandler MainAV)
        {
            this.MainAV = MainAV;
        }

        #region TrackEvents

        void newTrack_KeyUp(object sender, KeyEventArgs e)
        {
            _ActiveTrack = (Track)sender;
            TimelineMaster_KeyUp(sender, e);

        }

        private void track1_MouseDown(object sender, MouseEventArgs e)
        {
            if (CurrentTool != null) CurrentTool.HandleTrackMouseDown((Track)sender,TrackPanel  ,PixelToTime(e.X+((Track)sender).Left) ,e);
        }

        private void track1_MouseMove(object sender, MouseEventArgs e)
        {
            if (CurrentTool != null) CurrentTool.HandleTrackMouseMove((Track)sender,TrackPanel, PixelToTime(e.X + ((Track)sender).Left), e);
        }

        private void track1_MouseUp(object sender, MouseEventArgs e)
        {
            if (CurrentTool != null) CurrentTool.HandleTrackMouseUp((Track)sender, TrackPanel, PixelToTime(e.X + ((Track)sender).Left), e);
            SetPriorities();
        }
        #endregion
        private void ShowCurrentFrame(int x, int y)
        {
            

        }

        public void SetPriorities()
        {
            MainAV.currentProject.MakeHistoryToken();
            Tracks.Sort(delegate(Track  V1, Track V2) { return V1.Top.CompareTo(V2.Top); });

            
            int cc = 1;
            foreach (Track tt in Tracks)
            {
                tt.SetPriority(cc);
                
                cc++;
            }

        }

        public void ClearTrackHighLights()
        {
            foreach (Track t in Tracks)
            {
                t.Highlight(false);
                t.Refresh();
            }
        }

        public void RefreshFrameViewer()
        {
            int TotalFrames = 0;
            int FrameN = 0;
            try
            {
                Bitmap b = MainAV.GetFrame(SelectedFrameTime, out FrameN, out TotalFrames);
                MainAV.DefaultViewer.ShowFrame(b, SelectedFrameTime, FrameN, TotalFrames);
            }
            catch { }


        }

        #region Time_Indicators
        public void ShowSelectTimeIndicator(double Showtime)
        {
            SelectorLocator.Visible = true ;
            MoveSelectTimeIndicator(Showtime);
        }
        public void MoveSelectTimeIndicator(double Time)
        {
            SelectorLocator.Left = TimeToPixel(Time);
        }
        public void MoveTimeIndicator(double Time)
        {
            float fsV =(float) Time;
            TimeLocation.Left = TimeToPixel(Time);
            label1.Text = Math.Round( fsV, 2).ToString();
            SelectedFrameTime = fsV;
            RefreshFrameViewer();
        }
        public void MoveTimeIndicatorQuietly(double Time)
        {
            float fsV = (float)Time;
            TimeLocation.Left = TimeToPixel(Time);
            label1.Text = Math.Round(fsV, 2).ToString();
            SelectedFrameTime = fsV;
        }
        public void HideSelectTimeIndicator()
        {
            SelectorLocator.Visible = false;
        }
        #endregion


        private void frameSelector1_MouseUp(object sender, MouseEventArgs e)
        {

            try
            {
                float fsV = frameSelector1.Value;

                TimeLocation.Left =(int)TimeToPixel(fsV)  ;
                label1.Text = Math.Round(fsV, 2).ToString();
                SelectedFrameTime = fsV;
                RefreshFrameViewer();

            }
            catch
            { }
        }

        private void TimelineMaster_Resize(object sender, EventArgs e)
        {
            int THeight =this.Height -frameSelector1.Height ;
            if (TrackPanel.Height <THeight ) TrackPanel.Height = THeight ;
            
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            TrackPanel.Left = (int)((double)e.NewValue / -100 * ((double)TrackPanel.Width - panel2.Width));
        }

        private void UpdateCurrentTool(Tools.ITimeLineTool NewTool)
        {
            CurrentTool = NewTool;
            CurrentTool.SetTimeLine(this);

            TrackPanel.Cursor = CurrentTool.MouseIcon(); 
        }

        private void CutButton_Click(object sender, EventArgs e)
        {
           UpdateCurrentTool( new Tools.CutTrackTool());
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateCurrentTool( new Tools.SelectAndMove() );
            
        }

        private void TrackPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (CurrentTool != null) CurrentTool.HandleMouseDown(PixelToTime(e.X), e);
        }

        private void TrackPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (CurrentTool != null) CurrentTool.HandleMouseMove(PixelToTime(e.X), e);
        }

        private void TrackPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (CurrentTool != null) CurrentTool.HandleMouseUp(PixelToTime(e.X), e);
            SetPriorities();
        }
        
        public int ScrollBarPosition
        {
            get { return hScrollBar1.Value   ; }
            set { 
               hScrollBar1.Value =value ;
                  hScrollBar1_Scroll(this,new ScrollEventArgs(ScrollEventType.Last,value ));
            }
        }

        private void CutAllButton_Click(object sender, EventArgs e)
        {
            CurrentTool = new Tools.CutAllTracksTool();
            CurrentTool.SetTimeLine(this);
        }

        private void frameSelector1_Load(object sender, EventArgs e)
        {

        }


        private void TimelineMaster_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (ActiveTrack != null)
                {
                    Track linkedTrack =(Track) ActiveTrack.LinkedTrack;
                    MainAV.DeleteChunk(ActiveTrack.BaseChunk.ChunkName);
                    
                    Tracks.Remove(ActiveTrack);
                    Controls.Remove(ActiveTrack);
                    ActiveTrack.Dispose();
                    ActiveTrack = null;
                    if (linkedTrack != null)
                    {
                        linkedTrack.LinkedTrack = null;
                        DialogResult ret = MessageBox.Show("Do you wish to delete the linked track also?", "", MessageBoxButtons.YesNo);
                        if (ret == DialogResult.Yes)
                        {
                            MainAV.DeleteChunk(linkedTrack.BaseChunk.ChunkName);
                            Tracks.Remove(linkedTrack);
                            Controls.Remove(linkedTrack);
                            linkedTrack.Dispose();
                            linkedTrack = null;
                        }
                    }


                }

            }
        }

        private void FilterButton_Click(object sender, EventArgs e)
        {
            UpdateCurrentTool( new Tools.AddFilterTool() );
            
        }

        private void TrackPanel_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void TrackPanel_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;

        }
        
        private void TrackPanel_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(CoreAV.RawVideoFile  )))
            {
                string filename= ( (CoreAV.RawVideoFile) e.Data.GetData(typeof(CoreAV.RawVideoFile ))).Filename;
                string Trackname = Path.GetFileNameWithoutExtension(  filename) + Generals.GetRandomString(3);
                MainAV.AddTrack( filename   , Trackname  );
                SetPriorities();
            }
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            TrackPanel.Top  = (int)((double)e.NewValue / -100 * ((double)TrackPanel.Height - panel2.Height ));
        }

      

       

        

        
    }
}
