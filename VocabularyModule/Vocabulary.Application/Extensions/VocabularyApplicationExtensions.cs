using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MediatR;

namespace Vocabulary.Application.Extensions
{
    public static class VocabularyApplicationExtensions
    {
        public static void AddVocabularyApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(VocabularyApplicationExtensions).Assembly));
        }
    }
}