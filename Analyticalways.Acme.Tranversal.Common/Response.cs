using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyticalways.Acme.Tranversal.Common
{
    public class Response<T>
    {
        public bool success { get; set; }

        public bool error { get; set; }

        public string message { get; set; }

        public dynamic result { get; set; }
    }
}
