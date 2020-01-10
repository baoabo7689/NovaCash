using System;
using System.Collections.Generic;
using System.Data;
using Fanex.Data;
using Fanex.Data.Repository;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using NovaCash.Sportsbook.Clients.Configurations;
using NovaCash.Sportsbook.Clients.Criteria;
using NovaCash.Sportsbook.Clients.Models;

namespace NovaCash.Sportsbook.Clients.Repositories
{
    public class BetDetailRepository : IBetDetailRepository
    {
        private readonly string connectionString = AppSettings.Settings.BetDetailConnection;
        private readonly IDynamicRepository dynamicRepository;

        public BetDetailRepository(IDynamicRepository dynamicRepository = null)
        {
            this.dynamicRepository = dynamicRepository ?? new DynamicRepository();
        }

        public void InsertBetDetailBatch(InsertBetDetailBatchCriteria criteria)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                using (var sqlTxn = conn.BeginTransaction(IsolationLevel.ReadCommitted))
                {
                    InsertBetDetails(conn, sqlTxn, criteria);

                    if (criteria.BetDetailResult.Data != null)
                    {
                        UpdateBetDetailLastVersion(conn, sqlTxn, new UpdateBetDetailLastVersionCriteria
                        {
                            Version = criteria.BetDetailResult.Data.last_version_key
                        });
                    }

                    sqlTxn.Commit();
                }
            }
        }

        public void InsertBetDetails(
            MySqlConnection conn,
            MySqlTransaction sqlTxn,
            InsertBetDetailBatchCriteria criteria)
        {
            if (!criteria.IsValid())
            {
                return;
            }

            foreach (var betDetail in criteria.BetDetailResult.Data.BetDetails)
            {
                var betDetailCriteria = new InsertBetDetailCriteria
                {
                    BetDetail = betDetail
                };

                var cmd = new MySqlCommand(betDetailCriteria.GetSettingKey(), conn)
                {
                    CommandType = CommandType.StoredProcedure,
                    Transaction = sqlTxn
                };

                cmd.Parameters.AddWithValue("BetDetail", JsonConvert.SerializeObject(betDetailCriteria.BetDetail));
                cmd.ExecuteNonQuery();
            }
        }

        public int SelectBetDetailLastVersion(SelectBetDetailLastVersionCriteria criteria)
        {
            var result = 0;
            using (var objDb = ObjectDbFactory.CreateInstance(criteria.GetSettingKey()))
            {
                using (var reader = objDb.ExecuteReader(criteria))
                {
                    if (reader.Read())
                    {
                        result = Convert.ToInt32(reader["val"]);
                    }
                }
            }

            return result;
        }

        public void UpdateBetDetailLastVersion(
            MySqlConnection conn,
            MySqlTransaction sqlTxn,
            UpdateBetDetailLastVersionCriteria criteria)
        {
            if (criteria.IsInvalid())
            {
                return;
            }

            var cmd = new MySqlCommand(criteria.GetSettingKey(), conn)
            {
                CommandType = CommandType.StoredProcedure,
                Transaction = sqlTxn
            };

            cmd.Parameters.AddWithValue("Version", criteria.Version);
            cmd.ExecuteNonQuery();
        }

        public IEnumerable<BetDetail> SelectBetDetails(SelectBetDetailsCriteria criteria)
            => dynamicRepository.Fetch<BetDetail>(criteria);
    }
}