using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIAndReactUI.Model
{
    public class CountryHistory
    {

        [Key]
        public int Id { get; set; }
        public string Country { get; set; }

        public string CountryCode { get; set; }

        public string Province { get; set; }

        public string City { get; set; }

        public string CityCode { get; set; }

        public string Lat { get; set; }

        public string Lon { get; set; }
        public long Cases { get; set; }

        public string Status { get; set; }

        public string Date { get; set; }
    }
}
