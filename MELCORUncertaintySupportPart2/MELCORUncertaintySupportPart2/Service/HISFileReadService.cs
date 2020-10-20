using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MELCORUncertaintySupportPart2.Service
{
    public class HISFileReadService
    {
        private static string targetStr = "FILE1  'PLOT.ptf'";

        private string[] readLines;

        public int targetLineIdx
        {
            set;
            get;
        }

        /// <summary>
        /// I/O에 많은 시간이 소요가 된다.
        /// 복사할 내용이 담긴 .his 파일을 최소한으로 읽어야한다.
        /// ReadFiles()로 파일을 한 번만 읽도록 하였다.
        /// </summary>
        /// <param name="filePath">
        /// 읽어야할 .his 파일 경로명
        /// </param>
        public void ReadFiles(string filePath)
        {
            this.readLines = File.ReadAllLines(filePath);
            this.targetLineIdx = Array.FindIndex(readLines, row => row.Contains(targetStr));
        }

        /// <summary>
        /// GetReadLines()를 통해서 읽은 파일의 내용을 넘겨준다.
        /// type이 object인 이유는 Clone()이 object type이기 때문이다.
        /// </summary>
        /// <returns>
        /// 'return this.readLines;'로만 값을 넘기면
        /// MakeHISFile()에서 문자열 변경이 진행될 때
        /// 이곳도 같이 문자열이 변경이 되는 현상이 발생한다.
        /// 하지만, 'return this.readLines.Clone();'으로 값을 넘기면
        /// MakeHISFile()에서 문자열 변경이 되어도
        /// 이곳에서는 문자열 변경이 이루어지지 않는다.
        /// </returns>
        public object GetReadLines()
        {
            return this.readLines.Clone();
        }
    }
}
