using Microsoft.EntityFrameworkCore;
using Vocabulary.Core.Entity;
using Vocabulary.Infrastructure.Data;

namespace Vocabulary.Infrastructure.Repository
{
    public class VocabularyRepository : IVocabularyRepository
    {
        private readonly VocabularyDbContext _context;

        public VocabularyRepository(VocabularyDbContext context)
        {
            _context = context;
        }

        public async Task<List<VocabularyItem>> GetAllAsync()
        {
            return await _context.VocabularyItems.Include(v => v.Examples).ToListAsync();
        }

        public async Task<VocabularyItem?> GetByWordAsync(string word)
        {
            return await _context.VocabularyItems
                .Include(v => v.Examples)
                .FirstOrDefaultAsync(x => x.Word == word);
        }

        public async Task<VocabularyItem?> GetByIdAsync(Guid id)
        {
            return await _context.VocabularyItems
                .Include(v => v.Examples)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<VocabularyItem> CreateAsync(VocabularyItem item)
        {
            _context.VocabularyItems.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<VocabularyItem> UpdateAsync(VocabularyItem item)
        {
            _context.VocabularyItems.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var item = await _context.VocabularyItems.FindAsync(id);
            if (item == null)
                return false;

            _context.VocabularyItems.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteByWordAsync(string word)
        {
            var item = await _context.VocabularyItems.FirstOrDefaultAsync(x => x.Word == word);
            if (item == null)
                return false;

            _context.VocabularyItems.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
