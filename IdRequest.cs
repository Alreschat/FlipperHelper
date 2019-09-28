using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Flipper_Extended
{
    public struct Status
    {
        public string option;
    }
    public struct Exchange
    {
        public Status status;
        public List<string> have;
        public List<string> want;
        public int? minimum;
    }
    class IdRequest
    {
        public Exchange exchange;
        public IdRequest()
        {
            exchange.have = new List<string>();
            exchange.want = new List<string>();
        }
    }
}
