using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentSolution
{
    public class Record
    {
        public string FullPath { get; set; }
        public int Size { get; set; }
        public string CreationDate { get; set; }
        public string FileType { get; set; }

        public Record(string fullPath, int size, string date,string filetype)
        {
            this.FullPath = fullPath;
            this.CreationDate = date;
            this.Size = size;
            this.FileType = filetype;
        }

        public override string ToString()
        {
            return $"Path of File {FullPath} size= {Size} Date= {CreationDate}";
        }

    }
}
