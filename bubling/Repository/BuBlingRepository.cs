using System;

using SQLite.Net;
using Xamarin.Forms;

namespace bubling
{
    public class BuBlingRepository
    {
        private SQLiteConnection conn;
        private object _lock = new object();

        public BuBlingRepository()
        {
            this.conn = DependencyService.Get<ISQLite>().GetConnection();
            this.CreateDatabase();

            #if DEBUG
            this.MockUsuario();
            #endif
        }

        private void CreateDatabase()
        {
            this.conn.CreateTable<Usuario>();
        }

        public bool InserirUsuario(string email)
        {
            if (!String.IsNullOrEmpty(email))
            {
                var newUsuario = new Usuario { Email = email, Logado = true };

                lock (_lock)
                    return this.conn.InsertOrReplace(newUsuario) > 0;
            }

            return false;
        }

        public string RetornarEmailUsuarioLogado()
        {
            lock (_lock)
                return this.conn.Table<Usuario>().FirstOrDefault().Email;
        }

        public bool UsuarioLogado()
        {
            lock (_lock)
                return this.conn.Table<Usuario>().FirstOrDefault().Logado;
        }

        private void MockUsuario()
        {
            var u = new Usuario
            {
                Email = "contato@rm.eti.br",
                Logado = true
            };

            this.conn.InsertOrReplace(u);
        }
    }
}

