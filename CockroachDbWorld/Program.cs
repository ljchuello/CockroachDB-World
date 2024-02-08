using DataMax;

namespace CockroachDbWorld
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MainAsync().Wait();
        }

        static async Task MainAsync()
        {
            SqLiteClient db = new SqLiteClient("C:\\Users\\LJChuello\\OneDrive\\db.db", true);
        }
    }
}
