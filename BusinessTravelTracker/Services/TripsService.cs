using BusinessTravelTracker.Interfaces;
using BusinessTravelTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessTravelTracker.Services
{
    public class TripsService : ITripsService
    {
        private readonly Supabase.Client _client;

        public TripsService(Supabase.Client client)
        {
           _client = client;
        }
        public async Task AddTripAsync(Trips trip)
        {
            await _client.From<Trips>().Insert(trip);
        }

        public async Task DeleteTripAsync(int id)
        {
            await _client.From<Expense>().Delete();
        }
        

        public async Task<IEnumerable<Trips>> GetAllTripsAsync()
        {
            var result = await _client.From<Trips>().Get();
            return result.Models;
        }

        public async Task UpdateTripAsync(Trips trip)
        {
            await _client.From<Trips>().Update(trip);
        }
    }
}
