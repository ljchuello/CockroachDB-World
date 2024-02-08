using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Reflection;

namespace DataMax
{
    public class Schema
    {
        public string FldName { set; get; }
        public bool FldPK { set; get; }
        public bool FldIndex { set; get; }
        public string TypeSqLite { set; get; }
        public Type TypeDotNet { set; get; }
        public string Default { set; get; }

        string _dbFile;

        public Schema(string dbFile)
        {
            _dbFile = dbFile;
        }

        private Schema()
        {

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

                // Campos
                List<Schema> listField = new List<Schema>();
                foreach (var property in typeof(T).GetProperties())
                {
                    SqLiteFld fldAttribute = (SqLiteFld)Attribute.GetCustomAttribute(property, typeof(SqLiteFld));
                    if (fldAttribute != null)
                    {
                        Schema schema = new Schema();
                        schema.FldName = fldAttribute.FldName;
                        schema.FldPK = fldAttribute.FldPK;
                        schema.FldIndex = fldAttribute.FldIndex;
                        Type dataType = property.PropertyType;

                        switch (Type.GetTypeCode(dataType))
                        {
                            case TypeCode.String:
                                schema.TypeSqLite = "text";
                                schema.Default = "''";
                                break;

                            case TypeCode.DateTime:
                                schema.TypeSqLite = "text";
                                schema.Default = "-2208971024";
                                break;

                            case TypeCode.Boolean:
                            case TypeCode.Int16:
                            case TypeCode.Int32:
                            case TypeCode.Int64:
                                schema.TypeSqLite = "integer";
                                schema.Default = "0";
                                break;

                            case TypeCode.Decimal:
                            case TypeCode.Double:
                                schema.TypeSqLite = "real";
                                schema.Default = "0";
                                break;
                        }

                        // add
                        listField.Add(schema);
                    }
                }

                // Create
                TblCreate(tableName, listField);
                TblAddColumn(tableName, listField);
            }
        }

        private void TblCreate(string tblName, List<Schema> listField)
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
            string query = $"CREATE TABLE {tblName} (";

            // For
            int i = 0;
            foreach (Schema row in listField)
            {
                // +1
                i++;

                query = i != listField.Count
                    ? $"{query}{row.FldName} {row.TypeSqLite} {(row.FldPK ? "primary key" : "")} default {row.Default}, "
                    : $"{query}{row.FldName} {row.TypeSqLite} {(row.FldPK ? "primary key" : "")} default {row.Default});";
            }

            // Create
            using (SQLiteConnection db = new SQLiteConnection(_dbFile))
            {
                db.Open();
                SQLiteCommand sqlCommand = new SQLiteCommand();
                sqlCommand.Connection = db;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = query;
                sqlCommand.ExecuteNonQuery();
            }
        }

        private void TblAddColumn(string tblName, List<Schema> listField)
        {
            // Get table info
            DataTable dataTable = new DataTable();
            using (SQLiteConnection db = new SQLiteConnection(_dbFile))
            {
                db.Open();
                SQLiteCommand sqlCommand = new SQLiteCommand();
                sqlCommand.Connection = db;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = $"pragma table_info({tblName});";
                using (SQLiteDataReader reader = sqlCommand.ExecuteReader())
                {
                    dataTable.Load(reader);
                }
            }

            // Check
            foreach (Schema schema in listField)
            {
                // Flag
                bool existe = false;

                // For dt
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    string dtName = $"{dataRow["name"]}";
                    if (dtName == schema.FldName)
                    {
                        existe = true;
                        break;
                    }
                }

                // No existe
                if (existe == false)
                {
                    // Creamos
                    using (SQLiteConnection db = new SQLiteConnection(_dbFile))
                    {
                        db.Open();
                        SQLiteCommand sqlCommand = new SQLiteCommand();
                        sqlCommand.Connection = db;
                        sqlCommand.CommandType = CommandType.Text;
                        sqlCommand.CommandText = $"ALTER TABLE {tblName} ADD {schema.FldName} {schema.TypeSqLite} default {schema.Default};";
                        sqlCommand.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
