using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace MovieEditor
{
    public class CursorHandling
    {
        public struct IconInfo
        {
            public bool fIcon;
            public int xHotspot;
            public int yHotspot;
            public IntPtr hbmMask;
            public IntPtr hbmColor;
        }
        [DllImport("user32.dll")]
        public static extern IntPtr CreateIconIndirect(ref IconInfo icon);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetIconInfo(IntPtr hIcon, ref IconInfo pIconInfo);

        private static Cursor LoadCursor(Bitmap bmp, int xHotSpot, int yHotSpot)
        {
            IconInfo tmp = new IconInfo();
            GetIconInfo(bmp.GetHicon(), ref tmp);
            tmp.xHotspot = xHotSpot;
            tmp.yHotspot = yHotSpot;
            tmp.fIcon = false;
            return new Cursor(CreateIconIndirect(ref tmp));
        }
        [DllImport("user32.dll")]
        static extern IntPtr LoadCursorFromFile(string lpFileName);

        public static Cursor LoadCursor(string Filename)
        {
            IntPtr ptr = LoadCursorFromFile(Filename);
            return new Cursor(ptr);
        }
        
        public static Cursor LoadCursor(Stream BitmapStream,int xHotSpot,int yHotSpot)
        {
           
            Bitmap b = new Bitmap(BitmapStream);
            return  LoadCursor(b, xHotSpot, yHotSpot);
        }

        public static Cursor LoadDifficultCursor(Type type,string ResourceName )
        {
            try
            {
                return  new Cursor(type , ResourceName );
        
            }
            catch
            {
                Assembly executingAssembly = Assembly.GetExecutingAssembly();

               // this is to help debug stupid errors.  Most common is forgetting
                // to set the resource name as embedded.
                //string[] resources = executingAssembly.GetManifestResourceNames();

                System.IO.Stream stream = executingAssembly.GetManifestResourceStream(
                    type.Namespace.ToString() + "." + ResourceName );
           
                byte[] buf = new byte[stream.Length];
                stream.Read(buf, 0, buf.Length);
                stream.Close();
                string Filename =Generals.TempFilename()  + ".cur" ;
                FileStream fs = new FileStream(Filename  , FileMode.CreateNew);
                BinaryWriter bw = new BinaryWriter(fs);
                bw.Write(buf);
                bw.Close();
                fs.Close();
                return  LoadCursor(Filename);
            }
        }
         
    }
}
