using System.Data.SQLite;
using System.IO;
using DataMax.Table;

namespace DataMax
{
    public class SqLiteClient
    {
        private string _dbFile;
        private Schema _schema;

        public SqLiteClient(string dbFile, bool check = false)
        {
            // Verificar si el archivo de la base de datos existe
            if (!File.Exists(dbFile))
            {
                SQLiteConnection.CreateFile(dbFile);
            }

            // Set
            _dbFile = $"Data Source={dbFile};Version=3;";
            _schema = new Schema(_dbFile);

            // Init
            AdmNode = new AdmNode(_dbFile);

            // Update
            if (check)
            {
                // Exec Update
                _schema.Update();
            }
        }

        public AdmNode AdmNode { get; private set; }
    }
}
