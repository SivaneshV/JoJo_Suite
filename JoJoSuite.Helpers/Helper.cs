using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoJoSuite.Helpers
{
    public abstract class Helper
    {
        public static Worksheet _WorkSheet { get; set; }
        public static Workbook _WorkBook { get; set; }
    }
}
