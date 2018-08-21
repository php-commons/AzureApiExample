using PHP.AzureApiExample.Domain;

namespace PHP.AzureApiExample.Api.ViewModels
{
    /// <summary>
    /// The default view model for persons in the api.
    /// </summary>
    public class PersonJsonViewModel : IViewModel<Person>
    {
        /// <summary>
        /// First name of the person.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of the person.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Create a new Person domain object from this view model object.
        /// </summary>
        /// <returns></returns>
        public Person ToDomainObject()
        {
            return new Person(FirstName, LastName);
        }
    }
}
