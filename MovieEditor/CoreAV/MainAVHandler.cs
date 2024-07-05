using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.IO;

namespace MovieEditor.CoreAV
{
    public class MainAVHandler
    {
        AviSynthScriptEnvironment asse;
        
        Dictionary<string, Chunk> Chunks = new Dictionary<string, Chunk>();

        public void ClearEveryThing()
        {
            currentProject.MediaLibrary.Clear();
            Chunks.Clear();
            TimeLineGUI.ClearEveryThing();
        }
        public void Undo()
        {
            currentProject.Undo();
        }
        public void Redo()
        {
            currentProject.Redo();
        }
        
        #region Filters

            List<Filters.IFilter> Filters = new List<Filters.IFilter>();
            FilterManager _FilterManager = new FilterManager();

            public void RemoveFilter(Filters.IFilter filter)
            {
                Filters.Remove(filter);

            }
            public void AddFilter(Filters.IFilter newFilter)
            {
                Filters.Add(newFilter);
            }
            public Filters.IFilter LoadFilter(string FilterType)
            {
                return  _FilterManager.GetFilter(FilterType);

            }
            public List< Filters.IFilter> GetLoadedFilters()
            {
                return Filters;
            }
            public List<FilterManager.FilterDescriptionHolder > GetPossibleFilters()
            {
                return _FilterManager.PossibleFilters();
            }
            public Filters.IFilter[] GetFiltersAtTime(double Time)
            {
                List<Filters.IFilter> fs = new List<Filters.IFilter>();
                foreach (Filters.IFilter f in Filters)
                {
                    if (f.ClipStartTime <= Time && f.ClipEndTime >= Time)
                    {
                        fs.Add(f);
                    }
                }
                return fs.ToArray();
            }
            private Filters.IFilter ActiveFilter;
            public Filters.IFilter CurrentActiveFilter
            {
                get { return ActiveFilter; }
                set { ActiveFilter = value; }
            }
            public void SetTimelineToFilterMode()
            {
                TimeLineGUI.StartFilterTool();
            }
        #endregion

        #region RawFiles

           
            internal  RawVideoFile LoadMediaFile(string Filename)
            {
                if (File.Exists(Filename) == false)
                {
                    if (File.Exists(Filename + ".avi") == false)
                    {
                        if (File.Exists(currentProject.ProjectDirectory + Filename) == true)
                        {
                            Filename = currentProject.ProjectDirectory + Filename;
                        }
                        if (File.Exists(currentProject.ProjectDirectory + Filename + ".avi") == true)
                        {
                            Filename = currentProject.ProjectDirectory + Filename;
                        }
                    }
                }
                RawVideoFile rvf=new RawVideoFile(Filename,asse,OwnerForm.Handle);
                if (rvf.HasClip ==true)
                    currentProject.AddRawMediaFile(rvf);
                return rvf;
            }
            public void RefreshRawFileViewer()
            {
                _RawFileViewer.RefreshFileExplorer(currentProject.MediaLibrary);
            }

        #endregion

            private SoundEngines.SoundEngine _SoundEngine;
        private Project CurrentProject;
        public Project currentProject
        {
            get { return CurrentProject; }
            set { CurrentProject = value; }
        }

        public SoundEngines.SoundEngine soundEngine
        {
            get { return _SoundEngine; }
        }
        private VideoViewer.IViewer MainViewer;
        private FileExplorer _RawFileViewer;

        //private TimeLine.TimelineMaster TimeLineGUI = null;
        private ITimeLine TimeLineGUI = null;

        public VideoViewer.IViewer DefaultViewer
        {
            get { return MainViewer; }
            set {
                
                MainViewer = value;
                MainViewer.SetAVCore(this);
                MainViewer.PlayMovie += new MovieEditor.VideoViewer.PlayMovieRequest(MainViewer_PlayMovie);
                MainViewer.MoveFrame += new MovieEditor.VideoViewer.MoveFrameRequest(MainViewer_MoveFrame);
            
            }
        }

        void MainViewer_MoveFrame(MovieEditor.VideoViewer.IViewer sender, int newFrame, bool Quietly)
        {
            if (Quietly == true)
            {
                TimeLineGUI.MoveTimeIndicatorQuietly((double)newFrame / currentProject.FrameRate);
            }
            else 
                TimeLineGUI.MoveTimeIndicator((double)newFrame / currentProject.FrameRate);
        }

