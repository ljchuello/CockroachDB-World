using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Globalization;
using System.IO;

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

            // Update
            if (check)
            {
                // Exec Update
                _schema.Update();
            }
        }

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

        public void Insert(dynamic obj)
        {
            // ++
            int i = 0;

            // Obtener el tipo del objeto dinámico
            Type objectType = obj.GetType();

            // Obtener el nombre de la tabla a partir del atributo personalizado SqLiteTlb
            var tblNameAttribute = (SqLiteTlb)Attribute.GetCustomAttribute(objectType, typeof(SqLiteTlb));
            string tableName = tblNameAttribute.TblName;

            // Query
            string query = $"INSERT INTO {tblNameAttribute.TblName} (";

            // Iterar sobre las propiedades del objeto dinámico
            i = 0;
            foreach (var property in objectType.GetProperties())
            {
                // Sum
                i++;

                // Obtener el atributo personalizado SqLiteFld de cada propiedad
                var fldAttribute = (SqLiteFld)Attribute.GetCustomAttribute(property, typeof(SqLiteFld));

                // Ultimo
                query = i != objectType.GetProperties().Length
                    ? $"{query}{fldAttribute.FldName}, "
                    : $"{query}{fldAttribute.FldName})";
            }

            // Values
            query = $"{query} VALUES (";

            // Iterar sobre las propiedades del objeto dinámico
            i = 0;
            foreach (var property in objectType.GetProperties())
            {
                // Sum
                i++;

                // Ultimo
                query = i != objectType.GetProperties().Length
                    ? $"{query}'{property.GetValue(obj)}', "
                    : $"{query}'{ToDbRemplazar(property.GetValue(obj))}');";
            }
        }

        static string ToDbRemplazar(DateTime dDateTime)
        {
            return $"{dDateTime:yyyy-MM-dd HH:mm:ss.fff}";
        }

        static string ToDbRemplazar(string cadena)
        {
            return cadena.Replace("'", "''");
        }

        static string ToDbRemplazar(decimal dDecimal)
        {
            CultureInfo cultureInfo = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            cultureInfo.NumberFormat.NumberDecimalSeparator = ".";
            cultureInfo.NumberFormat.NumberGroupSeparator = "";
            cultureInfo.NumberFormat.NumberDecimalDigits = 8;
            if (dDecimal == 0)
            {
                return "0";
            }
            return dDecimal.ToString("n8", cultureInfo);
        }

        static string ToDbRemplazar(long lLong)
        {
            CultureInfo cultureInfo = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            cultureInfo.NumberFormat.NumberDecimalSeparator = ".";
            cultureInfo.NumberFormat.NumberGroupSeparator = "";
            cultureInfo.NumberFormat.NumberDecimalDigits = 0;
            if (lLong == 0)
            {
                return "0";
            }
            return lLong.ToString("n0", cultureInfo);
        }

        static string ToDbRemplazar(bool bBool)
        {
            return bBool ? "1" : "0";
        }

        static string ToDbRemplazar(int iInt)
        {
            return Convert.ToString(iInt);
        }
    }

}
