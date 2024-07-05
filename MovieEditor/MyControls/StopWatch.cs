using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MovieEditor.MyControls
{
    public class StopWatch
    {
        System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
      

      
        public void StartStopWatch()
        {
            stopWatch.Reset();
            stopWatch.Start();
        }
        public void StopStopWatch()
        {
            
            stopWatch.Stop();
            //stopWatch.Reset();
        }
        public double GetStopWatchValue()
        {
            return (double)stopWatch.ElapsedMilliseconds / 1000;
        }
        public long GetStopWatchInterval()
        {
            return stopWatch.ElapsedMilliseconds;
        }


    }
}
