using BusinessTravelTracker.Interfaces;
using BusinessTravelTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessTravelTracker.Services
{
    public class ExpensesService : IExpenseService
    {
        private readonly Supabase.Client _client;

        public ExpensesService(Supabase.Client client)
        {
            _client = client;
        }

        public async Task AddExpenseAsync(Expense expense)
        {
            await _client.From<Expense>().Insert(expense);
        }

        public async Task DeleteExpenseAsync(int id)
        {
            await _client.From<Expense>().Delete();
        }

        public async Task<IEnumerable<Expense>> GetAllExpensesAsync()
        {
            var result = await _client.From<Expense>().Get();
            return result.Models;
        }

        public async Task<IEnumerable<Expense>> GetAllExpensesByTripIdAsync(int tripId)
        {
            var result = await _client.From<Expense>().Where(e => e.TripId == tripId).Get();
            return result.Models;
        }

        public async Task UpdateExpenseAsync(Expense expense)
        {
            await _client.From<Expense>().Update(expense);
        }
    }
}
