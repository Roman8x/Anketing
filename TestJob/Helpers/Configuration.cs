using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TestJob.Helpers
{
    public class Configuration
    {
        private string workingDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Анкеты");

        public Configuration() {
            WorkingDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Анкеты");
        }
        public string WorkingDirectory
        {
            get => workingDirectory;
            set
            {
                workingDirectory = value;
                if (value != null && !System.IO.Directory.Exists(value))
                    System.IO.Directory.CreateDirectory(value);
            }
        }
    }
}