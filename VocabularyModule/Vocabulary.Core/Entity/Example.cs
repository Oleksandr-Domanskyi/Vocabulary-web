namespace Vocabulary.Core.Entity;

public class Example : BaseEntity<Example>
{
    public Guid WordId { get; set; } = default!;
    public string Sentences { get; set; } = default!;
}
