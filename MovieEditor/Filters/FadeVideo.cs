using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MovieEditor.CoreAV;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MovieEditor.Filters
{
    [FilterProps("Fade Video","Fades video to White or Black","0.1")]
    public class FadeVideo:Filters.IFilter 
    {
        private double _StartTime;
        private double _EndTime;
        private Dictionary<string, string> _Properties = new Dictionary<string, string>();

        public FadeVideo()
        {
            _MyControl = new Glass.GlassButton();// MyControls.ShapeControl.ShapeControl();

            _MyControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            //_MyControl.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(61)))), ((int)(((byte)(255)))), ((int)(((byte)(28)))));
            //_MyControl.BorderStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            //_MyControl.BorderWidth = 3;
            //_MyControl.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(142)))), ((int)(((byte)(101)))), ((int)(((byte)(0)))));
            
            _MyControl.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            _MyControl.Location = new System.Drawing.Point(153, 92);
            _MyControl.Name = "label3";
            //_MyControl.Shape = MovieEditor.MyControls.ShapeControl.ShapeType.RoundedRectangle;
            _MyControl.Size = new System.Drawing.Size(135, 59);
            //_MyControl.SurroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            _MyControl.TabIndex = 9;
            //_MyControl.UseGradient = false ;

            _Properties.Add("FadeColor", "Black");
        }
        private Color FadeColor = Color.Black ;
        public string FilterName
        {
            get { return "Fade Video"; }
        }
        public string FilterDescription
        {
            get { return "Fades video to White or Black"; }
        }
        public double ClipStartTime
        {
            get { return _StartTime; }
            set { _StartTime = value; }
        }
        /// <summary>
        /// Used only for filters that move from one track to another
        /// this shows where the transition is from one to another.
        /// </summary>
        public double ClipMidTime
        {
            get { return 0; }
            set {;}
        }
        public double ClipEndTime
        {
            get { return _EndTime; }
            set { _EndTime = value; }
        }
        //private MyControls.ShapeControl.ShapeControl  _MyControl;
        private System.Windows.Forms.Control  _MyControl;
        public System.Windows.Forms.Control GUI
        {
            get { return _MyControl;}
            set { _MyControl = value; }

        }
        public eFilterType FilterType
        {
            get { return (eFilterType.End); }
        }
        public Bitmap TransformFrame(Bitmap InputFrame, double GlobalTime, vidFrameIndex[] FrameList)
        {
            int Darkness =(int)(255* (GlobalTime - _StartTime)/(_EndTime -_StartTime )) ;
            if (FadeColor == Color.Black)
                Darkness = 255 - Darkness;
            AdjustBrightnessMatrix(InputFrame, Darkness);
            return InputFrame;
        }

        private static void AdjustBrightnessMatrix(Bitmap img, int value)
        {

            if (value == 0) // No change, so just return

                return;



            float sb = (float)value / 255F;

            float[][] colorMatrixElements =

                  {

                        new float[] {1,  0,  0,  0, 0},

                        new float[] {0,  1,  0,  0, 0},

                        new float[] {0,  0,  1,  0, 0},

                        new float[] {0,  0,  0,  1, 0},

                        new float[] {sb, sb, sb, 1, 1}

                  };



            ColorMatrix cm = new ColorMatrix(colorMatrixElements);

            ImageAttributes imgattr = new ImageAttributes();

            Rectangle rc = new Rectangle(0, 0, img.Width, img.Height);

            Graphics g = Graphics.FromImage(img);

            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            imgattr.SetColorMatrix(cm);

            g.DrawImage(img, rc, 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, imgattr);



            //Clean everything up

            imgattr.Dispose();

            g.Dispose();

        }
        public Dictionary<string, string> GetFilterProperties()
        {
            return _Properties;
        }
        public void SetFilterProperties(Dictionary<string, string> Properties)
        {
            this._Properties = Properties;
            try
            {
                if (_Properties["FadeColor"]=="Black")
                    FadeColor=Color.Black;
                else
                    FadeColor=Color.White ;
            }
            catch { }
        }

        private IFilterPropertiesGui  _PropertiesGui =(IFilterPropertiesGui) new FadeVideoProperties();
        public UserControl   FilterPropertiesGui
        {
            get { return _PropertiesGui.GetGui()  ;}
        }
        public void SetFilterProp(string Propname, string PropValue)
        {
            try
            {

                _Properties.Remove(Propname);
            }
            catch
            { }

            _Properties.Add(Propname ,PropValue );
            try
            {
                if (_Properties["FadeColor"] == "Black")
                    FadeColor = Color.Black;
                else
                    FadeColor = Color.White;
            }
            catch { }
        }

        public IFilter Clone()
        {
            FadeVideo fv = new FadeVideo();
            Dictionary<string, string> FilterProps = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> kvp in _Properties)
                FilterProps.Add(kvp.Key, kvp.Value);
            fv.SetFilterProperties(FilterProps);

            return fv;
        }
    }
}
