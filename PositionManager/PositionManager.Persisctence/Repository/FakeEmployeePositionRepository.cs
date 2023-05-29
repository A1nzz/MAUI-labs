using PositionManager.Domain.Abstractions;
using PositionManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PositionManager.Persisctence.Repository
{
    public class FakeEmployeePositionRepository : IRepository<EmployeePosition>
    {
        List<EmployeePosition> _positions;
        public FakeEmployeePositionRepository()
        {
            _positions = new List<EmployeePosition>()
        {
            new EmployeePosition {Name = "SEO", Salary=1000, Id = 1},
            new EmployeePosition {Name = "Manager", Salary = 500, Id = 2},
            new EmployeePosition { Name = "Employee", Salary = 100, Id = 3}
        };
        }

        public Task<EmployeePosition> GetByIdAsync(int id, CancellationToken cancellationToken = default, params Expression<Func<EmployeePosition, object>>[]? includesProperties) => throw new NotImplementedException();
        public async Task<IReadOnlyList<EmployeePosition>> ListAllAsync(CancellationToken cancellationToken = default)
        {
            return await Task.Run(() => _positions);
        }
        public Task<IReadOnlyList<EmployeePosition>> ListAsync(Expression<Func<EmployeePosition, bool>> filter, CancellationToken cancellationToken = default, params Expression<Func<EmployeePosition, object>>[]? includesProperties) => throw new NotImplementedException();
        public Task AddAsync(EmployeePosition entity, CancellationToken cancellationToken = default) => throw new NotImplementedException();
        public Task UpdateAsync(EmployeePosition entity, CancellationToken cancellationToken = default) => throw new NotImplementedException();
        public Task DeleteAsync(EmployeePosition entity, CancellationToken cancellationToken = default) => throw new NotImplementedException();
        public Task<EmployeePosition> FirstOrDefaultAsync(Expression<Func<EmployeePosition, bool>> filter, CancellationToken cancellationToken = default) => throw new NotImplementedException();
    }
}
