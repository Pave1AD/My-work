using AutoMapper;
using EnterpirseCheckpoint.Utilities.Exceptions;
using Enterprise.Checkpoint.Interfaces.DataAccessInterfaces;
using Enterprise.Checkpoint.Interfaces.Services;
using Enterprise.Checkpoint.Interfaces.Utilities;
using EnterpriseCheckpoint.Models.Models;
using System.Threading;

namespace EnterpriseCheckpoint.Services.Services
{
    public class SalaryRateService : ISalaryRateService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SalaryRateService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(SalaryRate salaryRate, CancellationToken cancellationToken = default)
        {
            var salaryRepository = await _unitOfWork.GetRepository<SalaryRate>();

            await salaryRepository.CreateAsync(salaryRate, cancellationToken);

            await _unitOfWork.CommitAsync();
        }

        public async Task<SalaryRate> UpdateSalaryRateAsync(SalaryRate salaryRate, CancellationToken cancellationToken = default)
        {
            var salaryRepository = await _unitOfWork.GetRepository<SalaryRate>();
            var oldSalaryRate = await GetSalaryRateByEmployeeIdAsync(salaryRate.EmployeeId, cancellationToken);
            if (oldSalaryRate is null)
            {
                await CreateAsync(salaryRate, cancellationToken);
                return salaryRate;
            }
            return await salaryRepository.UpdateAsync(salaryRate, cancellationToken);
        }

        public async Task<SalaryRate?> GetSalaryRateByEmployeeIdAsync(int employeeId, CancellationToken cancellation = default)
        {
            var salaryRepository = await _unitOfWork.GetRepository<SalaryRate>();
            var salaryRate = (await salaryRepository.ReadEntitiesByPredicate((s => s.EmployeeId == employeeId), cancellationToken: cancellation)).FirstOrDefault();
            return salaryRate;
        }

        public async Task<SalaryRate?> GetSalaryRateByIdAsync(int salaryRateId, CancellationToken cancellation = default)
        {
            var salaryRepository = await _unitOfWork.GetRepository<SalaryRate>();
            return await salaryRepository.ReadEntityByIdAsync(salaryRateId, cancellation);
        }
    }
    
}
