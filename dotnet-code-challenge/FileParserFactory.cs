using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace dotnet_code_challenge
{
    /// <summary>   
    /// Parser factory
    /// </summary>
    public static class FileParserFactory
    {
        //TODO XPaths should be moved to config file
        private static string horseNameXPath = @"//meeting/races/race/horses/horse/@name";
        private static string horseNumberXPath = "//meeting/races/race/horses/horse[@name=\"{0}\"]/number";
        private static string horsePriceXPath = "//meeting/races/race/prices/price/horses/horse[@number=\"{0}\"]/@Price";

        /// <summary>
        /// Create context for file parsing
        /// </summary>
        /// <param name="inputFileType">InputFileType enum</param>
        /// <returns>Parser context</returns>
        public static IFileParser CreateContext(InputFileType inputFileType)
        {
            IFileParser context = null;
            switch (inputFileType)
            {
                case InputFileType.Xml:
                    context = new XMLParser(horseNameXPath, horseNumberXPath, horsePriceXPath);
                    break;

                case InputFileType.Json:
                    context = new JSONParser();
                    break;
            }
            return context;
        }
    }
}
