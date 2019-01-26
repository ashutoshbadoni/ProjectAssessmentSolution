using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace AssessmentSolution
{
    public static class Utility
    {
        /// <summary>
        /// Shows Total number of count of selected type, aggrigate sum of all file of selected type
        /// and average size of a selected type file
        /// </summary>
        /// <param name="type"></param>
        /// <param name="fileData"></param>
        internal static void ShowFileInfo(string type, Data fileData)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Total Number of {type} files are - {fileData.GetCount(type)}" + Environment.NewLine);
            sb.Append($"Aggregate size of {type} file is - {fileData.GetSize(type)}" + Environment.NewLine);
            sb.Append($"Average size of {type} files is - {fileData.GetAverageSize(type)}" + Environment.NewLine);
            WriteLine(sb);
        }
       
        /// <summary>
        /// Method takes file name and returns extension
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        internal static string GetFileTYpe(string fileName)
        {
            return fileName?.Substring(fileName.LastIndexOf('.'));
        }
        
        /// <summary>
        /// checks for the files extensions in filename
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>if fileName has xml or dll or nf or config it returns true else false</returns>
        internal static bool IsFileInContext(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName) &&
                (fileName.IndexOf(Constants.CONFIG, StringComparison.OrdinalIgnoreCase) > 0 ||
                fileName.IndexOf(Constants.Dll, StringComparison.OrdinalIgnoreCase) > 0 ||
                fileName.IndexOf(Constants.NF, StringComparison.OrdinalIgnoreCase) > 0 ||
                fileName.IndexOf(Constants.XML, StringComparison.OrdinalIgnoreCase) > 0))
                return true;
            else
                return false;
        }
        /// <summary>
        /// Handles Aggregate exceptions
        /// </summary>
        /// <param name="ae"></param>
        internal static void HandleAggregateExceptions(AggregateException ae)
        {
            ae.Handle((e) => {

                var ignoredExceptions = new List<Exception>();
                foreach (var ex in ae.Flatten().InnerExceptions)
                {
                    if (ex is ArgumentException || ex is NullReferenceException || ex is DivideByZeroException)
                    {
                        Console.WriteLine("Encountered error while processing request.");
                    }
                    else
                    {
                        ignoredExceptions.Add(ex);
                    }
                    Debug.WriteLine(ex.Message);
                }
                if (ignoredExceptions.Count > 0) throw new AggregateException(ignoredExceptions);
                return true;
            });
        }
    }
}
