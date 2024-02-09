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

        [SqLiteFld("Saldo", false, true)]
        public decimal Saldo { set; get; } = 0;

        public AdmNode(string dbFile)
        {
            _dbFile = dbFile;
        }

        public AdmNode()
        {

        }
    }
}
