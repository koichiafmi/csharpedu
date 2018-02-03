using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace DbMultiTool
{
    class DbConnection : IDisposable
    {
        NpgsqlCommand _com;
        NpgsqlConnection _con;
        NpgsqlDataAdapter _da;
        //NpgsqlTransaction _tran;
        NpgsqlConnectionStringBuilder _constr;

        public DbConnection(string server, string database, string user, string password)
        {
            _constr = new NpgsqlConnectionStringBuilder();
            _constr.Host = server;
            _constr.Database = database;
            _constr.Username = user;
            _constr.Password = password;

            //接続テスト
            _con = new NpgsqlConnection(_constr.ConnectionString);
            _con.Open();
            _con.Close();
        }

        public void Open()
        {
            _con = new NpgsqlConnection(_constr.ConnectionString);
            _con.Open();
        }


        public DataSet ExecuteSql(string sql)
        {
            DataSet ds = new DataSet();

            _com = new NpgsqlCommand(sql, _con);
            _da = new NpgsqlDataAdapter(_com);

            _da.Fill(ds);

            return ds;
        }

        //public void Commit()
        //{

        //}

        //public void RollBack()
        //{

        //}

        #region IDisposable Support
        private bool disposedValue = false; // 重複する呼び出しを検出するには

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: マネージ状態を破棄します (マネージ オブジェクト)。
                }

                // TODO: アンマネージ リソース (アンマネージ オブジェクト) を解放し、下のファイナライザーをオーバーライドします。
                // TODO: 大きなフィールドを null に設定します。

                disposedValue = true;
            }
        }

        // TODO: 上の Dispose(bool disposing) にアンマネージ リソースを解放するコードが含まれる場合にのみ、ファイナライザーをオーバーライドします。
        // ~DbConnection() {
        //   // このコードを変更しないでください。クリーンアップ コードを上の Dispose(bool disposing) に記述します。
        //   Dispose(false);
        // }

        // このコードは、破棄可能なパターンを正しく実装できるように追加されました。
        public void Dispose()
        {
            // このコードを変更しないでください。クリーンアップ コードを上の Dispose(bool disposing) に記述します。
            Dispose(true);
            // TODO: 上のファイナライザーがオーバーライドされる場合は、次の行のコメントを解除してください。
            // GC.SuppressFinalize(this);
        }
        #endregion

    }
}
