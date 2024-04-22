using ConfigurationReader.Commands;
using ConfigurationReader.Models;
using ConfigurationReader.Services;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;

namespace ConfigurationReader.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Configuration> Configurations { get; set; }
        public MainViewModel()
        {
            Configurations = new ObservableCollection<Configuration>();
        }

        private string _fileLocation;
        public string FileLocation {
            get { return _fileLocation; } 
            set
            {
                _fileLocation = value;
                OnPropertyChanged("FileLocation");
            }
        }

        private string _wrongPathMessage;
        public string WrongPathMessage
        {
            get { return _wrongPathMessage; }
            set
            {
                _wrongPathMessage = value;
                OnPropertyChanged("WrongPathMessage");
            }
        }

        private string _serializationErrorMessage;
        public string SerializationErrorMessage
        {
            get { return _serializationErrorMessage; }
            set
            {
                _serializationErrorMessage = value;
                OnPropertyChanged("SerializationErrorMessage");
            }
        }

        private SelectFileCommand _selectFileCommand;
        public SelectFileCommand SelectFileCommand
        {
            get
            {
                return _selectFileCommand ??
                    (_selectFileCommand = new SelectFileCommand(
                        p =>
                        {
                            var dialog = new OpenFileDialog();

                            if (dialog.ShowDialog() == true)
                            {
                                SerializationErrorMessage = null;

                                var fileLocation = dialog.FileName;
                                FileLocation = fileLocation;
                            }
                        })                                             
                    );
            }
        }

        private AddConfigurationCommand _addConfigurationCommand;
        public AddConfigurationCommand AddConfigurationCommand
        {
            get
            {
                return _addConfigurationCommand ??
                    (_addConfigurationCommand = new AddConfigurationCommand(                        
                        p =>
                        {
                            SerializationErrorMessage = null;
                            try
                            {                         
                                Configurations.Add(Deserializer.DeserializeFile<Configuration>(FileLocation?.Trim()));
                                FileLocation = null;
                            }
                            catch
                            {
                                SerializationErrorMessage = "Ошибка десериализации";
                            }
                        },
                        p => ValidateFilePathAndExtension()));
            }
        }

        private bool ValidateFilePathAndExtension()
        {
            var trimmedFileLocation = FileLocation?.Trim();
            if (string.IsNullOrEmpty(trimmedFileLocation))
            {
                WrongPathMessage = null;
                return false;
            }

            var isPathValid = Validator.ValidateFilePath(trimmedFileLocation);
            var fileExists = File.Exists(trimmedFileLocation);

            var extension = Path.GetExtension(trimmedFileLocation).ToLower();
            var isExtensionValid = Deserializer.SupportedExtensions.Contains(extension);

            WrongPathMessage = isPathValid ? (fileExists ? (isExtensionValid ? null : "Расширение файла не поддерживается") : "Файл не найден") : "Неверный путь до файла";
            return isPathValid && fileExists && isExtensionValid;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
