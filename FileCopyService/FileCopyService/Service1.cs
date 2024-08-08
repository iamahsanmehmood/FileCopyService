using System;
using System.IO;
using System.ServiceProcess;
using System.Threading;

namespace FileCopyService
{
    public partial class Service1 : ServiceBase
    {
        private FileSystemWatcher _watcher;
        private string _sourceFolder;
        private string _destinationFolder;
        private string _configFilePath = AppDomain.CurrentDomain.BaseDirectory + "config.txt";

        public Service1()
        {
            InitializeComponent(); // Ensures components are initialized
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                ReadConfig(); // Read configuration from file

                // Initialize the FileSystemWatcher
                _watcher = new FileSystemWatcher
                {
                    Path = _sourceFolder,
                    Filter = "*.txt", // Monitor only text files
                    NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite,
                    EnableRaisingEvents = true // Start monitoring
                };

                _watcher.Created += OnFileCreated; // Hook up event handler

                Log("Service started. Monitoring folder: " + _sourceFolder);
            }
            catch (Exception ex)
            {
                Log($"Error in OnStart: {ex.Message}");
                throw; // Fail service start if configuration is incorrect
            }
        }

        protected override void OnStop()
        {
            try
            {
                if (_watcher != null)
                {
                    _watcher.EnableRaisingEvents = false;
                    _watcher.Dispose(); // Cleanup resources
                }

                Log("Service stopped.");
            }
            catch (Exception ex)
            {
                Log($"Error in OnStop: {ex.Message}");
            }
        }

        private void OnFileCreated(object sender, FileSystemEventArgs e)
        {
            try
            {
                // Wait briefly to ensure the file is completely written
                Thread.Sleep(3000);

                string fileName = e.Name;
                string sourceFilePath = Path.Combine(_sourceFolder, fileName);
                string destinationFilePath = Path.Combine(_destinationFolder, fileName);

                File.Copy(sourceFilePath, destinationFilePath, true);

                Log($"File '{fileName}' copied to '{destinationFilePath}'");
            }
            catch (Exception ex)
            {
                Log($"Error copying file: {ex.Message}");
            }
        }

        private void ReadConfig()
        {
            try
            {
                string[] lines = File.ReadAllLines(_configFilePath);

                foreach (string line in lines)
                {
                    if (line.StartsWith("SourceFolder="))
                    {
                        _sourceFolder = line.Substring("SourceFolder=".Length).Trim();
                    }
                    else if (line.StartsWith("DestinationFolder="))
                    {
                        _destinationFolder = line.Substring("DestinationFolder=".Length).Trim();
                    }
                }

                if (string.IsNullOrEmpty(_sourceFolder) || string.IsNullOrEmpty(_destinationFolder))
                {
                    throw new Exception("Source or destination folder is not specified in the config file.");
                }
            }
            catch (Exception ex)
            {
                Log($"Error reading config file: {ex.Message}");
                throw; // Stop the service if configuration is incorrect
            }
        }

        private void Log(string message)
        {
            string logFilePath = AppDomain.CurrentDomain.BaseDirectory + "service.log";
            File.AppendAllText(logFilePath, $"{DateTime.Now}: {message}{Environment.NewLine}");
        }
    }
}
