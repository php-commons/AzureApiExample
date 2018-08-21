using System;
using System.Collections.Concurrent;
using System.Diagnostics.Contracts;
using System.Linq;

namespace PHP.AzureApiExample.Domain.Services
{
    public class PersonRepository : IPersonRepository
    {
        private static ConcurrentDictionary<int, Person> _persons = new ConcurrentDictionary<int, Person>();

        public Person GetById(int id)
        {
            Contract.Requires(id > 0);
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));

            var found = _persons.TryGetValue(id, out Person value);
            if (found)
                return value;

            return null;
        }

        public int AddPerson(Person person)
        {
            Contract.Requires(person != null);
            Contract.Requires(String.IsNullOrEmpty(person.FirstName) == false);
            Contract.Requires(String.IsNullOrEmpty(person.LastName) == false);
            Contract.Ensures(Contract.Result<int>() > 0);
            if (person == null)
                throw new ArgumentNullException(nameof(person));
            if (String.IsNullOrEmpty(person.FirstName))
                throw new ArgumentOutOfRangeException(nameof(person));
            if (String.IsNullOrEmpty(person.LastName))
                throw new ArgumentOutOfRangeException(nameof(person));
            
            int newId;

            bool wasAdded = false;
            do
            {
                // Use the largest key to seed the new id, if any keys exist, otherwise
                // start at one.
                newId = _persons.Keys.Any() ? _persons.Keys.Max() + 1 : 1;
                wasAdded = _persons.TryAdd(newId, person);
            } while (wasAdded == false);

            return newId;
        }

        public Person UpdatePerson(int id, Person person)
        {
            Contract.Requires(id > 0);
            Contract.Requires(person != null);
            Contract.Requires(String.IsNullOrEmpty(person.FirstName) == false);
            Contract.Requires(String.IsNullOrEmpty(person.LastName) == false);
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));
            if (person == null)
                throw new ArgumentNullException(nameof(person));
            if (String.IsNullOrEmpty(person.FirstName))
                throw new ArgumentOutOfRangeException(nameof(person));
            if (String.IsNullOrEmpty(person.LastName))
                throw new ArgumentOutOfRangeException(nameof(person));

            if (_persons.ContainsKey(id) == false)
                return null;

            var newPerson = _persons.GetOrAdd(id, person);
            return newPerson;
        }

        public bool TryDelete(int id)
        {
            Contract.Requires(id > 0);
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));

            return _persons.TryRemove(id, out Person _);
        }
    }
}
