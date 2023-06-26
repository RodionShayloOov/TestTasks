using NLog;

namespace ZenTotem.BusinessLogicLayer
{
    public class JsonEmployeeRepository : IEmployeeRepository
    {
        private readonly IFileManager _fileManager;
        private readonly IIdGenerator<int> _idGenerator;
        private readonly IStorageRepository _storageRepository;
        private readonly ILogger _logger;

        public JsonEmployeeRepository(
            IFileManager fileManager,
            IIdGenerator<int> idGenerator,
            ILogger logger,
            IStorageRepository storageRepository)
        {
            _fileManager = fileManager;
            _idGenerator = idGenerator;
            _logger = logger;
            _storageRepository = storageRepository;
        }

        public void Add(Employee employee)
        {
            try
            {
                employee.Id = _idGenerator.GetId();

                _fileManager.WriteToEnd(employee.GetJson());
                _storageRepository.Add(employee, employee.Id);

                _logger.Debug($"Сотруднику {employee.LastName} {employee.FirstName} был успешно сохранен с Id = {employee.Id}");
            }
            catch (Exception ex)
            {
                _logger.Error($"при добавление сотрудника {employee.FirstName} {employee.LastName} произошла ошибкой: {ex.Message}");
                throw ex;
            }
        }

        public void Update(Employee employee)
        {
            try
            {
                _fileManager.Write(employee.Id, employee.GetJson());
                _storageRepository.Update(employee, employee.Id);

                _logger.Debug($"Сотрудник {employee.LastName} {employee.FirstName} c id - {employee.Id} был успешно обновлен");
            }
            catch (Exception ex)
            {
                _logger.Error($"при обновление сотрудника {employee.FirstName} {employee.LastName} произошла ошибкой: {ex.Message}");
                throw ex;
            }
        }

        public void Delete(int id)
        {
            try
            {
                _fileManager.Write(id, string.Empty);
                _storageRepository.Delete<Employee>(id);

                _logger.Debug($"Сотрудник с id - {id} был успешно удален");
            }
            catch (Exception ex)
            {
                _logger.Error($"при удаление Сотрудник с id - {id} произошла ошибка : {ex.Message}");
                throw ex;
            }
        }

        public Employee GetEmployee(int id)
        {
            try
            {
                var employee = _storageRepository.Get<Employee>(id);

                if (employee != null)
                {
                    _logger.Debug($"сотрудник с id - {id} успешно найден.");
                    return employee;
                }

                var employeeJson = _fileManager.Read(id);
                if (string.IsNullOrEmpty(employeeJson))
                    throw new Exception("Пользователя с данным id не существует");

                return employeeJson.GetEmployee();
            }
            catch (Exception ex)
            {
                _logger.Error($"При поиске сотрудника с id {id} произошла ошибка: {ex.Message}");
                throw ex;
            }
        }

        public IEnumerable<Employee> GetEmployees()
        {
            try
            {
                var employees = _fileManager.ReadAll();             

                _logger.Debug($"поиск всех сотрудников отработал успешно найдено - {employees.Count()} сотрудников");

                return employees.Select(employee => employee.GetEmployee());
            }
            catch (Exception ex)
            {
                _logger.Debug($"Поиск всех сотрудников завершился ошибкой: {ex.Message}");
                throw ex;
            }
        }
    }
}