using Autofac;
using EnterpirseCheckpoint.ViewModelParameters;
using Enterprise.Checkpoint.Interfaces.Services;
using EnterpriseCheckpoint.Models.DTOs;
using EnterpriseCheckpoint.Models.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace EnterpirseCheckpoint.ViewModels
{
    public class SalaryViewModel : ViewModelBaseWithParameters<User>
    {
        private User? _user = null;
        private readonly IEmployeeService _employeeService;
        private readonly IOrganizationService _organizationService;
        private readonly IComponentContext _componentContext;

        public SalaryViewModel(IEmployeeService employeeService, IOrganizationService organizationService, IComponentContext componentContext)
        {
            _employeeService = employeeService;
            _organizationService = organizationService;
            _componentContext = componentContext;
        }

        public ObservableCollection<EmployeeWithSalaryDto> Employees { get; set; } = [];
        
        public override User? CurrentUser 
        { 
            get => _user; 
            set => _user = value; 
        }

        public override Task SetAdditionalParameter(User parameter)
        {
            _user = parameter;
            return Task.CompletedTask;
        }

        public async Task LoadEmployeesAsync()
        {
            var organization = await _organizationService.GetOrganizationByUserAsync(_user!);
            Employees.Clear();
            foreach (var employee in await _employeeService.GetOrganizationEmployeesAsync(organization.Id))
            {
                var updatedEmployee = await _employeeService.GetEmployeeWithSalaryByIdAsync(employee.Id);
                Employees.Add(updatedEmployee);
            }
        }

        public void GoToSalaryRateSetUp(int employeeId)
        {
            var editSalaryViewModel = _componentContext.Resolve<EditSalaryRateViewModel>();
            editSalaryViewModel.SetAdditionalParameter(new ChangeScheduleParameter
            {
                CurrentUser = _user!,
                EmployeeId = employeeId
            });
            ChangeView(editSalaryViewModel);
        }
    }
}
