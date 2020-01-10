using System.Collections.Generic;
using NovaCash.Sportsbook.Clients.Criteria;

namespace NovaCash.Sportsbook.Clients.ExcelServices
{
    public class SampleExcelService
    {
        public static void Export()
        {
            var testData = new List<dynamic>
            {
                new { trans_id = 1, vendor_member_id = "a1", winlost_amount = 10, stake = 10 },
                new { trans_id = 2, vendor_member_id = "a2", winlost_amount = 10, stake = 10 },
                new { trans_id = 3, vendor_member_id = "a3", winlost_amount = 10, stake = 10 }
            };

            var exporter = new GenericExcelService(new ExportExcelCriteria
            {
                SheetName = "Sheet1",
                FileName = "Sample.xlsx",
                ColumnWidths = new[] { 15, 20, 20, 20 },
                Data = testData
            });

            exporter.Export();
        }
    }
}