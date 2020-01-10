using System.Collections.Generic;
using MySql.Data.MySqlClient;
using NovaCash.Sportsbook.Clients.Criteria;
using NovaCash.Sportsbook.Clients.Models;

namespace NovaCash.Sportsbook.Clients.Repositories
{
    public interface IBetDetailRepository
    {
        void InsertBetDetailBatch(InsertBetDetailBatchCriteria criteria);

        int SelectBetDetailLastVersion(SelectBetDetailLastVersionCriteria criteria);

        void UpdateBetDetailLastVersion(MySqlConnection conn, MySqlTransaction sqlTxn, UpdateBetDetailLastVersionCriteria criteria);

        IEnumerable<BetDetail> SelectBetDetails(SelectBetDetailsCriteria criteria);
    }
}