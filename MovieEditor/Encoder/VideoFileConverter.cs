using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Collections;
using System.IO;
using System.Runtime.InteropServices;

using DirectShowLib;
using AviFile;


namespace MovieEditor.Encoder
{
    public partial class VideoFileConverter : Form
    {
        
        public VideoFileConverter()
        {
            InitializeComponent();
        }
        // The main com object
        FilterGraph fg = null;
        // The graphbuilder interface ref
        IGraphBuilder gb = null;
        // The mediacontrol interface ref
        IMediaControl mc = null;
        // The mediaevent interface ref
        IMediaEventEx me = null;

        // Matroska support filter interface
        IBaseFilter matroska_mux = null;

       
        // one "global" hr
        int hr;


        public void ConvertVideoFile(Form MainForm, string FileNameIn, string FileNameOut)
        {
            CloseInterfaces();
            string Extension = Path.GetExtension(FileNameOut).ToLower();
            
            

            
            switch (Extension)
            {
                case "wmv":
                    InitInterfaces();
                    Convert2Wmv(MainForm,  FileNameIn, FileNameOut);
                    break;
                case "avi":
                    ConvertToCompressedAVI(MainForm, FileNameIn, FileNameOut);
                    break;
            }
            
        }


        // 
        // This method create the filter graph
        //
        void InitInterfaces()
        {
            try
            {
                fg = new FilterGraph();
                gb = (IGraphBuilder)fg;
                mc = (IMediaControl)fg;
                me = (IMediaEventEx)fg;
            }
            catch (Exception)
            {
                MessageBox.Show("Couldn't start");
            }
        }

        //
        //This method stop the filter graph and ensures that we stop
        //sending messages to our window
        //
        void CloseInterfaces()
        {
            if (me != null)
            {
                hr = mc.Stop();
                DsError.ThrowExceptionForHR(hr);

                hr = me.SetNotifyWindow(IntPtr.Zero, WM_GRAPHNOTIFY, IntPtr.Zero);
                DsError.ThrowExceptionForHR(hr);
            }
            mc = null;
            me = null;
            gb = null;
            if (matroska_mux != null)
                Marshal.ReleaseComObject(matroska_mux);
            matroska_mux = null;
            if (fg != null)
                Marshal.ReleaseComObject(fg);
            fg = null;
        }

        void ConvertToCompressedAVI(Form MainForm, string fileNameIn,string fileNameOut)
        {
            //open compressed file
            AviManager aviManager = new AviManager(fileNameIn , true);
            VideoStream aviStream = aviManager.GetVideoStream();
            //create un-/re-compressed file
            VideoStream newStream;
            AviManager newManager = aviStream.DecompressToNewFile(fileNameOut, true , out newStream);

            //close compressed file
            aviManager.Close();
            //save and close un-/re-compressed file
            newManager.Close();
        }
       
        //
        // This method convert the input file to an wmv file
        //
        void Convert2Wmv(Form MainForm,string fileNameIn, string fileNameOut)
        {
            try
            {


                hr = me.SetNotifyWindow(MainForm.Handle, WM_GRAPHNOTIFY, IntPtr.Zero);
                DsError.ThrowExceptionForHR(hr);

                // here we use the asf writer to create wmv files
                WMAsfWriter asf_filter = new WMAsfWriter();
                IFileSinkFilter fs = (IFileSinkFilter)asf_filter;

                hr = fs.SetFileName(fileNameOut, null);
                DsError.ThrowExceptionForHR(hr);

                hr = gb.AddFilter((IBaseFilter)asf_filter, "WM Asf Writer");
                DsError.ThrowExceptionForHR(hr);

                hr = gb.RenderFile(fileNameIn, null);
                DsError.ThrowExceptionForHR(hr);

                hr = mc.Run();
                DsError.ThrowExceptionForHR(hr);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error converting to wmv: " + ex.Message);
            }
        }


        int WM_GRAPHNOTIFY = 0x00008001;

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_GRAPHNOTIFY)
            {
                int p1, p2;
                EventCode code;

                if (me == null)
                    return;
                while (me.GetEvent(out code, out p1, out p2, 0) == 0)
                {
                    if (code == EventCode.Complete)
                    {
                        label1.Text = "done";
                        
                        mc.Stop();
                    }

                    me.FreeEventParams(code, p1, p2);
                }
                return;
            }
            base.WndProc(ref m);
        }
    }
}
