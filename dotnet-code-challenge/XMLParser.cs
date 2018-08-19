using dotnet_code_challenge.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace dotnet_code_challenge
{
    public class XMLParser : IFileParser
    {
        private string _horseNameXPath;
        private string _horsePriceXPath;
        private string _horseNumberXPath;

        /// <summary>
        /// XML parser contructor
        /// </summary>
        /// <param name="horseNameXPath">XPath to find horse name</param>
        /// <param name="horseNumberXPath">XPath to find unique indentifier for each horse</param>
        /// <param name="horsePriceXPath">XPath to find the price</param>
        public XMLParser(string horseNameXPath, string horseNumberXPath, string horsePriceXPath)
        {
            _horseNameXPath = horseNameXPath;
            _horsePriceXPath = horsePriceXPath;
            _horseNumberXPath = horseNumberXPath;
        }

        /// <summary>
        /// Parse XML file
        /// </summary>
        /// <param name="inputFilePath">input file path</param>
        /// <returns>list of horses and prices in ascending order</returns>
        public IList<Horse> Parse(string inputFilePath)
        {
            var horses = new List<Horse>();

            try
            {
                XmlDocument document = new XmlDocument();
                document.Load(inputFilePath);

                var names = document.SelectNodes(_horseNameXPath);
                double price;
                var number = string.Empty;
                var priceInString = string.Empty;
                foreach (XmlNode name in names)
                {
                    var horse = new Horse { Name = name.InnerText };
                    number = document.SelectSingleNode(string.Format(_horseNumberXPath, horse.Name)).InnerText;
                    priceInString = document.SelectSingleNode(string.Format(_horsePriceXPath, number)).InnerText;
                    if (double.TryParse(priceInString, out price))
                    {
                        horse.Price = price;
                    }
                    horses.Add(horse);
                }
            }
            catch (XmlException)
            {
                throw new Exception("Invalid xml.");
            }
            catch (Exception)
            {
                //TODO: Log exception 
            }
            return horses;
        }
    }
}
