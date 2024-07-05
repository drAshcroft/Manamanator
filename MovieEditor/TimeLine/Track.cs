using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using MovieEditor.CoreAV;

namespace MovieEditor.TimeLine
{
   
    public abstract partial class Track : UserControl, CoreAV.ITrack 
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowDC(IntPtr hwnd);
        [DllImport("user32.dll")]
        private static extern int ReleaseDC(IntPtr hwnd, IntPtr hdc);

        private Color _borderColor = Color.Black;
        protected Color _highLightColor = Color.Red;
        private int _borderWidth = 1;
        protected Bitmap _BackGround = null;
        public int BorderWidth
        {
            get { return _borderWidth; }
            set { _borderWidth = value; }
        }

        public Color BorderColor
        {
            get { return _borderColor; }
            set { _borderColor = value; }
        }

        public Color HighLightColor
        {
            get { return _highLightColor; }
            set { _highLightColor = value; }
        }

        public void Highlight(bool HighlightOn)
        {
            if (HighlightOn == true)
            {
                _borderColor = _highLightColor;
                _borderWidth = 3;
            }
            else
            {
                _borderWidth = 1;
                _borderColor = Color.Black;
            }
            try
            {
                Bitmap b = new Bitmap(_BackGround);
                Graphics g = Graphics.FromImage(b);
                //g.DrawImageUnscaled(_BackGround, new Point(0, 0));
                g.DrawRectangle(new Pen(_borderColor, _borderWidth), new Rectangle(_borderWidth, _borderWidth, Vis.Width - 2 * _borderWidth, Vis.Height - 2 * _borderWidth));

                Vis.Image = b;
            }
            catch { }
            Vis.Refresh();

        }
        protected double  mSecondsPerPixel;

        private Track mLinkedTrack;

        public abstract CoreAV.Chunk BaseChunk
        {
            get;
        }

        public virtual  void ResetChunkProps()
        {
            double Secs = BaseChunk.ChunkProperties.Duration  / mSecondsPerPixel;
            this.Width = (int)Secs; ;


        }

        public CoreAV.ITrack LinkedTrack
        {
            set { mLinkedTrack =(Track) value; }
            get { return mLinkedTrack; }
        }
        public double SecondsPerPixel
        {
            set { mSecondsPerPixel = value; }

        }
        public Track()
        {
            InitializeComponent();
          /*  SetStyle(ControlStyles.DoubleBuffer,                    true);
            SetStyle(ControlStyles.AllPaintingInWmPaint,           false);
            SetStyle(ControlStyles.ResizeRedraw,                    true);
            SetStyle(ControlStyles.UserPaint,                       true);
            SetStyle(ControlStyles.SupportsTransparentBackColor,    true);
            */
        }

        private void Vis_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
        }

        private void Vis_MouseMove(object sender, MouseEventArgs e)
        {
            base.OnMouseMove(e);
        }

        private void Vis_MouseUp(object sender, MouseEventArgs e)
        {
            base.OnMouseUp(e);
        }

        public abstract void SetStartTime(double Second);

        public abstract void DrawVis();

        public abstract void SetPriority(int Priority);

        public void AddFilter(Filters.IFilter filter)
        {
            BaseChunk.FilterList.Add(filter);
            ResetFilterControls();
            
        }
        public void ResetFilterControls()
        {

            List<System.Windows.Forms.Control> Controls = new List<System.Windows.Forms.Control>();
            double SecondsToPixel =1/ mSecondsPerPixel ;
            
            foreach (Filters.IFilter filter in BaseChunk.FilterList   )
            {
                filter.GUI.Top = this.Top;
                filter.GUI.Height = this.Height;
                int Width = (int)((filter.ClipEndTime-filter.ClipStartTime  ) * SecondsToPixel);
                if (filter.FilterType == Filters.eFilterType.Start)
                {
                    filter.GUI.Left = this.Left;
                    filter.GUI.Width = Width;
                }
                if (filter.FilterType == Filters.eFilterType.End)
                {
                    filter.GUI.Left = this.Right - Width;
                    filter.GUI.Width = Width;
                }
                else
                {
                    int start = (int)((filter.ClipStartTime) * SecondsToPixel);
                    filter.GUI.Left = start;
                    filter.GUI.Width = Width;
                }
                Controls.Add(filter.GUI);
            }

            
        }

       /* protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (this.BorderStyle == BorderStyle.FixedSingle)
            {
               // Graphics g= Vis.CreateGraphics();
               // g.DrawRectangle(new Pen(_borderColor, _borderWidth), new Rectangle(_borderWidth , _borderWidth , Vis.Width-2*_borderWidth , Vis.Height-2*_borderWidth ));
                //g.Clear(_borderColor);

                //g.Dispose();
                IntPtr hDC = GetWindowDC(this.Handle);
                Graphics g = Graphics.FromHdc(hDC);
                g.DrawImageUnscaled(_BackGround, new Point(0, 0));
               
                ControlPaint.DrawBorder(
                    g,
                    new Rectangle(0, 0, this.Width, this.Height),
                    _borderColor,
                    _borderWidth,
                    ButtonBorderStyle.Solid,
                    _borderColor,
                    _borderWidth,
                    ButtonBorderStyle.Solid,
                    _borderColor,
                    _borderWidth,
                    ButtonBorderStyle.Solid,
                    _borderColor,
                    _borderWidth,
                    ButtonBorderStyle.Solid);
                g.Dispose();
                ReleaseDC(Handle, hDC);
            }
        }
*/
        

    }
}
