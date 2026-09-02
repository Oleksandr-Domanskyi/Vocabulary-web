using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Vocabulary.Infrastructure.Data;
using Vocabulary.Infrastructure.Repository;

namespace Vocabulary.Infrastructure.Extensions
{
    public static class VocabularyInfrastructureExtensions
    {
        public static void AddVocabularyInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<VocabularyDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Default")));

            services.AddScoped<IVocabularyRepository, VocabularyRepository>();
        }
    }
}