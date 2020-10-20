using MELCORUncertaintySupportPart2.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MELCORUncertaintySupportPart2.Service
{
    public class HISFileOpenService
    {
        public HISFile file = new HISFile();

        public void DivideFilePath(string filePath)
        {
            this.file.name = Path.GetFileName(filePath);
            this.file.path = Path.GetDirectoryName(filePath);
            this.file.fullPath = filePath;
        }
    }
}
