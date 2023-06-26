namespace ZenTotem.BusinessLogicLayer
{
    public interface IStorageRepository
    {
        void Add<T>(T value, object id) where T : class;

        T Get<T>(object id) where T : class;

        void Update<T>(T value, object id) where T : class;

        void Delete<T>(object id) where T : class;
    }
}