using ConfigurationReader.Models;
using ConfigurationReader.Services;
using Newtonsoft.Json;
using System;
using System.IO;
using Xunit;

namespace ConfigurationReader.Tests.DeserializerTests
{
    public class JsonDeserializerTests
    {
        private readonly string _jsonFileDirectory = $@"{Environment.CurrentDirectory}\TestFiles";
        private readonly Configuration _correctJsonConfiguration = new Configuration
        {
            Name = "Конфигурация 3",
            Description = "Описание Конфигурации 3"
        };

        [Fact]
        public void CorrectJSONTest()
        {
            var resultJsonConfiguration = Deserializer.DeserializeFile<Configuration>($@"{_jsonFileDirectory}\CorrectConfiguration.json");

            Assert.Equal(_correctJsonConfiguration.Name, resultJsonConfiguration.Name);
            Assert.Equal(_correctJsonConfiguration.Description, resultJsonConfiguration.Description);
        }

        [Fact]
        public void DifferentExtensionRegisterJSONTest()
        {
            var resultJsonConfiguration = Deserializer.DeserializeFile<Configuration>($@"{_jsonFileDirectory}\CorrectConfigurationWithDifferentExtReg.JSOn");

            Assert.Equal(_correctJsonConfiguration.Name, resultJsonConfiguration.Name);
            Assert.Equal(_correctJsonConfiguration.Description, resultJsonConfiguration.Description);
        }

        [Fact]
        public void WrongContentJSONTest()
        {
            Assert.Throws<JsonSerializationException>(() => Deserializer.DeserializeFile<Configuration>($@"{_jsonFileDirectory}\ConfigurationWithWrongContent.json"));
        }

        [Fact]
        public void MissingFileJSONTest()
        {
            Assert.Throws<FileNotFoundException>(() => Deserializer.DeserializeFile<Configuration>($@"{_jsonFileDirectory}\MissingFile.json"));
        }
    }
}
