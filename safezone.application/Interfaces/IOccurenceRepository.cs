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
