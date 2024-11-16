using BusinessTravelTracker.Commands;
using BusinessTravelTracker.Interfaces;
using BusinessTravelTracker.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BusinessTravelTracker.ViewModels
{
    public class TripsViewModel:BaseViewModel
    {
        private readonly ITripsService tripService;

        public ObservableCollection<Trips> Trips { get; set; }

        private Trips selectedTrip;

        public Trips SelectedTrip
        {
            get { return selectedTrip; }
            set 
            { 
                selectedTrip = value;
                OnPropertyChanged();
            }
        }

        private string destination;

        public string Destination
        {
            get { return destination; }
            set 
            { 
                destination = value;
                OnPropertyChanged();
            }
        }

        private DateTime startDate;

        public DateTime StartDate
        {
            get { return startDate; }
            set 
            { 
                startDate = value;
                OnPropertyChanged();
            }
        }

        private DateTime endDate;

        public DateTime EndDate
        {
            get { return endDate; }
            set 
            {
                endDate = value;
                OnPropertyChanged();
            }
        }

        private string purpose;

        public string Purpose
        {
            get { return purpose; }
            set 
            { 
                purpose = value;
                OnPropertyChanged();
            }
        }



        public ICommand AddTripCommand { get; }
        public ICommand UpdateTripCommand { get; }
        public ICommand DeleteTripCommand { get; }

        public TripsViewModel(ITripsService service)
        {
            tripService = service;
            Trips = new ObservableCollection<Trips>();
            LoadTripsAsync();

            AddTripCommand = new RelayCommand(async _ => await AddTripAsync());
            DeleteTripCommand = new RelayCommand(async _ => await DeleteTripAsync());
            UpdateTripCommand = new RelayCommand(async _ => await UpdateTripAsync());
        }

        private async Task AddTripAsync()
        {
            try
            {
                var newTrip = new Trips
                {
                    Destination = Destination,
                    StartDate = StartDate,
                    EndDate = EndDate,
                    Purpose = Purpose
                };
                await tripService.AddTripAsync(newTrip);
                Trips.Add(newTrip);
                MessageBox.Show("Uspesno dodano novo poslovno potovanje", "Uspeh", MessageBoxButton.OK, MessageBoxImage.Information);

                Destination = Purpose = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Napaka", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task DeleteTripAsync()
        {
            try
            {
                if (SelectedTrip != null)
                {
                    await tripService.DeleteTripAsync(SelectedTrip.Id);
                    Trips.Remove(SelectedTrip);
                    MessageBox.Show("Poslovno potovanje izbrisano iz sistema", "Uspeh", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Prosimo izberite poslovno potovanje katero zelite izbrisati", "Napaka", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Napaka", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task UpdateTripAsync()
        {
            try
            {
                if (SelectedTrip != null)
                {
                    await tripService.UpdateTripAsync(SelectedTrip);
                    LoadTripsAsync();
                    MessageBox.Show("Podatki uspesno posodobljeni", "Uspeh", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Prosimo izberite potovanje katero zelite posodobiti", "Napaka", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Napaka", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void LoadTripsAsync()
        {
            try
            {
                var trips = await tripService.GetAllTripsAsync();
                Trips.Clear();
                foreach (var trip in trips)
                {
                    Trips.Add(trip);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Napaka", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
