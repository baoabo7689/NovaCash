using NovaCash.Sportsbook.Clients.Criteria;
using NovaCash.Sportsbook.Clients.Repositories;

namespace NovaCash.Sportsbook.Clients.ExcelServices
{
    public class SportsbookExcelService
    {
        private readonly IBetDetailRepository repository;

        public SportsbookExcelService(IBetDetailRepository repository = null)
        {
            this.repository = repository ?? new BetDetailRepository();
        }

        public void ExportBetDetail()
        {
            var criteria = new ExportExcelCriteria
            {
                SheetName = "BetDetail",
                FileName = "BetDetail.xlsx",
                ColumnWidths = new[] { 15, 20, 20, 20 },
                Data = repository.SelectBetDetails(new SelectBetDetailsCriteria())
            };

            var exporter = new GenericExcelService(criteria);
            exporter.Export();
        }
    }
}