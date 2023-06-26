using Newtonsoft.Json;

namespace ZenTotem.BusinessLogicLayer
{
    public static class Exstensions
    {
        public static string GetJson(this Employee employee)
        {
            return JsonConvert.SerializeObject(employee);
        }

        public static Employee GetEmployee(this string json)
        {
            return JsonConvert.DeserializeObject<Employee>(json);
        }
    }
}
