using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Drawing;

namespace ATSManual.Import
{
    public static class Importer
    {


        private static string GetTemplate(string content)
        {
            return Properties.Resources.exportTemplate.Replace("${table}", content);
        }


        private static void FormatExcel(Excel.Range formatRange, Action<Excel.Range> formatter)
        {
            formatter(formatRange);
            ReleaseObject(formatRange);
        }

        public static float Luminance(Color color)
        {
            int[] colors = new int[] { color.R, color.G, color.B };
            var a = colors.Select(col =>
            {
                float v = col / 255f;
                return v <= 0.03928f ? v / 12.92f : (float)Math.Pow((v + 0.055f) / 1.055f, 2.4f);
            }).ToArray();

            return a[0] * 0.2126f + a[1] * 0.7152f + a[2] * 0.0722f;
        }

        public static float Contrast(Color a, Color b)
        {
            var lum1 = Luminance(a);
            var lum2 = Luminance(b);

            float brightest = Math.Max(lum1, lum2);
            float darkest = Math.Min(lum1, lum2);

            return (brightest + 0.05f) / (float)(darkest + 0.05f);
        }

        public static void ExportExcelFile(string path)
        {
            var items = App.store.Tree.items;


            Excel.Application excelApp = new Excel.Application();
            if (excelApp == null) return;

            Excel.Workbook workBook = null;
            Excel.Worksheet workSheet = null;

            try
            {

                //Type.Missing;

                //object misValue = Type.Missing; //System.Reflection.Missing.Value;

                excelApp.DisplayAlerts = false;
                excelApp.Workbooks.Add(Type.Missing);

                workBook = excelApp.Workbooks.Item[1];


                // Удаляет лишние листы
                for (int i = 0; i < workBook.Worksheets.Count; i++)
                    workBook.Worksheets[2].Delete();


                workSheet = workBook.Worksheets[1];

                workSheet.Cells[1, 1] = "Номер";
                workSheet.Cells[1, 2] = "Владелец";
                workSheet.Cells[1, 3] = "Данные";
                workSheet.Cells[1, 4] = "Примечание";


                FormatExcel(workSheet.Range["a1", "d1"], (frm) =>
                {
                    frm.EntireColumn.NumberFormat = "@";
                    frm.EntireRow.Font.Bold = true;
                    frm.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                });

                FormatExcel(workSheet.Range["a2", $"a{items.Count + 1}"], (frm) =>
                {
                    frm.Font.Color = ColorTranslator.ToOle(Color.Red);
                    frm.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                });




                for (int i = 0; i < items.Count; i++)
                {
                    var sub = items[i];

                    var dataRow = new object[] { sub.subscriber.phone, sub.subscriber.subscriberName, string.Join(", ", sub.data.Select(d => d.key)), sub.subscriber.description };



                    for (int j = 0; j < dataRow.Length; j++)
                    {
                        workSheet.Cells[i + 2, j + 1] = dataRow[j];
                        FormatExcel(workSheet.Range[$"a{i + 2}"], (frm) =>
                        {
                            var converter = new ColorConverter();

                            var statusColor = Forms.ManualForm.ConvertStatus((Model.SubscriberStatus)sub.subscriber.statusType);
                            if (statusColor == null) return;

                            var color = (Color)statusColor;

                            frm.Interior.Color = ColorTranslator.ToOle(color);

                            float contrast = Contrast(color, Color.IndianRed);

                            if (contrast < 3f)
                                frm.Font.Color = ColorTranslator.ToOle(System.Drawing.Color.White);
                        });
                    }
                }

                workSheet.Columns.AutoFit();

                //var dir = System.Windows.Forms.Application.StartupPath + "\\backup";

                //Directory.CreateDirectory(dir);
                //Import.Importer.ExportExcelFile();
                //path = dir + $"\\meow.xlsx";

                workBook.SaveAs(path, Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlShared, Excel.XlSaveConflictResolution.xlOtherSessionChanges, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                workBook.Close(true, Type.Missing, Type.Missing);
                excelApp.Quit();

                //System.Windows.Forms.MessageBox.Show("Экспорт успешно завершён");
            }
            catch (Exception ex)
            {
                Logging.Logger.Log("Ошибка при экспорте: " + ex.Message, Logging.Logger.MessageType.Error);
                System.Windows.Forms.MessageBox.Show("Произошла ошибка при экспорте: " + ex.Message, "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                ReleaseObject(workSheet);
                ReleaseObject(workBook);
                ReleaseObject(excelApp);

            }
        }

        public struct ImportData
        {
            public string[] row;
            public Model.SubscriberStatus status;
            public bool IsEmpty { get { return row.All(r => string.IsNullOrWhiteSpace(r)); } }
        }

        public static List<ImportData> ImportExcelFile(string path)
        {
            List<ImportData> result = new List<ImportData>();
            Excel.Application excelApp = new Excel.Application();
            if (excelApp == null) return result;

            Excel.Workbook workBook = null;
            Excel.Worksheet workSheet = null;

            try
            {
                excelApp.DisplayAlerts = false;
                workBook = excelApp.Workbooks.Open(path, 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

                workSheet = workBook.Worksheets[1];


                for (int i = 2; i <= workSheet.Rows.Count; i++)
                {


                    //converter.
                    string[] row = new string[4];
                    var unparsedColor = (int)workSheet.Cells[i, 1].Interior.Color;
                    System.Drawing.Color color = System.Drawing.ColorTranslator.FromOle(unparsedColor);

                    for (int j = 1; j <= 4; j++)
                    {
                        var content = workSheet.Cells[i, j].Text.ToString();
                        row[j - 1] = string.IsNullOrWhiteSpace(content) ? null : content;
                    }

                    ImportData data = new ImportData() { row = row, status = Forms.ManualForm.ConvertColor(color) };
                    if (data.IsEmpty) break;

                    result.Add(data);
                }

                workBook.Close(true, Type.Missing, Type.Missing);
                excelApp.Quit();
            }
            catch (Exception ex)
            {
                Logging.Logger.Log("Ошибка при импорте: " + ex.Message, Logging.Logger.MessageType.Error);
                System.Windows.Forms.MessageBox.Show("Произошла ошибка при импорте: " + ex.Message, "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                ReleaseObject(workSheet);
                ReleaseObject(workBook);
                ReleaseObject(excelApp);
            }

            return result;


            //// Активный лист
            //workSheet = (Excel.Worksheet)workBook.Worksheets[1];


            //Excel.Range formatRange;
            //formatRange = workSheet.Range["a1"];
            //formatRange.EntireColumn.Font.Bold = true;

            //workBook.Worksheets.Add(misValue, misValue, misValue, misValue);
            //workBook.Worksheets[2].Name = "привет";


        }


        private static void ReleaseObject(object obj)
        {
            try
            {
                Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                Logging.Logger.Log("Ошибка очистки памяти СOM Excel", Logging.Logger.MessageType.Error);
                obj = null;
            }
            finally
            {
                GC.Collect();
            }
        }

        /// <summary>
        /// Импортирует нужное количество столбцов построчно из XML-файла Excel документа 
        /// </summary>
        /// <param name="path">Путь до .xml файла</param>
        /// <param name="columns">Количество столбцов</param>
        /// <param name="validator">Проверяющий коллбек</param>
        /// <returns></returns>
        public static List<string[]> ImportRows(string path, int columns, Predicate<string[]> validator = null)
        {
            var importedRows = new List<string[]>();

            XNamespace ss = "urn:schemas-microsoft-com:office:spreadsheet";

            using (FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                var xml = XElement.Load(stream);
                var rows = xml.Descendants(ss + "Row").Skip(1);

                var rowId = 1;

                foreach (var row in rows)
                {
                    var extractedRow = new string[columns];
                    var cells = row.Elements().ToList();

                    for (var i = 0; i < Math.Min(columns, cells.Count); i++)
                    {
                        var cell = cells[i];

                        var indexAttribute = cell.Attribute(ss + "Index");
                        var index = indexAttribute != null ? int.Parse(indexAttribute.Value) - 1 : i;

                        if (index > columns) break;

                        var value = cell.Value.Trim();

                        extractedRow[index] = value != "" ? value : null;
                    }

                    if (validator(extractedRow))
                        importedRows.Add(extractedRow);
                    else throw new InvalidConstraintException($"Row {rowId} ({string.Join(" | ", extractedRow)}) doesn't match validator.");

                    rowId++;
                }

                return importedRows;
            }
        }

        private static string GetRowTemplate(string content)
        {
            return $"<Row ss:AutoFitHeight=\"0\">{content}</Row>";
        }

        private static string GetColumnTemplate(int? width = null)
        {
            var widthAttr = width != null ? $"ss:Width=\"{width}.0\"" : "";
            return $"<Column {widthAttr} />";
        }

        private static string GetCellTemplate(string data, string dataType = "String")
        {
            return $"<Cell><Data ss:Type=\"{dataType}\">{data}</Data></Cell>";
        }

        public static void Export(string path)
        {
            var items = App.store.Tree.items;
            var columns = new string[] { GetColumnTemplate(30), GetColumnTemplate(204), GetColumnTemplate(50), GetColumnTemplate(145) };
            var rows = items.Select(item =>
            {
                var sub = item.subscriber;

                var cells = new string[] {
                    GetCellTemplate(sub.phone.ToString()),
                    GetCellTemplate(sub.subscriberName),
                    GetCellTemplate(string.Join(", ", item.data.Select(d => d.key))),
                    GetCellTemplate(sub.description)
                };

                return GetRowTemplate(string.Join("", cells));
            });

            var headerRow = GetRowTemplate(string.Join("", new string[] { GetCellTemplate("Номер"), GetCellTemplate("Владелец"), GetCellTemplate("Данные"), GetCellTemplate("Примечание") }));

            var tmp = GetTemplate(string.Join("", columns) + headerRow + string.Join("", rows));

            try
            {

                File.WriteAllText(path, tmp, Encoding.UTF8);
                Logging.Logger.Log($"База данных экспортирована по пути {path}");
            }
            catch (System.IO.IOException ex)
            {
                Logging.Logger.Log($"Ошибка при экспорте базы данных по пути {path}");
                System.Windows.Forms.MessageBox.Show("Ошибка при экспорте базы данных: " + ex.Message, "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
    }
}
