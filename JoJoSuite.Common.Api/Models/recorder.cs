using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JoJoSuite.Common.Api.Models
{
    public class Recorder
    {
        public string element { get; set; }
        public string value { get; set; }
        public string scrLoc { get; set; }
        public string absXpath { get; set; }
        public string[] relXpath { get; set; }

    }
    public class RelXpath
    {
        public string xpath { get; set; }        
    }

}
