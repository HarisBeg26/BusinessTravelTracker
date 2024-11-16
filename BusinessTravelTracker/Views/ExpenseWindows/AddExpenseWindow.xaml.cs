using BusinessTravelTracker.Interfaces;
using BusinessTravelTracker.Services;
using BusinessTravelTracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BusinessTravelTracker.Views.ExpenseWindows
{
    /// <summary>
    /// Interaction logic for AddExpenseWindow.xaml
    /// </summary>
    public partial class AddExpenseWindow : Window
    {
        private readonly ExpensesViewModel _viewModel;
        public AddExpenseWindow(IExpenseService expenseService, ITripsService tripService)
        {
            InitializeComponent();

            _viewModel = new ExpensesViewModel(expenseService, tripService);
            DataContext = _viewModel;

            Loaded += async (sender, args) =>
            {
                await LoadTripsAsync(tripService);
            };
        }

        private async Task LoadTripsAsync(ITripsService tripService)
        {
            try
            {
                var trips = await tripService.GetAllTripsAsync();
                if (trips == null || !trips.Any())
                {
                    MessageBox.Show("No trips found", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                _viewModel.Trips = new System.Collections.ObjectModel.ObservableCollection<Models.Trips>(trips);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load trips: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