        public void InitializeSoundForPlay(double BufferTime)
        {
            currentProject.InitializeSound(_SoundEngine, BufferTime);
        }
        public void InitializeSoundForPlay(int BufferSize)
        {
            currentProject.InitializeSound(_SoundEngine, BufferSize );
        }
        void MainViewer_PlayMovie(MovieEditor.VideoViewer.IViewer sender, PlayPlan PlayPlan)
        {

          /* // byte[] StartBuffer = new byte[_SoundEngine.BufferSize];
            List<byte[]> frames = new List<byte[]>();
            int numFrames =  GetFrameNumber(AudioFrameOffset );
            
            int WritePosition = 0;
            
            for (int i = 0; i < numFrames; i++)
            {
                byte [] nBuffer= PlayPlan.GetFrameAudioBuffer(i, 1/currentProject.FrameRate);
                if (nBuffer != null)
                {
                    //for (int j = 0; j < nBuffer.Length; j++)
                    //    StartBuffer[j + WritePosition] = nBuffer[j];
                    frames.Add(nBuffer);
                    WritePosition += nBuffer.Length;
                }
                else
                {
                    WritePosition += (int)(1 / currentProject.FrameRate * _SoundEngine.AverageBytesPerSecond);
                    frames.Add ( new byte[(int)(1 / currentProject.FrameRate * _SoundEngine.AverageBytesPerSecond)]);
                }
            }
            byte[] StartBuffer = new byte[WritePosition  ];
            WritePosition =0;
            foreach (byte[] b in frames )
            {
                for (int j=0;j<b.Length ;j++)
                {
                    StartBuffer[WritePosition + j] = b[j];
                }
                WritePosition += b.Length;
            }
            //_SoundEngine.PreInitialBuffer(StartBuffer);
            
            currentProject.InitializeSound(_SoundEngine,StartBuffer );
            */
            double dt;
            PlayPlan.GetFrame(0,out dt);
            //currentProject.InitializeSound(_SoundEngine, dt*3);
            //_SoundEngine.PreInitialBuffer( PlayPlan.GetFrameAudioBuffer(0, dt));
            //_SoundEngine.Play();
        }
        //todo: this is really a hack.  The project file should set the properties and then all the audio should be converted to 
        //these properties.  This would solve the inevitable problem that the audio files may be different
        public void AudioProperties(out int NumChannels, out int BytesPerSample, out AudioSampleType SampleType, out long SamplesPerSecond)
        {
            AudioChunk ac = null;
            foreach (Chunk tc in Chunks.Values)
            {
                if (tc.GetType() == typeof(AudioChunk))
                    ac = (AudioChunk)tc;
            }
            if (ac != null)
                ac.AudioProperties(out NumChannels, out BytesPerSample, out SampleType, out SamplesPerSecond);
            else
            {
                NumChannels = 0;
                BytesPerSample = 0;
                SampleType = AudioSampleType.Unknown;
                SamplesPerSecond = 0;
            }
        }

        public FileExplorer RawFileViewer
        {
            get { return _RawFileViewer; }
            set { _RawFileViewer = value; }
        }
        public ITimeLine  TimeMaster
        {
            set { TimeLineGUI = value; }
            get { return TimeLineGUI; }
        }

