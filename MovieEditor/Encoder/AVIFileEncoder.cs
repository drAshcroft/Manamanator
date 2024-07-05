using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using AviFile;
using System.Media;
using System.IO;
using System.Windows.Forms;

using System.Reflection;
using System.Diagnostics;

namespace MovieEditor.Encoder
{
    public class AVIFileEncoder:IEncoder 
    {
        private CoreAV.MainAVHandler MainAVHandler;
        public void SetMainAV(CoreAV.MainAVHandler MainAV)
        {
            MainAVHandler = MainAV;
        }
        public static void ConvertToAVI(string Filename,string FinalFilename)
        {
           string ShellString = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\ffmpeg\\ffmpeg.exe" ;
           string ShellArgs = " -y -i \"" + Filename + "\" -an ";
           
           ShellArgs += "\"" + FinalFilename  + "\"";
           Process p= Process.Start(new ProcessStartInfo(ShellString, ShellArgs));
           p.WaitForExit();
          // System.Diagnostics.Debug.Print(ShellString + ShellArgs);
        }
        public static void ConvertToWav(string Filename,string FinalFilename)
        {
           
           string ShellString = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\ffmpeg\\ffmpeg.exe" ;
           string ShellArgs = " -y -i \"" + Filename 
               + "\" ";
           ShellArgs += "-acodec pcm_s16le -ab 128 -ar 44100 -ac 1  ";
           ShellArgs += "\"" + FinalFilename  + "\"";
           Process p= Process.Start(new ProcessStartInfo(ShellString, ShellArgs));
           p.WaitForExit();
          // System.Diagnostics.Debug.Print(ShellString + ShellArgs);
        }
        private void ConvertToFinalFormat(string Filename, CoreAV.PlayPlan Plan)
        {

           
           string ShellString = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\ffmpeg\\ffmpeg.exe" ;
           string ShellArgs = " -y -i \"" + Filename 
               + "\" ";
           if (Plan.FinalFormat.AudioCodec !="")
               ShellArgs += "-acodec " + Plan.FinalFormat.AudioCodec + " ";
           if (Plan.FinalFormat.TargetFormat != "")
               ShellArgs += "-f " + Plan.FinalFormat.TargetFormat + " ";
           ShellArgs += "\"" + Plan.FinalFormat.DestinationFile + "\"";
           Process p= Process.Start(new ProcessStartInfo(ShellString, ShellArgs));
           p.WaitForExit();
           System.Diagnostics.Debug.Print(ShellString + ShellArgs);
        }
        public void EncodePlayPlan(CoreAV.PlayPlan Plan)
        {
            MainAVHandler.InitializeSoundForPlay(5000);
            string fPath = Generals.TempPathName();
            try { File.Delete(fPath + "\\temp.avi"); }            catch { }
            try { File.Delete(fPath + "\\temp.wav"); }            catch { }
            
            AviManager aviManager = new AviManager( fPath + "\\temp.avi", false);
            double NextDeltaT = 0;
            Bitmap NextImage=null;
            int j = 0;
            while (NextImage == null)
            {
                NextImage = Plan.GetFrame(j, out NextDeltaT);
                j++;
            }
            VideoStream aviStream = aviManager.AddVideoStream(false,MainAVHandler.currentProject.FrameRate , NextImage );
            double FrameTime=1/MainAVHandler.currentProject.FrameRate ;
            int FrameCount = Plan.FrameCount();

            FileStream fs = new FileStream(fPath + "\\temp.wav", FileMode.Create, FileAccess.Write);

            MainAVHandler.soundEngine.CreateWaveForSave (fs, (short)(8 * MainAVHandler.currentProject.BytesPerSample), (short)MainAVHandler.currentProject.NumChannels, (int)MainAVHandler.currentProject.SamplesPerSecond);

            for (int i = 0; i < FrameCount; i++)
            {
                
                NextImage = Plan.GetFrame(i, out NextDeltaT);
               // MainAVHandler.DefaultViewer.ShowFrame(NextImage, i * FrameTime, i, FrameCount);

                byte[] Audio= Plan.GetFrameAudioBuffer(i, NextDeltaT );
                //ms.Write(Audio,0,Audio.Length );
                MainAVHandler.soundEngine.WriteWaveChunktoSave(ref Audio);


                if (NextImage != null)
                {
                    aviStream.AddFrame(NextImage);
                    if (NextImage != MainAVHandler.currentProject.BlankFrame )
                         NextImage.Dispose();
                }

            }
            MainAVHandler.soundEngine.CloseWaveFile();
            fs.Close();

            aviManager.AddAudioStream(fPath+ "\\temp.wav", 0);
           
            aviManager.Close();

            ConvertToFinalFormat(fPath + "\\temp.avi", Plan);
        }
    }
}
