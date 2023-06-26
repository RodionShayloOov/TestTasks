using System.Text;
using ZenTotem.BusinessLogicLayer;

namespace ZenTotem.DataLayer
{
    public class FileManager : IFileManager
    {
        private readonly string _path;
        private Mutex _mutex;

        public FileManager(string path)
        {
            _path = path;
            try
            {
                _mutex = Mutex.OpenExisting("EmployeeMytex");
            }
            catch (WaitHandleCannotBeOpenedException)
            {
                _mutex = new Mutex(false, "EmployeeMytex");
            }
        }

        public string Read(int line)
        {

            if (!File.Exists(_path))
                return string.Empty;

            _mutex.WaitOne();

            string resault;
            using (StreamReader reader = new StreamReader(_path))
            {
                for (int i = 1; i < line; i++) { reader.ReadLine(); }
                resault = reader.ReadLine();
            }

            _mutex.ReleaseMutex();

            return resault;
        }

        public IEnumerable<string> ReadAll()
        {
            var resault = new List<string>();
            if (!File.Exists(_path))
                return resault;
            
            _mutex.WaitOne();

            using (StreamReader reader = new StreamReader(_path))
            {
                string? line;
                while ((line =  reader.ReadLine()) != null)
                {
                    if (string.IsNullOrEmpty(line))
                        continue;

                    resault.Add(line);
                }
            }

            _mutex.ReleaseMutex();

            return resault;
        }

        public void Write(int line, string text)
        {
            _mutex.WaitOne();

            string[] arrLine = File.ReadAllLines(_path);
            arrLine[line - 1] = text;
            File.WriteAllLines(_path, arrLine);

            _mutex.ReleaseMutex();
        }

        public void WriteToEnd(string text)
        {
            _mutex.WaitOne();

            using (StreamWriter writer = new StreamWriter(_path, true, Encoding.UTF8))
            {
                writer.WriteLine(text);
            }

            _mutex.ReleaseMutex();
        }
    }
}
