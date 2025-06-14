using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using safezone.domain.Entities;

namespace safezone.application.Interfaces
{
    public interface IOccurenceRepository
    {
        Task<Occurrence> GetOccurrenceByIdAsync(int id);
        Task<List<Occurrence>> GetAllOccurrencesAsync();
        Task UpdateOccurrenceAsync(Occurrence occurrence);
        Task AddOccurrenceAsync(Occurrence occurrence);
        Task DeleteOccurrenceAsync(int id);

    }
}
