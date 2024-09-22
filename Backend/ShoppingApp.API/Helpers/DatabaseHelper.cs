namespace ShoppingApp.API.Helpers
{
    public class DatabaseHelper
    {
        private const string DB_NAME = "ShoppingDb.db";
        public static string GetDatabasePath()
        {
            // Get the directory of the current assembly
            string basePath = AppContext.BaseDirectory;

            // Navigate 3 levels up to reach the project root
            string projectRoot = Path.GetFullPath(Path.Combine(basePath, @"..\..\.."));

            // Combine with the database file name
            string dbPath = Path.Combine(projectRoot, DB_NAME);

            return dbPath;
        }
    }
}
