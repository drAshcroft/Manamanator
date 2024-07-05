using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MovieEditor.CoreAV
{
    public abstract class Chunk
    {
        protected List<Filters.IFilter> _FilterList = new List<Filters.IFilter>();

        public List<Filters.IFilter> FilterList
        {
            get { return _FilterList; }
        }

        public List<Filters.IFilter> GetAllFilters()
        {
            return _FilterList;
        }

        public abstract  Chunk Clone();

        public class RawFileProperties
        {
            /// <summary>
            /// Contains the filename where the original information can be found
            /// </summary>
            public string Filename;
            /// <summary>
            /// This holds the offset time.  This is the time that the clip would start
            /// if the whole clip showed.  This is stored as an offset from the time that
            /// the shown clip is started
            /// </summary>
            public double StartOffsetTime;

            /// <summary>
            /// This is the total length of this clip that is available to be played
            /// </summary>
            public double TotalDuration;

            public RawFileProperties(string Filename, double StartOffsetTime, double TotalDuration)
            {
                this.Filename = Filename;
                this.StartOffsetTime = StartOffsetTime;
                this.TotalDuration = TotalDuration;

            }
            public RawFileProperties Clone()
            {
                RawFileProperties newRFP = new RawFileProperties(Filename, StartOffsetTime, TotalDuration);
                return newRFP;
            }
        }
        public class ClipProperties
        {
            /// <summary>
            /// The global start time for this clip. 
            /// </summary>
            public double StartTime;
            /// <summary>
            /// The global duration of this clip.
            /// </summary>
            public double Duration;
            /// <summary>
            /// Playbackspeed.  2 Means twice as fast;  .5 means twice as slow as the original
            /// </summary>
            public double SpeedRatio=1;
            public ClipProperties(double StartTime, double Duration, double SpeedRatio)
            {
                this.StartTime = StartTime;
                this.Duration = Duration;
                this.SpeedRatio = SpeedRatio;

            }
            public ClipProperties Clone()
            {
                ClipProperties cp = new ClipProperties(StartTime, Duration, SpeedRatio);
                return cp;
            }
            public double EndTime
            {
                get { return StartTime + Duration; }
            }
        }
        
        protected double GetInternalTime(double GlobalTime)
        {
            return GlobalTime-_ClipProperties.StartTime  + _RawFileProps.StartOffsetTime;
        }

        public abstract double GetCutTime(double Time);

        protected RawFileProperties _RawFileProps;
        protected ClipProperties _ClipProperties;
        public RawFileProperties  RawFileProps
        {
            get { return _RawFileProps ; }
            set { _RawFileProps = value; }
        }
        public ClipProperties ChunkProperties
        {
            get { return _ClipProperties; }
            set { _ClipProperties = value; }

        }
        public string ChunkName;
        //Total video stats
        protected ITrack LinkedTrack;
        public ITrack GuiTrack
        {
            get { return LinkedTrack; }
            set { LinkedTrack = value; }

        }

        protected int mPriority;

        public int Priority
        {
            get { return mPriority; }
            set { mPriority = value; }
        }
    }
}
