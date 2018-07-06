using System;
using System.IO;
using System.ServiceProcess;
using System.Timers;

namespace UploadTempCleaner
{
    public partial class UploadTempCleaner : ServiceBase
    {
        public string ROOT_FOLDER => System.Configuration.ConfigurationSettings.AppSettings["path"];

        public string EXTENSIONS => System.Configuration.ConfigurationSettings.AppSettings["extensions"];

        private static Timer LocalTimer => new Timer(3600000);

        public UploadTempCleaner()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            LocalTimer.Elapsed += (sender, e) => RemoveFile();
            LocalTimer.Start();
        }

        private void RemoveFile()
        {
            try
            {
                var files = Directory.GetFiles(ROOT_FOLDER, "*" + EXTENSIONS, SearchOption.AllDirectories);
                foreach (var f in files)
                {
                    var fileDate = File.GetCreationTime(f);

                    if (fileDate.Day < DateTime.Now.Day)
                        File.Delete(f);

                    eventLog1.WriteEntry("Delete File from Dir :" + f);
                }
            }
            catch (Exception e)
            {
                eventLog1.WriteEntry(e.Message);
            }
        }

        protected override void OnStop()
        {
            eventLog1.WriteEntry("Stop Services UploadTempCleaner");
            LocalTimer.Stop();
        }
    }
}
