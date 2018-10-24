using SQLite;

namespace GradeBuddy.Data
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
