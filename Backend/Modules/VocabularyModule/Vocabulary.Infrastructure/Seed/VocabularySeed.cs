
using Microsoft.EntityFrameworkCore;
using Vocabulary.Core.Entity;
using Vocabulary.Core.Entity.Enum;
using Vocabulary.Core.Entity.Translation;
using Vocabulary.Core.Entity.Vocabulary;
using Vocabulary.Infrastructure.Data;
using VocabularyDictionary = Vocabulary.Core.Entity.Vocabulary.Dictionary;

namespace Vocabulary.Infrastructure.Seed;

/// <summary>
/// Seeds the global vocabulary and repairs missing related data on subsequent runs.
/// Existing words and their non-empty values are preserved.
/// </summary>
public static class VocabularySeed
{
    public static async Task SeedAsync(
        VocabularyDbContext context,
        CancellationToken cancellationToken = default)
    {
        var existingWords = await context.VocabularyModels
            .Where(x => x.UserId == null)
            .Include(x => x.Examples)
            .Include(x => x.Dictionaries)
            .Include(x => x.Translations)
                .ThenInclude(x => x.TranslationExamples)
            .AsSplitQuery()
            .ToListAsync(cancellationToken);

        var existingByWord = existingWords
            .GroupBy(x => x.Word, StringComparer.OrdinalIgnoreCase)
            .ToDictionary(
                group => group.Key,
                group => group.First(),
                StringComparer.OrdinalIgnoreCase);

        var seedWords = new List<VocabularyModel>
        {
            CreateVocabularyItem(
                "Eloquent", WordType.Adjectives, Level.B2,
                "Красномовний",
                "Здатний виразно й переконливо висловлювати думки усно або письмово.",
                "/ˈel.ə.kwənt/",
                "Fluent, persuasive, and expressive in speaking or writing.",
                [
                    ("The eloquent speech moved the audience to tears.",
                     "Красномовна промова зворушила аудиторію до сліз."),
                    ("She was known for her eloquent writing style.",
                     "Вона була відома своїм красномовним стилем письма.")
                ]),

            CreateVocabularyItem(
                "Ephemeral", WordType.Adjectives, Level.C1,
                "Швидкоплинний",
                "Такий, що триває дуже короткий час; тимчасовий.",
                "/ɪˈfem.ər.əl/",
                "Lasting for a very short time; temporary.",
                [
                    ("The beauty of cherry blossoms is ephemeral.",
                     "Краса цвітіння сакури швидкоплинна."),
                    ("Fame can be ephemeral for many celebrities.",
                     "Слава багатьох знаменитостей може бути швидкоплинною.")
                ]),

            CreateVocabularyItem(
                "Ubiquitous", WordType.Adjectives, Level.C1,
                "Всюдисущий",
                "Присутній або поширений майже всюди.",
                "/juːˈbɪk.wɪ.təs/",
                "Present, appearing, or found everywhere.",
                [
                    ("Smartphones have become ubiquitous in modern society.",
                     "Смартфони стали всюдисущими в сучасному суспільстві."),
                    ("Coffee shops are ubiquitous in major cities.",
                     "Кав'ярні зустрічаються повсюди у великих містах.")
                ]),

            CreateVocabularyItem(
                "Pragmatic", WordType.Adjectives, Level.B2,
                "Практичний",
                "Орієнтований на практичні та реалістичні рішення.",
                "/præɡˈmæt.ɪk/",
                "Dealing with things in a practical, realistic way.",
                [
                    ("He took a pragmatic approach to solving the problem.",
                     "Він застосував прагматичний підхід до вирішення проблеми."),
                    ("The company adopted a pragmatic business strategy.",
                     "Компанія обрала прагматичну бізнес-стратегію.")
                ]),

            CreateVocabularyItem(
                "Ambiguous", WordType.Adjectives, Level.B2,
                "Неоднозначний",
                "Такий, що допускає кілька тлумачень або є нечітким.",
                "/æmˈbɪɡ.ju.əs/",
                "Open to more than one interpretation; unclear or vague.",
                [
                    ("The statement was ambiguous and could be misunderstood.",
                     "Твердження було неоднозначним і могло бути неправильно зрозумілим."),
                    ("His response was intentionally ambiguous.",
                     "Його відповідь була навмисно неоднозначною.")
                ]),

            CreateVocabularyItem(
                "Meticulous", WordType.Adjectives, Level.C1,
                "Ретельний",
                "Дуже уважний до деталей, точний і ретельний.",
                "/məˈtɪk.jə.ləs/",
                "Showing great attention to detail; very careful and precise.",
                [
                    ("The artist was meticulous in her work.",
                     "Художниця була надзвичайно ретельною у своїй роботі."),
                    ("He kept meticulous records of all transactions.",
                     "Він ретельно вів записи всіх операцій.")
                ]),

            CreateVocabularyItem(
                "Tenacious", WordType.Adjectives, Level.C1,
                "Наполегливий",
                "Наполегливий і рішучий у досягненні мети.",
                "/təˈneɪ.ʃəs/",
                "Holding firmly to something; persistent and determined.",
                [
                    ("Her tenacious attitude helped her overcome obstacles.",
                     "Її наполегливість допомогла їй подолати перешкоди."),
                    ("The team was tenacious in pursuit of victory.",
                     "Команда наполегливо прагнула перемоги.")
                ]),

            CreateVocabularyItem(
                "Resilient", WordType.Adjectives, Level.B2,
                "Стійкий",
                "Здатний швидко відновлюватися після труднощів.",
                "/rɪˈzɪl.i.ənt/",
                "Able to recover quickly from difficulties; tough.",
                [
                    ("The resilient community rebuilt after the disaster.",
                     "Стійка громада відбудувалася після катастрофи."),
                    ("She showed a resilient spirit during tough times.",
                     "Вона продемонструвала стійкість духу у важкі часи.")
                ]),

            CreateVocabularyItem(
                "Verbose", WordType.Adjectives, Level.B2,
                "Багатослівний",
                "Такий, що використовує більше слів, ніж необхідно.",
                "/vɜːˈbəʊs/",
                "Using or containing more words than necessary; wordy.",
                [
                    ("His verbose writing style made the article difficult to read.",
                     "Його багатослівний стиль письма ускладнював читання статті."),
                    ("She tends to be verbose in meetings.",
                     "Вона схильна бути багатослівною на зустрічах.")
                ]),

            CreateVocabularyItem(
                "Serendipity", WordType.Nouns, Level.C1,
                "Щаслива випадковість",
                "Щасливе й корисне відкриття або подія, що сталася випадково.",
                "/ˌser.ənˈdɪp.ə.ti/",
                "The occurrence of events by chance in a happy or beneficial way.",
                [
                    ("By serendipity, we met at the right place and time.",
                     "Завдяки щасливому випадку ми зустрілися в потрібному місці та в потрібний час."),
                    ("Finding that old letter was pure serendipity.",
                     "Знайти той старий лист було справжньою щасливою випадковістю.")
                ]),

            CreateVocabularyItem(
                "Nostalgia", WordType.Nouns, Level.B2,
                "Ностальгія",
                "Теплі, іноді сумні спогади та туга за минулим.",
                "/nɒsˈtæl.dʒə/",
                "A sentimental longing for the past.",
                [
                    ("She felt nostalgia when visiting her childhood home.",
                     "Вона відчула ностальгію, відвідавши будинок свого дитинства."),
                    ("The song evoked nostalgia for the 80s.",
                     "Пісня викликала ностальгію за вісімдесятими.")
                ]),

            CreateVocabularyItem(
                "Diligent", WordType.Adjectives, Level.B2,
                "Старанний",
                "Старанний і сумлінний у виконанні обов’язків.",
                "/ˈdɪl.ɪ.dʒənt/",
                "Having or showing care in one's work or duties.",
                [
                    ("His diligent effort resulted in excellent grades.",
                     "Його старанна праця принесла відмінні оцінки."),
                    ("She was diligent in completing her responsibilities.",
                     "Вона старанно виконувала свої обов'язки.")
                ]),

            CreateVocabularyItem(
                "Benevolent", WordType.Adjectives, Level.C1,
                "Доброзичливий",
                "Добрий, доброзичливий і готовий допомагати іншим.",
                "/bəˈnev.əl.ənt/",
                "Well-meaning and kindly; showing goodwill.",
                [
                    ("The benevolent king was loved by his people.",
                     "Доброзичливого короля любив його народ."),
                    ("Her benevolent nature made her popular in the community.",
                     "Її доброзичлива вдача зробила її популярною в громаді.")
                ]),

            CreateVocabularyItem(
                "Candid", WordType.Adjectives, Level.B2,
                "Відвертий",
                "Щирий, прямолінійний і відвертий у висловлюваннях.",
                "/ˈkæn.dɪd/",
                "Truthful and straightforward; frank.",
                [
                    ("He was candid about his mistakes.",
                     "Він відверто говорив про свої помилки."),
                    ("She gave a candid interview about her experiences.",
                     "Вона дала відверте інтерв'ю про свій досвід.")
                ]),

            CreateVocabularyItem(
                "Gregarious", WordType.Adjectives, Level.C1,
                "Товариський",
                "Товариський, любить перебувати в компанії інших.",
                "/ɡrɪˈɡeə.ri.əs/",
                "Fond of being in company; enjoying the company of others.",
                [
                    ("He was naturally gregarious and loved social events.",
                     "Він був товариським від природи й любив громадські заходи."),
                    ("Humans are gregarious creatures.",
                     "Люди — товариські істоти.")
                ]),

            CreateVocabularyItem(
                "Impeccable", WordType.Adjectives, Level.C1,
                "Бездоганний",
                "Бездоганний, такий, що відповідає найвищим стандартам.",
                "/ɪmˈpek.ə.bəl/",
                "In accordance with the highest standards; faultless.",
                [
                    ("The service at the restaurant was impeccable.",
                     "Обслуговування в ресторані було бездоганним."),
                    ("She maintained an impeccable appearance.",
                     "Вона завжди підтримувала бездоганний зовнішній вигляд.")
                ]),

            CreateVocabularyItem(
                "Lucrative", WordType.Adjectives, Level.B2,
                "Прибутковий",
                "Такий, що приносить значний прибуток.",
                "/ˈluː.krə.tɪv/",
                "Producing a great deal of profit; highly profitable.",
                [
                    ("Tech industry jobs are often lucrative.",
                     "Робота в технологічній галузі часто є прибутковою."),
                    ("She found a lucrative opportunity in real estate.",
                     "Вона знайшла прибуткову можливість у сфері нерухомості.")
                ]),

            CreateVocabularyItem(
                "Obfuscate", WordType.Verbs, Level.C1,
                "Заплутувати",
                "Навмисно робити щось незрозумілим або заплутаним.",
                "/ˈɒb.fʌs.keɪt/",
                "To deliberately make something unclear or obscure.",
                [
                    ("He tried to obfuscate the truth with confusing statements.",
                     "Він намагався приховати правду за заплутаними твердженнями."),
                    ("The code was intentionally obfuscated for security.",
                     "Код навмисно заплутали з міркувань безпеки.")
                ]),

            CreateVocabularyItem(
                "Paradigm", WordType.Nouns, Level.C1,
                "Парадигма",
                "Зразок, модель або усталений спосіб розуміння чогось.",
                "/ˈpær.ə.daɪm/",
                "A typical example or pattern of something; a model.",
                [
                    ("The scientific paradigm shifted with new discoveries.",
                     "Наукова парадигма змінилася завдяки новим відкриттям."),
                    ("This company is a paradigm of success.",
                     "Ця компанія є взірцем успіху.")
                ]),

            CreateVocabularyItem(
                "Zealous", WordType.Adjectives, Level.B2,
                "Завзятий",
                "Сповнений запалу й ентузіазму щодо певної справи.",
                "/ˈzel.əs/",
                "Having or showing great energy or enthusiasm in pursuit of a cause.",
                [
                    ("He was zealous in his pursuit of justice.",
                     "Він завзято прагнув справедливості."),
                    ("The team approached the project with zealous determination.",
                     "Команда взялася за проєкт із завзятою рішучістю.")
                ])
        };

        foreach (var seedWord in seedWords)
        {
            if (existingByWord.TryGetValue(seedWord.Word, out var existing))
            {
                AddMissingRelations(existing, seedWord);
            }
            else
            {
                context.VocabularyModels.Add(seedWord);
            }
        }

        await context.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Adds missing seeded examples, Ukrainian translations and Cambridge references
    /// without replacing existing entities or changing their identifiers.
    /// </summary>
    private static void AddMissingRelations(
        VocabularyModel existing,
        VocabularyModel seedWord)
    {
        if (!existing.Dictionaries.Any(x =>
                x.Name == "Cambridge Dictionary" &&
                x.Language == LanguagesType.EN))
        {
            existing.Dictionaries.Add(CreateDictionary(existing.Id));
        }

        var seedTranslation = seedWord.Translations.Single();
        var translation = existing.Translations.FirstOrDefault(x =>
            x.LanguageCode == LanguagesType.UK);

        if (translation is null)
        {
            translation = new TranslationModel
            {
                Id = Guid.NewGuid(),
                VocabularyId = existing.Id,
                TranslatedWord = seedTranslation.TranslatedWord,
                TranslatedDefinition = seedTranslation.TranslatedDefinition,
                LanguageCode = LanguagesType.UK
            };

            existing.Translations.Add(translation);
        }

        foreach (var seedExample in seedWord.Examples)
        {
            var example = existing.Examples.FirstOrDefault(x =>
                x.Sentences == seedExample.Sentences);

            if (example is null)
            {
                example = new Example
                {
                    Id = Guid.NewGuid(),
                    WordId = existing.Id,
                    Sentences = seedExample.Sentences
                };

                existing.Examples.Add(example);
            }

            if (translation.TranslationExamples.Any(x => x.ExampleId == example.Id))
                continue;

            var seedExampleTranslation = seedTranslation.TranslationExamples
                .Single(x => x.ExampleId == seedExample.Id);

            translation.TranslationExamples.Add(new TranslationExample
            {
                Id = Guid.NewGuid(),
                ExampleId = example.Id,
                TranslationId = translation.Id,
                TranslatedSentences = seedExampleTranslation.TranslatedSentences
            });
        }
    }

    private static VocabularyModel CreateVocabularyItem(
        string word,
        WordType wordType,
        Level level,
        string translatedWord,
        string translatedDefinition,
        string transcription,
        string definition,
        (string Sentence, string Translation)[] examples)
    {
        var vocabularyId = Guid.NewGuid();
        var translationId = Guid.NewGuid();

        var originalExamples = examples
            .Select(example => new Example
            {
                Id = Guid.NewGuid(),
                WordId = vocabularyId,
                Sentences = example.Sentence
            })
            .ToList();

        var translatedExamples = examples
            .Select((example, index) => new TranslationExample
            {
                Id = Guid.NewGuid(),
                ExampleId = originalExamples[index].Id,
                TranslationId = translationId,
                TranslatedSentences = example.Translation
            })
            .ToList();

        return new VocabularyModel
        {
            Id = vocabularyId,
            UserId = null,
            Word = word,
            WordType = wordType,
            Level = level,
            Definition = definition,
            Transcription = transcription,
            CreatedAt = DateTime.UtcNow,
            Examples = originalExamples,
            Dictionaries = [CreateDictionary(vocabularyId)],
            Translations =
            [
                new TranslationModel
                {
                    Id = translationId,
                    VocabularyId = vocabularyId,
                    TranslatedWord = translatedWord,
                    TranslatedDefinition = translatedDefinition,
                    LanguageCode = LanguagesType.UK,
                    TranslationExamples = translatedExamples
                }
            ],
            ImageUrl = null
        };
    }

    private static VocabularyDictionary CreateDictionary(Guid vocabularyId)
        => new()
        {
            Id = Guid.NewGuid(),
            VocabularyId = vocabularyId,
            Name = "Cambridge Dictionary",
            DictionaryUrl = "https://dictionary.cambridge.org/",
            Language = LanguagesType.EN
        };
}
