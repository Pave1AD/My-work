using Autofac;
using EnterpirseCheckpoint.ViewModelParameters;
using Enterprise.Checkpoint.Interfaces.Services;
using EnterpriseCheckpoint.Models.DTOs;
using EnterpriseCheckpoint.Models.Models;
using ReactiveUI;
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EnterpirseCheckpoint.ViewModels
{
    public class EditSalaryRateViewModel : ViewModelBaseWithParameters<ChangeScheduleParameter>
    {
        private readonly IComponentContext _componentContext;
        private readonly IEmployeeService _employeeService;
        private readonly ISalaryRateService _salaryRateService;
        private int _employeeId;

        private User? _currentUser = null;
        private Employee? _employee = null!;
        private SalaryRate _salaryRate = null!;
        public SalaryRate SalaryRate
        {
            get => _salaryRate;
            set
            {
                this.RaiseAndSetIfChanged(ref _salaryRate, value);
            }
        }
        public Employee? Employee
        {
            get => _employee;
            set
            {
                this.RaiseAndSetIfChanged(ref _employee, value);
            }
        }
        //TODO: Replace IComponentContext with INavigationService
        public EditSalaryRateViewModel(IComponentContext componentContext, IEmployeeService employeeService, ISalaryRateService salaryRateService)
        {
            _componentContext = componentContext;
            _employeeService = employeeService;
            _salaryRateService = salaryRateService;

            UpdateSalaryRateCommand = ReactiveCommand.Create(UpdateSalaryRateAsync);
            GoBackCommand = ReactiveCommand.Create(GoBack);
        }

        public ICommand UpdateSalaryRateCommand { get; set; }
        public ICommand GoBackCommand { get; set; }

        public override User? CurrentUser 
        { 
            get => _currentUser;
            set => _currentUser = value;
        }

        public async Task LoadEmployeeAsync()
        {
            Employee = await _employeeService.GetEmployeeByIdAsync(_employeeId);
            SalaryRate = await _salaryRateService.GetSalaryRateByEmployeeIdAsync(_employeeId) ?? new SalaryRate()
            {
                EmployeeId = _employeeId,
                Rate = 0.0f
            };
            if (Employee is null)
            {
                await GoBack();
                return;
            }
        }

        public async Task UpdateSalaryRateAsync()
        {
            await _salaryRateService.UpdateSalaryRateAsync(SalaryRate);

            await GoBack();
        }

        public async Task GoBack()
        {
            var homeViewModel = _componentContext.Resolve<HomeViewModel>();
            await homeViewModel.SetAdditionalParameter(_currentUser!);
            ChangeView(homeViewModel);
            await homeViewModel.InitializeTabs();
        }

        public override Task SetAdditionalParameter(ChangeScheduleParameter parameter)
        {
            _employeeId = parameter.EmployeeId;
            _currentUser = parameter.CurrentUser;

            return Task.CompletedTask;
        }
    }
}
