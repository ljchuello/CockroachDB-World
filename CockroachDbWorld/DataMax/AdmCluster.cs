using SQLite;

namespace CockroachDbWorld.DataMax
{
    [Table("AdmCluster")]
    public class AdmCluster
    {
        [PrimaryKey]
        public string Id { set; get; } = string.Empty;

        [Indexed]
        public string Descripcion { set; get; } = string.Empty;

        public string ApiToken { set; get; } = string.Empty;

        public string Region { set; get; } = string.Empty;

        public string NodeSize { set; get; } = string.Empty;

        public long SrvCurrent { set; get; } = 0;

        public long ElbCurrent { set; get; } = 0;

        public DateTime AddAt { set; get; } = new DateTime(1900, 01, 01);
        
        public DateTime EditAt { set; get; } = new DateTime(1900, 01, 01);

        [Indexed]
        public bool Activo { set; get; } = false;
    }
}
