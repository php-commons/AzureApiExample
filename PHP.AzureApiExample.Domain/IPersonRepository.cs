namespace PHP.AzureApiExample.Domain
{
    public interface IPersonRepository
    {
        Person GetById(int id);
        int AddPerson(Person person);
        Person UpdatePerson(int id, Person person);
        bool TryDelete(int id);
    }
}