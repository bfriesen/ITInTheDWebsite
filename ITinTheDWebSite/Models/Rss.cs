using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace ITinTheDWebSite.Models
{
    public class Rss
    {
        public string Link { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}