using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIAndReactUI.Model
{
    public class GlobalStat
    {
        [Key]
        public int GloabalStatId { get; set; }
        public long NewConfirmed { get; set; }
        public long TotalConfirmed { get; set; }

        public long NewDeaths { get; set; }
        public long TotalDeaths { get; set; }

        public long NewRecovered { get; set; }
        public long TotalRecovered { get; set; }

        public string Date { get; set; }

       
    }
}
