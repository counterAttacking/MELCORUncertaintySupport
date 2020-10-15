using MELCORUncertaintySupportPart1.Model;
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
    public partial class ExplorerForm : DockContent
    {
        private MainForm frmMain;

        public ExplorerForm(MainForm frmMain)
        {
            InitializeComponent();

            this.frmMain = frmMain;
        }

        public void AddDATFiles(List<DATFile> files)
        {
            foreach (var file in files)
            {
                var fileNodeTxt = Path.GetFileNameWithoutExtension(file.name);
                var fileNode = new TreeNode(fileNodeTxt);
                fileNode.Nodes.Add(file.path, file.name);
                this.tvwFiles.Nodes.Add(fileNode);
            }
        }

        public void DeleteDATFiles()
        {
            this.tvwFiles.Nodes.Clear();
        }

        private void TvwFiles_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void TvwFiles_DragDrop(object sender, DragEventArgs e)
        {
            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            var datFileReader = new DATFileReader();
            datFileReader.ReadFiles(files);
            this.AddDATFiles(datFileReader.files);
        }

        private void TvwFiles_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (this.tvwFiles.SelectedNode.Parent == null) // SelectedNode is Parent
            {
                return;
            }
            else // SelectedNode is child
            {
                try
                {
                    this.frmMain.ViewSelectedFile(e.Node.Name);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        public TreeView GetTvwFiles()
        {
            return this.tvwFiles;
        }
    }
}
