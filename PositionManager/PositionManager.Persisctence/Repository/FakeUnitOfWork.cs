using PositionManager.Domain.Abstractions;
using PositionManager.Domain.Entities;
using PositionManager.Persisctence.Data;

namespace PositionManager.Persisctence.Repository
{
    public class FakeUnitOfWork : IUnitOfWork
    {
        //private readonly AppDbContext _context;
        private readonly Lazy<IRepository<EmployeePosition>> _EmployeePositionRepository;
        private readonly Lazy<IRepository<Responsibility>> _responsibilityRepository;

        public FakeUnitOfWork(/*AppDbContext context*/)
        {
            //_context = context;
            _EmployeePositionRepository = new Lazy<IRepository<EmployeePosition>>(() =>
            new FakeEmployeePositionRepository());
            _responsibilityRepository = new Lazy<IRepository<Responsibility>>(() =>
            new FakePositionResponsibilitiesRepository());
        }

        public IRepository<EmployeePosition> PositionRepository => _EmployeePositionRepository.Value;

        public IRepository<Responsibility> ResponsibilityRepository => _responsibilityRepository.Value;

        public async Task CreateDatabaseAsync()
        {
            //await _context.Database.EnsureCreatedAsync();
        }
            
        public async Task RemoveDatbaseAsync()
        {
            //await _context.Database.EnsureDeletedAsync();
        }

        public async Task SaveAllAsync()
        {
            //await _context.SaveChangesAsync();
        }
    }
}
