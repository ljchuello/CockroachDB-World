﻿using System.Data;
using System.Data.SQLite;
using System.Threading.Tasks;

namespace DataMax.Table
{
    [SqLiteTlb("Node")]
    public class AdmNode
    {
        private readonly string _dbFile;

        [SqLiteFld("Id", true, false)]
        public string Id { set; get; } = string.Empty;

        [SqLiteFld("Descripcion", true, false)]
        public string Descripcion { set; get; } = string.Empty;

        [SqLiteFld("Edad", true, false)]
        public long Edad { set; get; } = 0;

        [SqLiteFld("Saldo", true, false)]
        public decimal Saldo { set; get; } = 0;

        public AdmNode(string dbFile)
        {
            _dbFile = dbFile;
        }
    }
}