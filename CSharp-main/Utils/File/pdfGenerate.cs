using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Utils.File
{
    public class pdfGenerate
    {
          public PdfResponse ExportToPdf(List<ExportToExcelModel> listTables, string fileName)
        {
            var response = new PdfResponse()
            {
                ErrorId = 1,
                FileDownloadName = fileName,
                FileNameOnDisk = Guid.NewGuid().ToString() + ".pdf"
            };

            string directory = Path.Combine(Constants.Files.Path, "Pdf");
            string fullPath = Path.Combine(directory, response.FileNameOnDisk);

            FileInfo FilePath = new FileInfo(fullPath);

            if (listTables != null && listTables.Count > 0)
            {
                try
                {
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    if (FilePath.Exists)
                    {
                        FilePath.Delete();
                    }

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        var document = new Document(PageSize.A4, 25f, 25f, 30f, 30f);

                        var writer = PdfWriter.GetInstance(document, stream);

                        document.Open();

                        foreach (ExportToExcelModel excelModel in listTables)
                        {
                            var table = new PdfPTable(excelModel.Table.Columns.Count);

                            table.WidthPercentage = 100;
                            table.SetWidths(Enumerable.Repeat((float)1 / excelModel.Table.Columns.Count, excelModel.Table.Columns.Count).ToArray());

                            var fontBold = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10);
                            var fontNormal = FontFactory.GetFont(FontFactory.HELVETICA, 10);

                            var title = new PdfPCell(new Phrase(excelModel.Name, fontBold));

                            title.Colspan = excelModel.Table.Columns.Count;
                            title.HorizontalAlignment = 1;

                            table.AddCell(title);

                            foreach (DataColumn column in excelModel.Table.Columns)
                            {
                                var cell = new PdfPCell(new Phrase(column.ColumnName, fontBold));

                                cell.HorizontalAlignment = 1;

                                table.AddCell(cell);
                            }

                            foreach (DataRow row in excelModel.Table.Rows)
                            {
                                foreach (DataColumn column in excelModel.Table.Columns)
                                {
                                    var cell = new PdfPCell(new Phrase(row[column].ToString(), fontNormal));

                                    cell.HorizontalAlignment = 1;

                                    table.AddCell(cell);
                                }
                            }

                            document.Add(table);
                        }

                        document.Close();
                        writer.Close();
                    }

                    response.ErrorId = 0;
                }
                catch (Exception e)
                {
                    response.ErrorId = 500;
                    response.Message = "Falha ao gerar o PDF";
                }
            }
            else
            {
                response.Message = "Falha ao gerar o PDF";
            }

            return response;
        }
    }
}