using Calculator.Pages.SQLiteDemo.Entities;

namespace Calculator.Pages.SQLiteDemo.Services
{
    public interface IDbService
    {
        IEnumerable<EmployeePosition> GetAllPositions();
        IEnumerable<Responsibilities> GetPositionResponsibility(int id);
        void Init();
    }
}
