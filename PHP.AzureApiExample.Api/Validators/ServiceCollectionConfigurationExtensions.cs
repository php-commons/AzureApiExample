using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PHP.AzureApiExample.Api.ViewModels;

namespace PHP.AzureApiExample.Api.Validators
{
    /// <summary>
    /// Used to add custom validators to the service collection (part of the ASP .Net Core DI container).
    /// </summary>
    public static class ServiceCollectionConfigurationExtensions
    {
        /// <summary>
        /// Add the validator for the PersonJsonViewModel class to the service collection. 
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <returns></returns>
        public static IServiceCollection AddValidators(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IValidator<PersonJsonViewModel>, PersonJsonViewModelValidator>();
            return serviceCollection;
        }
    }
}
