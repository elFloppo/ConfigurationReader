using ConfigurationReader.Models;
using ConfigurationReader.Services;
using System;
using Xunit;

namespace ConfigurationReader.Tests.DeserializerTests
{
    public class OtherDeserializerTests
    {
        private readonly string _fileDirectory = $@"{Environment.CurrentDirectory}\TestFiles";

        [Fact]
        public void WrongExtensionTest()
        {          
            Assert.Throws<Exception>(() => Deserializer.DeserializeFile<Configuration>($@"{_fileDirectory}\NotSupportedExtension.txt"));
        }

        [Fact]
        public void NullPathTest()
        {
            Assert.Throws<ArgumentNullException>(() => Deserializer.DeserializeFile<Configuration>(null));
        }
    }
}
