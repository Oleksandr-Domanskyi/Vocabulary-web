using Vocabulary.Core.Entity.Vocabulary;
namespace Vocabulary.Core.DTO.Request.Example;

public class ExampleRequest
{
    public Guid? Id { get; set; }
    public Guid? WordId { get; set; }
    public string Sentence { get; set; } = default!;
}
