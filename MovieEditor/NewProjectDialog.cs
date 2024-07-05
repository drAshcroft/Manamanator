using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MovieEditor
{
    public partial class NewProjectDialog : Form
    {
        private CoreAV.MainAVHandler Mains;
       
        Dictionary<string, int[]> VideoResolutionValues = new Dictionary<string, int[]>();
        private int[] ProjectVideoResolution = { -1, -1 };
        private double ProjectAudioResolution = -1;
        private int ProjectNumChannels = -1;
        private AudioSampleType  ProjectSampleType = AudioSampleType.Unknown ;
        private double ProjectFrameRate = -1;
        object [] VideoResolutions ={   "AutoDetermine",-1,-1
                                          ,"Custom",0,0
                                          ,"VHS           320 X 480 (NTSC)	4 : 3",320,480
                                          ,"Video disc    352 x 480     	4 : 3",352,480
                                          ,"NTSC          440 × 486 lines	16 : 9",440,486
                                          ,"UMD           480 × 272    	    16:9",480,272
                                          ,"SVCD          480 X 480     	1 : 1",480,480
                                          ,"PAL, SECAM    520 × 576 lines	4 : 3",520,576
                                          ,"S - VHS       530 X 480      	4 : 3",530,480
                                          ,"SDTV          640 X 480     	4 : 3",640,480
                                          ,"YouTube       640 X 480     	4 : 3",640,480
                                          ,"DVD           720 X 480     	4 : 3",720,480
                                          ,"HDTV          1280 X 720   	    16 : 9",1280,720
                                          ,"YouTube       1280 X 720   	    16 : 9",1280,720
                                          ,"Blu - Ray     1920 X 1080  	    16 : 9",1920,1080};
        Dictionary<string,double> AudioResolutionValues=new Dictionary<string,double>();
        object[] AudioResolutions = {
                                        "AutoDetect",-1d
                                       ,"32",32d
                                       ,"44.1",44.1d
                                       ,"YouTube",44.1d
                                       ,"48",48d};
        Dictionary<string, AudioSampleType > SampleTypesValues = new Dictionary<string, AudioSampleType >();

        object[] SampleTypes ={       "Auto Detect",AudioSampleType.Unknown 
                                     ,"8 bit",AudioSampleType.INT8 
                                     ,"16 bit",AudioSampleType.INT16 
                                     ,"24 bit",AudioSampleType.INT24
                                     ,"32 bit",AudioSampleType.INT32 
                                     ,"Float",AudioSampleType.FLOAT 
                               };

        public NewProjectDialog(CoreAV.MainAVHandler MAV)
        {
             
            InitializeComponent();
            Mains = MAV;
            lbVideoResolution.Items.Clear();
            lbAudioSampleRate.Items.Clear();
            lbAudioSampleType.Items.Clear();
            for (int i = 0; i < VideoResolutions.Length; i++)
            {
                if (VideoResolutions[i].GetType() == typeof(string))
                {
                    lbVideoResolution.Items.Add((string)VideoResolutions[i]);
                    VideoResolutionValues.Add((string)VideoResolutions[i], new int[] { (int)VideoResolutions[i + 1], (int)VideoResolutions[i + 2] });
                }
            }
            lbVideoResolution.SelectedIndex = 0;
            for (int i = 0; i < AudioResolutions.Length; i++)
            {
                if (AudioResolutions[i].GetType() == typeof(string))
                {
                    lbAudioSampleRate.Items.Add((string)AudioResolutions[i]);
                    AudioResolutionValues.Add((string)AudioResolutions[i],(double)AudioResolutions[i+1] );
                }
            }
            lbAudioSampleRate.SelectedIndex = 0;
            for (int i = 0; i < SampleTypes.Length; i++)
            {
                if (SampleTypes[i].GetType() == typeof(string))
                {
                    lbAudioSampleType .Items.Add((string)SampleTypes[i]);
                    SampleTypesValues.Add((string)SampleTypes [i],(AudioSampleType ) SampleTypes[i+1]);
                }
            }
            lbAudioSampleType.SelectedIndex = 0;

        }

       

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("You need to select a project name before adding new project.");
                return;
            }
           
            string ProjectDirectory = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\" + textBox1.Text  ;
            if (!System.IO.Directory.Exists(ProjectDirectory ))
                System.IO.Directory.CreateDirectory(ProjectDirectory );
            Mains.currentProject = new CoreAV.Project(Mains);
            Mains.currentProject.CreateProject(ProjectDirectory + "\\" + textBox1.Text + ".xml",textBox1.Text ,ProjectSampleType,ProjectAudioResolution,ProjectNumChannels,ProjectVideoResolution[0],ProjectVideoResolution[1],ProjectFrameRate  );
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lbVideoResolution_SelectedIndexChanged(object sender, EventArgs e)
        {
             ProjectVideoResolution= VideoResolutionValues[(string)lbVideoResolution.SelectedItem ];
             if ((string)lbVideoResolution.SelectedValue == "Custom")
             {
                 tBWidth.Enabled = true;
                 tBHeight.Enabled = true;
             }
             else
             {
                 tBWidth.Enabled = false ;
                 tBHeight.Enabled = false ;
             }
        }

        private void tBFrameRate_TextChanged(object sender, EventArgs e)
        {
            double.TryParse(tBFrameRate.Text, out ProjectFrameRate);
        }

        private void tBWidth_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(tBWidth.Text, out  ProjectVideoResolution[0]);
        }

        private void tBHeight_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(tBHeight.Text, out ProjectVideoResolution[1]);
        }

        private void cBAutodetectFrameRate_CheckedChanged(object sender, EventArgs e)
        {
            if (cBAutodetectFrameRate.Checked == true)
            {
                ProjectFrameRate = -1;
                tBFrameRate.Enabled = false; 
            }
            else
            {
                double.TryParse(tBFrameRate.Text, out ProjectFrameRate);
                tBFrameRate.Enabled = true;
            }
        }

        private void rBAutoDetect_CheckedChanged(object sender, EventArgs e)
        {
            if (rBAutoDetect.Checked == true)
                ProjectNumChannels = -1;
        }

        private void rBMono_CheckedChanged(object sender, EventArgs e)
        {
            if (rBMono.Checked == true)
                ProjectNumChannels = 1;
        }

        private void rB51Audio_CheckedChanged(object sender, EventArgs e)
        {
            if (rB51Audio.Checked == true)
                ProjectNumChannels = 6;
        }

        private void rBStereo_CheckedChanged(object sender, EventArgs e)
        {
            if (rBStereo.Checked == true)
                ProjectNumChannels = 2;
        }

        private void rBCustom_CheckedChanged(object sender, EventArgs e)
        {
            if (rBCustom.Checked == true)
            {
                udNumChannels.Enabled = true;
                ProjectNumChannels = (int)udNumChannels.Value;
            }
            else
                udNumChannels.Enabled = false;
        }

        private void lbAudioSampleRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProjectAudioResolution = AudioResolutionValues[(string)lbAudioSampleRate.SelectedItem];
            
        }

        private void lbAudioSampleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProjectSampleType = SampleTypesValues[(string)lbAudioSampleType.SelectedItem];
            
        }

    }
}
