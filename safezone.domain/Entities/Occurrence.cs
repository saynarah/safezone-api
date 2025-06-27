using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public User User { get; set; } //navegação para a entidade User

        public int UserId { get; set; } //chave estrangeira

    }
}
