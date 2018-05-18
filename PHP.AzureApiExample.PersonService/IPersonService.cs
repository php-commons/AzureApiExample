namespace PHP.AzureApiExample.PersonService.Controllers
{
    public interface IPersonService
    {
        Person GetById(int id);
    }
}