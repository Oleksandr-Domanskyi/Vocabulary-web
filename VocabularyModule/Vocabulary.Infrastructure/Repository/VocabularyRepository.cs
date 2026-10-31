using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Vocabulary.Core.Entity;
using Vocabulary.Core.Specifications;
using Vocabulary.Infrastructure.Data;

namespace Vocabulary.Infrastructure.Repository;

public class VocabularyRepository(VocabularyDbContext context) : IVocabularyRepository
{
    private readonly VocabularyDbContext _context = context;

    public Task<List<VocabularyModel>> GetAllAsync()
    {
        return _context.VocabularyModels.WithSpecification(new VocabularyGetAllSpecification()).ToListAsync();
    }

    public Task<VocabularyModel?> GetByWordAsync(string word)
    {
        return _context.VocabularyModels.WithSpecification(new VocabularyGetByIdOrNameSpecification()).FirstOrDefaultAsync(x => x.Word == word);
    }

    public async Task<VocabularyModel?> GetByIdAsync(Guid id)
    {
        var result = await _context.VocabularyModels
            .WithSpecification(new VocabularyGetByIdOrNameSpecification())
            .FirstOrDefaultAsync(x => x.Id == id);

        return result;
    }

    public async Task<VocabularyModel> CreateAsync(VocabularyModel item)
    {
        _context.VocabularyModels.Add(item);

        // Link each translation example to its corresponding original example.
        foreach (var translation in item.Translations)
        {
            if (translation.TranslationExamples.Count != item.Examples.Count)
                throw new ArgumentException("Examples count mismatch.");

            for (var i = 0; i < translation.TranslationExamples.Count; i++)
            {
                translation.TranslationExamples[i].ExampleId =
                    item.Examples[i].Id;
            }
        }
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task<VocabularyModel> UpdateAsync(VocabularyModel item)
    {
        _context.VocabularyModels.Update(item);
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var item = await _context.VocabularyModels.FindAsync(id);
        if (item == null)
            return false;

        _context.VocabularyModels.Remove(item);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteByWordAsync(string word)
    {
        var item = await _context.VocabularyModels.FirstOrDefaultAsync(x => x.Word == word);
        if (item == null)
            return false;

        _context.VocabularyModels.Remove(item);
        await _context.SaveChangesAsync();
        return true;
    }
}
