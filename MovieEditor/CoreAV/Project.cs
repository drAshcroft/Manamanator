using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Runtime.InteropServices;

using System.Drawing;
using System.IO;

namespace MovieEditor.CoreAV
{
    public class Project
    {
        Dictionary<string, RawVideoFile> _MediaLibrary = new Dictionary<string, RawVideoFile>();


        private MainAVHandler MainAV=null;
        private string ProjectFile=null;
        private string ProjectName=null;

        private Bitmap _BlankFrame = null;

        private bool AutoDetect_numChannels = false;
        private bool AutoDetect_SamplesPerSecond = false;
        private bool AutoDetect_AudioSampleType = false;
        private bool AutoDetect_VideoSize = false;
        private bool AutoDetect_VideoFrameRate = false;

        //Final Audio Targets
        private long _SamplesPerSecond;
        private int _numChannels;
        private int _BytesPerSample = 1;
        private AudioSampleType _AudioSampleType;
        private int _VideoWidth=320;
        private int _VideoHeight=240;
        private double _FrameRate=24d;
        public Bitmap BlankFrame
        {
            get { return _BlankFrame; }
        }

        #region RawFiles
        public void AddRawMediaFile(RawVideoFile RawFile)
        {
            MediaLibrary.Add(RawFile.Filename , RawFile);
            DoAutoDetect();
        }
        public RawVideoFile LoadMediaFile(string Filename)
        {
            bool Found = false;
            if (MediaLibrary.ContainsKey(Filename))
            {
                RawVideoFile rvf = MediaLibrary[Filename];
                // MediaLibrary.Add(Filename, rvf);
                return rvf;
            }
            else 
            {
                foreach (string s in MediaLibrary.Keys )
                {
                    if (s.Contains(Filename)==true )
                        return MediaLibrary[s];
                }
            }
            RawVideoFile rvf2 = MainAV.LoadMediaFile(Filename);
                //MediaLibrary.Add(Filename, rvf);
            return rvf2;
            
        }
        public Dictionary<string, RawVideoFile> MediaLibrary
        {
            get { return _MediaLibrary; }
        }

        #endregion

        #region ProjectProperties
        public string ProjectDirectory
        {
            get { return Path.GetDirectoryName(ProjectFile); }
        }
        private void DoAutoDetect()
        {
            if (AutoDetect_numChannels == true)
            {
                _numChannels = 0;
            }
            if (AutoDetect_SamplesPerSecond == true)
            {
                _SamplesPerSecond = 22000;
            }
            if (AutoDetect_AudioSampleType == true)
            {
                _AudioSampleType = AudioSampleType.Unknown;
            }
            if (AutoDetect_VideoSize == true)
            {
                _VideoWidth = 10;
                _VideoHeight = 10;
            }
            if (AutoDetect_VideoFrameRate == true)
            {
                _FrameRate = 1000;
            }
            foreach (KeyValuePair<string, RawVideoFile> kvp in MediaLibrary)
            {
               if (AutoDetect_numChannels ==true )
               {
                   if (kvp.Value.NumChannels > _numChannels) _numChannels = kvp.Value.NumChannels;
               }
               if (AutoDetect_SamplesPerSecond ==true)
               {
                   if (kvp.Value.SamplesPerSecond>_SamplesPerSecond ) _SamplesPerSecond = kvp.Value.SamplesPerSecond;
               } 
               if (AutoDetect_AudioSampleType ==true )
               {
                   if ((int)kvp.Value.AudioSampleType > (int)_AudioSampleType) _AudioSampleType = kvp.Value.AudioSampleType;
               }
               _BytesPerSample = (int)(_numChannels * (int)_AudioSampleType);

               if (kvp.Value.HasVideo )
               {
                   if (AutoDetect_VideoSize == true)
                   {
                       if (kvp.Value.VideoWidth > _VideoWidth || kvp.Value.VideoHeight > _VideoHeight)
                       {
                           _VideoHeight = kvp.Value.VideoHeight;
                           _VideoWidth = kvp.Value.VideoWidth;
                       }
                   }
                   if (AutoDetect_VideoFrameRate == true)
                   {
                       if (kvp.Value.FrameRate < _FrameRate) _FrameRate = kvp.Value.FrameRate;
                   }
               }
               
            }
            if (_FrameRate == 1000)
                _FrameRate = 15;
        }
        public double FrameRate
        {
            get {
                if (_FrameRate == -1)
                    DoAutoDetect();
                return _FrameRate ; }

        }
        public AudioSampleType AudioSampleType
        {
            get {
                if ( _AudioSampleType==AudioSampleType.Unknown )
                    DoAutoDetect();
                return _AudioSampleType; }
        }
        public long SamplesPerSecond
        {
            get { 
                if ( _SamplesPerSecond == -1 || _SamplesPerSecond==-1000)
                    DoAutoDetect();
                return _SamplesPerSecond; }
        }
        public int NumChannels
        {
            get {
                if ( _numChannels == -1)
                    DoAutoDetect();
                return _numChannels; }
        }
        public int BytesPerSample
        {
            get { 
                if ( _BytesPerSample <1)
                    DoAutoDetect();
                return _BytesPerSample; }
        }

