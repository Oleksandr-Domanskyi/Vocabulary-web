namespace Vocabulary.Core.DTO.Request.Transaltion;

public class TranslationExampleRequest
{
    public Guid? Id { get; set; }
    public Guid? ExampleId { get; set; }
    public string TranslatedSentence { get; set; } = default!;
}
