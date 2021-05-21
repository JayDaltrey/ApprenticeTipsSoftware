using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApprenticeTipsSoftware.Models
{
    public class ApprenticeshipModel
    {
        public string Route { get; set; }
        public string Name { get; set; }
        public string Reference { get; set; }
        public string Status { get; set; }
        public int Level { get; set; }
        public string Funding { get; set; }
        public int Duration { get; set; }
        public int CoreOptions { get; set; }
        public string Eqa { get; set; }
        public string Link { get; set; }

    }
}
