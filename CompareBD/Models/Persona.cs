using System.ComponentModel.DataAnnotations.Schema;

namespace PersonCompareDashboard.Models
{
    public class Persona
    {
        [Column("cedula")]
        public string Cedula { get; set; }

        [Column("nombre")]
        public string Nombre { get; set; }

        [Column("apellido")]
        public string Apellido { get; set; }

        [Column("edad")]
        public int Edad { get; set; }
    }

}
