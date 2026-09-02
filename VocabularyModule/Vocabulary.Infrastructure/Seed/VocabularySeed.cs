using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vocabulary.Core.Entity;
using Vocabulary.Infrastructure.Data;

namespace Vocabulary.Infrastructure.Seed
{
    public class VocabularySeed
    {
        public async Task seed(VocabularyDbContext context)
        {
            if (context.VocabularyItems.Any())
            {
                return;
            }

            var Words = new List<VocabularyItem>
            {
                CreateVocabularyItem("Eloquent", "Adj", "Fluent, persuasive, and expressive in speaking or writing.", "B2",
                    new[] { "The eloquent speech moved the audience to tears.", "She was known for her eloquent writing style." }),

                CreateVocabularyItem("Ephemeral", "Adj", "Lasting for a very short time; temporary.", "C1",
                    new[] { "The beauty of cherry blossoms is ephemeral.", "Fame can be ephemeral for many celebrities." }),

                CreateVocabularyItem("Ubiquitous", "Adj", "Present, appearing, or found everywhere.", "C1",
                    new[] { "Smartphones have become ubiquitous in modern society.", "Coffee shops are ubiquitous in major cities." }),

                CreateVocabularyItem("Pragmatic", "Adj", "Dealing with things in a practical, realistic way.", "B2",
                    new[] { "He took a pragmatic approach to solving the problem.", "The company adopted a pragmatic business strategy." }),

                CreateVocabularyItem("Ambiguous", "Adj", "Open to more than one interpretation; unclear or vague.", "B2",
                    new[] { "The statement was ambiguous and could be misunderstood.", "His response was intentionally ambiguous." }),

                CreateVocabularyItem("Meticulous", "Adj", "Showing great attention to detail; very careful and precise.", "C1",
                    new[] { "The artist was meticulous in her work.", "He kept meticulous records of all transactions." }),

                CreateVocabularyItem("Tenacious", "Adj", "Holding firmly to something; persistent and determined.", "C1",
                    new[] { "Her tenacious attitude helped her overcome obstacles.", "The team was tenacious in pursuit of victory." }),

                CreateVocabularyItem("Resilient", "Adj", "Able to recover quickly from difficulties; tough.", "B2",
                    new[] { "The resilient community rebuilt after the disaster.", "She showed a resilient spirit during tough times." }),

                CreateVocabularyItem("Verbose", "Adj", "Using or containing more words than necessary; wordy.", "B2",
                    new[] { "His verbose writing style made the article difficult to read.", "She tends to be verbose in meetings." }),

                CreateVocabularyItem("Serendipity", "Noun", "The occurrence of events by chance in a happy or beneficial way.", "C1",
                    new[] { "By serendipity, we met at the right place and time.", "Finding that old letter was pure serendipity." }),

                CreateVocabularyItem("Nostalgia", "Noun", "A sentimental longing for the past, typically for a period or place.", "B2",
                    new[] { "She felt nostalgia when visiting her childhood home.", "The song evoked nostalgia for the 80s." }),

                CreateVocabularyItem("Diligent", "Adj", "Having or showing care in one's work or duties.", "B2",
                    new[] { "His diligent effort resulted in excellent grades.", "She was diligent in completing her responsibilities." }),

                CreateVocabularyItem("Benevolent", "Adj", "Well-meaning and kindly; showing goodwill.", "C1",
                    new[] { "The benevolent king was loved by his people.", "Her benevolent nature made her popular in the community." }),

                CreateVocabularyItem("Candid", "Adj", "Truthful and straightforward; frank.", "B2",
                    new[] { "He was candid about his mistakes.", "She gave a candid interview about her experiences." }),

                CreateVocabularyItem("Gregarious", "Adj", "Fond of being in company; enjoying the company of others.", "C1",
                    new[] { "He was naturally gregarious and loved social events.", "Humans are gregarious creatures." }),

                CreateVocabularyItem("Impeccable", "Adj", "In accordance with the highest standards; faultless.", "C1",
                    new[] { "The service at the restaurant was impeccable.", "She maintained an impeccable appearance." }),

                CreateVocabularyItem("Lucrative", "Adj", "Producing a great deal of profit; highly profitable.", "B2",
                    new[] { "Tech industry jobs are often lucrative.", "She found a lucrative opportunity in real estate." }),

                CreateVocabularyItem("Obfuscate", "Verb", "To deliberately make something unclear or obscure.", "C1",
                    new[] { "He tried to obfuscate the truth with confusing statements.", "The code was intentionally obfuscated for security." }),

                CreateVocabularyItem("Paradigm", "Noun", "A typical example or pattern of something; a model.", "C1",
                    new[] { "The scientific paradigm shifted with new discoveries.", "This company is a paradigm of success." }),

                CreateVocabularyItem("Zealous", "Adj", "Having or showing great energy or enthusiasm in pursuit of a cause.", "B2",
                    new[] { "He was zealous in his pursuit of justice.", "The team approached the project with zealous determination." })
            };

            context.VocabularyItems.AddRange(Words);
            await context.SaveChangesAsync();
        }

        private VocabularyItem CreateVocabularyItem(string word, string type, string definition, string level, string[] exampleTexts)
        {
            var itemId = Guid.NewGuid();
            return new VocabularyItem
            {
                Id = itemId,
                Word = word,
                Type = type,
                Definition = definition,
                EnglishLevel = level,
                Examples = exampleTexts.Select(text => new Example
                {
                    Id = Guid.NewGuid(),
                    ExampleText = text,
                    WordId = itemId
                }).ToList()
            };
        }
    }
}