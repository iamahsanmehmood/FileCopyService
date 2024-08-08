# File Copy Service

**File Copy Service** is a Windows service designed to automatically copy files from a specified source folder to a destination folder. This service continuously monitors the source folder for new files and copies them to the destination as they appear.

## Features

- **Automatic File Monitoring**: The service watches the source folder for any new files and automatically copies them to the destination folder.
- **Customizable Configuration**: Configure source and destination folders via a simple text file.
- **Background Operation**: Runs silently in the background as a Windows service, requiring no manual intervention.

## Installation

### Prerequisites

- **Windows Operating System**: This service is designed to run on Windows.
- **Administrator Access**: You will need administrative privileges to install and manage the service.

### Installation Steps

1. **Download and Extract Files:**
   - Download the `FileCopyService.zip` package from the [releases](https://github.com/iamahsanmehmood/FileCopyService/releases) section.
   - Extract the contents to `C:\FileCopyService`.

2. **Edit Configuration:**
   - Open the `config.txt` file located in `C:\FileCopyService`.
   - Specify the source and destination folders:
     ```plaintext
     SourceFolder=C:\Path\To\Source\Folder
     DestinationFolder=D:\Path\To\Destination\Folder
     ```
   - Replace `C:\Path\To\Source\Folder` and `D:\Path\To\Destination\Folder` with your actual folder paths.

3. **Install the Service:**
   - Open **Command Prompt** as Administrator.
   - Navigate to the service directory:
     ```shell
     cd C:\FileCopyService
     ```
   - Run the following command to install the service:
     ```shell
     sc create FileCopyService binPath= "C:\FileCopyService\FileCopyService.exe"
     ```

4. **Start the Service:**
   - Start the service using the following command:
     ```shell
     net start FileCopyService
     ```

## Usage

- **Monitor Log Files**: Check the `service.log` file in `C:\FileCopyService` for any operational logs or errors.
- **Stop the Service**: To stop the service, run:
  ```shell
  net stop FileCopyService