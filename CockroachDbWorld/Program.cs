using CockroachDbWorld.Data;
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

            AdmNode _admNode = new AdmNode();
            _admNode.Id = $"{Guid.NewGuid()}";
            _admNode.Descripcion = "Leonardo Chuello";
            _admNode.Edad = 15;
            _admNode.Saldo = 2.22m;

            db.Insert(_admNode);
        }
    }
}
