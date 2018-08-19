using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Xunit;

namespace dotnet_code_challenge.Test
{
    public class FileParserFactoryTests
    {
        [Fact]
        public void XmlParserValidFileTest()
        {
            var testFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"TestFiles\Caulfield_Race1.xml");
            var context = FileParserFactory.CreateContext(InputFileType.Xml);
            var horses = context.Parse(testFilePath);
            Assert.True(horses.Count > 1);
        }

        [Fact]
        public void JsonParserValidJsonTest()
        {
            var testFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"TestFiles\Wolferhampton_Race1.json");
            var context = FileParserFactory.CreateContext(InputFileType.Json);
            var horses = context.Parse(testFilePath);
            Assert.True(horses.Count > 1);
        }

        [Fact]
        public void XmlParserInvalidFileTest()
        {
            var testFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"TestFiles\InvalidXML.xml");
            var context = FileParserFactory.CreateContext(InputFileType.Xml);
            Assert.Throws<Exception>(() => context.Parse(testFilePath));
        }

        [Fact]
        public void JsonParserInvalidJsonTest()
        {
            var testFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"TestFiles\InvalidJson.json");
            var context = FileParserFactory.CreateContext(InputFileType.Json);
            Assert.Throws<Exception>(() => context.Parse(testFilePath));
        }
    }
}
