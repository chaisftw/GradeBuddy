using SQLite;

namespace GradeBuddy.Data
{
    interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
