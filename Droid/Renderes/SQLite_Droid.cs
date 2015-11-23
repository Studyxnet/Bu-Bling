using System;
using Xamarin.Forms;
using bubling.Droid;
using System.IO;
using SQLite.Net;

[assembly: Dependency(typeof(SQLite_Droid))]
namespace bubling.Droid
{
    public class SQLite_Droid : ISQLite
    {
        public SQLite.Net.SQLiteConnection GetConnection()
        {
            var sqliteFilename = "bubling.db3";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, sqliteFilename);
            var plat = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
            var conn = new SQLiteConnection(plat, path);
            return conn;
        }
    }
}

