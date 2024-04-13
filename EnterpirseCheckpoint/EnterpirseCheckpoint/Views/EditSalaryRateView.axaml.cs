using Avalonia.Controls;
using EnterpirseCheckpoint.ViewModels;

namespace EnterpirseCheckpoint.Views
{
    public partial class EditSalaryRateView : UserControl
    {
        public EditSalaryRateView()
        {
            InitializeComponent();
        }

        private async void UserControl_Loaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (DataContext is not EditSalaryRateViewModel viewModel) return;
            await viewModel.LoadEmployeeAsync();
        }
    }
}
