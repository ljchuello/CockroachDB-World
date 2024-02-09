using DataMax;
using DataMax.Table;

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

            AdmNode _admNode = new AdmNode("abc13");
            _admNode = new AdmNode();

            db.Info(_admNode);
        }
    }
}
