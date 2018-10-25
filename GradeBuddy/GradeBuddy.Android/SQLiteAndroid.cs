using SQLite;
using Xamarin.Forms;
using GradeBuddy.Data;
using GradeBuddy.Droid;
using System.IO;
[assembly: Dependency(typeof(SQLiteAndroid))]

namespace GradeBuddy.Droid
{
    public class SQLiteAndroid : ISQLite
    {

        public SQLiteAndroid() { }

        public SQLiteConnection GetConnection()
        {
            var fileName = "gradeDatabase.db3";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentsPath, fileName);

            var con = new SQLite.SQLiteConnection(path);
            return con;
        }

    }
}