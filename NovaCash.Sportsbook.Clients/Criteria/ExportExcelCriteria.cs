using System.Collections.Generic;

namespace NovaCash.Sportsbook.Clients.Criteria
{
    public class ExportExcelCriteria
    {
        public string SheetName { get; set; } = "Sheet1";

        public string FileName { get; set; } = "Result.xlsx";

        public IEnumerable<dynamic> Data { get; set; }

        public int[] ColumnWidths { get; set; }
    }
}