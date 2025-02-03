using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyticalways.Acme.Tranversal.Common
{
    public class Response<T>
    {
        public bool Success { get; set; }

        public bool Error { get; set; }

        public string Message { get; set; }

        public dynamic Result { get; set; }
    }
}