        public int VideoWidth
        {
            get { 
                if (_VideoWidth  == -1)
                    DoAutoDetect();
                return _VideoWidth; }
        }
        public int VideoHeight
        {
            get { 
                if (_VideoHeight  == -1)
                    DoAutoDetect();
                return _VideoHeight; }
        }
        #endregion

        public Project(MainAVHandler mainAV)
        {
            MainAV = mainAV;
            SetupHistoryDB();
        }

        public void CreateProject(string ProjectFilename,string ProjectName,AudioSampleType SampleType,
            double AudioSampleRatekHZ,int NumChannels,int VideoWidth,int VideoHeight,double FrameRate)
        {
            if (SampleType ==AudioSampleType.Unknown )
                AutoDetect_AudioSampleType =true ;
            if (AudioSampleRatekHZ ==-1)
                AutoDetect_SamplesPerSecond =true ;
            if (NumChannels ==-1)
                AutoDetect_numChannels = true;
            if (VideoWidth ==-1)
                AutoDetect_VideoSize=true ;
            if (FrameRate ==-1)
                AutoDetect_VideoFrameRate =true ;
       
        //Final Audio Targets
            _SamplesPerSecond=(long)(AudioSampleRatekHZ*1000);
            _numChannels=NumChannels ;
            
            _AudioSampleType=SampleType ;
            _VideoWidth=VideoWidth ;
            _VideoHeight=VideoHeight ;
            _FrameRate=FrameRate ;

            _BytesPerSample =(int)( _numChannels *(int)_AudioSampleType  );
            CreateProject(ProjectFilename, ProjectName);
        }

        public void CreateProject(string ProjectFilename, string ProjectName)
        {
            this.ProjectName = ProjectName;
            ProjectFile = ProjectFilename;
            SaveProject ();
            SetupHistoryDB();
        }
        
        #region FileHandling
        public void OpenProject(string Filename)
        {
            
            XmlDocument xmlDoc = new XmlDocument();
            ProjectFile = Filename;
            try
            {
                xmlDoc.Load(Filename);
            }
            catch (System.IO.FileNotFoundException)
            {
                System.Windows.Forms.MessageBox.Show("This project does not exist.");
                return;
            }
            ReadProjectFile(xmlDoc.DocumentElement.ChildNodes);
            MainAV.RefreshRawFileViewer();
        }
        public void SaveProject()
        {
            if (ProjectFile != null && ProjectFile != "")
            {
                try
                {
                    
                    string filename = ProjectFile  ;

                    XmlDocument xmlDoc = new XmlDocument();

                    try
                    {
                        xmlDoc.Load(filename);
                        xmlDoc.RemoveAll();
                       // xmlDoc.Save(filename );
                        xmlDoc=null;
                        
                        System.IO.File.Delete(filename);
                    }
                    catch 
                    {


                    }
                     xmlDoc = new XmlDocument();

                    
                        //if file is not found, create a new xml file
                        XmlTextWriter xmlWriter = new XmlTextWriter(filename, System.Text.Encoding.UTF8);
                        xmlWriter.Formatting = Formatting.Indented;
                        xmlWriter.WriteProcessingInstruction("xml", "version='1.0' encoding='UTF-8'");
                        xmlWriter.WriteStartElement("Root");
                       
                        xmlWriter.Close();
                        xmlDoc.Load(filename);
                    
                    XmlNode root = xmlDoc.DocumentElement;
                    WriteXML(xmlDoc, root);
                    xmlDoc.Save(filename);
                }
                catch{}
            }
            else
                throw new Exception("No Filename Specified");
        }
        #endregion

