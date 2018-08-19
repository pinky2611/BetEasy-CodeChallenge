using dotnet_code_challenge.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace dotnet_code_challenge
{
    class Program
    {
        static void Main(string[] args)
        {
            StringBuilder errors = new StringBuilder();
            try
            {
                var inputFilesLocation = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"FeedData");

                var inputFiles = Directory.GetFiles(inputFilesLocation);

                foreach (var inputFile in inputFiles)
                {
                    var inputFileType = GetFileType(inputFile);
                    var context = FileParserFactory.CreateContext(inputFileType);
                    if (context != null)
                    {
                        var horses = context.Parse(inputFile);
                        var sortedHorsesList = SortHorseList(horses);
                        string raceName = Path.GetFileNameWithoutExtension(inputFile);
                        WriteHorseNames(raceName, sortedHorsesList);
                    }
                }
            }
            catch (Exception ex)
            {
                errors.AppendFormat("{0};", ex.Message);
                // log exception using log4net
            }
        }

        /// <summary>
        /// Get file type enum to decide the context
        /// </summary>
        /// <param name="inputFile">input file path</param>
        /// <returns>InputFileType enum</returns>
        static InputFileType GetFileType(string inputFile)
        {
            var fileExtenstion = Path.GetExtension(inputFile);
            var fileType = fileExtenstion.Substring(1);
            Enum.TryParse(fileType, true, out InputFileType inputFileType);
            return inputFileType;
        }

        /// <summary>
        /// Sort horses by price ascending order
        /// </summary>
        /// <param name="horses">list of horses</param>
        /// <returns>sorted list</returns>
        static List<Horse> SortHorseList(IList<Horse> horses) {

            var sortedList = new List<Horse>();
            if (horses != null) {
                sortedList = horses.OrderBy(x => x.Price).ToList();
            }
            return sortedList.ToList();
        }
        /// <summary>
        /// Write race and horses name to console
        /// </summary>
        /// <param name="raceName">race name (assumption : file name is the race name)</param>
        /// <param name="horses">list of horse names and prices in ascending order</param>
        static void WriteHorseNames(string raceName, IList<Horse> horses)
        {
            Console.WriteLine(raceName.Substring(0, raceName.Length - 1));
            foreach (Horse horse in horses)
            {
                Console.WriteLine("{0} - {1}", horse.Name, horse.Price);
            }
            Console.WriteLine("");
        }
    }
}
