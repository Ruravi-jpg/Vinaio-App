using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vinaio
{
    internal class ColumnMapping
    {
        public string DatabaseColumnName { get; set; }
        public string AppColumnName { get; set; }
        public bool ShowColumn { get; set; } = true;

        //public ColumnMapping(string databaseColumnName, string appColumnName, bool showColumn = true)
        //{
        //    DatabaseColumnName = databaseColumnName;
        //    AppColumnName = appColumnName;
        //    ShowColumn = showColumn;
        //}
    }
}
