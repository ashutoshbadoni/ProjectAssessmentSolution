using System;
using System.IO;
using System.Configuration;
using static System.Console;
using System.Diagnostics;

namespace AssessmentSolution
{
    class Program
    {
        static void Main(string[] args)
        {          
            //check if file exists in the given path
            if (!File.Exists(ConfigurationManager.AppSettings["FilePath"]))
            {
                WriteLine($"File does not exist at {ConfigurationManager.AppSettings["FilePath"]}");
                ReadLine();
                return;
            }
            try
            {
                //initialize and load data from file
                Data data = new Data(ConfigurationManager.AppSettings["FilePath"]);
                if (data?.Count > 0)
                    PrintFileInformation(data);
            }
            catch (FileNotFoundException efnf)
            {
                WriteLine($"Cannot file find at given file filePath {ConfigurationManager.AppSettings["FilePath"]}");
                Debug.WriteLine(efnf.FileName + " " + efnf.Message);
            }
            catch (PathTooLongException eptl)
            {
                WriteLine("Path is too long.");
                Debug.WriteLine(eptl.Message);
            }
            catch (UnauthorizedAccessException eue)
            {
                FileAttributes attr = (new FileInfo(ConfigurationManager.AppSettings["FilePath"])).Attributes;
                Console.Write("Unable to access file");
                if ((attr & FileAttributes.ReadOnly) > 0)
                    Console.Write("The file is read-only.");

                Debug.WriteLine(eue.Message);
            }
            catch (IndexOutOfRangeException eiob)
            {
                Console.WriteLine("Encountered error while reading file.");
                Debug.WriteLine(eiob.Message);
                ReadLine();
            }
            catch (Exception ex)
            {
                WriteLine("An exception occured while reading file. ");
            }
            
        }
        /// <summary>
        /// Method to facilitate processing user inputs to perticular action based on file type
        /// 
        /// </summary>
        /// <param name="data"></param>
        private static void PrintFileInformation(Data data)
        {
            if (data == null)
            {
                WriteLine("File does not contain data");
                return;
            }
            /*Prompt user for selecting a file type
            1 is for xml, 2 is for dll, 3 is for nf and 4 is for config files*/

            //bool flag to maintain program lifecycle. When set to false the program terminates.
            bool isExitPrompted = false;
            WriteLine("Please select" + Environment.NewLine +
                "1- for xml File" + Environment.NewLine +
                "2- for dll files" + Environment.NewLine +
                "3- for nf files" + Environment.NewLine +
                "4- for config files" + Environment.NewLine +
                "5- to exit" + Environment.NewLine);
            //continued iteration to get multiple user inputs
            var input = ReadLine();
            while (!isExitPrompted)
            {
                //check if input is valid int type
                if (int.TryParse(input, out int i))
                {
                    switch (i)
                    {
                        case 1:
                            Utility.ShowFileInfo(Constants.XML, data);
                            break;
                        case 2:
                            Utility.ShowFileInfo(Constants.Dll, data);
                            break;
                        case 3:
                            Utility.ShowFileInfo(Constants.NF, data);
                            break;
                        case 4:
                            Utility.ShowFileInfo(Constants.CONFIG, data);
                            break;
                        case 5:
                            isExitPrompted = true;
                            break;
                        default:
                            WriteLine("invalid input please select values between 1 and 5");
                            break;
                    }
                    if (!isExitPrompted)
                        input = ReadLine();
                }
                else
                {
                    //Prompt user to input a valid int number between 1 and 5
                    WriteLine("invalid input please select values between 1 and 5");
                    input = ReadLine();
                }

            }
        }

    }

}


