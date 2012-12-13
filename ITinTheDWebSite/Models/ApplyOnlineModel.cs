using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace ITinTheDWebSite.Models
{
    public class ApplyOnlineModel
    {
        //public File Transcript { get; set; }
        //public File Resume { get; set; }

        public string Transcript { get; set; }
        public byte[] Resume { get; set; }
    }
}