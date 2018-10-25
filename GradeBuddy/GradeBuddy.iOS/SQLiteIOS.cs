using SQLite;
using Xamarin.Forms;
using GradeBuddy.Data;
using GradeBuddy.iOS;
using System.IO;
[assembly: Dependency(typeof(SQLiteIOS))]

namespace GradeBuddy.iOS
{
    public class SQLiteIOS : ISQLite
    {

        public SQLiteIOS() { }

        public SQLite.SQLiteConnection GetConnection()
        {
            var filename = "gradeDatabase.db3";
            var documentPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var libraryPath = Path.Combine(documentPath, "..", "Library");
            var path = Path.Combine(libraryPath, filename);
            var connection = new SQLite.SQLiteConnection(path);
            return connection;
        }

    }
}