using DataMax;

namespace CockroachDbWorld.Data
{
    [SqLiteTlb("Node")]
    public class AdmNode
    {
        [SqLiteFld("Id", true, false)]
        public string Id { set; get; } = string.Empty;

        [SqLiteFld("Descripcion", false, true)]
        public string Descripcion { set; get; } = string.Empty;

        [SqLiteFld("Edad", false, false)]
        public long Edad { set; get; } = 0;

        [SqLiteFld("Saldo", false, true)]
        public decimal Saldo { set; get; } = 0;

        [SqLiteFld("AddAt", false, true)]
        public DateTime AddAt { set; get; } = new DateTime(1900, 01, 01);
    }
}