        public PlayPlan GetPlayPlan()
        {

            //get the total number of frames and the time brackets of the 
            //plan
            int TotalFrames = 0;
            double MaxTime = 0;
            double MinTime = 1000;
            int NumVidChunks = 0;
            int NumAudChunks = 0;
            foreach (Chunk tc in Chunks.Values)
            {
                if (MaxTime <= tc.ChunkProperties.EndTime)
                    MaxTime = tc.ChunkProperties.EndTime;
                if (MinTime > tc.ChunkProperties.StartTime)
                    MinTime = tc.ChunkProperties.StartTime;
                if (tc.GetType() == typeof(VideoChunk))
                    NumVidChunks++;
                if (tc.GetType() == typeof(AudioChunk))
                    NumAudChunks++;
            }
            TotalFrames = (int)(currentProject.FrameRate * (MaxTime - MinTime));



            vidFrameIndex[,] vidFrameAssign = new vidFrameIndex[NumVidChunks, TotalFrames];
            audFrameIndex[,] audFrameAssign = new audFrameIndex[NumAudChunks, TotalFrames];



            double[] TimePerFrame = new double[TotalFrames];
            //sort the video chunks by priority then start assigning based on what is on top
            List<Chunk> vidChunks = new List<Chunk>();// Chunks.Values);
            foreach (Chunk c in Chunks.Values)
                if (c.GetType() == typeof(VideoChunk)) vidChunks.Add(c);

            vidChunks.Sort(delegate(Chunk V1, Chunk V2) { return V1.Priority.CompareTo(V2.Priority); });

            for (int i = 0; i < vidChunks.Count; i++)
            {
                int FrameNStart = 0;
                int FrameNEnd = 0;
                double SecondStart = vidChunks[i].ChunkProperties.StartTime - MinTime;
                double SecondEnd = vidChunks[i].ChunkProperties.EndTime - MinTime;
                GetClosestFrameTime(SecondStart, out FrameNStart);
                GetClosestFrameTime(SecondEnd, out FrameNEnd);
                if (FrameNStart < 0) FrameNStart = 0;
                for (int j = FrameNStart; j < FrameNEnd; j++)
                {
                    if (vidFrameAssign[0, j] == null)
                    {
                        Filters.IFilter[] fs = GetFiltersAtTime(GetFrameTime(j));
                        vidFrameAssign[0, j] = new vidFrameIndex(GetFrameTime(j) + MinTime,(VideoChunk) vidChunks[i], fs);
                    }
                    else
                    {
                        for (int k = 1; k < NumVidChunks; k++)
                        {
                            if (vidFrameAssign[k, j] == null)
                            {
                                vidFrameAssign[k, j] = new vidFrameIndex(GetFrameTime(j) + MinTime,(VideoChunk) vidChunks[i], null);
                                k = NumVidChunks;
                            }
                        }
                    }
                }

            }

            //sort the audio chunks by priority then start assigning based on what is on top
            List<Chunk> audChunks = new List<Chunk>();// Chunks.Values);
            foreach (Chunk c in Chunks.Values)
                if (c.GetType() == typeof(AudioChunk)) audChunks.Add(c);

            audChunks.Sort(delegate(Chunk V1, Chunk V2) { return V1.Priority.CompareTo(V2.Priority); });

            for (int i = 0; i < audChunks.Count; i++)
            {
                int FrameNStart = 0;
                int FrameNEnd = 0;
                double SecondStart = audChunks[i].ChunkProperties.StartTime - MinTime;
                double SecondEnd = audChunks[i].ChunkProperties.EndTime - MinTime;
                GetClosestFrameTime(SecondStart, out FrameNStart);
                GetClosestFrameTime(SecondEnd, out FrameNEnd);
                if (FrameNEnd < TotalFrames) FrameNEnd = TotalFrames - 1;
                if (FrameNStart < 0) FrameNStart = TotalFrames;
                for (int j = FrameNStart; j < FrameNEnd; j++)
                {
                    if (audFrameAssign[0, j] == null)
                        audFrameAssign[0, j] = new audFrameIndex(GetFrameTime(j) + MinTime,(AudioChunk) audChunks[i], null);
                    else
                    {
                        for (int k = 1; k < NumAudChunks; k++)
                        {
                            if (audFrameAssign[k, j] == null)
                            {
                                audFrameAssign[k, j] = new audFrameIndex(GetFrameTime(j) + MinTime, (AudioChunk)audChunks[i], null);
                                k = NumVidChunks;
                            }
                        }
                    }
                }
            }
            double lastTime = 0;
            for (int i = 1; i < TotalFrames; i++)
            {
                double currentTime = GetFrameTime(i);
                TimePerFrame[i - 1] = currentTime - lastTime;
                lastTime = currentTime;
            }
            TimePerFrame[TotalFrames - 1] = TimePerFrame[TotalFrames - 2];
            PlayPlan pp=new PlayPlan(this, vidFrameAssign, audFrameAssign, TimePerFrame);
            pp.StartFrameOffset=(int)(currentProject.FrameRate * MinTime );
            return (pp);



        }
       
