using System;

using SQLite.Net;
using Xamarin.Forms;

namespace bubling.Droid
{
    public class BuBlingRepository_Droid
    {
        private SQLiteConnection conn;
        private object _lock = new object();

        public BuBlingRepository_Droid()
        {
            var sqlService = new SQLite_Droid();
            this.conn = sqlService.GetConnection();
        }

        public string RetornarEmailUsuarioLogado()
        {
            lock (_lock)
                return this.conn.Table<Usuario>().FirstOrDefault().Email;
        }
    }
}

