using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wakey_Wakey.Classes
{
    struct NetworkDevice
    {
        public int id { get; set; }
        public int port { get; set; }
        public string name { get; set; }
        public string ip { get; set; }
        public string mac { get; set; }
        public string subnet { get; set; }
        public string adapter { get; set; }
        public bool status { get; set; }
    }
}
