using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bochacha.Domain
{
    public class Equipment
    {
        public Guid id { get; set; }
        public string? type { get; set; } = String.Empty;
        public string? name { get; set; } = String.Empty;
        public int? serialID { get; set; }
        //public DateTime? addition_date { get; set; }

        //NAvi \__/\\\

        public Room Room { get; set; }
        public Guid RooId  { get; set; }

    }
}
