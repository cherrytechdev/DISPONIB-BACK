namespace ESAPI.IRepositories
{
    public interface IAuthMiddleware
    {
        public interface IGenderizeService
        {
            Task Invoke(string name);
        }
    }
}
