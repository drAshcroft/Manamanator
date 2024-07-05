using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.Threading;

namespace MovieEditor.VideoViewer
{
    public partial class Viewer : DockContent  ,IViewer 
    {
        public Viewer()
        {
            InitializeComponent();
        }

        private int CurrentFrame = 0;
        private int CurrentAudioFrame = 0;
        private int TotalFrames = 100;
        CoreAV.PlayPlan playPlan = null;

        private Bitmap NextImage = null;
        private double NextDeltaT = 0;

        public event PlayMovieRequest PlayMovie;
        public event MoveFrameRequest MoveFrame;
        public event StopMovieRequest StopMovie;
        CoreAV.MainAVHandler MainAV;

        public void SetAVCore(CoreAV.MainAVHandler MainAV)
        {
            
            this.MainAV = MainAV;
        }
        public void ShowFrame(Bitmap frame, double Second, int FrameN, int TotalFrames)
        {
            this.Text = Second.ToString();
            pictureBox1.Image = frame;
            pictureBox1.Refresh();
            CurrentFrame = FrameN;
            this.TotalFrames = TotalFrames;
            FrameSelector.Maximum = TotalFrames;
            FrameSelector.Value = FrameN;
        }


        private int AudioFramePreview = 100;
        private double FrameTime=1/24;
        MyControls.StopWatch sw = new MovieEditor.MyControls.StopWatch();
        private int PlayOffset = 0;
        private void BPlay_Click(object sender, EventArgs e)
        {
            double deltaT = 0;
            playPlan= MainAV.GetPlayPlan();
            TotalFrames = playPlan.FrameCount();
            if (CurrentFrame < 0) CurrentFrame = 0;
            pictureBox1.Image= playPlan.GetFrame(CurrentFrame ,out deltaT);
            FrameTime = deltaT;
            pictureBox1.Refresh();
            MainAV.InitializeSoundForPlay(deltaT * AudioFramePreview );
            //if (PlayMovie != null) PlayMovie(this,playPlan );

         //   CurrentFrame = 0;
         //   FrameSelector.Maximum = TotalFrames;
           

         //   CurrentFrame = 1;
            CurrentFrame++;
            
            NextImage = playPlan.GetFrame(CurrentFrame, out NextDeltaT);

            for (int i=0;i<AudioFramePreview ;i++)
                playPlan.PlayFrameAudio(i+CurrentFrame , deltaT);

            playPlan.PlayFrameAudio(CurrentFrame+ AudioFramePreview + 1, deltaT);
            CurrentAudioFrame = CurrentFrame +  AudioFramePreview + 1;
            MainAV.soundEngine.Play();
            PlayOffset = CurrentFrame;
            PlayTimer.Interval = (int)(deltaT * 1000 + 1);
            //PlayAudioTimer.Interval = (int)(deltaT * 1000 + 1);
            SleepInterval =(int)(deltaT * 1000 + 1);
            PlayTimer.Enabled = true;
            //PlayAudioTimer.Enabled = true;
            RunSound();
            sw.StartStopWatch();
             
        }
        private int SleepInterval;
        Thread AudioThread;
        private bool KillSound=false ;
        private void RunSound()
        {
            KillSound = false;
            AudioThread = new Thread(delegate()
                {
                    while (!KillSound )
                    {
                        
                        
                            CurrentAudioFrame++;
                            if ((CurrentAudioFrame + 1) <= TotalFrames)
                                playPlan.PlayFrameAudio(CurrentAudioFrame, FrameTime);
                            else
                            {
                                CurrentAudioFrame = 0;
                                playPlan.PlayFrameAudio(CurrentAudioFrame, FrameTime);
                            }
                        
                        Thread.Sleep(SleepInterval);
                    }
                    System.Diagnostics.Debug.Print("Stopping audio");


                });
            AudioThread.Start();
        }

        #region InterfaceControls
        private void bStop_Click(object sender, EventArgs e)
        {
            KillSound = true;
            MainAV.soundEngine.Stop();
            PlayTimer.Enabled = false;
            PlayAudioTimer.Enabled = false;
            
            sw.StopStopWatch();
            AudioThread.Abort();
            if (StopMovie != null) StopMovie(this);
            if (MoveFrame != null) MoveFrame(this, CurrentFrame,false );
        }

        private void bFrameUp_Click(object sender, EventArgs e)
        {
            if ( (CurrentFrame+1)<TotalFrames )
                if (MoveFrame != null) MoveFrame(this, CurrentFrame + 1,false );
        }

        private void bFrameDown_Click(object sender, EventArgs e)
        {
            if ((CurrentFrame-1)>0)
                if (MoveFrame != null) MoveFrame(this, CurrentFrame - 1,false );
        }

        private void FrameSelector_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.NewValue != CurrentFrame)
            {
                if (PlayTimer.Enabled)
                {
                    // CurrentFrame = e.NewValue;
                }
                else //if (FrameSelector.Value != e.NewValue )
                {
                    if (FrameSelector.Maximum == 10000)
                    {
                        int FrameNumber = 0;
                        int TotalFrames = 0;
                        double d=e.NewValue /10000;
                        MainAV.GetFrame(0, out FrameNumber, out TotalFrames);
                        FrameSelector.Maximum = TotalFrames;
                        FrameSelector_Scroll(sender, new ScrollEventArgs(ScrollEventType.First, (int)(TotalFrames * d)));
                        return;
                    }
                    CurrentFrame = e.NewValue;
                    if (MoveFrame != null) MoveFrame(this, e.NewValue,false );
                }
            }
        }
        #endregion

        #region PlayMovieHandling
        private void PlayTimer_Tick(object sender, EventArgs e)
        {
            double tt=sw.GetStopWatchValue();
            CurrentFrame =(int)( tt / FrameTime) + PlayOffset ;
            //System.Diagnostics.Debug.Print(tt.ToString () + ", " + CurrentFrame.ToString());
            if (MoveFrame != null) MoveFrame(this, CurrentFrame, true);
            if (CurrentFrame >= TotalFrames)
            {
                sw.StopStopWatch();
                sw.StartStopWatch();
                CurrentFrame = 0;
                PlayOffset = 0;
            }
            pictureBox1.Image = NextImage;
            FrameSelector.Value = CurrentFrame;
            
            double deltaT = 0;
            
            NextImage  = playPlan.GetFrame(CurrentFrame , out deltaT);
            pictureBox1.Refresh();
            FrameSelector.Value = CurrentFrame;
            
        }

        
        #endregion

        private void Viewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                MainAV.soundEngine.Stop();
            }
            catch { }
            try
            {
                if (AudioThread != null)
                    AudioThread.Abort();
            }
            catch { }
        }

        private void PlayAudioTimer_Tick(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.Print(sw.GetStopWatchValue().ToString());
            CurrentAudioFrame++;
            if ((CurrentAudioFrame + 1) <= TotalFrames)
                playPlan.PlayFrameAudio(CurrentAudioFrame, FrameTime);
            else
            {
                CurrentAudioFrame = 0;
                playPlan.PlayFrameAudio(CurrentAudioFrame , FrameTime);
            }

        }

    }
}
