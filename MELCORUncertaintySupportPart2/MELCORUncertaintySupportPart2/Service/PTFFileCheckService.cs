using MELCORUncertaintySupportPart2.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MELCORUncertaintySupportPart2.Service
{
    public class PTFFileCheckService
    {
        public List<PTFFile> ptfFiles = new List<PTFFile>();

        public void Check(string dirPath)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(dirPath);
            foreach (FileInfo file in directoryInfo.GetFiles())
            {
                if (file.Extension.Equals(".ptf"))
                {
                    var ptfFile = new PTFFile
                    {
                        path = dirPath,
                        name = Path.GetFileNameWithoutExtension(file.Name)
                    };
                    ptfFiles.Add(ptfFile);
                }
            }
        }
    }
}
