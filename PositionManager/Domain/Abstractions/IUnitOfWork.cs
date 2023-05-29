using PositionManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PositionManager.Domain.Abstractions
{
    public interface IUnitOfWork
    {
        IRepository<EmployeePosition> PositionRepository { get; }
        IRepository<Responsibility> ResponsibilityRepository { get; }
        public Task RemoveDatbaseAsync();
        public Task CreateDatabaseAsync();
        public Task SaveAllAsync();
    }
}
