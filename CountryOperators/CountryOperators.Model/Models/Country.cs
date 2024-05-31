using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountryOperators.Model.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string CountryCode { get; set; }
        public string Name { get; set; }
        public string CountryIso { get; set; }
        public ICollection<CountryDetails> Details { get; set; }
    }
}
