namespace ZenTotem.BusinessLogicLayer
{
    public interface IFileManager
    {
        public string Read(int line);

        public void Write(int line, string text);

        public void WriteToEnd(string text);

        public IEnumerable<string> ReadAll();
    }
}