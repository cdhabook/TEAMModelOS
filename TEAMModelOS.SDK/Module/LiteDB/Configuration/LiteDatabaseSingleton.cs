using LiteDB;

namespace TEAMModelOS.SDK.Module.LiteDB.Configuration
{
    public sealed  class LiteDatabaseSingleton
    {
        private static string _connectionString;
        private LiteDatabase database;

        private LiteDatabaseSingleton() { }



        public LiteDatabase GetDatabase()
        {
            if (database != null)
            {
                return database;
            }
            else
            {
                GetInstance(_connectionString);
                return database;
            }
        }

        public static LiteDatabaseSingleton GetInstance(string connectionString)
        {
            _connectionString = connectionString;
            return SingletonInstance.instance;
        }

        private static class SingletonInstance
        {
            public static LiteDatabaseSingleton instance = new LiteDatabaseSingleton()
            {
                database = new LiteRepository(_connectionString).Database
            };
        }
    }
}
