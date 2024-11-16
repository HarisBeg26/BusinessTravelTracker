using BusinessTravelTracker.Interfaces;
using BusinessTravelTracker.ViewModels;
using Microsoft.Extensions.DependencyInjection;
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
    /// Interaction logic for ExpenseListWindow.xaml
    /// </summary>
    public partial class ExpenseListWindow : Window
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IExpenseService expenseService;
        private readonly ITripsService tripsService;
        private readonly ExpensesViewModel expensesViewModel;
        public ExpenseListWindow(IExpenseService _expenseService, IServiceProvider _serviceProvider)
        {
            InitializeComponent();
            serviceProvider = _serviceProvider;
            expenseService = _expenseService;
            DataContext = expensesViewModel;
        }

        private void btn_AddExpenseRedirect_Click(object sender, RoutedEventArgs e)
        {
            var addExpenseWindow = serviceProvider.GetRequiredService<AddExpenseWindow>();
            addExpenseWindow.ShowDialog();
            this.Hide();
        }
    }
}
