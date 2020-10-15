using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MELCORUncertaintySupportPart1.Service
{
    public class DATFileWriter
    {
        private static string targetPGDIAStr = "PGDIA.txt";
        private static string targetPGOUTStr = "PGOUT.txt";
        private static string targetPRSTStr = "PRST";
        private static string targetPCDIAStr = "PCDIA.txt";
        private static string targetPCOUTStr = "PCOUT.txt";
        private static string targetPLOTStr = "PLOT.ptf";
        private static string targetPCMESStr = "PCMES.txt";

        private static string pgdiaStr = "PGDIA";
        private static string pgoutStr = "PGOUT";
        private static string prstStr = "PRST";
        private static string pcdiaStr = "PCDIA";
        private static string pcoutStr = "PCOUT";
        private static string plotStr = "PLOT";
        private static string pcmesStr = "PCMES";

        private static string txtExtension = ".txt";
        private static string rstExtension = ".rst";
        private static string ptfExtension = ".ptf";

        public void Run(TreeView treeView)
        {
            if (treeView.Nodes.Count <= 0)
            {
                MessageBox.Show("The file does not exist.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            TreeNodeCollection nodes = treeView.Nodes;
            foreach (TreeNode node in nodes)
            {
                foreach (TreeNode elem in node.Nodes)
                {
                    this.WriteFiles(elem.Name);
                }
            }
            MessageBox.Show("It's done.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void WriteFiles(string filePath)
        {
            StringBuilder stringBuilder = new StringBuilder();
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite))
            {
                using (StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8, false))
                {
                    while (!streamReader.EndOfStream)
                    {
                        var lineVal = streamReader.ReadLine();
                        var fileName = Path.GetFileNameWithoutExtension(filePath);
                        if (lineVal.Contains(targetPGDIAStr))
                        {
                            fileName = pgdiaStr + "_" + fileName + txtExtension;
                            lineVal = lineVal.Replace(targetPGDIAStr, fileName);
                        }
                        else if (lineVal.Contains(targetPGOUTStr))
                        {
                            fileName = pgoutStr + "_" + fileName + txtExtension;
                            lineVal = lineVal.Replace(targetPGOUTStr, fileName);
                        }
                        else if (lineVal.Contains(targetPRSTStr))
                        {
                            /*
                             * 이 프로그램으로 돌린 .dat 파일을
                             * 여러 번 돌리면 PRST에서
                             * PRST_CaseN.rst_CaseN.rst_CaseN.rst
                             * 와 같은 현상이 발생한다.
                             * 이를 방지하기 위한 방안으로
                             * .rst 확장자 존재 유무로 처리했다.
                             */
                            if (!lineVal.Contains(".rst"))
                            {
                                fileName = prstStr + "_" + fileName + rstExtension;
                                lineVal = lineVal.Replace(targetPRSTStr, fileName);
                            }
                        }
                        else if (lineVal.Contains(targetPCDIAStr))
                        {
                            fileName = pcdiaStr + "_" + fileName + txtExtension;
                            lineVal = lineVal.Replace(targetPCDIAStr, fileName);
                        }
                        else if (lineVal.Contains(targetPCOUTStr))
                        {
                            fileName = pcoutStr + "_" + fileName + txtExtension;
                            lineVal = lineVal.Replace(targetPCOUTStr, fileName);
                        }
                        else if (lineVal.Contains(targetPLOTStr))
                        {
                            fileName = plotStr + "_" + fileName + ptfExtension;
                            lineVal = lineVal.Replace(targetPLOTStr, fileName);
                        }
                        else if (lineVal.Contains(targetPCMESStr))
                        {
                            fileName = pcmesStr + "_" + fileName + txtExtension;
                            lineVal = lineVal.Replace(targetPCMESStr, fileName);
                        }
                        stringBuilder.AppendLine(lineVal);
                    }
                }
            }
            File.WriteAllText(filePath, stringBuilder.ToString(), Encoding.UTF8);
        }
    }
}
