using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using NovaCash.Sportsbook.Clients.Configurations;
using NovaCash.Sportsbook.Clients.Consts;
using NovaCash.Sportsbook.Clients.Criteria;
using NovaCash.Sportsbook.Clients.Resources;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace NovaCash.Sportsbook.Clients.ExcelServices
{
    public class GenericExcelService
    {
        private readonly ExportExcelCriteria criteria;
        private Type dataType;

        public GenericExcelService(ExportExcelCriteria criteria)
        {
            this.criteria = criteria;
        }

        public static string GetOutputPath(string fileName)
        {
            var output = Path.Combine(Directory.GetCurrentDirectory(), AppSettings.Settings.ExcelFolder);

            if (!Directory.Exists(output))
            {
                Directory.CreateDirectory(output);
            }

            return Path.Combine(output, fileName);
        }

        public static void SaveFile(string fileName, ExcelPackage p)
        {
            var output = GetOutputPath(fileName);
            p.SaveAs(new FileInfo(output));
        }

        public static void SetColumnWidths(ExcelWorksheet ws, int[] widths, int maxColumnIndex)
        {
            if (widths == null || widths.Length == 0)
            {
                return;
            }

            var i = 0;
            for (; i < widths.Length; i++)
            {
                ws.Column(i + 1).Width = widths[i];
            }

            for (; i < maxColumnIndex; i++)
            {
                ws.Column(i + 1).AutoFit();
            }
        }

        public static void SetHeader(ExcelWorksheet ws, IEnumerable<string> titles, int currentRow)
        {
            for (var i = 0; i < titles.Count(); i++)
            {
                var cell = ws.Cells[currentRow, i + 1];
                cell.Value = titles.ElementAt(i);
                cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                cell.Style.Fill.PatternType = ExcelFillStyle.Solid;

                var color = ColorTranslator.FromHtml(ExcelStyleConst.HeaderColor);
                cell.Style.Fill.BackgroundColor.SetColor(color);
            }
        }

        public void Export()
        {
            using (var p = new ExcelPackage())
            {
                var ws = p.Workbook.Worksheets.Add(criteria.SheetName);
                if (criteria.Data.Any())
                {
                    dataType = (criteria.Data as IEnumerable<object>).FirstOrDefault().GetType();
                    var propertyInfo = dataType.GetProperties();
                    ExportHeader(ws, propertyInfo);
                    ExportBody(ws, criteria.Data, propertyInfo);
                }
                else
                {
                    ExportEmptyData(ws);
                }

                SaveFile(criteria.FileName, p);
            }
        }

        private void ExportHeader(ExcelWorksheet ws, IEnumerable<PropertyInfo> propertyInfo)
        {
            SetColumnWidths(ws, criteria.ColumnWidths, maxColumnIndex: propertyInfo.Count());
            SetHeader(ws, propertyInfo.Select(p => p.Name), currentRow: 1);
            ws.View.FreezePanes(2, 1);
        }

        private void ExportBody(
            ExcelWorksheet ws,
            IEnumerable<dynamic> data,
            IEnumerable<PropertyInfo> propertyInfo)
        {
            var currentRow = 2;
            for (var i = 0; i < data.Count(); i++)
            {
                var bt = data.ElementAt(i);

                for (var j = 0; j < propertyInfo.Count(); j++)
                {
                    var prop = propertyInfo.ElementAt(j);
                    var propValue = prop.GetValue(bt, null);
                    ws.Cells[currentRow, j + 1].Value = propValue;
                }

                currentRow++;
            }
        }

        public void ExportEmptyData(ExcelWorksheet ws)
        {
            ws.Cells[1, 1].Value = Labels.ThereIsNoData;
        }
    }
}