using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PHP.AzureApiExample.Api
{
    /// <summary>
    /// A simple class to serialize validation errors into JSON format. 
    /// </summary>
    public class JsonValidationError
    {
        /// <summary>
        /// The validation error key.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// The validation error message. 
        /// </summary>
        public string ErrorMessage { get; set; }
    }

    /// <summary>
    /// A set of extensions to better work with ModelState validation in terms of JSON. 
    /// </summary>
    public static class ModelStateExtensions
    {
        /// <summary>
        /// From the model state in MVC, create an array of JsonValidationError objects
        /// to return as part of the BadRequest (or other) HTTP response body. 
        /// </summary>
        /// <param name="modelState"></param>
        /// <returns></returns>
        public static JsonValidationError[] GetJsonValidationErrors(this ModelStateDictionary modelState)
        {
            // Okay, this is a deep functional one liner to flatten the modelState dictionary into a simpler collection.
            // The trick is understanding SelectMany, which takes a Enumerable, creates a Enumerable of Enumerables given
            // the select function, and then flattens that Enumerable of Enumerables into an result Enumerable.             
            return modelState
                .SelectMany(
                    kv => kv.Value.Errors.Select(me => new JsonValidationError {ErrorMessage = me.ErrorMessage, Key = kv.Key}))
                .ToArray();
        }
    }
}
