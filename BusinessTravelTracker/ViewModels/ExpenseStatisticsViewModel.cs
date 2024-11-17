using BusinessTravelTracker.Interfaces;
using BusinessTravelTracker.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using OxyPlot.Series;
using OxyPlot;
using OxyPlot.Axes;

namespace BusinessTravelTracker.ViewModels
{
    public class ExpenseStatisticsViewModel : BaseViewModel
    {
        private readonly IExpenseService _expenseService;

        public double TotalExpenses { get; set; }
        public double AverageExpense { get; set; }
        public ObservableCollection<ExpenseCategoryStatistic> ExpensesByCategory { get; set; }

        // The PlotModel property for binding to the OxyPlot chart
        public PlotModel PlotModel { get; set; }

        public ExpenseStatisticsViewModel(IExpenseService expenseService)
        {
            _expenseService = expenseService;
            ExpensesByCategory = new ObservableCollection<ExpenseCategoryStatistic>();
            PlotModel = new PlotModel(); // Initialize PlotModel
            LoadStatistics();
        }

        private async void LoadStatistics()
        {
            try
            {
                // Fetch all expenses from the service
                var allExpenses = await _expenseService.GetAllExpensesAsync();

                // Calculate the total and average expenses
                TotalExpenses = allExpenses.Sum(expense => expense.Amount);
                AverageExpense = allExpenses.Any() ? allExpenses.Average(expense => expense.Amount) : 0;

                // Group expenses by category and calculate total amount per category
                var groupedByCategory = allExpenses
                    .GroupBy(expense => expense.Category)
                    .Select(group => new ExpenseCategoryStatistic
                    {
                        Category = group.Key,
                        TotalAmount = group.Sum(expense => expense.Amount)
                    }).ToList();

                // Clear previous statistics and update the collection
                ExpensesByCategory.Clear();
                foreach (var stat in groupedByCategory)
                {
                    ExpensesByCategory.Add(stat);
                }

                // Update PlotModel after loading data
                CreatePlotModel();

                // Notify UI that properties have changed
                OnPropertyChanged(nameof(TotalExpenses));
                OnPropertyChanged(nameof(AverageExpense));
                OnPropertyChanged(nameof(ExpensesByCategory));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load statistics: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CreatePlotModel()
        {
            // Initialize PlotModel
            var model = new PlotModel
            {
                Title = "Expenses by Category",
                PlotType = PlotType.XY,
                IsLegendVisible = true
            };

            // Create BarSeries for displaying categories and their total amounts
            var barSeries = new BarSeries
            {
                ItemsSource = ExpensesByCategory.Select(x => new BarItem { Value = x.TotalAmount }).ToList(),  // Bind the data to the bar series
                LabelPlacement = LabelPlacement.Inside,
                LabelFormatString = "{0:C}"  // Format the labels (optional)
            };

            // Set the X-axis labels based on category names
            model.Axes.Add(new CategoryAxis
            {
                Position = AxisPosition.Bottom,
                Key = "CategoryAxis",
                ItemsSource = ExpensesByCategory.Select(x => x.Category).ToList()
            });

            // Add the BarSeries to the PlotModel
            model.Series.Add(barSeries);

            // Set the PlotModel to the property
            PlotModel = model;
            OnPropertyChanged(nameof(PlotModel));  // Notify that PlotModel has been updated
        }
    }

    // This class represents the expense category and its total amount
    public class ExpenseCategoryStatistic
    {
        public string Category { get; set; }
        public double TotalAmount { get; set; }
    }
}
