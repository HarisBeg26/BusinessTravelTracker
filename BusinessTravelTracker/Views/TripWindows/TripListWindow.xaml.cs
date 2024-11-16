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

namespace BusinessTravelTracker.Views.TripWindows
{
    /// <summary>
    /// Interaction logic for TripListWindow.xaml
    /// </summary>
    public partial class TripListWindow : Window
    {
        private readonly IServiceProvider serviceProvider;
        private readonly TripsViewModel tripsViewModel;
        public TripListWindow(IServiceProvider _serviceProvider, TripsViewModel _tripsViewModel)
        {
            InitializeComponent();
            serviceProvider = _serviceProvider;
            tripsViewModel = _tripsViewModel;
            DataContext = tripsViewModel;
        }

        private void btn_AddTravelRedirect_Click(object sender, RoutedEventArgs e)
        {
            var addTravel = serviceProvider.GetRequiredService<AddTripWindow>();
            addTravel.Show();
            this.Hide();
        }
    }
}
