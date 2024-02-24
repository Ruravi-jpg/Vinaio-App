using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vinaio
{
    public class KeyValueHolder
    {
        public string Key { get; set; }
        public object Value { get; set; }

        internal void Add(string key, object v)
        {
            this.Key = key;
            this.Value = v;
        }
    }
}
