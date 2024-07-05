using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MovieEditor
{
    public partial class ExportOptions : Form
    {
        CoreAV.PlayPlan PlayPlan;

        public void SetPlayPlan(CoreAV.PlayPlan Plan)
        {
            PlayPlan = Plan;
        }
        public ExportOptions()
        {
            InitializeComponent();
        }

        private void ExportOptions_Load(object sender, EventArgs e)
        {
            ComboBoxDefinedFormat.Items.Clear();
           
            ComboBoxDefinedFormat.Items.Add("Flash (4:3 480x360)");
            ComboBoxDefinedFormat.Items.Add("Flash (4:3 640x480)");
            ComboBoxDefinedFormat.Items.Add("Flash (15:8 480x272)");
            ComboBoxDefinedFormat.Items.Add("Flash (15:8 750x400)");
            ComboBoxDefinedFormat.Items.Add("Flash (16:9 640x360)");
            ComboBoxDefinedFormat.Items.Add("Flash (Custom Size)");
            ComboBoxDefinedFormat.Items.Add("Flash (Input Size/Aspect Ratio)");
            /*ComboBoxDefinedFormat.Items.Add("AVI (MPEG4 DivX HQ 320x240)");
            ComboBoxDefinedFormat.Items.Add("AVI (MPEG4 DivX 320x240)");
            ComboBoxDefinedFormat.Items.Add("MP3 Sound (MP3)");
            ComboBoxDefinedFormat.Items.Add("MP4 (4:3 480x360)");
            ComboBoxDefinedFormat.Items.Add("MP4 (4:3 640x480)");
            ComboBoxDefinedFormat.Items.Add("MP4 (15:8 480x272)");
            ComboBoxDefinedFormat.Items.Add("MP4 (15:8 750x400)");
            ComboBoxDefinedFormat.Items.Add("MP4 (16:9 640x360)");
            ComboBoxDefinedFormat.Items.Add("MP4 (Custom Size)");
            ComboBoxDefinedFormat.Items.Add("MP4 (Input Size/Aspect Ratio)");
            ComboBoxDefinedFormat.Items.Add("Zune MPEG-4 Video 320x240");
            ComboBoxDefinedFormat.Items.Add("Custom Format");*/

            ComboBoxDefinedFormat.Items.Add("MPEG1 (4:3 480x360)");
            ComboBoxDefinedFormat.Items.Add("MPEG1 (4:3 640x480)");
            ComboBoxDefinedFormat.Items.Add("MPEG1 (15:8 480x272)");
            ComboBoxDefinedFormat.Items.Add("MPEG1 (15:8 750x400)");
            ComboBoxDefinedFormat.Items.Add("MPEG1 (16:9 640x360)");
            ComboBoxDefinedFormat.Items.Add("MPEG1 (Custom Size)");
            ComboBoxDefinedFormat.Items.Add("MPEG1 (Input Size/Aspect Ratio");
            ComboBoxDefinedFormat.Items.Add("QuickTime MOV (4:3 480x360)");
            ComboBoxDefinedFormat.Items.Add("QuickTime MOV (4:3 640x480)");
            ComboBoxDefinedFormat.Items.Add("QuickTime MOV (15:8 480x272)");
            ComboBoxDefinedFormat.Items.Add("QuickTime MOV (15:8 750x400)");
            ComboBoxDefinedFormat.Items.Add("QuickTime MOV (16:9 640x360)");
            ComboBoxDefinedFormat.Items.Add("QuickTime MOV (Custom Size)");
            ComboBoxDefinedFormat.Items.Add("QuickTime MOV (Input Size/Aspect Ratio)");
            ComboBoxDefinedFormat.Items.Add("WAVE Sound (WAV)");
            ComboBoxDefinedFormat.Items.Add("Windows Media WMV (4:3 480x360)");
            ComboBoxDefinedFormat.Items.Add("Windows Media WMV (4:3 640x480)");
            ComboBoxDefinedFormat.Items.Add("Windows Media WMV (15:8 480x272)");
            ComboBoxDefinedFormat.Items.Add("Windows Media WMV (15:8 750x400)");
            ComboBoxDefinedFormat.Items.Add("Windows Media WMV (16:9 640x360)");
            ComboBoxDefinedFormat.Items.Add("Windows Media WMV (Custom Size)");
            ComboBoxDefinedFormat.Items.Add("Windows Media WMV (Input Size/Aspect Ratio)");
            
        }

        private void CheckBoxOpenOutputFolderAfterConversion_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void ButtonBrowseOutput_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            TextBoxOutputFolder.Text = Path.GetDirectoryName(saveFileDialog1.FileName);
            TextBoxOutputFileName.Text = Path.GetFileNameWithoutExtension(saveFileDialog1.FileName);

        }
        private string SetSize(string size)
        {

            string[] sizeArray = size.Split(new char[] {  'x'});
            int width; 
            int height; 
            int.TryParse(sizeArray[0],out width);
            int.TryParse(sizeArray[1],out height );
            string returnValue = "";

          
                // Just return the input string.
            returnValue = size;
            


            return returnValue;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string formatType=(string) ComboBoxDefinedFormat.SelectedItem ;            
            string outFileExt = "";
            string         VideoCodec = "";
            string  AudioCodec="";
            string         VideoBitrate = "";
            string         VideoFrameRate = "";
            string         VideoSize = "";
            string         AudioBitrate = "";
            string         AudioChannels = "";
            string         AudioSamples = "";
            string         AudioVolume = "";
            string   ForceFormat="";
            string VideoAspectRatio="";
            string TargetFormat="";
            bool         Deinterlace = true;
            bool         SameQuality = true;
            switch (formatType) {
                case "WAVE Sound (WAV)":
                    outFileExt = ".wav";
                    AudioCodec = "pcm_s16le";
                    AudioBitrate = "128";
                    AudioChannels = "2";
                    AudioSamples = "41000";
                    AudioVolume = "256";
                    ForceFormat = "wav";
                    break;
                case "MP3 Sound (MP3)":
                    outFileExt = ".mp3";
                    AudioCodec = "mp3";
                    AudioBitrate = "256";
                    AudioChannels = "2";
                    AudioSamples = "44100";
                    AudioVolume = "256";
                    ForceFormat = "mp3";
                    break;
                case "Zune MPEG-4 Video 320x240":
                    outFileExt = ".mp4";
                    VideoCodec = "mpeg4";
                    VideoBitrate = "1500";
                    VideoFrameRate = "29.97";
                    VideoSize = SetSize("320x240");
                    //AudioCodec = "aac"
                    AudioBitrate = "128";
                    AudioChannels = "2";
                    AudioSamples = "48000";
                    AudioVolume = "256";
                    Deinterlace = true;
                    SameQuality = true;
                    break;
               
                case "QuickTime MOV (4:3 640x480)":
                    outFileExt = ".mov";
                    VideoFrameRate = "29.97";
                    VideoBitrate = "360";
                    VideoSize = SetSize("640x480");
                    AudioBitrate = "48";
                    AudioChannels = "2";
                    AudioSamples = "44100";
                    AudioVolume = "256";
                    Deinterlace = true;
                    SameQuality = true;
                    break;
                case "QuickTime MOV (15:8 480x272)":
                    outFileExt = ".mov";
                    VideoFrameRate = "29.97";
                    VideoBitrate = "360";
                    VideoSize = SetSize("480x272");
                    AudioBitrate = "48";
                    AudioChannels = "2";
                    AudioSamples = "44100";
                    AudioVolume = "256";
                    Deinterlace = true;
                    SameQuality = true;
                    break;
                case "QuickTime MOV (15:8 750x400)":
                    outFileExt = ".mov";
                    VideoFrameRate = "29.97";
                    VideoBitrate = "360";
                    VideoSize = SetSize("750x400");
                    AudioBitrate = "48";
                    AudioChannels = "2";
                    AudioSamples = "44100";
                    AudioVolume = "256";
                    Deinterlace = true;
                    SameQuality = true;
                    break;
                case "QuickTime MOV (4:3 480x360)":
                    outFileExt = ".mov";
                    VideoFrameRate = "29.97";
                    VideoBitrate = "360";
                    VideoSize = SetSize("480x360");
                    AudioBitrate = "48";
                    AudioChannels = "2";
                    AudioSamples = "44100";
                    AudioVolume = "256";
                    Deinterlace = true;
                    SameQuality = true;
                    break;
                case "QuickTime MOV (16:9 640x360)":
                    outFileExt = ".mov";
                    VideoFrameRate = "29.97";
                    VideoBitrate = "360";
                    VideoSize = SetSize("640x360");
                    AudioBitrate = "48";
                    AudioChannels = "2";
                    AudioSamples = "44100";
                    AudioVolume = "256";
                    Deinterlace = true;
                    SameQuality = true;
                    break;
                case "QuickTime MOV (Custom Size)":
                    outFileExt = ".mov";
                    VideoFrameRate = "29.97";
                    VideoBitrate = "360";
                    VideoSize = SetSize("480x360");
                    AudioBitrate = "48";
                    AudioChannels = "2";
                    AudioSamples = "44100";
                    AudioVolume = "256";
                    Deinterlace = true;
                    SameQuality = true;
                    break;
                case "QuickTime MOV (Input Size/Aspect Ratio)":
                    outFileExt = ".mov";
                    VideoFrameRate = "29.97";
                    VideoBitrate = "360";
                    AudioBitrate = "48";
                    AudioChannels = "2";
                    AudioSamples = "44100";
                    AudioVolume = "256";
                    Deinterlace = true;
                    SameQuality = true;
                    break;
                case "Windows Media WMV (4:3 480x360)":
                    outFileExt = ".wmv";
                    VideoCodec = "wmv2";
                    VideoSize = SetSize("480x360");
                    AudioChannels = "2";
                    AudioBitrate = "64";
                    AudioSamples = "44100";
                    AudioVolume = "256";
                    Deinterlace = true;
                    SameQuality = true;
                    break;
                case "Windows Media WMV (4:3 640x480)":
                    outFileExt = ".wmv";
                    VideoCodec = "wmv2";
                    VideoSize = SetSize("640x480");
                    AudioChannels = "2";
                    AudioBitrate = "64";
                    AudioSamples = "44100";
                    AudioVolume = "256";
                    Deinterlace = true;
                    SameQuality = true;
                    break;
                case "Windows Media WMV (15:8 480x272)":
                    outFileExt = ".wmv";
                    VideoCodec = "wmv2";
                    VideoSize = SetSize("480x272");
                    AudioChannels = "2";
                    AudioBitrate = "64";
                    AudioSamples = "44100";
                    AudioVolume = "256";
                    Deinterlace = true;
                    SameQuality = true;
                    break;
                case "Windows Media WMV (15:8 750x400)":
                    outFileExt = ".wmv";
                    VideoCodec = "wmv2";
                    VideoSize = SetSize("750x400");
                    AudioChannels = "2";
                    AudioBitrate = "64";
                    AudioSamples = "44100";
                    AudioVolume = "256";
                    Deinterlace = true;
                    SameQuality = true;
                    break;
                case "Windows Media WMV (16:9 640x360)":
                    outFileExt = ".wmv";
                    VideoCodec = "wmv2";
                    VideoSize = SetSize("640x360");
                    AudioChannels = "2";
                    AudioBitrate = "64";
                    AudioSamples = "44100";
                    AudioVolume = "256";
                    Deinterlace = true;
                    SameQuality = true;
                    break;
                case "Windows Media WMV (Custom Size)":
                    outFileExt = ".wmv";
                    VideoCodec = "wmv2";
                    VideoSize = SetSize("480x360");
                    AudioChannels = "2";
                    AudioBitrate = "64";
                    AudioSamples = "44100";
                    AudioVolume = "256";
                    Deinterlace = true;
                    SameQuality = true;
                    break;
                case "Windows Media WMV (Input Size/Aspect Ratio)":
                    outFileExt = ".wmv";
                    VideoCodec = "wmv2";
                    AudioChannels = "2";
                    AudioBitrate = "64";
                    AudioSamples = "44100";
                    AudioVolume = "256";
                    Deinterlace = true;
                    SameQuality = true;
                    break;
                case "MPEG1 (4:3 480x360)":
                    outFileExt = ".mpg";
                    VideoCodec = "mpeg1video";
                    VideoAspectRatio = "4:3";
                    VideoFrameRate = "29.97";
                    VideoSize = SetSize("480x360");
                    AudioChannels = "2";
                    AudioVolume = "256";
                    Deinterlace = true;
                    SameQuality = true;
                    break;
                case "MPEG1 (4:3 640x480)":
                    outFileExt = ".mpg";
                    VideoCodec = "mpeg1video";
                    VideoAspectRatio = "4:3";
                    VideoFrameRate = "29.97";
                    VideoSize = SetSize("640x480");
                    AudioChannels = "2";
                    AudioVolume = "256";
                    Deinterlace = true;
                    SameQuality = true;
                    break;
                case "MPEG1 (15:8 480x272)":
                    outFileExt = ".mpg";
                    VideoCodec = "mpeg1video";
                    VideoAspectRatio = "15:8";
                    VideoFrameRate = "29.97";
                    VideoSize = SetSize("480x272");
                    AudioChannels = "2";
                    AudioVolume = "256";
                    Deinterlace = true;
                    break;
                case "MPEG1 (15:8 750x400)":
                    outFileExt = ".mpg";
                    VideoCodec = "mpeg1video";
                    VideoAspectRatio = "15:8";
                    VideoFrameRate = "29.97";
                    VideoSize = SetSize("750x400");
                    AudioChannels = "2";
                    AudioVolume = "256";
                    Deinterlace = true;
                    break;
                case "MPEG1 (16:9 640x360)":
                    outFileExt = ".mpg";
                    VideoCodec = "mpeg1video";
                    VideoAspectRatio = "16:9";
                    VideoFrameRate = "29.97";
                    VideoSize = SetSize("640x360");
                    AudioChannels = "2";
                    AudioVolume = "256";
                    Deinterlace = true;
                    break;
                case "MPEG1 (Custom Size)":
                    outFileExt = ".mpg";
                    VideoCodec = "mpeg1video";
                    VideoFrameRate = "29.97";
                    VideoSize = SetSize("640x360");
                    AudioChannels = "2";
                    AudioVolume = "256";
                    Deinterlace = true;
                    break;
                case "MPEG1 (Input Size/Aspect Ratio":
                    outFileExt = ".mpg";
                    VideoCodec = "mpeg1video";
                    VideoFrameRate = "29.97";
                    AudioChannels = "2";
                    AudioVolume = "256";
                    Deinterlace = true;
                    break;
                case "MP4 (4:3 480x360)":
                    outFileExt = ".mp4";
                    VideoCodec = "mpeg4";
                    VideoAspectRatio = "4:3";
                    VideoBitrate = "";
                    VideoFrameRate = "29.97";
                    VideoSize = SetSize("480x360");
                    AudioBitrate = "128";
                    AudioChannels = "2";
                    AudioSamples = "44100";
                    AudioVolume = "256";
                    Deinterlace = true;
                    SameQuality = true;
                    break;
                case "MP4 (4:3 640x480)":
                    outFileExt = ".mp4";
                    VideoCodec = "mpeg4";
                    VideoAspectRatio = "4:3";
                    VideoBitrate = "";
                    VideoFrameRate = "29.97";
                    VideoSize = SetSize("640x480");
                    AudioBitrate = "128";
                    AudioChannels = "2";
                    AudioSamples = "44100";
                    AudioVolume = "256";
                    Deinterlace = true;
                    SameQuality = true;
                    break;
                case "MP4 (15:8 480x272)":
                    outFileExt = ".mp4";
                    VideoCodec = "mpeg4";
                    VideoAspectRatio = "15:8";
                    VideoBitrate = "";
                    VideoFrameRate = "29.97";
                    VideoSize = SetSize("480x272");
                    AudioBitrate = "128";
                    AudioChannels = "2";
                    AudioSamples = "44100";
                    AudioVolume = "256";
                    Deinterlace = true;
                    SameQuality = true;
                    break;
                case "MP4 (15:8 750x400)":
                    outFileExt = ".mp4";
                    VideoCodec = "mpeg4";
                    VideoAspectRatio = "15:8";
                    VideoBitrate = "";
                    VideoFrameRate = "29.97";
                    VideoSize = SetSize("750x400");
                    AudioBitrate = "128";
                    AudioChannels = "2";
                    AudioSamples = "44100";
                    AudioVolume = "256";
                    Deinterlace = true;
                    SameQuality = true;
                    break;
                case "MP4 (16:9 640x360)":
                    outFileExt = ".mp4";
                    VideoCodec = "mpeg4";
                    VideoAspectRatio = "16:9";
                    VideoBitrate = "";
                    VideoFrameRate = "29.97";
                    VideoSize = SetSize("640x360");
                    AudioBitrate = "128";
                    AudioChannels = "2";
                    AudioSamples = "44100";
                    AudioVolume = "256";
                    Deinterlace = true;
                    SameQuality = true;
                    break;
                case "MP4 (Custom Size)":
                    outFileExt = ".mp4";
                    VideoCodec = "mpeg4";
                    VideoBitrate = "";
                    VideoFrameRate = "29.97";
                    VideoSize = SetSize("640x360");
                    AudioBitrate = "128";
                    AudioChannels = "2";
                    AudioSamples = "44100";
                    AudioVolume = "256";
                    Deinterlace = true;
                    SameQuality = true;
                    break;
                case "MP4 (Input Size/Aspect Ratio)":
                    outFileExt = ".mp4";
                    VideoCodec = "mpeg4";
                    VideoBitrate = "";
                    VideoFrameRate = "29.97";
                    AudioBitrate = "128";
                    AudioChannels = "2";
                    AudioSamples = "44100";
                    AudioVolume = "256";
                    Deinterlace = true;
                    SameQuality = true;
                    break;
                case "Flash (4:3 480x360)":
                    outFileExt = ".flv";
                    VideoCodec = "flv";
                    AudioCodec = "pcm_s16le";
                    VideoFrameRate = "29.97";
                    VideoSize = SetSize("480x360");
                    AudioVolume = "256";
                    Deinterlace = true;
                    SameQuality = true;
                    break;
                case "Flash (4:3 640x480)":
                    outFileExt = ".flv";
                    VideoCodec = "flv";
                    VideoFrameRate = "29.97";
                    VideoSize = SetSize("640x480");
                    AudioVolume = "256";
                    AudioCodec = "pcm_s16le";
                    Deinterlace = true;
                    SameQuality = true;
                    break;
                case "Flash (15:8 750x400)":
                    outFileExt = ".flv";
                    VideoCodec = "flv";
                    VideoFrameRate = "29.97";
                    VideoSize = SetSize("750x400");
                    AudioVolume = "256";
                    AudioCodec = "pcm_s16le";
                    Deinterlace = true;
                    SameQuality = true;
                    break;
                case "Flash (16:9 640x360)":
                    outFileExt = ".flv";
                    VideoCodec = "flv";
                    VideoFrameRate = "29.97";
                    VideoSize = SetSize("640x360");
                    AudioVolume = "256";
                    AudioCodec = "pcm_s16le";
                    Deinterlace = true;
                    SameQuality = true;
                    break;
                case "Flash (Custom Size)":
                    outFileExt = ".flv";
                    VideoCodec = "flv";
                    VideoFrameRate = "29.97";
                    VideoSize = SetSize("480x360");
                    AudioVolume = "256";
                    AudioCodec = "pcm_s16le";
                    Deinterlace = true;
                    SameQuality = true;
                    break;
                case "Flash (Input Size/Aspect Ratio)":
                    outFileExt = ".flv";
                    VideoCodec = "flv";
                    VideoFrameRate = "29.97";
                    AudioVolume = "256";
                    AudioCodec = "pcm_s16le";
                    Deinterlace = true;
                    SameQuality = true;
                    break;
                case "AVI (MPEG4 DivX HQ 320x240)":
                    outFileExt = ".avi";
                    VideoCodec = "mpeg4";
                    VideoBitrate = "360";
                    VideoFrameRate = "29.97";
                    VideoSize = SetSize("320x240");
                    AudioChannels = "2";
                    AudioVolume = "256";
                    Deinterlace = true;
                    SameQuality = true;
                    break;
                case "AVI (MPEG4 DivX 320x240)":
                    outFileExt = ".avi";
                    VideoCodec = "mpeg4";
                    VideoBitrate = "360";
                    VideoFrameRate = "29.97";
                    VideoSize = SetSize("320x240");
                    AudioBitrate = "48";
                    AudioChannels = "2";
                    AudioSamples = "22050";
                    AudioVolume = "256";
                    Deinterlace = true;
                    SameQuality = true;
                    break;
                case "NTSC-VCD":
                    outFileExt = ".mpg";
                    TargetFormat = "ntsc-vcd";
                    break;
                case "NTSC-SVCD":
                    outFileExt = ".mpg";
                    TargetFormat = "ntsc-svcd";
                    break;
                case "NTSC-DVD":
                    outFileExt = ".mpg";
                    TargetFormat = "ntsc-dvd";
                    break;
                case "NTSC-DV":
                    outFileExt = ".avi";
                    TargetFormat = "ntsc-dv";
                    break;
                case "NTSC-DV50":
                    outFileExt = ".avi";
                    TargetFormat = "ntsc-dv50";
                    break;
                case "PAL-VCD":
                    outFileExt = ".mpg";
                    TargetFormat = "pal-vcd";
                    break;
                case "PAL-SVCD":
                    outFileExt = ".mpg";
                    TargetFormat = "pal-svcd";
                    break;
                case "PAL-DVD":
                    outFileExt = ".mpg";
                    TargetFormat = "pal-dvd";
                    break;
                case "PAL-DV":
                    outFileExt = ".avi";
                    TargetFormat = "pal-dv";
                    break;
                case "PAL-DV50":
                    outFileExt = ".avi";
                    TargetFormat = "pal-dv50";
                    break;
            }
            CoreAV.ConvertProps cp = PlayPlan.FinalFormat;
            cp.DestinationFile = TextBoxOutputFolder.Text  + TextBoxOutputFileName.Text + "\\" + outFileExt;
                

            cp.outFileExt = outFileExt;
            cp.VideoCodec = VideoCodec;
            cp.AudioCodec = AudioCodec;
            cp.VideoBitrate = VideoBitrate;
            cp.VideoFrameRate = VideoFrameRate;
            cp.VideoSize = VideoSize;
            cp.AudioBitrate = AudioBitrate;
            cp.AudioChannels = AudioChannels;
            cp.AudioSamples = AudioSamples;
            cp.AudioVolume = AudioVolume;
            cp.ForceFormat = ForceFormat;
            cp.VideoAspectRatio = VideoAspectRatio;
            cp.TargetFormat = TargetFormat;
            cp.Deinterlace = Deinterlace;
            cp.SameQuality = SameQuality;

            PlayPlan.FinalFormat = cp;
            ExportReady = true;
            this.Hide();
        }
        public bool ExportReady = false;
        private void button2_Click(object sender, EventArgs e)
        {
            ExportReady = false;
            this.Hide();
        }
    }
}
