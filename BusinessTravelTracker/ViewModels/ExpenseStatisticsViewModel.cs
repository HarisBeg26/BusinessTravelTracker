using BusinessTravelTracker.Interfaces;
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
        private readonly IExpenseService expenseService;

        public decimal TotalExpenses { get; set; }
        public decimal AverageExpenses { get; set; }

        public ObservableCollection<ExpenseCategoryStatistic> ExpensesByCategory { get; set; }

        public ExpenseStatisticsViewModel(IExpenseService _expenseService) 
        {
            expenseService = _expenseService;
            ExpensesByCategory = new ObservableCollection<ExpenseCategoryStatistic>();

            LoadStatistics();
        }

        private async void LoadStatistics()
        {
            try
            {
                var allExpenses = await expenseService.GetAllExpensesAsync();

                TotalExpenses = allExpenses.Sum(e => e.Amount);
                AverageExpenses = allExpenses.Any() ? allExpenses.Average(e => e.Amount) : 0;

                var groupedByCategory = allExpenses.GroupBy(e => e.Category).Select(group => new ExpenseCategoryStatistic
                {
                    Category = group.Key,
                    TotalAmount = group.Sum(e => e.Amount)
                });

                ExpensesByCategory.Clear();
                foreach (var stat in groupedByCategory)
                {
                    ExpensesByCategory.Add(stat);
                }

                OnPropertyChanged(nameof(TotalExpenses));
                OnPropertyChanged(nameof(AverageExpenses));
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
        public decimal TotalAmount { get; set; }
    }
}
