using Calculator.Pages.SQLiteDemo.Entities;
using SQLite;


namespace Calculator.Pages.SQLiteDemo.Services
{
    public class SQLiteService : IDbService
    {
        private static SQLiteOpenFlags Flags =
        SQLite.SQLiteOpenFlags.ReadWrite |
        SQLite.SQLiteOpenFlags.Create |
        SQLite.SQLiteOpenFlags.SharedCache;
        
        public const string DatabaseFilename = "SQLiteDemo.db";

        public static string DatabasePath => Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);

        private SQLiteConnection Database;
        

        public IEnumerable<EmployeePosition> GetAllPositions()
        {
            return Database.Table<EmployeePosition>().ToList();
        }
        public IEnumerable<Responsibilities> GetPositionResponsibility(int id)
        {
            return Database.Table<Responsibilities>().Where(x => x.EmployeePositionId == id).ToList();
        }
        public void Init()
        {
            Database = new SQLiteConnection(DatabasePath, Flags);
            if (Database == null) return;
            
            Database.DropTable<EmployeePosition>();
            Database.DropTable<Responsibilities>();
            
            Database.CreateTable<EmployeePosition>();
            Database.Insert(new EmployeePosition { Name = "Manager", Description = "ManagerDescription" });
            Database.Insert(new EmployeePosition { Name = "Developer", Description = "DeveloperDescription" });
            Database.Insert(new EmployeePosition { Name = "Tester", Description = "TesterDescription"});

            Database.CreateTable<Responsibilities>();
            Database.Insert(new Responsibilities { Name = "Team Support", Description = "TeamSupportDescription", EmployeePositionId = 1 });
            Database.Insert(new Responsibilities { Name = "Risk Calculation", Description = "RiskCalculationDescription", EmployeePositionId = 1 });
            Database.Insert(new Responsibilities { Name = "Front-End", Description = "Front-End-Description", EmployeePositionId = 2 });
            Database.Insert(new Responsibilities { Name = "Back-End", Description = "Back-EndDescription", EmployeePositionId = 2 });
            Database.Insert(new Responsibilities { Name = "FullSatck", Description = "FullSatckDescription", EmployeePositionId = 2 });
            Database.Insert(new Responsibilities { Name = "CheckErrors", Description = "CheckErrorsDescription", EmployeePositionId = 3 });
            Database.Insert(new Responsibilities { Name = "CheckPlatformDifferences", Description = "CheckPlatformDifferencesDescription", EmployeePositionId = 3 });
            
        }
    }
}
