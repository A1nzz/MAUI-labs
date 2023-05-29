using PositionManager.Application.Abstractions;
using PositionManager.Domain.Abstractions;
using PositionManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PositionManager.Application.Services
{
    public class EmployeePositionService : IEmployeePositionService
    {
        private IUnitOfWork _unitOfWork;

        public EmployeePositionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task AddAsync(EmployeePosition item)
        {
            return _unitOfWork.PositionRepository.AddAsync(item);
        }

        public Task DeleteAsync(EmployeePosition item)
        {
            return _unitOfWork.PositionRepository.DeleteAsync(item);
        }

        public Task<IReadOnlyList<EmployeePosition>> GetAllAsync()
        {
            return _unitOfWork.PositionRepository.ListAllAsync();
        }

        public Task<EmployeePosition> GetByIdAsync(int id)
        {
            return _unitOfWork.PositionRepository.GetByIdAsync(id);

        }

        public Task UpdateAsync(EmployeePosition item)
        {
            return _unitOfWork.PositionRepository.UpdateAsync(item);
        }

        public Task SaveChangesAsync()
        {
            return _unitOfWork.SaveAllAsync();
        }

    }
}
