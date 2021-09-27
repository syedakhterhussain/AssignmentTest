using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIAndReactUI.Model
{
    public class CountriesWiseSummery
    {
        [Key]
        public int CountryId { get; set; }
        
        public string ID { get; set; }

        public string Country { get; set; }

        public string CountryCode { get; set; }

        public string Slug { get; set; }
        public long NewConfirmed { get; set; }
        public long TotalConfirmed { get; set; }

        public long NewDeaths { get; set; }
        public long TotalDeaths { get; set; }

        public long NewRecovered { get; set; }
        public long TotalRecovered { get; set; }

        public string Date { get; set; }

        public Premium Premium { get; set; }
    }
}
