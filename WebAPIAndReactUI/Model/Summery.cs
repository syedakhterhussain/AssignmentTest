using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIAndReactUI.Model
{
    public class Summery
    {
      
        public int SummeryId { get; set; }
        public string ID { get; set; }
        public string Message { get; set; }

        public string Date { get; set; }

        public GlobalStat Global { get; set; }
        public List<CountriesWiseSummery> Countries { get; set; }


    }
}
