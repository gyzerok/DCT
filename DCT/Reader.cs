using System;
using System.Collections.Generic;
using System.IO;

namespace DCT
{
    public class Reader
    {
        private string filePath = "recorded.jf";
        private List<List<List<Tuple<int, int>>>>  matrices;
        private int quality;
        private FileStream fs;
        private BinaryReader r;

        public Reader()
        {
            this.Read();
            matrices = new List<List<List<Tuple<int, int>>>>();
            fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            r = new BinaryReader(fs);
        }

        private void Read()
        {
            r.ReadChar();
            this.quality = r.ReadInt32();
            r.ReadChar();
            


        }
    }
}
