using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vinaio
{
    public class CalculatedColumn
    {
        public string Name { get; set; }
        public string OptionalColumnName { get; set; }
        public Func<DataRow, Dictionary<string, object>, Object>? Calculation { get; set; }
        public string? Format { get; set; }
        public bool RequiresExternalValues { get; set; }

        public Object Calculate(DataRow row, Dictionary<string, object> parameters)
        {
            return Calculation(row, parameters);
        }
    }
}
