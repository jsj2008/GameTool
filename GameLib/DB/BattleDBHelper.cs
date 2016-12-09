using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace PublicUtilities
{
    /// <summary>
    /// WOW数据库
    /// </summary>
    public class BattleDBHelper : IDisposable
    {
        const string DBName = @"DB\DB.db3";
        static SQLiteConnection sqlCon = null;
        protected static LogManagerBase LogManager = WowLogManager.Instance;

        static BattleDBHelper()
        {
            string appPath = AppDomain.CurrentDomain.BaseDirectory;
            string dbPath = Path.Combine(appPath, DBName);
            if (File.Exists(dbPath))
            {
                string connectionString = string.Format("data source={0}", dbPath);
                sqlCon = new SQLiteConnection(connectionString);
                sqlCon.Open();
            }
        }

        private static bool IsSqlConOpened
        {
            get
            {
                return (null != sqlCon) && (sqlCon.State == ConnectionState.Open);
            }
        }

        private static object lockGetObj = new object();
        public static int GetHistoryCount(params WowLoginStates[] accountStates)
        {
            lock (lockGetObj)
            {
                if (IsSqlConOpened)
                {
                    //string sql = " select count(distinct email) as itemsCount from HistoryLists";
                    string sql = "select count(distinct accountid)  as itemsCount from HistoryLists";
                    if ((null != accountStates) && (accountStates.Length > 0))
                    {
                        sql += " where accountstate in(";
                        foreach (WowLoginStates item in accountStates)
                        {
                            sql += string.Format("\'{0}\',", item);
                        }
                        //Remove the last ,
                        if (sql.EndsWith(","))
                        {
                            sql = sql.Substring(0, sql.Length - 1);
                        }

                        sql += ")";
                    }

                    using (SQLiteCommand sqlCmd = new SQLiteCommand(sql, sqlCon))
                    {
                        using (SQLiteDataReader reader = sqlCmd.ExecuteReader())
                        {
                            reader.Read();
                            object obj = reader["itemsCount"];
                            int i = Convert.ToInt32(obj);
                            return i;
                        }
                    }
                }
                return 0;
            }
        }

        public static IList<int> GetQueriedIndex()
        {
            IList<int> indexList = new List<int>();
            if (IsSqlConOpened)
            {
                string sql = "select Queriedid from QueriedAccount";
                using (SQLiteCommand sqlCmd = new SQLiteCommand(sql, sqlCon))
                {
                    using (SQLiteDataReader reader = sqlCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int i = Convert.ToInt32(reader["Queriedid"]);
                            indexList.Add(i);
                        };
                    }
                }
            }
            return indexList;
        }

        public static bool IsQueried(int userIndex)
        {
            if (IsSqlConOpened)
            {
                string sql = string.Format("SELECT queriedid from queriedaccount where queriedid={0}", userIndex);
                using (SQLiteCommand sqlCmd = new SQLiteCommand(sql, sqlCon))
                {
                    using (SQLiteDataReader reader = sqlCmd.ExecuteReader())
                    {
                        bool isQueried = reader.Read();
                        return isQueried;
                    }
                }
            }
            return false;
        }

        public static bool ClearTable(params WOWTableName[] tableNames)
        {
            if (IsSqlConOpened)
            {
                foreach (WOWTableName t in tableNames)
                {
                    ClearTable(t);
                }
                return true;
            }

            return false;
        }

        public static bool ClearTable(WOWTableName tableName)
        {
            if (IsSqlConOpened)
            {
                string sql = string.Format("delete from {0}", tableName);
                using (SQLiteCommand sqlCmd = new SQLiteCommand(sql, sqlCon))
                {
                    int i = sqlCmd.ExecuteNonQuery();
                    return i > 0;
                }
            }

            return false;
        }

        private static IList<string> sqlHistoryList = new List<string>();

        public static bool InsertHistory(UserAccountItem account)
        {
            if (IsSqlConOpened)
            {
                string sql = string.Format(" insert into HistoryLists (AccountId,AccountState,Email,Password,AccountDetail,SubAccountDetail)" +
                                           " values ({0},\'{1}\',\'{2}\',\'{3}\',\'{4}\',\'{5}\') ",
                                           account.Index, account.State, account.EMail, account.Password,
                                           account.AccountStateString, account.SubAccountDetailString);

                if (!string.IsNullOrEmpty(sql))
                {
                    //return RunSql(sql);
                    sqlHistoryList.Add(sql);
                    if (sqlHistoryList.Count % INSERTMAX == 0)
                    {
                        return InsertData(sqlHistoryList);
                    }
                }
            }

            return false;
        }

        private static object lockInsertObj = new object();
        private static int INSERTMAX = 10;
        private static IList<string> sqlList = new List<string>();

        public static bool InsertQueriedItems(UserAccountItem account)
        {
            lock (lockInsertObj)
            {
                if (IsSqlConOpened)
                {
                    string sql = string.Format(" insert into QueriedAccount (QueriedId,QueriedEmail) values ({0},\'{1}\')",
                                             account.Index, account.EMail);

                    if (!string.IsNullOrEmpty(sql))
                    {
                        sqlList.Add(sql);
                        if (sqlList.Count % INSERTMAX == 0)
                        {
                            return InsertData(sqlList);
                        }
                    }
                }
                return false;
            }
        }

        private static bool RunSql(string sql)
        {
            if (!string.IsNullOrEmpty(sql))
            {
                try
                {
                    using (SQLiteCommand cmd = sqlCon.CreateCommand())
                    {
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    LogManager.Error(string.Format("执行Insert sql时出错：{0}", ex.Message));
                }
            }
            return false;
        }

        private static bool InsertData(IList<string> sqlList)
        {
            if ((sqlList == null) || (sqlList.Count == 0))
            {
                return false;
            }

            try
            {
                using (SQLiteTransaction trans = sqlCon.BeginTransaction())
                {
                    using (SQLiteCommand cmd = sqlCon.CreateCommand())
                    {
                        foreach (string sql in sqlList)
                        {
                            cmd.CommandText = sql;
                            int result = cmd.ExecuteNonQuery();
                        }
                    }
                    trans.Commit();
                }
                LogManager.Info(string.Format("Insert batch data to DB:{0} items", sqlList.Count));
                sqlList.Clear();
                return true;
            }
            catch (Exception ex)
            {
                LogManager.Error(string.Format("执行Insert sql时出错：{0}", ex.Message));
            }
            return false;
        }

        static object saveObj = new object();
        public static bool SaveCacheData()
        {
            lock (saveObj)
            {
                InsertData(sqlList);
                return InsertData(sqlHistoryList);
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (IsSqlConOpened)
            {
                using (sqlCon)
                {
                    sqlCon.Close();
                }
            }
        }

        #endregion IDisposable Members
    }

    /// <summary>
    /// WOW表
    /// </summary>
    public enum WOWTableName
    {
        QueriedAccount,
        HistoryLists
    }
}