using MELCORUncertaintySupportPart1.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MELCORUncertaintySupportPart1.Service
{
    public class DATFileReader
    {
        public List<DATFile> files = new List<DATFile>();

        public void ReadFiles(string[] openFiles)
        {
            foreach (var file in openFiles)
            {
                if (Path.GetExtension(file).Equals(".dat"))
                {
                    try
                    {
                        var datFile = new DATFile
                        {
                            name = Path.GetFileName(file),
                            path = file
                        };
                        files.Add(datFile);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Can't not load DAT File", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }
        }
    }
}
