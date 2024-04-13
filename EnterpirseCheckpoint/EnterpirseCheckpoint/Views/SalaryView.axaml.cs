using Avalonia.Controls;
using Avalonia.Input;
using EnterpirseCheckpoint.ViewModels;
using EnterpriseCheckpoint.Models.DTOs;

namespace EnterpirseCheckpoint.Views
{
    public partial class SalaryView : UserControl
    {
        public SalaryView()
        {
            InitializeComponent();
        }

        private void DataGrid_DoubleTapped(object? sender, TappedEventArgs e)
        {
            if (DataContext is not SalaryViewModel viewModel) return;

            if(sender is not DataGrid dataGrid) return;

            if (dataGrid.SelectedItem is not EmployeeWithSalaryDto selectedItem) return;
            viewModel.GoToSalaryRateSetUp(selectedItem.Id);
        }

        private async void UserControl_Loaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (DataContext is not SalaryViewModel viewModel) return;
            await viewModel.LoadEmployeesAsync();
        }
    }
}
