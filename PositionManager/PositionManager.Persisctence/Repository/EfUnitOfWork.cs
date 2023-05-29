using PositionManager.Domain.Abstractions;
using PositionManager.Domain.Entities;
using PositionManager.Persisctence.Data;


namespace PositionManager.Persisctence.Repository
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly Lazy<IRepository<EmployeePosition>> _EmployeePositionRepository;
        private readonly Lazy<IRepository<Responsibility>> _responsibilityRepository;
        public EfUnitOfWork(AppDbContext context)
        {
            _context = context;
            _EmployeePositionRepository = new Lazy<IRepository<EmployeePosition>>(() =>
            new EfRepository<EmployeePosition>(context));
            _responsibilityRepository = new Lazy<IRepository<Responsibility>>(() =>
            new EfRepository<Responsibility>(context));
        }



        IRepository<EmployeePosition> IUnitOfWork.PositionRepository => _EmployeePositionRepository.Value;
        IRepository<Responsibility> IUnitOfWork.ResponsibilityRepository => _responsibilityRepository.Value;
        public async Task CreateDatabaseAsync()
        {
            await _context.Database.EnsureCreatedAsync();
        }
        public async Task RemoveDatbaseAsync()
        {
            await _context.Database.EnsureDeletedAsync();
        }
        public async Task SaveAllAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
