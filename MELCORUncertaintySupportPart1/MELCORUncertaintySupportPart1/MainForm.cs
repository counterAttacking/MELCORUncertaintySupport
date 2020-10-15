using MELCORUncertaintySupportPart1.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace MELCORUncertaintySupportPart1
{
    public partial class MainForm : Form
    {
        private ExplorerForm frmExplorer;

        public MainForm()
        {
            InitializeComponent();

            this.frmExplorer = new ExplorerForm(this);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.frmExplorer.Show(this.dockPnlMain, DockState.DockLeft);
        }

        private void MsiOpenFiles_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "DAT File|*.dat",
                Multiselect = true
            };

            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            var datFileReader = new DATFileReader();
            datFileReader.ReadFiles(openFileDialog.FileNames);
            this.frmExplorer.AddDATFiles(datFileReader.files);
        }

        private void MsiDeleteAllFiles_Click(object sender,EventArgs e)
        {
            this.frmExplorer.DeleteDATFiles();
        }

        private void MsiRun_Click(object sender, EventArgs e)
        {
            var datFileWriter = new DATFileWriter();
            datFileWriter.Run(this.frmExplorer.GetTvwFiles());
        }

        public void ViewSelectedFile(string selectedFile)
        {
            var frmTextViewer = new TextViewerForm(selectedFile)
            {
                TabText = Path.GetFileName(selectedFile)
            };
            frmTextViewer.Show(this.dockPnlMain, DockState.Document);
        }
    }
}
