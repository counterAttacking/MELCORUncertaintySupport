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
    public partial class TextViewerForm : DockContent
    {
        private string filePath;

        public TextViewerForm(string filePath)
        {
            InitializeComponent();

            this.filePath = filePath;
        }

        private void TextViewerForm_Load(object sender, EventArgs e)
        {
            var str = File.ReadAllText(this.filePath);
            this.txtFileContent.Text = str;
        }
    }
}
