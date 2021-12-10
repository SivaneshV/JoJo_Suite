using JoJoExcel = Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoJoSuite.Actions.Helper
{
    public interface IHelper
    {
        JoJoExcel.Workbook _WorkBook { get; set; }
    }
}
