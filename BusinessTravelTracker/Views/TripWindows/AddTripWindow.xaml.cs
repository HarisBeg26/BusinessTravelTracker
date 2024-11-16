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

namespace BusinessTravelTracker.Views.TripWindows
{
    /// <summary>
    /// Interaction logic for AddTripWindow.xaml
    /// </summary>
    public partial class AddTripWindow : Window
    {
        private readonly TripsViewModel viewModel;
        public AddTripWindow(TripsViewModel _viewModel)
        {
            InitializeComponent();
            viewModel = _viewModel;
            DataContext = viewModel;
        }
    }
}
