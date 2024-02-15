using CockroachDbWorld.DataMax;
using CockroachDbWorld.Libreria;
using SQLite;

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
            Log.Print("Bienvenido!.\n");
            SQLiteConnection db = new SQLiteConnection("C:\\Users\\LJChuello\\OneDrive\\db.db");
            db.CreateTables<AdmCluster, AdmCluster>();

            while (true)
            {
                try
                {
                    // Menu
                    Log.Print("\n");
                    Log.Print("1. Gestionar Cluster\n");
                    Log.Print("2. Gestionar Nodos\n");
                    Log.Print("3. Gestionar Balanceadores\n");
                    Log.Print("4. Salir\n");
                    Log.Print("\n");

                    // menu
                    Log.Print("Seleccione una opción: ", LogType.Info);
                    switch (Console.ReadLine())
                    {
                        case "1":
                            break;

                        case "2":
                            break;

                        case "3":
                            break;

                        case "4":
                            Environment.Exit(0);
                            break;

                        default:
                            Log.Print("\n");
                            Log.Print("Ingrese una opción válida\n");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Log.Print($"Ha ocurrido un error; {ex.Message}", LogType.Error);
                }
            }
        }
    }
}
