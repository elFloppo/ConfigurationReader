using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ConfigurationReader.Services
{
    public static class Deserializer
    {
        /// <summary>
        /// Десериализация файла
        /// </summary>
        /// <typeparam name="T">Тип выходного значения</typeparam>
        /// <param name="filePath">Путь до файла</param>
        /// <returns>Объект указанного типа</returns>
        /// <exception cref="ArgumentNullException">Значение пути было равно null</exception>
        /// <exception cref="Exception">Десериализация файлов данного типа не поддерживается</exception>
        public static T DeserializeFile<T>(string filePath) where T : class, new()
        {
            if (filePath == null)
                throw new ArgumentNullException(nameof(filePath));

            var extension = Path.GetExtension(filePath).ToLower();
            switch (extension)
            {
                case ".xml":
                    return DeserializeXMLFile<T>(filePath);
                case ".csv":
                    return DeserializeCSVFile<T>(filePath);
                case ".json":
                    return DeserializeJSONFile<T>(filePath);
                default:
                    throw new Exception();
            }
        }

        /// <summary>
        /// Поддерживаемые десериализатором расширения
        /// </summary>
        public static ReadOnlyCollection<string> SupportedExtensions = Array.AsReadOnly(new string[]
        {
            ".xml",
            ".csv",
            ".json"
        });

        /// <summary>
        /// Десериализация XML-файла
        /// </summary>
        /// <typeparam name="T">Тип выходного значения</typeparam>
        /// <param name="path">Путь до файла</param>
        /// <returns>Объект указанного типа</returns>
        /// <exception cref="Exception">Ошибка десериализации</exception>
        private static T DeserializeXMLFile<T>(string path)
        {
            using (var file = File.OpenRead(path))
            {
                var deserializedObject = (T)new XmlSerializer(typeof(T)).Deserialize(file);

                foreach (var property in typeof(T).GetProperties())
                    if (property.GetValue(deserializedObject) == null)
                        throw new Exception();
                
                return deserializedObject;
            }
        }

        /// <summary>
        /// Десериализация CSV-файла
        /// </summary>
        /// <typeparam name="T">Тип выходного значения</typeparam>
        /// <param name="path">Путь до файла</param>
        /// <returns>Объект указанного типа</returns>
        /// <exception cref="Exception">Ошибка десериализации</exception>
        private static T DeserializeCSVFile<T>(string path) where T : new()
        {
            var fileContent = File.ReadAllText(path, Encoding.UTF8);
            var fileContentList = fileContent.Split(';');
           
            var properties = typeof(T).GetProperties();

            if (fileContentList.Count() != properties.Count())
                throw new Exception();
            
            var index = 0;
            var obj = new T();
            foreach (var field in properties)
            {
                field.SetValue(obj, fileContentList[index]);
                index++;
            }

            return obj;
        }

        /// <summary>
        /// Десериализация JSON-файла
        /// </summary>
        /// <typeparam name="T">Тип выходного значения</typeparam>
        /// <param name="path">Путь до файла</param>
        /// <returns>Объект указанного типа</returns>
        private static T DeserializeJSONFile<T>(string path)
        {
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
        }       
    }
}
