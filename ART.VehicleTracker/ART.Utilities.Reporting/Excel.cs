using ClosedXML.Excel;
using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
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

                PdfWriter writer = new PdfWriter(filename + ".pdf");
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf);

                // Make a title on the PDF
                Paragraph title = new Paragraph(filename)
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                    .SetFontSize(20);

                document.Add(title);

                Paragraph subtitle = new Paragraph("Information")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                    .SetFontSize(15);

                document.Add(subtitle);

                Table table = new Table(cols, false);


                for(int iRow = 1; iRow <= rows; iRow++)
                {
                    for (int iCol = 1; iCol <= cols; iCol++)
                    {
                        xlWS.Cell(iRow, iCol).Value = data[iRow - 1, iCol - 1];

                        // PDF Cell
                        Cell cell = new Cell(1, 1);
                        cell.Add(new Paragraph(data[iRow - 1, iCol - 1]));

                        if (iRow == 1)
                        {
                            xlWS.Cell(iRow, iCol).Style.Font.Bold = true;
                            cell.SetBold();
                            cell.SetBackgroundColor(ColorConstants.LIGHT_GRAY);
                            cell.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                            xlWS.Cell(iRow, iCol).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                        }
                        else
                        {
                            xlWS.Cell(iRow, iCol).Style.Font.Bold = false;
                            cell.SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT);

                            if (iRow % 2 == 0)
                            {
                                xlWS.Cell(iRow, iCol).Style.Fill.SetBackgroundColor(XLColor.Cyan);
                                cell.SetBackgroundColor(ColorConstants.CYAN);
                            }
                        }

                        table.AddCell(cell);
                    }
                }

                //Autosize the column widths
                xlWS.Columns().AdjustToContents();

                //Deal with the borders
                IXLRange range = xlWS.Range(xlWS.Cell(1, 1).Address, xlWS.Cell(rows, cols).Address);
                range.Style.Border.InsideBorder = (XLBorderStyleValues.Thin);
                range.Style.Border.OutsideBorder = (XLBorderStyleValues.Thin);

                xlWB.SaveAs(filename + ".xlsx");
                table.SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER);
                document.Add(table);
                document.Close();

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
