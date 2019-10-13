using Core.Contracts;
using Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Insecure_Deserialization.Controllers
{
    public class ValuesController : ApiController
    {
        private readonly ILoadFlights _loadFlights;
        public ValuesController(ILoadFlights loadFlights)
        {
            _loadFlights = loadFlights;
        }

        // GET api/values
        public IEnumerable<Flight> Get()
        {
            return _loadFlights.GetFlights();
        }

        // GET api/values/FLY001
        public Flight Get(string flightName)
        {
            return _loadFlights.GetFlight(flightName);
        }

        public IHttpActionResult Post(Flight flight)
        {
            try
            {
                _loadFlights.SaveFlight(flight);
            }
            catch (Exception ex)
            {
                return BadRequest("We encoutered some issues processing this flight.");
            }

            return Ok("Flight has been received.");
        }
    }
}