        public string [] CutChunk(Chunk ChunkToCut, double Time)
        {
            string[] Names = new string[2];
            Names[0] = ChunkToCut.ChunkName;
            Chunk NewChunk = ChunkToCut.Clone();
            double Duration1=Time-ChunkToCut.ChunkProperties.StartTime ;
            double Duration2=ChunkToCut.ChunkProperties.EndTime - Time;

            ChunkToCut.ChunkProperties.Duration  = Duration1 ;
            string sValue=NewChunk.ChunkName + "_";
            if (Chunks.ContainsKey(sValue))
            {
                while (Chunks.ContainsKey(sValue))
                    sValue += "_";
            }
            Names[1] = sValue;
            NewChunk.ChunkName = sValue;
            NewChunk.ChunkProperties.StartTime =ChunkToCut.GetCutTime( Time);
            NewChunk.ChunkProperties.Duration = Duration2;
            NewChunk.RawFileProps.StartOffsetTime += Duration1;
            NewChunk.GuiTrack = null;

            Chunks.Add(sValue, NewChunk);
            ChunkToCut.GuiTrack.ResetChunkProps();
            TimeLineGUI.AddTrack(NewChunk);
            NewChunk.GuiTrack.Top = ChunkToCut.GuiTrack.Top;
            return Names;
        }
        private int GetTopChunk(double Second)
        {
            int FrameN = 0;
           
            Second = GetClosestFrameTime(Second, out FrameN);
            
            List<Chunk> Overlaps = new List<Chunk>();
            
            foreach (Chunk tc in Chunks.Values)
            {
                if (tc.GetType() == typeof(VideoChunk))
                {
                    if ((Second >= tc.ChunkProperties.StartTime) && (Second <= tc.ChunkProperties.EndTime))
                    {
                        Overlaps.Add(tc);
                       
                    }
                }
            }
            Overlaps.Sort(delegate(Chunk V1, Chunk V2) { return V1.Priority.CompareTo(V2.Priority); });
            return Overlaps[0].Priority;
        }

        #region FramesAndTracks
        public byte[] GetFrameAudio(string Trackname, double FrameTime, double FrameDuration)
        {
            if (Trackname != null)
            {
                Chunk c = Chunks[Trackname];
               // double FrameTime = GetFrameTime(FrameNumber);
                if (c != null && c.GetType()==typeof(AudioChunk))
                {
                   return  (((AudioChunk)c).GetFrame(FrameTime ,FrameDuration ));
                }
            }
            
            byte[] b = new byte[((int)Math.Round (CurrentProject.SamplesPerSecond  * FrameDuration)*_SoundEngine.BytesPerSample ) ];
            return b;
           
        }
        public void PlayFrameAudio(string Trackname, double FrameTime, double FrameDuration)
        {
            _SoundEngine.WriteNextBuffer(0, GetFrameAudio(Trackname, FrameTime, FrameDuration));
         
        }
        public Bitmap GetFrame(string Trackname, double FrameTime)
        {
            Chunk c = Chunks[Trackname];
            if (c != null && c.GetType() == typeof(VideoChunk))
            {
                return ((VideoChunk)c).GetFrame(FrameTime);

            }
            return currentProject.BlankFrame;
        }
        public Bitmap GetFrame(string Trackname, int FrameNumber)
        {
            Chunk c = Chunks[Trackname];
            if (c != null && c.GetType()==typeof(VideoChunk))
            {
               return  ((VideoChunk)c).GetFrame(FrameNumber);

            }
            return currentProject.BlankFrame ;
        }
        public Bitmap GetFrame(double Second, out int FrameNumber,out int TotalFrames)
        {
            int FrameN = 0;
            Second = GetClosestFrameTime(Second,out FrameN );
            FrameNumber = FrameN;
            List<Chunk> Overlaps = new List<Chunk>();
            bool FoundOne = false;
            double MaxVidTime = 0;
            foreach (Chunk tc in Chunks.Values  )
            {
                if (tc.GetType() == typeof(VideoChunk))
                {
                    if ( (Second>=tc.ChunkProperties.StartTime ) && ( Second <=tc.ChunkProperties. EndTime  ))
                    {
                        Overlaps.Add(tc);
                        FoundOne = true;
                        //return ((VideoChunk)tc).GetFrame(Second);
                    }
                    if (tc.ChunkProperties.EndTime > MaxVidTime) MaxVidTime = tc.ChunkProperties  .EndTime;
                }
            }
            TotalFrames = 0;
            if (!FoundOne) return currentProject.BlankFrame ;

            
            MaxVidTime = GetClosestFrameTime(MaxVidTime,out  TotalFrames  );

            Overlaps.Sort(delegate(Chunk V1, Chunk V2) { return V1.Priority.CompareTo(V2.Priority); });

            return ((VideoChunk)Overlaps[0]).GetFrame(Second); 
        }

