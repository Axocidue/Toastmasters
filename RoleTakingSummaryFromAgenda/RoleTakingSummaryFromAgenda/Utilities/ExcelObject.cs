using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace RoleTakingSummaryFromAgenda.Utilities
{
    

    public class ExcelObject: IDisposable
    {
        private Excel.Application excelApp;
        private Excel.Workbook workbook;
        private Excel.Worksheet worksheet;
        private Excel.Range range;
        private Excel.Range range1;
        private Excel.Range range2;

        public ExcelObject(string filename)
        {
            excelApp = new Excel.Application();

            excelApp.Visible = false;
            excelApp.DisplayAlerts = false;
            excelApp.AlertBeforeOverwriting = false;

            workbook = excelApp.Workbooks.Open(filename, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            worksheet = workbook.Worksheets[1] as Excel.Worksheet;
        }

        public void Quit()
        {
            if (workbook != null)
                workbook.Close(null, null, null);
            if (excelApp != null)
            {
                excelApp.Workbooks.Close();
                excelApp.Quit();
            }
            if (range != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(range);
                range = null;
            }
            if (range1 != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(range1);
                range1 = null;
            }
            if (range2 != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(range2);
                range2 = null;
            }
            if (worksheet != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                worksheet = null;
            }
            if (workbook != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                workbook = null;
            }
            if (excelApp != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
                excelApp = null;
            }
            GC.Collect();
        }

        public void Dispose()
        {
            Quit();
        }

        public string At(int rowNumber, int columnNumber)
        {
            return (worksheet.Cells[rowNumber, columnNumber] as Excel.Range).Text.ToString();
        }
        
    }
}
