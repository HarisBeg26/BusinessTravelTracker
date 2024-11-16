using BusinessTravelTracker.Interfaces;
using BusinessTravelTracker.Views.ExpenseWindows;
using BusinessTravelTracker.Views.TripWindows;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BusinessTravelTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IExpenseService _expenseService;
        private readonly ITripsService _tripsService;
        private readonly IServiceProvider _serviceProvider;
        public MainWindow(IServiceProvider serviceProvider, ITripsService tripsService, IExpenseService expenseService)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _tripsService = tripsService;
            _expenseService = expenseService;
        }

        private void btn_RedirectTravel_Click(object sender, RoutedEventArgs e)
        {
            var travelList = _serviceProvider.GetRequiredService<TripListWindow>();
            travelList.Show();
            this.Hide();
        }

        private void btn_RedirectExpense_Click(object sender, RoutedEventArgs e)
        {
            var expenseList = _serviceProvider.GetRequiredService<ExpenseListWindow>();
            expenseList.Show();
            this.Hide();
        }
    }
}