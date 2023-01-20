using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bochacha.Domain
{
    public class Request
    {

        public Guid id { get; set; }
        public string? type { get; set; } = String.Empty;
        public string? contents { get; set; } = String.Empty;
        public string? status { get; set; } = String.Empty;
        public DateTime? date { get; set; }

        //NAvi \__/\\\

        public USER? USER { get; set; }
        public Guid UserId  { get; set; }

    }
}
