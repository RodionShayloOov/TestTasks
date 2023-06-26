namespace ZenTotem.BusinessLogicLayer
{
    public interface IEmployeeRepository
    {
        public void Add(Employee employee);

        public void Update(Employee employee);

        public void Delete(int id);

        public Employee GetEmployee(int id);

        public IEnumerable<Employee> GetEmployees();
    }
}
