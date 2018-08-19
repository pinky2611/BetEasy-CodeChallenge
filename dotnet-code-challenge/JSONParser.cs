using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using dotnet_code_challenge.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace dotnet_code_challenge
{
    public class JSONParser : IFileParser
    {
        /// <summary>
        /// Parse JSON file
        /// </summary>
        /// <param name="inputFilePath">input file path</param>
        /// <returns>list of horses and prices in ascending order</returns>
        public IList<Horse> Parse(string inputFilePath)
        {
            var horses = new List<Horse>();
            try
            {
                using (StreamReader r = new StreamReader(inputFilePath))
                {
                    var json = r.ReadToEnd();
                    var jobj = JObject.Parse(json);
                    var selections = jobj.SelectTokens("..Selections[*]");
                    foreach (JToken selection in selections)
                    {
                        var name = (string)selection.SelectToken(".Tags.name");
                        var price = (double)selection.SelectToken(".Price");

                        horses.Add(new Horse { Name = name, Price = price });
                    }
                }
            }
            catch (JsonException)
            {
                throw new Exception("Invalid json format.");
            }
            catch (Exception)
            {
                //TODO Log exception e.g., invalid Json
            }
            return horses;
        }
    }
}
