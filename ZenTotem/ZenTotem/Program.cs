using NLog;
using System.Configuration;
using ZenTotem.BusinessLogicLayer;
using ZenTotem.DataLayer;
using ZenTotem.ViewLayer;

var connectionString = ConfigurationManager.ConnectionStrings["EmployeeJson"].ConnectionString;
var fileManager = new FileManager(connectionString);
var hours = ConfigurationManager.AppSettings["CacheRefreshHours"];
var cache = new InMemoryRepository(fileManager);
var logger = LogManager.GetCurrentClassLogger();
var idGenerator = new NumberIdGenerator(fileManager);
var repository = new JsonEmployeeRepository(fileManager, idGenerator, logger, cache);
var controller = new Controller(repository);

Console.WriteLine(
    @"
    Add - добавить сотрудника
    Update - обновить данные сотрудника
    Dellete - удалить сотрудника
    Get - Получить данные сотрудника
    GetAll - получить данные всех сотрудников");


while (true)
{
    Console.WriteLine("_________________________________________________________________________________________________________");
    Console.WriteLine("Выберите действие");
    var command = Console.ReadLine();

    switch (command.ToLower())
    {
        case "add":
            controller.Add(); break;
        case "update":
            controller.Update(); break;
        case "get":
            controller.Get(); break;
        case "dellete":
            controller.Delete(); break;
        case "getall":
            controller.GetAll(); break;
        default:
            return;
    }

    Console.WriteLine("_________________________________________________________________________________________________________");
}
