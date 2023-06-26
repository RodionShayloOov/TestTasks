using Microsoft.Extensions.Caching.Memory;
using ZenTotem.BusinessLogicLayer;

namespace ZenTotem.DataLayer
{
    // Данный кэш не стоит использовать, если будет работать сразу несколько процессов данного приложения.
    // Лучше создать другую имплементацию интерфейса IStorageRepository - RedisRepository, или не использовать кэш.

    public class InMemoryRepository : IStorageRepository
    {
        private readonly IMemoryCache _cache;

        public InMemoryRepository(IFileManager fileManager)
        {
            _cache = new MemoryCache(new MemoryCacheOptions());            

            foreach (var item in fileManager.ReadAll())
            {
                var employee = item.GetEmployee();
                _cache.Set(employee.Id, employee);
            }
        }

        public void Add<T>(T value, object id) where T : class
        {
            _cache.Set(id, value);
        }

        public void Delete<T>(object id) where T : class
        {
            _cache.Remove(id);
        }

        public T Get<T>(object id) where T : class
        {
            return _cache.Get<T>(id);
        }

        public void Update<T>(T value, object id) where T : class
        {
            _cache.Remove(id);
            _cache.Set(id, value);
        }
    }
}
