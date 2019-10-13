using Core.Contracts;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Infrastructure
{
    public class LoadFromFile : ILoadFlights
    {
        private const string path = "D:\\objects_insecure_deserialization";
        public Flight GetFlight(string flightName)
        {
            try
            {
                using (FileStream fs = new FileStream($"{path}\\{flightName}.dat", FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    //formatter.Binder = new DeserializationBinder();
                    Flight flight = (Flight)formatter.Deserialize(fs);

                    return flight;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return null;
        }

        public List<Flight> GetFlights()
        {
            List<Flight> flights = new List<Flight>();
            try
            {
                string[] files = System.IO.Directory.GetFiles(path, "*.dat");

                for (var index = 0; index < files.Length; index++)
                {
                    using (FileStream fs = new FileStream($"{files[index]}", FileMode.Open))
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        
                        formatter.Binder = new DeserializationBinder();

                        Flight flight = (Flight)formatter.Deserialize(fs);

                        flights.Add(flight);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return flights;
        }

        public void SaveFlight(Flight flight)
        {
            try
            {
                using (FileStream fs = new FileStream($"{path}\\{flight.Name}.dat", FileMode.Create))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(fs, flight);
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }

    public class DeserializationBinder : SerializationBinder
    {
        public override Type BindToType(string assemblyName, string typeName)
        {
            return typeof(Flight);
        }
    }
}
