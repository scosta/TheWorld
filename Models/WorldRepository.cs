using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace TheWorld.Models
{
    public class WorldRepository : IWorldRepository
    {
        private WorldContext _context;
        private ILogger<WorldRepository> _logger;

        public WorldRepository(WorldContext context, ILogger<WorldRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void AddTrip(Trip trip)
        {
            _context.Add(trip);
        }
        
        public void AddStop(string tripName, string userName, Stop stop)
        {
            var theTrip = GetTripByName(tripName, userName);
            if (theTrip.Stops.Count() == 0)
            {
                stop.Order = 1;
            }
            else
            {
                stop.Order = theTrip.Stops.Max(s => s.Order) + 1;
            }
            theTrip.Stops.Add(stop);
            _context.Stops.Add(stop);
        }

        public IEnumerable<Trip> GetAllTrips()
        {
            try
            {
                return _context.Trips.OrderBy(t => t.Name).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get trips from database.", ex);
                return null;
            }
        }
        
        public IEnumerable<Trip> GetAllTripsWithStops()
        {
            try
            {
                return _context.Trips
                    .Include(t => t.Stops)
                    .OrderBy(t => t.Name)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get trips with stops from database.", ex);
                return null;
            }
        }
        
        public IEnumerable<Trip> GetUserTripsWithStops(string userName)
        {
            try
            {
                return _context.Trips
                    .Include(t => t.Stops)
                    .Where(t => t.UserName == userName)
                    .OrderBy(t => t.Name)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get trips with stops from database.", ex);
                return null;
            }
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }
        
        public Trip GetTripByName(string tripName, string userName)
        {
            return _context.Trips.Include(t => t.Stops)
                .Where(t => t.Name == tripName && t.UserName == userName)
                .FirstOrDefault();
        }
    }
}