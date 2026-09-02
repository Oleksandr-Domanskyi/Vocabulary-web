using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Vocabulary.Infrastructure.Extensions;
using Vocabulary.Application.Extensions;

namespace Vocabulary.API.Extentions
{
    public static class VocabularExtensions
    {
        public static void AddVocabulary(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddVocabularyInfrastructure(configuration);
            services.AddVocabularyApplication();

            AddVocabularyAPIServices(services, configuration);
        }

        private static void AddVocabularyAPIServices(IServiceCollection services, IConfiguration configuration)
        {
        }
    }
}