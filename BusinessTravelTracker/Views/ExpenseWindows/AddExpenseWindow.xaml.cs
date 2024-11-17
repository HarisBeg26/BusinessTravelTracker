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

        }
    }
}
