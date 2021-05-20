using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApprenticeTipsSoftware.Models
{
    public class ContactModel
    {
        public string EmailAddress { get; set; }
        public string Comment { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string ContactNumber { get; set; }
        public string Qualification { get; set; }

        //checkbox values

        public int Agriculture { get; set; }
        public int Business { get; set; }
        public int Care { get; set; }
        public int Catering { get; set; }
        public int Construction { get; set; }
        public int Creative { get; set; }
        public int Digital { get; set; }
        public int Education { get; set; }
        public int Engineering { get; set; }
        public int Hair { get; set; }
        public int Health { get; set; }
        public int Legal { get; set; }
        public int Protective { get; set; }
        public int Sales { get; set; }
        public int Transport { get; set; }

    }
}