        #region XMLHandling
        private double S2D(string ConvertString)
        {
            double d = 0;
            Double.TryParse(ConvertString, out d);
            return d;
        }
        private void LoadTracks(XmlNodeList TrackNodes)
        {
            foreach (XmlNode tn in TrackNodes )
            {
                string sFilename = tn.Attributes["RawFilename"].Value;
                string sType = tn.Attributes["Type"].Value;
                string sName = tn.Name;
                string sDuration = tn.Attributes["ClipDuration"].Value;
                string sIOffset = tn.Attributes["InternalOffset"].Value;
                string sPriority = tn.Attributes["Priority"].Value;
                string sStartTime = tn.Attributes["ClipStartTime"].Value;
                string sTotalDuration = tn.Attributes["TotalDuration"].Value;
                string sClipSpeed = tn.Attributes["ClipSpeed"].Value;
                string sTop = tn.Attributes["Top"].Value;
                string sHeight = tn.Attributes["Height"].Value;

                double dDuration = S2D(sDuration);
                double dIOffset = S2D(sIOffset);
                double dPriority = S2D(sPriority);
                double dStartTime = S2D(sStartTime);
                double dTotalDuration = S2D(sTotalDuration);
                double dClipSpeed = S2D(sClipSpeed);
                double dTop = S2D(sTop);
                double dHeight = S2D(sHeight);

                List<Filters.IFilter> ChunkFilters = new List<Filters.IFilter>();

                foreach (XmlNode ptn in tn.ChildNodes)
                {
                    if (ptn.Name == (tn.Name + "Filters"))
                    {
                        if (ptn.Attributes["NumFilters"].Value != "0")
                        {
                            foreach (XmlNode FilterNode in ptn.ChildNodes)
                            {
                                double dfStartTime;
                                double dfMidTime;
                                double dfEndTime;
                                string FilterSystemType = FilterNode.Attributes["FilterSystemType"].Value;
                                string FilterType = FilterNode.Attributes["FilterType"].Value;

                                string StartTime = FilterNode.Attributes["StartTime"].Value;
                                string MidTime = FilterNode.Attributes["MidTime"].Value;
                                string EndTime = FilterNode.Attributes["EndTime"].Value;

                                double.TryParse(StartTime, out dfStartTime);
                                double.TryParse(MidTime, out dfMidTime);
                                double.TryParse(EndTime, out dfEndTime);

                                string FilterName = FilterNode.Attributes["FilterName"].Value;
                                Dictionary<string, string> Props = new Dictionary<string, string>();
                                foreach (XmlNode PropsNode in FilterNode.ChildNodes)
                                {
                                    foreach (XmlAttribute PropAtrrib in PropsNode.Attributes)
                                        Props.Add(PropAtrrib.Name, PropAtrrib.Value);
                                }
                                try
                                {
                                    Filters.IFilter filter = MainAV.LoadFilter(FilterSystemType);
                                    filter.ClipStartTime = dfStartTime;
                                    filter.ClipMidTime = dfMidTime;
                                    filter.ClipEndTime = dfEndTime;
                                    filter.SetFilterProperties(Props);
                                    ChunkFilters.Add(filter);
                                }
                                catch { }
                            }
                        }
                    }
                }
                System.Windows.Forms.Application.DoEvents();
                if (sType.Contains("AudioChunk"))
                    MainAV.AddTrack(sFilename, "AudioChunk", sName, dStartTime, dIOffset, dDuration, dClipSpeed, (int)dTop, (int)dHeight, ChunkFilters);
                if (sType.Contains("VideoChunk"))
                    MainAV.AddTrack(sFilename, "VideoChunk", sName, dStartTime, dIOffset, dDuration, dClipSpeed, (int)dTop, (int)dHeight, ChunkFilters);
                // XmlNodeList children = tn.ChildNodes;
            }      
        }
        private void ReadProjectFile(XmlNodeList InputNodes )
        {
            foreach (XmlNode xn in InputNodes)
            {
                if (xn.Name == "_Projectfiles")
                {

                    foreach (XmlAttribute xa in xn.Attributes)
                    {
                        string Filename = Path.GetFileName( xa.Value);
                        MainAV.currentProject.LoadMediaFile(Filename );
                        MainAV.RawFileViewer.AddFileIcon(Filename   );

                    }
                }
            }
             
             foreach (XmlNode xn in InputNodes )
             {
                 
                 if (xn.Name == "_Properties")
                 {
                     ProjectName = xn.Attributes["Projectname"].Value ;
                     try
                     {
                         string junk = xn.Attributes["AutoDetect_numChannels"].Value;
                         bool.TryParse(junk, out AutoDetect_numChannels);

                         junk = xn.Attributes["AutoDetect_SamplesPerSecond"].Value;
                         bool.TryParse(junk, out AutoDetect_SamplesPerSecond);
                         junk = xn.Attributes["AutoDetect_AudioSampleType"].Value;
                         bool.TryParse(junk, out AutoDetect_AudioSampleType);
                         junk = xn.Attributes["AutoDetect_VideoSize"].Value;
                         bool.TryParse(junk, out AutoDetect_VideoSize);
                         junk = xn.Attributes["AutoDetect_VideoFrameRate"].Value;
                         bool.TryParse(junk, out AutoDetect_VideoFrameRate);


                         junk = xn.Attributes["_SamplesPerSecond"].Value;
                         long.TryParse(junk, out _SamplesPerSecond);
                         junk = xn.Attributes["_numChannels"].Value;
                         int.TryParse(junk, out _numChannels);
                         junk = xn.Attributes["_BytesPerSample"].Value;
                         int.TryParse(junk, out _BytesPerSample);
                         junk = xn.Attributes["_AudioSampleType"].Value;
                         int iJunk = 0;
                         int.TryParse(junk, out iJunk);
                         _AudioSampleType = (AudioSampleType)iJunk;
                         junk = xn.Attributes["_VideoWidth"].Value;
                         int.TryParse(junk, out _VideoWidth);
                         junk = xn.Attributes["_VideoHeight"].Value;
                         int.TryParse(junk, out _VideoHeight);
                         junk = xn.Attributes["_FrameRate"].Value;
                         double.TryParse(junk, out _FrameRate);
                     }
                     catch { }
                 }
                 if (xn.Name == "Tracks")
                 {

                     LoadTracks(xn.ChildNodes);
 
                     //now link the tracks
                     foreach (XmlNode tn in xn.ChildNodes)
                     {
                         bool sLinked = (tn.Attributes["Linked"].Value == "True" );
                         string sLinkedTrackname="";
                         try
                         {
                             if (sLinked == true)
                                 sLinkedTrackname = tn.Attributes["LinkedTrack"].Value;

                             string sName = tn.Name;

                             if (sLinked)
                                 MainAV.LinkTracks(sName, sLinkedTrackname);
                         }
                         catch ( Exception ex)
                         {
                             System.Diagnostics.Debug.Print("Readprojectfile:" + ex.Message);
                         }
                     }
                 }
             }
             MainAV.AudioProperties(out _numChannels,out _BytesPerSample, out _AudioSampleType,out  _SamplesPerSecond);
             short BlockAlign = (short)(_numChannels * (_BytesPerSample));
             long AverageBytesPerSecond = _SamplesPerSecond * BlockAlign;

             Chunk[] Chunks = MainAV.AllChunks;
             for (int i = 0; i < Chunks.Length && _BlankFrame == null;i++ )
             {
                 if (Chunks[i].GetType() == typeof(VideoChunk))
                 {
                     _BlankFrame = ((VideoChunk)Chunks[i]).GetFrame(0);
                     Graphics g = Graphics.FromImage(_BlankFrame);
                     g.Clear(Color.Black);
                     _VideoHeight = _BlankFrame.Height;
                     _VideoWidth = _BlankFrame.Width;
                 }
             }

        }
        private void WriteXML(XmlDocument xmlDoc,XmlNode root)
        {

                XmlElement ProjectFiles = xmlDoc.CreateElement("_Projectfiles");
                root.AppendChild(ProjectFiles);
                int jj=0;
                foreach (KeyValuePair<string, RawVideoFile> kvp in MediaLibrary)
                {
                    ProjectFiles.SetAttribute("File" + jj.ToString(), kvp.Value.Filename);
                    jj++;
                }
                //Put in all the properties
                XmlElement ProjectInfo = xmlDoc.CreateElement("_Properties");
                root.AppendChild(ProjectInfo);
                ProjectInfo.SetAttribute("Projectname", ProjectName);
                ProjectInfo.SetAttribute("AutoDetect_numChannels",AutoDetect_numChannels.ToString() );

                ProjectInfo.SetAttribute("AutoDetect_SamplesPerSecond",AutoDetect_SamplesPerSecond.ToString());
                ProjectInfo.SetAttribute("AutoDetect_AudioSampleType", AutoDetect_AudioSampleType .ToString());
                ProjectInfo.SetAttribute("AutoDetect_VideoSize", AutoDetect_VideoSize.ToString());
                ProjectInfo.SetAttribute("AutoDetect_VideoFrameRate", AutoDetect_VideoFrameRate.ToString());

                //Final Audio Targets
                ProjectInfo.SetAttribute("_SamplesPerSecond",_SamplesPerSecond.ToString());
                ProjectInfo.SetAttribute("_numChannels",_numChannels.ToString());
                ProjectInfo.SetAttribute("_BytesPerSample", _BytesPerSample.ToString());
                ProjectInfo.SetAttribute("_AudioSampleType",_AudioSampleType.ToString());
                ProjectInfo.SetAttribute("_VideoWidth",_VideoWidth .ToString());
                ProjectInfo.SetAttribute("_VideoHeight",_VideoHeight.ToString());
                ProjectInfo.SetAttribute("_FrameRate", _FrameRate.ToString());
                XmlElement AllTracks = xmlDoc.CreateElement("Tracks");
                root.AppendChild(AllTracks);
                //Now save all the tracks
                XmlElement[] Tracks = new XmlElement[MainAV.AllChunks.Length];
                for (int i=0;i<Tracks.Length ;i++)
                {
                    Tracks[i]=xmlDoc.CreateElement("Track" + i.ToString());
                    
                    AllTracks.AppendChild(Tracks[i]);
                    Chunk c=MainAV.AllChunks[i];
                     
                    string TrackFilename = System.IO.Path.GetFileName(c.RawFileProps.Filename);
                    Tracks[i].SetAttribute("RawFilename", TrackFilename  );
                    Tracks[i].SetAttribute("Type", c.GetType().ToString()); 
                    
                    Tracks[i].SetAttribute("ClipDuration", c.ChunkProperties.Duration.ToString());
                    Tracks[i].SetAttribute("InternalOffset", c.RawFileProps.StartOffsetTime .ToString());
                    Tracks[i].SetAttribute("Priority", c.Priority.ToString());
                    Tracks[i].SetAttribute("ClipStartTime", c.ChunkProperties.StartTime .ToString());
                    Tracks[i].SetAttribute("TotalDuration", c.RawFileProps.TotalDuration.ToString());
                    Tracks[i].SetAttribute("ClipSpeed", c.ChunkProperties.SpeedRatio.ToString());
                    //Tracks[i].SetAttribute("", c.);
                    try
                    {
                        Tracks[i].SetAttribute("Top", c.GuiTrack.Top.ToString());
                        Tracks[i].SetAttribute("Height", c.GuiTrack.Height.ToString());
                   
                        if (c.GuiTrack.LinkedTrack == null)
                            Tracks[i].SetAttribute("Linked", "False");
                        else
                        {
                            Tracks[i].SetAttribute("Linked", "True");
                            for (int j = 0; j < MainAV.AllChunks.Length; j++)
                            {
                                if (MainAV.AllChunks[j].GuiTrack == c.GuiTrack.LinkedTrack)
                                    Tracks[i].SetAttribute("LinkedTrack", "Track" + j.ToString());
                            }
                        }
                    }
                    catch { }
                    XmlElement Filters = xmlDoc.CreateElement("Track" + i.ToString() + "Filters");
                    Filters.SetAttribute("NumFilters", c.FilterList.Count.ToString());
                    Tracks[i].AppendChild(Filters);
                    int cc=0;
                    foreach (Filters.IFilter f in c.FilterList)
                    {
                        XmlElement filter = xmlDoc.CreateElement("Track" + i.ToString() + "Filter" + cc.ToString ());
                        filter.SetAttribute("FilterSystemType", f.GetType().ToString());
                        filter.SetAttribute("FilterType"  ,f.FilterType.ToString()); 
                        filter.SetAttribute("StartTime" ,f.ClipStartTime.ToString() );
                        filter.SetAttribute("MidTime", f.ClipMidTime.ToString());
                        filter.SetAttribute("EndTime", f.ClipEndTime.ToString());
                        filter.SetAttribute("FilterName", f.FilterName);

                        
                        XmlElement filterprops=xmlDoc.CreateElement("Track" + i.ToString() + "Filter" + cc.ToString () + "Props");
                        filterprops.SetAttribute("NumProperties", f.GetFilterProperties().Count.ToString());
                        
                        foreach (KeyValuePair<string, string> kvp in f.GetFilterProperties())
                        {
                            filterprops.SetAttribute(kvp.Key,kvp.Value);
                            
                        }
                        filter.AppendChild(filterprops);
                        Filters.AppendChild(filter);
                        cc++;
                    }
                }

                XmlElement UpperFilters = xmlDoc.CreateElement("AllFilters");
                UpperFilters.SetAttribute("NumFilters", MainAV.GetLoadedFilters().Count.ToString() );
                root.AppendChild(UpperFilters);
                int ccc = 0; 
                foreach (Filters.IFilter f in  MainAV.GetLoadedFilters() )
                {
                    XmlElement filter = xmlDoc.CreateElement("AllFilters" + "Filter" + ccc.ToString());
                    filter.SetAttribute("FilterSystemType", f.GetType().ToString());
                    filter.SetAttribute("FilterType", f.FilterType.ToString());
                    filter.SetAttribute("StartTime", f.ClipStartTime.ToString());
                    filter.SetAttribute("MidTime", f.ClipMidTime.ToString());
                    filter.SetAttribute("EndTime", f.ClipEndTime.ToString());
                    filter.SetAttribute("FilterName", f.FilterName);


                    XmlElement filterprops = xmlDoc.CreateElement("AllFilters" + "Filter" + ccc.ToString() + "Props");
                    filterprops.SetAttribute("NumProperties", f.GetFilterProperties().Count.ToString());

                    foreach (KeyValuePair<string, string> kvp in f.GetFilterProperties())
                    {
                        filterprops.SetAttribute(kvp.Key, kvp.Value);

                    }
                    filter.AppendChild(filterprops);
                    UpperFilters.AppendChild(filter);
                    ccc++;
                }
        }
        #endregion

