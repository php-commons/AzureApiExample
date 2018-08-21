namespace PHP.AzureApiExample.Api.ViewModels
{
    /// <summary>
    /// A common intrrface for all view models that can be translated to a underlying domain
    /// model.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IViewModel<out T> 
    {
        /// <summary>
        /// Return a new domain object based on this view model.
        /// </summary>
        /// <returns></returns>
        T ToDomainObject();
    }
}