        public void LinkTracks(string Track1, string Track2)
        {
            Chunk T1 = null;
            Chunk T1a = null;
            Chunk T2 = null;
            Chunk T2a = null;
            bool FoundT1=false; 
            bool FoundT2=false ;
            //because the tracks can be linked from one source, have to check if they have the same name
            if (Chunks.ContainsKey(Track1 + "A"))
            {
                T1 = Chunks[Track1 + "A"];
                FoundT1=true ;
            }
            if (Chunks.ContainsKey(Track1 + "V"))
            {
                T1a = Chunks[Track1 + "V"];
                FoundT1=true ;
            }
            if (Chunks.ContainsKey(Track1))
            {
                T1 = Chunks[Track1];
                FoundT1=true ;
            }

            if (Chunks.ContainsKey(Track2 + "A"))
            {
                T2 = Chunks[Track2 + "A"];
                FoundT2=true; 
            }
            if (Chunks.ContainsKey(Track2 + "V"))
            {
                T2a = Chunks[Track2 + "V"];
                FoundT2=true ;
            }
            if (Chunks.ContainsKey(Track2))
            {
                T2 = Chunks[Track2];
                FoundT2=true ;
            }

            if (!FoundT1 || !FoundT2)
            {
                return;
               // throw new Exception("Chunk not Found");
            }
            //now if there is only one name use that name
            Chunk dT1 = null;
            Chunk dT2 = null;
            if (T1 != null && T1a == null)
                dT1 = T1;
            if (T2 != null && T2a == null)
                dT2 = T2;

            if (T1a != null && T1 == null)
                dT1 = T1a;
            if (T2a != null && T2 == null)
                dT2 = T2a;

            //if one is selected, but the other is not, check if the selected one has a type and select the opposite for the other
            if (dT1 != null && dT2 == null)
            {
                if (dT1.GetType() == typeof(AudioChunk))
                    dT2 = T2a;
                else
                    dT2 = T2;
            }
            if (dT2 != null && dT1 == null)
            {
                if (dT2.GetType() == typeof(AudioChunk))
                    dT1 = T1a;
                else
                    dT1 = T1;
            }
            //if both are not determined, then link both
            //??
            if (dT1 == null || dT2 == null)
                return;
              //  throw new Exception("Tracks not found");
            if (dT2 !=null )
                dT1.GuiTrack.LinkedTrack  = dT2.GuiTrack;
            if (dT1 !=null)
                dT2.GuiTrack.LinkedTrack = dT1.GuiTrack;

        }
        public void AddTrack(string Filename, string TrackType, string TrackName, double ClipStartTime, double InternalStartTime, double ClipDuration, double SpeedUpRatio, int GUITop, int GUIHeight,List< Filters.IFilter> Filters)
        {
            CoreAV.RawVideoFile rvf2 = currentProject.LoadMediaFile(Filename);
            
            if (TrackType == "AudioChunk")
            {
                AudioChunk ac = rvf2.GetAudioChunk();
                if (ac != null)
                {
                    ac.FilterList.AddRange( Filters);
                    Chunks.Add(TrackName, ac);
                    ac.ChunkName = TrackName;

                    if (ClipDuration == -1)
                        ClipDuration = ac.ChunkProperties.Duration;
                    ac.ChunkProperties.Duration = ClipDuration;
                    ac.ChunkProperties.StartTime = ClipStartTime;
                    ac.RawFileProps.StartOffsetTime = InternalStartTime;
                    ac.ChunkProperties.SpeedRatio = SpeedUpRatio;

                    ac.GuiTrack = TimeLineGUI.AddTrack(ac);

                    if (GUITop != -1)
                    {
                        ac.GuiTrack.Top = GUITop;
                        ac.GuiTrack.Height = GUIHeight;
                    }
                }
            }
            else if (TrackType == "VideoChunk")
            {
                VideoChunk vc = rvf2.GetVideoChunk();
                if (vc != null)
                {
                    vc.FilterList.AddRange(Filters);
                    Chunks.Add(TrackName, vc);

                    vc.ChunkName = TrackName;
                    if (ClipDuration == -1)
                        ClipDuration = vc.RawFileProps.TotalDuration;
                    vc.ChunkProperties.Duration = ClipDuration;
                    vc.ChunkProperties.StartTime = ClipStartTime;
                    vc.RawFileProps.StartOffsetTime = InternalStartTime;
                    vc.ChunkProperties.SpeedRatio = SpeedUpRatio;

                    vc.GuiTrack = TimeLineGUI.AddTrack(vc);
                    if (GUITop != -1)
                    {
                        vc.GuiTrack.Top = GUITop;
                        vc.GuiTrack.Height = GUIHeight;
                        vc.GuiTrack.ResetFilterControls();
                    }
                }
            }
            else
            {
                VideoChunk vc = rvf2.GetVideoChunk();
                AudioChunk ac = rvf2.GetAudioChunk();
                if (vc != null)
                    Chunks.Add(TrackName + "V", vc);
                if (ac != null)
                    Chunks.Add(TrackName + "A", ac);
                vc.FilterList.AddRange(Filters);
                ac.ChunkName = TrackName + "A";
                vc.ChunkName = TrackName + "V";
                ITrack[] tracks = null;
                if (rvf2.HasClip)
                {
                    if (ClipDuration == -1)
                        ClipDuration = ac.RawFileProps .TotalDuration;
                    ac.ChunkProperties.Duration  = ClipDuration;
                    ac.ChunkProperties .StartTime = ClipStartTime;
                    ac.RawFileProps.StartOffsetTime  = InternalStartTime;
                    ac.ChunkProperties .SpeedRatio = SpeedUpRatio;

                    vc.ChunkProperties.Duration  = ClipDuration;
                    vc.ChunkProperties.StartTime = ClipStartTime;
                    vc.RawFileProps.StartOffsetTime = InternalStartTime;
                    vc.ChunkProperties .SpeedRatio = SpeedUpRatio;


                    tracks = TimeLineGUI.AddAudioVideoTrack(vc, ac);
                    vc.GuiTrack = tracks[0];
                    ac.GuiTrack = tracks[1];
                }
            }
            
        }
        public void AddTrack(string Filename, string TrackType,string TrackName)
        {
            AddTrack(Filename, TrackType, TrackName, 0, 0, -1, 1, -1, -1,new List<Filters.IFilter>());
        }
        public void AddTrack(string Filename, string Trackname)
        {

            CoreAV.RawVideoFile rvf2 =currentProject.LoadMediaFile(Filename );
            VideoChunk vc = rvf2.GetVideoChunk();
            AudioChunk ac = rvf2.GetAudioChunk();
            if (vc != null)
            {
                Chunks.Add(Trackname + "V", vc);
                vc.ChunkName = Trackname + "V";
            }
            if (ac != null)
            {
                Chunks.Add(Trackname + "A", ac);
                ac.ChunkName = Trackname + "A";
            }
            
            ITrack[] tracks=null ;
            if (rvf2.HasClip)
            {
                if (ac != null && vc != null)
                {
                    tracks = TimeLineGUI.AddAudioVideoTrack(vc, ac);
                    vc.GuiTrack = tracks[0];
                    ac.GuiTrack = tracks[1];
                }
                else if (ac != null)
                {
                    ac.GuiTrack = TimeLineGUI.AddTrack(ac);

                }
                else if (vc != null)
                {
                    vc.GuiTrack = TimeLineGUI.AddTrack(vc);
                }
            }
            
        }
        public void DeleteChunk(string Chunkname)
        {
            Chunks.Remove(Chunkname);
            
        }
        #endregion

        public Chunk[] AllChunks
        {
            get { return Chunks.Values .ToArray(); }

        }
        private Control OwnerForm;
        public MainAVHandler(Control MainOwnerForm)
        {
            asse = new AviSynthScriptEnvironment();
            _SoundEngine = new SoundEngines.SoundEngine(MainOwnerForm);
            _SoundEngine.InitializeSound();
            OwnerForm = MainOwnerForm;
            _FilterManager.GetPluginFilters();
        }
       

        public double GetClosestFrameTime(double TimeIn, out int FrameNumber)
        {
            if (currentProject != null)
            {
                double FrameNum = Math.Floor(TimeIn * currentProject.FrameRate);
                FrameNumber = (int)FrameNum;
                return FrameNum / currentProject.FrameRate;
            }
            else
            {
                FrameNumber = 0;
                return 0;
            }
        }
        public double GetFrameTime(int FrameNumber)
        {
            return ((double) FrameNumber / currentProject.FrameRate);
        }
        public int GetFrameNumber(double FrameTime)
        {
            return (int)(FrameTime * currentProject.FrameRate);
        }
    }
}
