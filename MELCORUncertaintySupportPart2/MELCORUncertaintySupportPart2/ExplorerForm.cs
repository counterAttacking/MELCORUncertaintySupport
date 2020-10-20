using MELCORUncertaintySupportPart2.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace MELCORUncertaintySupportPart2
{
    public partial class ExplorerForm : DockContent
    {
        private MainForm frmMain;
        private List<string> colNames;

        public ExplorerForm(MainForm frmMain)
        {
            InitializeComponent();

            this.frmMain = frmMain;
            this.colNames = new List<string>();
        }

        private void ExplorerForm_Load(object sender, EventArgs e)
        {
            this.ColNamesSetting();
            this.dgvHISs.ColumnCount = this.colNames.Count;
            for (var i = 0; i < this.colNames.Count; i++)
            {
                this.dgvHISs.Columns[i].Name = this.colNames[i];
                if (i == this.colNames.Count - 1)
                {
                    this.dgvHISs.Columns[i].Visible = false;
                }
            }
        }

        private void ColNamesSetting()
        {
            this.colNames.Add("Path of his File");
            this.colNames.Add("his File Name");
            this.colNames.Add("Full Path");
        }

        public void AddHISFile(HISFile file)
        {
            this.dgvHISs.Rows.Add(file.path, file.name, file.fullPath);
        }

        public DataGridView GetDgvHISs()
        {
            return this.dgvHISs;
        }
    }
}
