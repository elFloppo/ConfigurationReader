using Newtonsoft.Json;
using System.Xml.Serialization;

namespace ConfigurationReader.Models
{
    [XmlRoot(ElementName = "config", IsNullable = false )]
    public class Configuration
    {
        [XmlElement(ElementName = "name", IsNullable = false)]
        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }

        [XmlElement(ElementName = "description", IsNullable = false)]
        [JsonProperty("description", Required = Required.Always)]
        public string Description {  get; set; }
    }
}
