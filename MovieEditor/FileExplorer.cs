using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using MovieEditor.Encoder;
using System.IO;
namespace MovieEditor
{
    public partial class FileExplorer :DockContent  
    {
        CoreAV.MainAVHandler  MainAv=null;
        public FileExplorer(CoreAV.MainAVHandler MainAv)
        {
            InitializeComponent();
            this.MainAv = MainAv;
        }

        public void RefreshFileExplorer(Dictionary<string,CoreAV. RawVideoFile> MediaLibrary)
        {

            foreach (KeyValuePair<string, CoreAV.RawVideoFile> kvp in MediaLibrary)
            {
                bool Found = false;
                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    if (listView1.Items[i].Text.Contains(kvp.Value.Filename) == true)
                    {
                        Found = true;
                    }
                    if (kvp.Value.Filename.Contains(listView1.Items[i].Text) == true)
                    {
                        Found = true;
                    }

                }
                if (Found==false   )
                {
                    CoreAV.RawVideoFile rvf = kvp.Value;
                    imageList1.Images.Add(rvf.Filename , rvf.FileIcon);
                    ListViewItem lvi = listView1.Items.Add(rvf.Filename );
                    lvi.ImageKey = rvf.Filename ;
                }

            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (MainAv.currentProject == null)
            {

                NewProjectDialog npd = new NewProjectDialog(MainAv);
                npd.ShowDialog(this);
            }

           
            openFileDialog1.Multiselect = true;

            openFileDialog1.ShowDialog();
            string[] filenames = openFileDialog1.FileNames;
            for (int i = 0; i < filenames.Length; i++)
            {
                string infile = System.IO.Path.GetFileName(filenames[i]);
                string projectFile =MainAv.currentProject.ProjectDirectory + "\\" +Path.GetFileNameWithoutExtension( infile);

                string[] VideoFormats = {".mpg",".mpeg",".avi",".mov",".mwv",".flv" };
                List<string> lVideoFormats=new List<string>();
                lVideoFormats.AddRange(VideoFormats);
                if (lVideoFormats.Contains(Path.GetExtension(infile).ToLower()) == true)
                {
                    AVIFileEncoder.ConvertToAVI(filenames[i], projectFile + ".avi");
                    AVIFileEncoder.ConvertToWav(filenames[i], projectFile + ".wav");
                }
                else
                {
                    if (Path.GetExtension(infile).ToLower() != ".wav")
                    {
                        AVIFileEncoder.ConvertToWav(filenames[i], projectFile + ".wav");
                        projectFile = projectFile + ".wav";
                    }
                    else
                    {
                        projectFile = projectFile + ".wav";
                        try
                        {
                           System.IO.File.Copy(filenames[i], projectFile, true);
                        }
                        catch { }
                    }
                }

                CoreAV.RawVideoFile rvf= MainAv.currentProject.LoadMediaFile(projectFile );
               // imageList1.Images.Add(projectFile, rvf.FileIcon);
                string StrippedFilename=Path.GetFileName( projectFile);
                if (rvf.HasClip ==true )

                    /*ListViewItem lvi=*/ listView1.Items.Add( StrippedFilename  ,"Crystal_Clear_app_aktion.png");
                
            }
           
        }
        public void AddFileIcon(string Filename)
        {

            listView1.Items.Add(Filename, "Crystal_Clear_app_aktion.png");
                
        }
        private void listView1_DragDrop(object sender, DragEventArgs e)
        {

        }

        private void listView1_DragEnter(object sender, DragEventArgs e)
        {
            
        }
        private bool bMouseDown = false;
        private int MDX = 0;
        private int MDY = 0;
        //private string LandedFile = "";
        private void listView1_MouseDown(object sender, MouseEventArgs e)
        {
            bMouseDown = true;
            MDX = e.X;
            MDY = e.Y;
        }

        private void listView1_MouseMove(object sender, MouseEventArgs e)
        {
            if (bMouseDown == true)
            {
                if (((e.X - MDX) ^ 2 + (e.Y - MDY) ^ 2) > 100)
                {
                //    listView1.
                }
            }
        }

        private void listView1_MouseUp(object sender, MouseEventArgs e)
        {
            bMouseDown = false;
           // LandedFile = "";
        }

        private void listView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            // Remember the Item being dragged if it is a file node (ImageIndex == 2)
            
            ListViewItem  aNode = (ListViewItem )e.Item;
            CoreAV.RawVideoFile rvf= MainAv.currentProject.LoadMediaFile(aNode.Text);

            DragDropEffects CurrentEffect;
            CurrentEffect = DragDropEffects.Move;
                // DoDragDrop sets things in motion for the drag drop operation by passing the data and the current effect
                // Data can be a bitmap, string or something that implements the IDataObject interface
            this.DoDragDrop( rvf  , CurrentEffect);
        }
    }
}
