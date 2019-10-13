using Models;
using System.Collections.Generic;

namespace Core.Contracts
{
    public interface ILoadFlights
    {
        List<Flight> GetFlights();
        void SaveFlight(Flight flight);
        Flight GetFlight(string flightName);
    }
}
