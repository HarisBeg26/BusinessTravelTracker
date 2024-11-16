using BusinessTravelTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessTravelTracker.Interfaces
{
    public interface ITripsService
    {
        Task<IEnumerable<Trips>> GetAllTripsAsync();

        Task<Trips> GetTripByIdAsync(int id);
        Task AddTripAsync(Trips trip);
        Task UpdateTripAsync(Trips trip);
        Task DeleteTripAsync(int id);
    }
}
