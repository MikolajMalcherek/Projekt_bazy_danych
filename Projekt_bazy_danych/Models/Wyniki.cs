using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bazy.Models
{
    public class Wyniki
    {
        [Key]
        public int idwyniki { get; set; }

        [Required]
        public int wynik { get; set; }

        [ForeignKey("Zawodnik")]
        public int idzawodnicy { get; set; }

        [ForeignKey("Miejscowosci")]
        public int idmiejscowosci { get; set; }

        public virtual Zawodnik Zawodnik { get; set; }
        public virtual Miejscowosci Miejscowosci { get; set; }
    }
}


