using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogAPIFact
{
    public static class DogFactsBLL
    {
        public static async Task<List<DogFact>> ObtenerDogFactsAsync()
        {
            var facts = await DogFactsDAL.GetDogFactsAsync();
            return facts;
        }
    }
}
