using PositionManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PositionManager.Application.Abstractions
{
    public interface IResponsibilityService : IBaseService<Responsibility>
    {
        Task<IReadOnlyList<Responsibility>> ListAsync(Expression<Func<Responsibility, bool>> filter, CancellationToken cancellationToken = default, params Expression<Func<Responsibility, object>>[]? includesProperties);

    }
}
