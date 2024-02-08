using System;
using System.Data;
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

        [SqLiteFld("Descripcion", false, true)]
        public string Descripcion { set; get; } = string.Empty;

        [SqLiteFld("Edad", false, false)]
        public long Edad { set; get; } = 0;

        [SqLiteFld("Saldo", false, false)]
        public decimal Saldo { set; get; } = 0;

        [SqLiteFld("AddAt", false, false)]
        public DateTime AddAt { set; get; } = new DateTime(1900, 01, 01);

        public AdmNode(string dbFile)
        {
            _dbFile = dbFile;
        }
    }
}
