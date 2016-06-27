using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Office365ConnectorSDK
{
    public class Section
    {
        public string title { get; set; }
        public string activityTitle { get; set; }
        public string activitySubtitle { get; set; }
        public string activityImage { get; set; }
        public string activityText { get; set; }
        public List<Fact> facts { get; set; }
        public List<Image> images { get; set; }
        public string text { get; set; }
        public bool? markdown { get; set; }
        public List<PotentialAction> potentialAction { get; set; }
    }
}
