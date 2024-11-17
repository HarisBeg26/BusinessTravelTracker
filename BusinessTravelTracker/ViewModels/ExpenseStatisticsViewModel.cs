using BusinessTravelTracker.Interfaces;
using BusinessTravelTracker.Models;
using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BusinessTravelTracker.ViewModels
{
    public class ExpenseStatisticsViewModel:BaseViewModel
    {
        private readonly IExpenseService _expenseService;

        public double TotalExpenses { get; set; }
        public double AverageExpense { get; set; }
        public ObservableCollection<ExpenseCategoryStatistic> ExpensesByCategory { get; set; }

        public ExpenseStatisticsViewModel(IExpenseService expenseService)
        {
            _expenseService = expenseService;
            ExpensesByCategory = new ObservableCollection<ExpenseCategoryStatistic>();

            LoadStatistics();
        }

        private async void LoadStatistics()
        {
            try
            {
                var allExpenses = await _expenseService.GetAllExpensesAsync();

                TotalExpenses = allExpenses.Sum(expense => expense.Amount);
                AverageExpense = allExpenses.Any() ? allExpenses.Average(expense => expense.Amount) : 0;

                var groupedByCategory = allExpenses
                    .GroupBy(expense => expense.Category)
                    .Select(group => new ExpenseCategoryStatistic
                    {
                        Category = group.Key,
                        TotalAmount = group.Sum(expense => expense.Amount)
                    });

                ExpensesByCategory.Clear();
                foreach (var stat in groupedByCategory)
                {
                    ExpensesByCategory.Add(stat);
                }

                OnPropertyChanged(nameof(TotalExpenses));
                OnPropertyChanged(nameof(AverageExpense));
                OnPropertyChanged(nameof(ExpensesByCategory));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load statistics: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    public class ExpenseCategoryStatistic
    {
        public string Category { get; set; }
        public double TotalAmount { get; set; }
    }
}
