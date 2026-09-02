using Vocabulary.Core.Entity;

namespace Vocabulary.Infrastructure.Repository
{
    public interface IVocabularyRepository
    {
        Task<List<VocabularyItem>> GetAllAsync();
        Task<VocabularyItem?> GetByWordAsync(string word);
        Task<VocabularyItem?> GetByIdAsync(Guid id);
        Task<VocabularyItem> CreateAsync(VocabularyItem item);
        Task<VocabularyItem> UpdateAsync(VocabularyItem item);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> DeleteByWordAsync(string word);
    }
}
