using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vinaio
{
    internal static class ModifiedCellTracker
    {
        public static List<ModifiedCell> ModifiedCells { get; } = new List<ModifiedCell>();

        public static void Reset()
        {
            ModifiedCells.Clear();
        }   
    }
}
