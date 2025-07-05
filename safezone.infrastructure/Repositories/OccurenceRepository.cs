using Microsoft.EntityFrameworkCore;
using safezone.application.Interfaces;
using safezone.domain.Entities;
using safezone.infrastructure.Persistence;

namespace safezone.infrastructure.Repositories
{
    public class OccurenceRepository : IOccurenceRepository
    {
        private readonly AppDbContext _context;

        public OccurenceRepository(AppDbContext context) 
            => _context = context;
        public async Task<Occurrence> GetOccurrenceByIdAsync(int id)
        {
            var occurrence = await _context.Occurrences.FindAsync(id);
            if (occurrence == null)
                throw new KeyNotFoundException($"Occurrence with ID {id} not found.");
            return occurrence;
        }

        public async Task<List<Occurrence>> GetAllOccurrencesAsync() 
            => await _context.Occurrences.ToListAsync();

        public async Task UpdateOccurrenceAsync(Occurrence occurrence)
        {
            _context.Occurrences.Update(occurrence);
            await _context.SaveChangesAsync();
        }

        public async Task AddOccurrenceAsync(Occurrence occurrence)
        {
            _context.Occurrences.Add(occurrence);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOccurrenceAsync(int id)
        {
            var occurrence = await _context.Occurrences.FindAsync(id);
            if (occurrence == null)
                throw new KeyNotFoundException($"Occurrence with ID {id} not found.");

            _context.Occurrences.Remove(occurrence);
            await _context.SaveChangesAsync();
        }

    }
}
