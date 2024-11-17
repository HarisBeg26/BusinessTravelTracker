using BusinessTravelTracker.Commands;
using BusinessTravelTracker.Interfaces;
using BusinessTravelTracker.Models;
using BusinessTravelTracker.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BusinessTravelTracker.ViewModels
{
    public class ExpensesViewModel:BaseViewModel
    {
        private readonly IExpenseService _expenseService;
        private readonly ITripsService _tripsService;

        public ObservableCollection<Expense> Expenses { get; set; }
        public ObservableCollection<Trips> Trips { get; set; }

        public int TripId { get; set; }

        private Expense selectedExpense;

        public Expense SelectedExpense
        {
            get { return selectedExpense; }
            set 
            { 
                selectedExpense = value;
                OnPropertyChanged();
            }
        }

        private int selectedTripId;

        public int SelectedTripId
        {
            get { return selectedTripId; }
            set 
            { 
                selectedTripId = value;
                OnPropertyChanged();
            }
        }


        private decimal amount;

        public decimal Amount
        {
            get { return amount; }
            set 
            { 
                amount = value;
                OnPropertyChanged();
            }
        }


        private string description;

        public string Description
        {
            get { return description; }
            set 
            { 
                description = value;
                OnPropertyChanged();
            }
        }

        private string category;

        public string Category
        {
            get { return category; }
            set 
            { 
                category = value;
                OnPropertyChanged();
            }
        }



        public ICommand AddExpenseCommand { get; set; }
        public ICommand UpdateExpenseCommand { get; set; }
        public ICommand DeleteExpenseCommand { get; set; }

        public ExpensesViewModel(IExpenseService expenseService, ITripsService tripsService)
        {
            _expenseService = expenseService;
            _tripsService = tripsService;
            Expenses = new ObservableCollection<Expense>();
            Trips = new ObservableCollection<Trips>();
            LoadExpenses();
            LoadTripsAsync();

            AddExpenseCommand = new RelayCommand(async _ => await AddExpenseAsync());
            UpdateExpenseCommand = new RelayCommand(async _ => await UpdateExpenseAsync());
            DeleteExpenseCommand = new RelayCommand(async _ => await DeleteExpenseAsync());
        }

        private async Task DeleteExpenseAsync()
        {
            try
            {
                if (SelectedExpense != null)
                {
                    await _expenseService.DeleteExpenseAsync(SelectedExpense.Id);
                    Expenses.Remove(SelectedExpense);
                    MessageBox.Show("Strošek izbrisan", "Uspeh", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Prosimo izberite strošek, ki ga želite izbrisati", "Napaka", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Napaka", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task UpdateExpenseAsync()
        {
            try
            {
                if (SelectedExpense != null)
                {
                    await _expenseService.UpdateExpenseAsync(SelectedExpense);
                    LoadExpenses();
                    MessageBox.Show("Podatki uspešno posodobljeni", "Uspeh", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Prosimo izberite strošek, ki ga želite posodobiti", "Napaka", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Napaka", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task AddExpenseAsync()
        {
            try
            {
                if (SelectedTripId <= 0)
                {
                    MessageBox.Show("Please select a valid trip before adding an expense.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var newExpense = new Expense
                {
                    TripId = SelectedTripId,
                    Description = Description,
                    Amount = Amount,
                    Category = Category
                };

                await _expenseService.AddExpenseAsync(newExpense);
                Expenses.Add(newExpense);
                MessageBox.Show("Expense successfully added.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // Reset fields
                Amount = 0;
                Description = string.Empty;
                SelectedTripId = 0; // Reset selection
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async void LoadExpenses()
        {
            try
            {
                var expenses = await _expenseService.GetAllExpensesAsync();
                Expenses.Clear();
                foreach (var expense in expenses)
                {
                    Expenses.Add(expense);
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
                var trips = await _tripsService.GetAllTripsAsync();
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Trips.Clear();
                    foreach (var trip in trips)
                    {
                        Trips.Add(trip);
                    }
                    // Set default selection
                    if (Trips.Count > 0)
                    {
                        SelectedTripId = Trips[0].Id;
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load trips: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
