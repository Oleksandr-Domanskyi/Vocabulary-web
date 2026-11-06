using Vocabulary.Core.Entity.Vocabulary;

namespace Vocabulary.Infrastructure.Repository;

public interface IVocabularyRepository
{
    Task<List<VocabularyModel>> GetAllAsync();
    Task<VocabularyModel?> GetByWordAsync(string word);
    Task<VocabularyModel?> GetByIdAsync(Guid id);
    Task<VocabularyModel> CreateAsync(VocabularyModel item);
    Task<VocabularyModel> UpdateAsync(VocabularyModel item);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> DeleteByWordAsync(string word);
}
