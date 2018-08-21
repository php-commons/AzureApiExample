using System;
using System.Diagnostics.Contracts;

namespace PHP.AzureApiExample.Domain
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Person(string firstName, string lastName)
        {
            Contract.Requires(String.IsNullOrEmpty(firstName));
            Contract.Requires(String.IsNullOrEmpty(lastName));
            if (String.IsNullOrEmpty(firstName))
                throw new ArgumentOutOfRangeException(nameof(firstName), "cannot be null or empty");
            if (String.IsNullOrEmpty(firstName))
                throw new ArgumentOutOfRangeException(nameof(lastName), "cannot be null or empty");

            FirstName = firstName;
            LastName = lastName;
        }

        public void Deconstruct(out string firstName, out string lastName)
        {
            firstName = FirstName;
            lastName = LastName;
        }
    }
}