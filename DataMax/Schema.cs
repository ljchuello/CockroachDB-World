using System;
using System.Data;
using System.Data.SQLite;
using System.Reflection;

namespace DataMax
{
    public class Schema
    {
        string _dbFile;

        public Schema(string dbFile)
        {
            _dbFile = dbFile;
        }

        public void Update()
        {
            Assembly assembly = Assembly.GetExecutingAssembly(); // Obtener la assembly actual
            Type[] types = assembly.GetTypes(); // Obtener todos los tipos en la assembly

            foreach (Type type in types)
            {
                // Verificar si la clase tiene el atributo SQLiteTbl
                if (type.GetCustomAttributes(typeof(SqLiteTlb), true).Length > 0)
                {
                    // Ejecutar el método ExecUpdate con el tipo genérico
                    MethodInfo method = typeof(Schema).GetMethod("ExecUpdate", BindingFlags.NonPublic | BindingFlags.Instance);
                    MethodInfo genericMethod = method.MakeGenericMethod(type);
                    genericMethod.Invoke(this, null);
                }
            }
        }

        private void ExecUpdate<T>()
        {
            // Obtener el valor del atributo SqLiteTlb aplicado a la clase
            var tblNameAttribute = (SqLiteTlb)Attribute.GetCustomAttribute(typeof(T), typeof(SqLiteTlb));

            // Verificar si se encontró el atributo de nombre de tabla
            if (tblNameAttribute != null)
            {
                // Set table name
                string tableName = tblNameAttribute.TblName;

                // Create

                #region Exist = false

                // Creamos
                if (exist == false)
                {
                    TblCreate(tableName, typeof(T).GetProperties());
                }

                #endregion
            }
        }

        private void TblCreate(string tblName, PropertyInfo[] listField)
        {
            // Existe
            using (SQLiteConnection db = new SQLiteConnection(_dbFile))
            {
                db.Open();
                SQLiteCommand sqlCommand = new SQLiteCommand();
                sqlCommand.Connection = db;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = $"SELECT 1 AS 'Exist' FROM sqlite_master WHERE type = 'table' AND name = '{tblName}' LIMIT 1;";
                using (SQLiteDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (Convert.ToBoolean(reader["Exist"]))
                        {
                            return;
                        }
                    }
                }
            }

            // Creamos

            foreach (var property in listField)
            {
                SqLiteFld fldAttribute = (SqLiteFld)Attribute.GetCustomAttribute(property, typeof(SqLiteFld));
                if (fldAttribute != null)
                {
                    Console.Write("");
                }
            }
        }
    }
}
