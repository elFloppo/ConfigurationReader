using ConfigurationReader.Models;
using ConfigurationReader.Services;
using System;
using System.IO;
using Xunit;

namespace ConfigurationReader.Tests.DeserializerTests
{
    public class CsvDeserializerTests
    {
        private readonly string _csvFileDirectory = $@"{Environment.CurrentDirectory}\TestFiles";
        private readonly Configuration _correctCsvConfiguration = new Configuration
        {
            Name = "Конфигурация 2",
            Description = "Описание Конфигурации 2"
        };

        [Fact]
        public void CorrectCSVTest()
        {
            var resultCsvConfiguration = Deserializer.DeserializeFile<Configuration>($@"{_csvFileDirectory}\CorrectConfiguration.csv");

            Assert.Equal(_correctCsvConfiguration.Name, resultCsvConfiguration.Name);
            Assert.Equal(_correctCsvConfiguration.Description, resultCsvConfiguration.Description);
        }

        [Fact]
        public void DifferentExtensionRegisterCSVTest()
        {          
            var resultCsvConfiguration = Deserializer.DeserializeFile<Configuration>($@"{_csvFileDirectory}\CorrectConfigurationWithDifferentExtReg.CSv");

            Assert.Equal(_correctCsvConfiguration.Name, resultCsvConfiguration.Name);
            Assert.Equal(_correctCsvConfiguration.Description, resultCsvConfiguration.Description);
        }

        [Fact]
        public void WrongContentCSVTest()
        {
            Assert.Throws<Exception>(() => Deserializer.DeserializeFile<Configuration>($@"{_csvFileDirectory}\ConfigurationWithWrongContent.csv"));
        }

        [Fact]
        public void MissingFileCSVTest()
        {
            Assert.Throws<FileNotFoundException>(() => Deserializer.DeserializeFile<Configuration>($@"{_csvFileDirectory}\MissingFile.csv"));
        }
    }
}
