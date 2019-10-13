using fastJSON;
using InsecureDeserialization_Demo_2.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace InsecureDeserialization_Demo_2.Controllers
{
    public class ValuesController : ApiController
    {

        // POST api/values
        [HttpPost]
        public IHttpActionResult Post()
        {
            try
            {
                // Deserialize flight from raw request.
                var result = Request.Content.ReadAsStringAsync().Result;

                //var flight = JsonConvert.DeserializeObject(result,
                //new JsonSerializerSettings
                //{
                //    TypeNameHandling = TypeNameHandling.Auto
                //});

                // Explicit cast to JObject
                JObject flight = (JObject)JsonConvert.DeserializeObject(result,
                new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.None
                });
                var obj = flight.ToObject(typeof(Flight));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest("We encoutered some issues processing the flight.");
            }

            return Ok($"Flight has been processed.");
        }

        public IHttpActionResult Post2()
        {
            try
            {
                var result = Request.Content.ReadAsStringAsync().Result;
                var serializer = new JavaScriptSerializer(new SimpleTypeResolver());
                var flight = serializer.Deserialize(result, typeof(Flight));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest("We encoutered some issues processing the flight.");
            }

            return Ok($"Flight has been processed.");
        }

    }
}
