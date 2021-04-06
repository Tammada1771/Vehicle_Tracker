using ClosedXML.Excel;
using System;

namespace ART.Utilities.Reporting
{
    public static class Excel
    {
        public static void Export(string filename, string[,] data)
        {
            try
            {
                IXLWorkbook xlWB = new XLWorkbook();
                IXLWorksheet xlWS = xlWB.AddWorksheet(filename);

                int rows = data.GetLength(0);
                int cols = data.GetLength(1);

                for(int iRow = 1; iRow <= rows; iRow++)
                {
                    for (int iCol = 1; iCol <= cols; iCol++)
                    {
                        xlWS.Cell(iRow, iCol).Value = data[iRow - 1, iCol - 1];

                        if (iRow == 1)
                        {
                            xlWS.Cell(iRow, iCol).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                            xlWS.Cell(iRow, iCol).Style.Font.Bold = true;
                        }
                        else
                        {
                            xlWS.Cell(iRow, iCol).Style.Font.Bold = false;

                            if (iRow % 2 == 0)
                            {
                                xlWS.Cell(iRow, iCol).Style.Fill.SetBackgroundColor(XLColor.Cyan);
                            }
                        }

                    }
                }

                //Autosize the column widths
                xlWS.Columns().AdjustToContents();

                //Deal with the borders
                IXLRange range = xlWS.Range(xlWS.Cell(1, 1).Address, xlWS.Cell(rows, cols).Address);
                range.Style.Border.InsideBorder = (XLBorderStyleValues.Thin);
                range.Style.Border.OutsideBorder = (XLBorderStyleValues.Thin);

                xlWB.SaveAs(filename + ".xlsx");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
