using System;
using System.Collections.Generic;
using System.Text;

namespace Starships
{
    class StarshipResponse
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public string Previous { get; set; }
        public IEnumerable<Starship> Results { get; set; }
    }
}
