using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApprenticeTipsSoftware.Models
{
    public class ApprenticeshipFinderRequest
    {
        public string Route { get; set; }

        public string Status { get; set; }

        public string Level { get; set; }

        public string Duration { get; set; }

        //bool values
        public bool BoolRoute { get; set; }
        public bool BoolStatus { get; set; }
        public bool BoolLevel { get; set; }
        public bool BoolDuration { get; set; }
    }
}
