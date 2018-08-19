using dotnet_code_challenge.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace dotnet_code_challenge
{
    /// <summary>
    /// File Parser Interface
    /// </summary>
    public interface IFileParser
    {
        /// <summary>
        /// Parse the file to get the list of horses
        /// </summary>
        /// <param name="inputFilePath">input file path</param>
        /// <returns>List of horses and prices in price ascending order</returns>
        IList<Horse> Parse(string inputFilePath);
    }
}
