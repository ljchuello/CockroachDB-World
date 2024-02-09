using System;
using System.Collections.Generic;
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

        public string Select<T>()
        {
            var tblNameAttribute = (SqLiteTlb)Attribute.GetCustomAttribute(typeof(T), typeof(SqLiteTlb));

            List<string> list = new List<string>();
            foreach (var property in typeof(T).GetProperties())
            {
                SqLiteFld fldAttribute = (SqLiteFld)Attribute.GetCustomAttribute(property, typeof(SqLiteFld));
                list.Add(fldAttribute.FldName);
            }

            return $"SELECT {string.Join(", ", list)} FROM {tblNameAttribute.TblName}";
        }

        public void Info(dynamic obj)
        {
            // Obtener el tipo del objeto dinámico
            Type objectType = obj.GetType();

            // Obtener el nombre de la tabla a partir del atributo personalizado SqLiteTlb
            var tblNameAttribute = (SqLiteTlb)Attribute.GetCustomAttribute(objectType, typeof(SqLiteTlb));
            string tableName = tblNameAttribute.TblName;
        }
    }
}
