using ConfigurationReader.Models;
using ConfigurationReader.Services;
using System;
using System.IO;
using Xunit;

namespace ConfigurationReader.Tests.DeserializerTests
{
    public class XmlDeserializerTests
    {
        private readonly string _xmlFileDirectory = $@"{Environment.CurrentDirectory}\TestFiles";
        private readonly Configuration _correctXmlConfiguration = new Configuration
        {
            Name = "Конфигурация 1",
            Description = "Описание Конфигурации 1"
        };

        [Fact]
        public void CorrectXMLTest()
        {
            var resultXmlConfiguration = Deserializer.DeserializeFile<Configuration>($@"{_xmlFileDirectory}\CorrectConfiguration.xml");

            Assert.Equal(_correctXmlConfiguration.Name, resultXmlConfiguration.Name);
            Assert.Equal(_correctXmlConfiguration.Description, resultXmlConfiguration.Description);
        }

        [Fact]
        public void DifferentExtensionRegisterXMLTest()
        {
            var resultXmlConfiguration = Deserializer.DeserializeFile<Configuration>($@"{_xmlFileDirectory}\CorrectConfigurationWithDifferentExtReg.XMl");

            Assert.Equal(_correctXmlConfiguration.Name, resultXmlConfiguration.Name);
            Assert.Equal(_correctXmlConfiguration.Description, resultXmlConfiguration.Description);
        }

        [Fact]
        public void WrongContentXMLTest()
        {
            Assert.Throws<Exception>(() => Deserializer.DeserializeFile<Configuration>($@"{_xmlFileDirectory}\ConfigurationWithWrongContent.xml"));
        }

        [Fact]
        public void MissingFileXMLTest()
        {
            Assert.Throws<FileNotFoundException>(() => Deserializer.DeserializeFile<Configuration>($@"{_xmlFileDirectory}\MissingFile.xml"));
        }
    }
}
