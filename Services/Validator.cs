using System.Text.RegularExpressions;

namespace ConfigurationReader.Services
{
    public static class Validator
    {
        public static bool ValidateFilePath(string path)
        {
            return path != null && Regex.IsMatch(path, @"^(?:[a-zA-Z]\:|\\\\[\w\.]+\\[\w.$]+)\\(?:[\w]+\\)*\w([\w.])+$");
        }
    }
}
