using System;

using SQLite.Net.Attributes;

namespace bubling
{
    [Table("Usuario")]
    public class Usuario
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }

        public string Email { get; set; }

        public bool Logado { get; set; }
    }
}

