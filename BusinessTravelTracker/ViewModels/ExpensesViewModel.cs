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

        private DateTime date;

        public DateTime Date
        {
            get { return date; }
            set 
            { 
                date = value;
                OnPropertyChanged();
            }
        }

        private DateTime createdAt;

        public DateTime CreatedAt
        {
            get { return createdAt; }
            set 
            { 
                createdAt = value;
                OnPropertyChanged();
            }
        }

        private DateTime updatedAt;

        public DateTime UpdatedAt
        {
            get { return updatedAt; }
            set 
            { 
                updatedAt = value;
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
                var newExpense = new Expense
                {
                    TripId = SelectedTripId,
                    Description = Description,
                    Amount = Amount,
                    Date = Date
                };

                await _expenseService.AddExpenseAsync(newExpense);
                Expenses.Add(newExpense);
                MessageBox.Show("Uspesno dodan strošek", "Uspeh", MessageBoxButton.OK, MessageBoxImage.Information);

                // Reset fields
                Amount = 0;
                Description = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Napaka", MessageBoxButton.OK, MessageBoxImage.Error);
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
                var trips = await _tripsService.GetAllTripsAsync().ConfigureAwait(false);
                Trips.Clear();  // Clear any previous data
                foreach (var trip in trips)
                {
                    Trips.Add(trip);  // Add the new trips to the ObservableCollection
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load trips: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
