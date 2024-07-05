using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using System.Threading;


namespace MovieEditor.TimeLine
{
    public class AudioTrack:Track 
    {
        private CoreAV.AudioChunk audioChunk;
        

        public AudioTrack(CoreAV.AudioChunk AC,double SecondsPerPixel):base ()
        {
             
            InitializeComponent();
            this.SecondsPerPixel = SecondsPerPixel;
            audioChunk = AC;
            _highLightColor = Color.Blue;
        }
        public override  CoreAV.Chunk BaseChunk
        {
            get { return (CoreAV.Chunk)audioChunk; }
        }

        private double[] Envelope;
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // AudioTrack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "AudioTrack";
            this.ResumeLayout(false);

        }


        public override void DrawVis()
        {
            if (audioChunk == null)
                return;
            if (Envelope == null || Envelope.Length < (this.Width * 2))
            {
                Envelope = audioChunk.MakeEnvelope(this.Width);

                if (Envelope != null)
                {
                    Bitmap b = new Bitmap(this.Width, this.Height);
                    Graphics g = Graphics.FromImage(b);
                    g.Clear(Color.White);
                    Point LP = new Point(0, 0);
                    Point P = new Point(0, 0);
                    for (int i = 0; i < this.Width; i++)
                    {
                        P.X = i;
                        P.Y = (int)(Envelope[2 * i] * this.Height);
                        LP.X = i;
                        LP.Y = (int)(Envelope[2 * i + 1] * this.Height) + 1;
                        // g.DrawLine(Pens.Red ,new Point(i,0),new Point(i,this.Height ));
                       // System.Diagnostics.Debug.Print(P.X.ToString() + " " + P.Y.ToString() + " " + LP.X.ToString() + " " + LP.Y.ToString());
                        g.DrawLine(Pens.Red, LP, P);

                    }
                    //b.Save("c:\\test.bmp");
                    _BackGround = b;
                    Vis.Image = b;
                    Vis.Refresh();
                }
            }
            this.Refresh();

        }

        public override  void SetStartTime(double Second)
        {
            audioChunk.ChunkProperties.StartTime  = Second;
        }
        public override void SetPriority(int Priority)
        {
            audioChunk.Priority = Priority;
        }
    }
}
