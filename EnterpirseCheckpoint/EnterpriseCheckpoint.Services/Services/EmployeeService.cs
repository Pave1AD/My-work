using AutoMapper;
using Enterprise.Checkpoint.Interfaces.DataAccessInterfaces;
using Enterprise.Checkpoint.Interfaces.Services;
using EnterpriseCheckpoint.DataAccess;
using EnterpriseCheckpoint.Models.DTOs;
using EnterpriseCheckpoint.Models.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseCheckpoint.Services.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateAsync(Employee employee, CancellationToken cancellationToken = default)
        {
            var employeeRepository = await _unitOfWork.GetRepository<Employee>();

            await employeeRepository.CreateAsync(employee, cancellationToken);

            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<EmployeeDto>> GetOrganizationEmployeesAsync(int organizationId, CancellationToken cancellationToken = default)
        {
            var employeeRepository = await _unitOfWork.GetRepository<Employee>();

            var organizationEmployees = await employeeRepository.ReadEntitiesByPredicate(e => e.OrganizationId == organizationId, cancellationToken: cancellationToken);

            return _mapper.Map<IEnumerable<EmployeeDto>>(organizationEmployees);
        }

        public async Task<Employee?> GetEmployeeByIdAsync(int employeeId, CancellationToken cancellationToken = default)
        {
            var employeeRepository = await _unitOfWork.GetRepository<Employee>();
            return await employeeRepository.ReadEntityByIdAsync(employeeId, cancellationToken);
        }

        public async Task<Employee> UpdateEmployeeAsync(Employee employee, CancellationToken cancellationToken = default)
        {
            var employeeRepository = await _unitOfWork.GetRepository<Employee>();
            return await employeeRepository.UpdateAsync(employee, cancellationToken);
        }

        public async Task<EmployeeWithSalaryDto> GetEmployeeWithSalaryByIdAsync(int employeeId, CancellationToken cancellation = default)
        {
            var employee = await GetEmployeeByIdAsync(employeeId, cancellation);
            var rateRepositroy = await _unitOfWork.GetRepository<SalaryRate>();
            var salaryRate =
                (await rateRepositroy.ReadEntitiesByPredicate((s => s.EmployeeId == employeeId), cancellationToken: cancellation)).FirstOrDefault();
            float salary = 0.0f;
            float rate = 0.0f;
            if (salaryRate is not null)
            {
                rate = salaryRate.Rate;
            }
            if (employee != null)
            {
                var sqlQuery = @"
                                WITH CTE AS (
                                    SELECT EmployeeId
                                    ,      LAG(AssignmentDate) OVER (PARTITION BY EmployeeId ORDER BY AssignmentDate) AS PreviousAssignmentDate
                                    ,      AssignmentDate
                                    ,      IsEntered
                                    ,      LAG(IsEntered) OVER (PARTITION BY EmployeeId ORDER BY AssignmentDate) AS PreviousIsEntered
                                    FROM dbo.EmployeeDayAssignments
                                )
                                
                                SELECT SUM(CAST(DATEDIFF(SECOND, PreviousAssignmentDate, AssignmentDate) AS FLOAT) / 3600) AS TotalHoursWorked
                                FROM CTE
                                WHERE IsEntered = 0 
                                  AND PreviousIsEntered = 1
                                  AND EmployeeId = @EmployeeId
                                GROUP BY EmployeeId;";

                var sqlParameters = new List<SqlParameter> { new("@EmployeeId", employeeId) };

                var hoursWorked = await _unitOfWork.ExecuteSqlQueryAsync(sqlQuery, sqlParameters, "TotalHoursWorked");

                if (salaryRate != null && hoursWorked != null)
                {
                    salary = Convert.ToSingle(hoursWorked) * salaryRate.Rate;
                }
            }

            var employeeWithSalary = new EmployeeWithSalaryDto
            {
                Id = employeeId,
                Role = employee?.Role ?? string.Empty,
                Name = employee?.User.Name ?? string.Empty,
                Surname = employee?.User.Surname ?? string.Empty,
                Salary = salary,
                SalaryRate = rate
            };

            return employeeWithSalary;
        }
    }
}
