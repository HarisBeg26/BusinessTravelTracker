using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Windows;
using Supabase;
using BusinessTravelTracker.Interfaces;
using BusinessTravelTracker.ViewModels;
using BusinessTravelTracker.Views.ExpenseWindows;
using BusinessTravelTracker.Views.TripWindows;

namespace BusinessTravelTracker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; set; }
        private IConfiguration Configuration { get; set; }
        protected override async void OnStartup(StartupEventArgs e)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).
                AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();    

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider(); 

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Configuration);

            services.AddSingleton(provider =>
            {
                var supabaseSettings = new SupabaseOptions
                {
                    AutoRefreshToken = true,
                    AutoConnectRealtime = true,
                };
                var supabaseClient = new Client(
                    Configuration["SupabaseSettings:Url"],
                    Configuration["SupabaseSettings:AnonKey"],
                    supabaseSettings);
                return supabaseClient;
            });

            services.AddTransient<TripsViewModel>();
            services.AddTransient<ExpensesViewModel>();

            services.AddTransient<MainWindow>();
            services.AddTransient<AddExpenseWindow>();
            services.AddTransient<AddTripWindow>();
            services.AddTransient<TripListWindow>();
            services.AddTransient<ExpenseListWindow>();
        }

        private void InitializeSupabase()
        {
            var supabaseClient = ServiceProvider.GetRequiredService<Client>();
            try
            {
                supabaseClient.InitializeAsync().Wait();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to initialize Supabase: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown();
                throw;
            }
        }
    }

}
