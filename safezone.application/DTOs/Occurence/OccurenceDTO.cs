using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using safezone.domain.Enums;

namespace safezone.application.DTOs.Occurence
{
    public class OccurenceDTO
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public TypeOccurence Type { get; set; }

        public double Latitude { get; set; }


        public double Longitude { get; set; }


    }
}
