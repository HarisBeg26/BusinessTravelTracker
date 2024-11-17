using BusinessTravelTracker.Interfaces;
using BusinessTravelTracker.Models;
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

namespace BusinessTravelTracker.Views
{
    /// <summary>
    /// Interaction logic for ExpensesChartWindow.xaml
    /// </summary>
    public partial class ExpensesChartWindow : Window
    {
        private readonly ExpenseStatisticsViewModel _viewModel;
        public ExpensesChartWindow(IExpenseService expenseService)
        {
            InitializeComponent();

            _viewModel = new ExpenseStatisticsViewModel(expenseService);
            DataContext = _viewModel;
        }
    }
}
