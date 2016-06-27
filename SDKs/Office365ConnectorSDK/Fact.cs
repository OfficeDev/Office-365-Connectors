using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Office365ConnectorSDK
{
    public class Fact
    {
        public Fact() { }
        public Fact(string name, string value)
        {
            this.name = name;
            this.value = value;
        }
        public string name { get; set; }
        public string value { get; set; }
    }
}
