using Vocabulary.Core.Entity.Enum;

namespace Vocabulary.Application.Common.Models;

public class Filters
{
    public Guid? UserId { get; set; }
    public string? SearchTerm { get; set; }
    public WordType? WordType { get; set; }
    public LanguagesType? Language { get; set; }

    public bool? IsFavorite { get; set; }
    public bool? IsLearned { get; set; }
    public bool? IsArchived { get; set; }

}
