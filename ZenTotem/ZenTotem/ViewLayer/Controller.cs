using ZenTotem.BusinessLogicLayer;

namespace ZenTotem.ViewLayer
{
    public class Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public Controller(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public void Add()
        {
            try
            {
                var employee = new Employee();

                Console.WriteLine("Введите имя");
                employee.FirstName = Console.ReadLine();

                Console.WriteLine("Введите Фамилию");
                employee.LastName = Console.ReadLine();

                Console.WriteLine("Введите доход в час");             
                if (!decimal.TryParse(Console.ReadLine(),out decimal salary))
                {
                    Console.WriteLine("Ошибка: Введенно не число");
                    return;
                }

                employee.SalaryPerHour = salary;


                _employeeRepository.Add(employee);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Add завершился ошибкой:{ex.Message}");
            }
        }

        public void Delete()
        {
            try
            {
                Console.WriteLine("Введите Id пользователя");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("Ошибка: Введенно не число");
                    return;
                }

                var emlpoyee = _employeeRepository.GetEmployee(id);
                _employeeRepository.Delete(emlpoyee.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Delete завершился ошибкой:{ex.Message}");
            }
        }

        public void Update()
        {
            try
            {
                Console.WriteLine("Введите Id пользователя");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("Ошибка: Введенно не число");
                    return;
                }

                var emlpoyee = _employeeRepository.GetEmployee(id);

                Console.WriteLine("Что бы сохронить прежнее значение ничего не вводите");

                Console.WriteLine($"Введите новое знаечение для поля имя. Старое значение {emlpoyee.FirstName}.");
                var name = Console.ReadLine();

                emlpoyee.FirstName = string.IsNullOrEmpty(name) ?
                    emlpoyee.FirstName :
                    name;

                Console.WriteLine($"Введите новое знаечение для поля фамилия. Старое значение {emlpoyee.LastName}.");
                var lastName = Console.ReadLine();

                emlpoyee.LastName = string.IsNullOrEmpty(lastName) ?
                    emlpoyee.LastName :
                    lastName;

                Console.WriteLine($"Введите новое знаечение для поля Доход в час. Старое значение {emlpoyee.SalaryPerHour}.");
                var salaryPerHour = Console.ReadLine();

                emlpoyee.SalaryPerHour = string.IsNullOrEmpty(salaryPerHour) ?
                    emlpoyee.SalaryPerHour :
                    Convert.ToDecimal(salaryPerHour);

                _employeeRepository.Update(emlpoyee);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Update завершился ошибкой:{ex.Message}");
            }
        }

        public void Get()
        {
            try
            {
                Console.WriteLine("Введите Id пользователя");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("Ошибка: Введенно не число");
                    return;
                }

                var emlpoyee = _employeeRepository.GetEmployee(id);

                Console.WriteLine($"Id = {emlpoyee.Id} FirstName = {emlpoyee.FirstName}  LastName = {emlpoyee.LastName} SalaryPerHour = {emlpoyee.SalaryPerHour}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Get завершился ошибкой:{ex.Message}");
            }
        }

        public void GetAll()
        {
            try
            {
                var employees = _employeeRepository.GetEmployees();
                if (!employees.Any())
                {
                    Console.WriteLine("ни один сотрудник еще не добавлен");
                    return;
                }

                foreach (var emlpoyee in employees)
                {
                    Console.WriteLine($"Id = {emlpoyee.Id} FirstName = {emlpoyee.FirstName}  LastName = {emlpoyee.LastName} SalaryPerHour = {emlpoyee.SalaryPerHour}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetAll завершился ошибкой:{ex.Message}");
            }

        }
    }
}
