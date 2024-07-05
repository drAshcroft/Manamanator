using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

using System.Media;
using System.IO;
using WeifenLuo.WinFormsUI.Docking;

namespace MovieEditor
{
    public partial class Form1 : Form
    {
        CoreAV.MainAVHandler MainAV=null;

        TimeLine.TimelineMaster TimeMaster;
        FileExplorer _FileExplorer;
        FilterToolKit _FilterToolKit;
        public Form1()
        {
            InitializeComponent();

            MainAV = new MovieEditor.CoreAV.MainAVHandler(this);
        }

        private void CreateBasicLayout()
        {
            /*pToolsForm =(PaintToolBars) ReloadContent("","Micromanager_net.PaintToolBars");// new PaintToolBars();
            SPForm = (UI.GUIDeviceForm)ReloadContent("", "Micromanager_net.StagePropertiesForm");//new UI.GUIDeviceForm ();
           
         
            if (SPForm != null) SPForm.Show(DockPanel, WeifenLuo.WinFormsUI.Docking.DockState.DockLeft);
           */
            
            VideoViewer.Viewer v = new MovieEditor.VideoViewer.Viewer();
            MainAV.DefaultViewer = v;
            _FileExplorer.Show(DockPanel, WeifenLuo.WinFormsUI.Docking.DockState.DockTop);
            v.Show(DockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Float );
            TimeMaster.Show(DockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
            
            _FilterToolKit.Show(DockPanel, WeifenLuo.WinFormsUI.Docking.DockState.DockRight);
        }

        private IDockContent ReloadContent(string persistString, string extraInformation)
        {
            if (persistString == "") persistString = extraInformation;
           
            switch (persistString)
            {
                case "MovieEditor.VideoViewer.Viewer":
                    VideoViewer.Viewer v = new MovieEditor.VideoViewer.Viewer();
                    //TimeMaster.DefaultViewer(v);
                    MainAV.DefaultViewer = v;
                    return v;
                case "MovieEditor.TimeLine.TimelineMaster":
                    return TimeMaster;
                case "MovieEditor.FileExplorer":
                    return _FileExplorer;
                case "MovieEditor.FilterToolKit":
                    return _FilterToolKit;
                default:
                    return (null);
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TimeMaster = new MovieEditor.TimeLine.TimelineMaster();
             _FileExplorer =new FileExplorer(MainAV );
             _FilterToolKit = new FilterToolKit();
             _FilterToolKit.SetMainAV(MainAV);
            TimeMaster.SetMainAV(MainAV);
            MainAV.TimeMaster = TimeMaster;
            MainAV.RawFileViewer = _FileExplorer;
            string GraphconfigFile = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.config");
            // Apply a gray professional renderer as a default renderer
            //ToolStripManager.Renderer = oDefaultRenderer;
            //oDefaultRenderer.RoundedEdges = true   ;



            // Set DockPanel properties
            DockPanel.ActiveAutoHideContent = null;
            DockPanel.Parent = this;

            //VS2005Extender.VS2005Style.Extender.SetSchema(DockPanel, VS2005Extender.VS2005Style.Extender.Schema.FromBase);

            DockPanel.SuspendLayout(true);

            //todo: load the configfile dependant on the user so each person gets their own look and feel
            if (System.IO.File.Exists(GraphconfigFile))
            {
                try
                {
                    DockPanel.LoadFromXml(GraphconfigFile, ReloadContent);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print(ex.Message);
                    CreateBasicLayout();
                }
            }
            else
            {
                // Load a basic layout
                CreateBasicLayout();
            }


            DockPanel.ResumeLayout(true, true);


           // CoreAV.RawVideoFile rvf = MainAV.LoadFile("C:\\Documents and Settings\\Administrator\\Desktop\\AVISynth\\MovieEditor\\103_0222_xvid.avi");
           // if (rvf.HasClip    )
           //     TimeMaster.AddTrack(rvf.GetVideoChunk(), rvf.GetAudioChunk());

            //CoreAV.RawVideoFile rvf2 = MainAV.LoadFile("C:\\Documents and Settings\\Administrator\\Desktop\\AVISynth\\MovieEditor\\103_0228_xvid.avi");
            //if (rvf2.HasClip)
            //    TimeMaster.AddTrack(rvf2.GetVideoChunk(), rvf2.GetAudioChunk());
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            MainAV.AddTrack(openFileDialog1.FileName,Path.GetFileName( openFileDialog1.FileName ));
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            string configFile = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.config");

            DockPanel.SaveAsXml(configFile);
            
            while (DockPanel.Contents.Count > 0)
            {
                DockContent dc = (DockContent)DockPanel.Contents[0];
                dc.Close();
            }

            Generals.ClearTempFiles();
        }

        private void showPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DockContent dc =(DockContent) ReloadContent("MovieEditor.VideoViewer.Viewer", "");
            dc.Show(DockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Float);
        }

        private void newProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {

            NewProjectDialog npd = new NewProjectDialog(MainAV);
            npd.Show(this );
            
        }

        private void openProjecttToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter="Project files (*.xml)|*.xml";
            openFileDialog1.ShowDialog();
            CoreAV.Project p=new CoreAV.Project (MainAV);
            MainAV.currentProject = p; 
            p.OpenProject(openFileDialog1.FileName);
            
        }

        private void saveProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainAV.currentProject.SaveProject();
        }

        private void eportVideoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportOptions eo = new ExportOptions();
            CoreAV.PlayPlan pp = MainAV.GetPlayPlan();
            eo.SetPlayPlan(pp);
            eo.ShowDialog();
            if (eo.ExportReady == true)
            {
                Encoder.IEncoder encoder = new Encoder.AVIFileEncoder();
                encoder.SetMainAV(MainAV);
                encoder.EncodePlayPlan(pp);
            }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainAV.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainAV.Redo();
        }

        
    }
}