        #region HistoryFunctions

            private int CurrentUndoLocation = 0;
            List<XmlElement> UndoHistory = new List<XmlElement>();
            private XmlDocument HistoryHolder=null;
            public void SetupHistoryDB()
            {
                try
                    {
                        
                        string filename =Generals.TempPathName()  + "\\History.xml"  ;

                        XmlDocument xmlDoc = new XmlDocument();

                        try
                        {
                            xmlDoc.Load(filename);
                            xmlDoc.RemoveAll();
                           // xmlDoc.Save(filename );
                            xmlDoc=null;
                            
                            System.IO.File.Delete(filename);
                        }
                        catch 
                        {


                        }
                         xmlDoc = new XmlDocument();

                        
                            //if file is not found, create a new xml file
                            XmlTextWriter xmlWriter = new XmlTextWriter(filename, System.Text.Encoding.UTF8);
                            xmlWriter.Formatting = Formatting.Indented;
                            xmlWriter.WriteProcessingInstruction("xml", "version='1.0' encoding='UTF-8'");
                            xmlWriter.WriteStartElement("Root");
                           
                            xmlWriter.Close();
                            xmlDoc.Load(filename);
                        
                        XmlNode root = xmlDoc.DocumentElement;
                        XmlElement FirstState = xmlDoc.CreateElement("State0");
                        
                        WriteXML(xmlDoc, FirstState );
                        UndoHistory.Add(FirstState);
                        HistoryHolder = xmlDoc;
                    }
                    catch{}
               

            }
            public void MakeHistoryToken()
            {
                if (CurrentUndoLocation < UndoHistory.Count)
                {
                    UndoHistory.RemoveRange(CurrentUndoLocation, UndoHistory.Count - CurrentUndoLocation);
                }

                XmlElement CurrentState = HistoryHolder.CreateElement("State"
                    + CurrentUndoLocation.ToString());

                WriteXML(HistoryHolder , CurrentState);
                UndoHistory.Add(CurrentState);

                CurrentUndoLocation++;
            }
            public void Undo()
            {
                if (CurrentUndoLocation >0)
                {
                    CurrentUndoLocation--;
                    
                    XmlElement UndoMemento= UndoHistory[CurrentUndoLocation];
                    MainAV.ClearEveryThing();
                    ReadProjectFile(UndoMemento.ChildNodes);
                }
            }
            public void Redo()
            {
                if (CurrentUndoLocation <UndoHistory.Count )
                {
                    CurrentUndoLocation++;

                    XmlElement UndoMemento = UndoHistory[CurrentUndoLocation];
                    MainAV.ClearEveryThing();
                    ReadProjectFile(UndoMemento.ChildNodes);
                }

            }
        #endregion


