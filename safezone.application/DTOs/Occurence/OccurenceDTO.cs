﻿using safezone.domain.Enums;

namespace safezone.application.DTOs.Occurence
{
    public class OccurenceDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public TypeOccurence Type { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Address { get; set; }
        public int UserId { get; set; }
    }
}
