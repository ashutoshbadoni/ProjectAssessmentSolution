using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace AssessmentSolution
{
    public class Data : IEnumerable<Record>
    {
        private List<Record> records = new List<Record>();
       
        public Data()
        {

        }
        public Data(string filePath)
        {
            ReadTextFile(filePath);
        }
        public void Add(Record record)
        {
            records.Add(record);
            
        }

        public IEnumerator<Record> GetEnumerator()
        {
            return records.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return records.GetEnumerator();
        }
        /// <summary>
        /// This method finds total number of records of a file type(xml,dll,nf,config)
        /// </summary>
        /// <param name="fileType"></param>
        /// <returns> int count of records</returns>
        public int GetCount(string fileType=null)
        {
            if (string.IsNullOrEmpty(fileType))
                return this.Count();
            else
            return this.Count(x => x.FileType == fileType);
        }
        /// <summary>
        /// This method finds total sum of size for records of a passed file type(xml,dll,nf,config)
        /// </summary>
        /// <param name="fileType"></param>
        /// <returns>int sum of all record's size property</returns>
        public int GetSize(string fileType)
        {
            var filteredRecord = this.Where(x => x?.FileType == fileType);
            if (filteredRecord != null && filteredRecord.Count() > 0)
                return this.Where(x => x?.FileType == fileType).Sum(y => y.Size);
            else
                return 0;


        }
        /// <summary>
        /// This method finds average of size number of records of a file type(xml,dll,nf,config)
        /// </summary>
        /// <param name="fileType"></param>
        /// <returns>double average of record's size property</returns>
        public double GetAverageSize(string fileType)
        {
            var filteredRecord = this.Where(x => x?.FileType == fileType);
            if (filteredRecord != null && filteredRecord.Count() > 0)
                return Math.Floor( this.Where(x => x?.FileType == fileType).Average(y => y.Size) * 100)/100; 
            else
                return 0;
        }

        /// <summary>
        /// Reads all the text from a location(filePath)
        /// if the file is xml, nf, config,dll it adds the record to Data 
        /// </summary>
        /// <param name="filePath"></param>
        public  void ReadTextFile(string filePath)
        {
            try
            {
                //read all lines of the file located at filepath
                string[] readText = File.ReadAllLines(filePath);
                foreach (string row in readText)
                {
                    //check and add to records collection if the file is xml.nf,config,dll
                    if (Utility.IsFileInContext(row))
                    {
                        string[] record = row.Split(new[] { "^|^" }, StringSplitOptions.None);
                        string type = Utility.GetFileTYpe(record[0]);
                        this.Add(new Record(record[0].Trim('^'), int.Parse(record[1]), record[2].Trim('^'), type));
                    }
                }
            }
            catch (IOException ioe)
            {
                WriteLine("Incorrect File Path " + ioe.Message);
                ReadLine();
            }
            catch (Exception ex)
            {
                WriteLine("An exception has occoured" + ex.Message);
                ReadLine();
            }
        }

    }    
    
}
