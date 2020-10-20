using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MELCORUncertaintySupportPart2.Service
{
    public class HISFileWriteService
    {
        private HISFileReadService fileReadService;
        private string hisFilePath;

        public HISFileWriteService(string filePath)
        {
            this.fileReadService = new HISFileReadService();
            this.hisFilePath = filePath;
        }

        public void ReadHISFile()
        {
            this.fileReadService.ReadFiles(hisFilePath);
        }

        public void WriteHISFile(string createPath, string ptfName)
        {
            var lineIdx = this.fileReadService.targetLineIdx;
            var lines = (string[])this.fileReadService.GetReadLines();
            lines[lineIdx] = lines[lineIdx].Replace("PLOT.ptf", ptfName + ".ptf");
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var str in lines)
            {
                stringBuilder.AppendLine(str);
            }
            File.WriteAllText(createPath, stringBuilder.ToString());
        }
    }
}
