using MELCORUncertaintySupportPart2.Service;
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

namespace MELCORUncertaintySupportPart2
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
                Filter = "HIS File|*.his"
            };

            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            var hisFileOpenService = new HISFileOpenService();
            hisFileOpenService.DivideFilePath(openFileDialog.FileName);
            this.frmExplorer.AddHISFile(hisFileOpenService.file);
        }

        private void MsiMakeHISFiles_Click(object sender, EventArgs e)
        {
            var dgvHISs = this.frmExplorer.GetDgvHISs();
            if (dgvHISs.Rows.Count < 0)
            {
                return;
            }
            for (var i = 0; i < dgvHISs.Rows.Count; i++)
            {
                var ptfFileCheckService = new PTFFileCheckService();
                ptfFileCheckService.Check(dgvHISs[0, i].Value.ToString());

                var hisFileWriteService = new HISFileWriteService(dgvHISs[2, i].Value.ToString());
                hisFileWriteService.ReadHISFile();

                foreach (var file in ptfFileCheckService.ptfFiles)
                {
                    var tmp = file.name;
                    tmp = tmp.Replace("PLOT_", "");
                    var createFilePath = file.path + "\\" + tmp + ".his";
                    hisFileWriteService.WriteHISFile(createFilePath, file.name);
                }
            }
        }
    }
}
