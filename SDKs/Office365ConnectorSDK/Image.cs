using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Office365ConnectorSDK
{
    public class Image
    {
        public Image() { }
        public Image(string image) { this.image = image; }
        public Image(string image, string title) { this.image = image; this.title = title; }
        public string title { get; set; }
        public string image { get; set; }
    }
}
