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
using Tao.OpenAl;

namespace MovieEditor.TimeLine
{
    public partial class AudioVisualize : UserControl
    {
        private CoreAV.AudioChunk MyChunk;
        private double [] Envelope;
        
        public AudioVisualize()
        {
            InitializeComponent();
        }
        
        public void AddData(CoreAV.AudioChunk audioChunk)
        {
            MyChunk = audioChunk;
            this.Refresh();
        }
       
        protected override void OnPaint(PaintEventArgs e)
        {
            if (MyChunk == null)
                return;
            if (Envelope == null || Envelope.Length < (this.Width * 2))
            {
                Envelope = MyChunk.MakeEnvelope(this.Width);
            
            }
            if (Envelope != null)
            {
                Point LP = new Point(0, 0);
                Point P=new Point(0,0);
                for (int i = 0; i < this.Width; i++)
                {
                    P.X = i;
                    P.Y = (int)(Envelope[2 * i] * this.Height  );
                    LP.X = i;
                    LP.Y = (int)(Envelope[2 * i + 1] * this.Height);
                    e.Graphics.DrawLine(Pens.GreenYellow, LP, P);

                }
            }
            base.OnPaint(e);
        }
    }

   
}
