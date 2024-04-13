using EnterpriseCheckpoint.Models.DTOs;
using EnterpriseCheckpoint.Models.Models;

namespace Enterprise.Checkpoint.Interfaces.Services
{
    public interface ISalaryRateService
    {
        Task CreateAsync(SalaryRate salaryRate, CancellationToken cancellationToken = default);
        Task<SalaryRate> UpdateSalaryRateAsync(SalaryRate salaryRate, CancellationToken cancellationToken = default);
        Task<SalaryRate?> GetSalaryRateByEmployeeIdAsync(int employeeId, CancellationToken cancellation = default);
        Task<SalaryRate?> GetSalaryRateByIdAsync(int salaryRateId, CancellationToken cancellation = default);
    }
}
