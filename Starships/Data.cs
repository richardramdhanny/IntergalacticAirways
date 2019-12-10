using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Starships
{
    class Data
    {
        private readonly string baseUrl = "https://swapi.co/api";
        private RestClient RestClient { get; set; }

        public Data()
        {
            var uri = new Uri(baseUrl);
            RestClient = new RestClient(uri);
        }

        public IEnumerable<string> FindStarships(int passengers)
        {
            var suitableStarships = new List<string>();
            var path = string.Empty;
            while (path != null)
            {
                var results = GetStarships(path);
                var next = results.Next;
                
                if (next != null)
                {
                    var nextUri = new Uri(next);
                    path = nextUri.Query;
                } 
                else
                {
                    path = null;
                }

                var ships = results.Results.Where(s => s.Passengers != "unknown");

                foreach (var ship in ships)
                {
                    int.TryParse(ship.Passengers, out int shipCapacity);
                    if (shipCapacity >= passengers)
                    {
                        var pilots = ship.Pilots;
                        if (pilots.Length > 0)
                        {
                            foreach (var pilotUri in pilots)
                            {
                                var pilotName = string.Empty;
                                if (!string.IsNullOrEmpty(pilotUri))
                                {
                                    pilotName = GetPilot(pilotUri).Name;
                                }
                                suitableStarships.Add($"{ship.Name} - {pilotName}");
                            }
                        } 
                        else
                        {
                            suitableStarships.Add($"{ship.Name} - Unknown");
                        }
                        
                    }                    
                }                
            }

            return suitableStarships;
        }

        private StarshipResponse GetStarships(string path)
        {            
            var starshipRequest = new RestRequest($"starships/{path}");

            var response = RestClient.Execute<StarshipResponse>(starshipRequest);
            return DeserializeData<StarshipResponse>(response.Content);            
        }

        private Pilot GetPilot(string fullPath)
        {
            var starshipRequest = new RestRequest(fullPath);

            var response = RestClient.Execute<Pilot>(starshipRequest);
            return DeserializeData<Pilot>(response.Content);
        }

        private T DeserializeData<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }

    }
}
