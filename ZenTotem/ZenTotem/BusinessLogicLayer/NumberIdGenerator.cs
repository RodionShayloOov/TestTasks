namespace ZenTotem.BusinessLogicLayer
{
    public class NumberIdGenerator : IIdGenerator<int>
    {
        private readonly IFileManager _fileManager;
        private int _id;

        public NumberIdGenerator(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        public int GetId()
        {
            if (_id > 0)
                return _id++;

            var jsonEmployees = _fileManager.ReadAll();
            _id = jsonEmployees.Any() ?
                jsonEmployees.Last().GetEmployee().Id + 1
                : 1 ;

            return _id++;
        }
    }
}
