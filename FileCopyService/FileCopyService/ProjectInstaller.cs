using System.ComponentModel;
using System.ServiceProcess;

namespace FileCopyService
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        private ServiceProcessInstaller serviceProcessInstaller;
        private ServiceInstaller serviceInstaller;

        public ProjectInstaller()
        {
            InitializeComponent();

            serviceProcessInstaller = new ServiceProcessInstaller();
            serviceInstaller = new ServiceInstaller();

            // Service will run under system account
            serviceProcessInstaller.Account = ServiceAccount.LocalSystem;

            // Service Information
            serviceInstaller.ServiceName = "FileCopyService";
            serviceInstaller.DisplayName = "File Copy Service";
            serviceInstaller.StartType = ServiceStartMode.Automatic;

            // Add installers to collection. Order is not important
            Installers.Add(serviceProcessInstaller);
            Installers.Add(serviceInstaller);
        }
    }
}
