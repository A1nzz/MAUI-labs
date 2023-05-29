using PositionManager.Application.Abstractions;
using PositionManager.Domain.Abstractions;
using PositionManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PositionManager.Application.Services
{
    public class ResponsibilityService : IResponsibilityService
    {
        private IUnitOfWork _unitOfWork;

        public ResponsibilityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task AddAsync(Responsibility item)
        {
            return _unitOfWork.ResponsibilityRepository.AddAsync(item);
        }

        public Task DeleteAsync(Responsibility item)
        {
            return _unitOfWork.ResponsibilityRepository.DeleteAsync(item);
        }

        public Task<IReadOnlyList<Responsibility>> GetAllAsync()
        {
            return _unitOfWork.ResponsibilityRepository.ListAllAsync();
        }

        public Task<Responsibility> GetByIdAsync(int id)
        {
            return _unitOfWork.ResponsibilityRepository.GetByIdAsync(id);
        }

        public Task UpdateAsync(Responsibility item)
        {
            return _unitOfWork.ResponsibilityRepository.UpdateAsync(item);
        }
        public Task<IReadOnlyList<Responsibility>> ListAsync(Expression<Func<Responsibility, bool>> filter, CancellationToken cancellationToken = default, params Expression<Func<Responsibility, object>>[]? includesProperties)
        {
            return _unitOfWork.ResponsibilityRepository.ListAsync(filter, cancellationToken, includesProperties);
        }
        public Task SaveChangesAsync()
        {
            return _unitOfWork.SaveAllAsync();
        }
    }
}
