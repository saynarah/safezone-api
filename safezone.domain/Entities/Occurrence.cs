using safezone.domain.Enums;

namespace safezone.domain.Entities
{
    public class Occurrence
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public TypeOccurence Type { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Address { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
