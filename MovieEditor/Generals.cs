using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
namespace MovieEditor
{
    public class Generals
    {
        static Random r = new Random((int)DateTime.Now.ToFileTimeUtc());
        public static string GetRandomString(int numChars)
        {

            return System.IO.Path.GetFileNameWithoutExtension(Path.GetTempFileName()); ;
        }
        public static string TempPathName()
        {
            string TempPath = System.IO.Path.GetDirectoryName(
              Application.ExecutablePath ) + "\\ScratchFolder";
            if (Directory.Exists(TempPath) == false)
            {
                Directory.CreateDirectory(TempPath);
            }

            return TempPath;
        }
        public static string TempFilename()
        {
            string TempPath = TempPathName() + "\\" + System.IO.Path.GetFileNameWithoutExtension(Path.GetTempFileName());
           
            return TempPath;
        }
        public static void ClearTempFiles()
        {
            string TempPath = TempPathName();
            string[] Files=null;
            Files  = Directory.GetFiles(TempPath);
            for (int i = 0; i < Files.Length; i++)
            {
                try
                {
                    File.Delete(Files[i]);
                }
                catch { }
            }
            
        }
    }
}
