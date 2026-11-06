using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Vocabulary.Infrastructure.Extensions;
using Vocabulary.Application.Extensions;

namespace Vocabulary.API.Extensions;

public static class VocabularExtensions
{
    public static void AddVocabularyModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddVocabularyInfrastructure(configuration);
        services.AddVocabularyApplication();
    }
}