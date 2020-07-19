using System.IO;
using System.Data.SQLite;

namespace SP_Project_v1_0
{
    class Database
    {
        public SQLiteConnection myConnection;
        public Database()
        {
            myConnection = new SQLiteConnection("Data Source=record.sqlite3");
            if (!File.Exists("./record.sqlite3"))
            {
                SQLiteConnection.CreateFile("record.sqlite3");
                System.Console.WriteLine("Database file created");
            }
        }

        public void OpenConnection()
        {
            if (myConnection.State != System.Data.ConnectionState.Open)
            {
                myConnection.Open();
            }
        }

        public void CloseConnection()
        {
            if (myConnection.State != System.Data.ConnectionState.Closed)
            {
                myConnection.Clone();
            }
        }
    }
}