        public void InitializeSound(SoundEngines.SoundEngine SoundEngine, byte[] StartBuffer)
        {
            short BlockAlign = (short)(_numChannels * (_BytesPerSample));
            long AverageBytesPerSecond = _SamplesPerSecond * BlockAlign;

            SoundEngine.SetupBuffer(StartBuffer.Length , _numChannels, _BytesPerSample, _AudioSampleType, _SamplesPerSecond);
            SoundEngine.PreInitialBuffer(StartBuffer);
        }
        public void InitializeSound(SoundEngines.SoundEngine SoundEngine, int BufferSize)
        {

            
            short BlockAlign = (short)(_numChannels  * (_BytesPerSample));
            long AverageBytesPerSecond = _SamplesPerSecond * BlockAlign;
           // byte[] Buffer = new byte[(int)(FrameTime*AverageBytesPerSecond) ];
            SoundEngine.SetupBuffer( (int)BufferSize   ,  _numChannels, _BytesPerSample, _AudioSampleType , _SamplesPerSecond);
        }
        public void InitializeSound(SoundEngines.SoundEngine SoundEngine, double  BufferTime)
        {


            short BlockAlign = (short)(_numChannels * (_BytesPerSample));
            long AverageBytesPerSecond = _SamplesPerSecond * BlockAlign;
            // byte[] Buffer = new byte[(int)(FrameTime*AverageBytesPerSecond) ];
            SoundEngine.SetupBuffer(BufferTime, _numChannels, _BytesPerSample, _AudioSampleType, _SamplesPerSecond);
        }
        
    }
}
