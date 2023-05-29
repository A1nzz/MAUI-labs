using Microsoft.EntityFrameworkCore;
using PositionManager.Domain.Abstractions;
using PositionManager.Domain.Entities;
using System.Linq.Expressions;

namespace PositionManager.Persisctence.Repository
{
    public class FakePositionResponsibilitiesRepository : IRepository<Responsibility>
    {
        List<Responsibility> _positionResponsibilities;
        public FakePositionResponsibilitiesRepository()
        {
            _positionResponsibilities = new List<Responsibility>()
            {
                new Responsibility {Id=1, Name="Name1", Importance=3,Description = "Managing assets", EmployeePositionId = 1 },
                new Responsibility {Id=2, Name="Name2", Importance=1,Description = "Making major decisions", EmployeePositionId = 1 },
                new Responsibility {Id=3, Name="Name3", Importance=1,Description = "Setting goals", EmployeePositionId = 1 },
                new Responsibility {Id=4, Name="Name4", Importance=4,Description = "Monitor company perfomance", EmployeePositionId = 1 },
                new Responsibility {Id=5, Name="Name5", Importance=1,Description = "Setting precedence for the working culture and environment", EmployeePositionId = 1 },
                new Responsibility {Id=6, Name="Name6", Importance=1,Description = "Manage the software development team", EmployeePositionId = 2 },
                new Responsibility {Id=7, Name="Name7", Importance=10,Description = "Control troject timelines", EmployeePositionId = 2 },
                new Responsibility {Id=8, Name="Name8", Importance=1,Description = "Hiring and recruiting", EmployeePositionId = 2 },
                new Responsibility {Id=9, Name="Name9", Importance=2,Description = "Manage the tools", EmployeePositionId = 2 },
                new Responsibility {Id=10,Name="Name10",Importance=1, Description = "Ensure software quality", EmployeePositionId = 2 },
                new Responsibility {Id=11,Name="Name11",Importance=3, Description = "Writing and implementing efficient code", EmployeePositionId = 3 },
                new Responsibility {Id=12,Name="Name12",Importance=11, Description = "Maintaining and upgrading existing systems", EmployeePositionId = 3 },
                new Responsibility {Id=13,Name="Name13",Importance=1, Description = "Testing and evaluating new programs", EmployeePositionId = 3 },
                new Responsibility {Id=14,Name="Name14",Importance=1, Description = "Designing programs", EmployeePositionId = 3 },
                new Responsibility {Id=15,Name="Name15",Importance=1, Description = "Training users", EmployeePositionId = 3 }
            };
        }
        public Task<Responsibility> GetByIdAsync(int id, CancellationToken cancellationToken = default, params Expression<Func<Responsibility, object>>[]? includesProperties) => throw new NotImplementedException();
        public Task<IReadOnlyList<Responsibility>> ListAllAsync(CancellationToken cancellationToken = default) => throw new NotImplementedException();
        public Task<IReadOnlyList<Responsibility>> ListAsync(Expression<Func<Responsibility, bool>> filter, CancellationToken cancellationToken = default, params Expression<Func<Responsibility, object>>[]? includesProperties)
        {
            var query = _positionResponsibilities.AsQueryable().Where(filter);

            if (includesProperties != null)
            {
                query = includesProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            }

            return Task.FromResult((IReadOnlyList<Responsibility>)query.ToList());
        }
        public Task AddAsync(Responsibility entity, CancellationToken cancellationToken = default) => throw new NotImplementedException();
        public Task UpdateAsync(Responsibility entity, CancellationToken cancellationToken = default) => throw new NotImplementedException();
        public Task DeleteAsync(Responsibility entity, CancellationToken cancellationToken = default) => throw new NotImplementedException();
        public Task<Responsibility> FirstOrDefaultAsync(Expression<Func<Responsibility, bool>> filter, CancellationToken cancellationToken = default) => throw new NotImplementedException();
    }
}
