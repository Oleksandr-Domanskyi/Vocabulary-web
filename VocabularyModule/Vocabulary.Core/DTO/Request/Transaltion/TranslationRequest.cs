namespace Vocabulary.Core.DTO.Request.Transaltion;

public class TranslationRequest
{
    public Guid? Id { get; set; }
    public Guid? WordId { get; set; }
    public string TranslatedWord { get; set; } = default!;
    public string TranslatedDefinition { get; set; } = default!;
    public string LanguageCode { get; set; } = "UK";
    public List<TranslationExampleRequest> TranslationExamples { get; set; } = [];
}
