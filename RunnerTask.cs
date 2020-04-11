using System;
using System.Collections.Generic;
using System.Text;

namespace PublishRunner
{
    public class RunnerTask
    {
        /*
             "Path2Repo": "D:\\Netsdl\\NetsdlGeneralFileProvider",
    "Paht2SSHKey": "~/.ssh/id_ras-netsdl",
    "ServerName": "chenchu@47.99.98.62"
             */
        public string Path2Repo { get; set; }
        public string Paht2SSHKey { get; set; }
        public string ServerName { get; set; }
        public string Branch { get; set; }
    }
}
