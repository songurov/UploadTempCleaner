using System.ServiceProcess;

namespace UploadTempCleaner
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            var servicesToRun = new ServiceBase[]
            {
                new UploadTempCleaner()
            };
            ServiceBase.Run(servicesToRun);
        }
    }
}
