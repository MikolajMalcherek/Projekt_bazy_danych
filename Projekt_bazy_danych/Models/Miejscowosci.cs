using System.ComponentModel.DataAnnotations;

namespace bazy.Models
{
    public class Miejscowosci
    {
        [Key]
        public int idmiejscowosci { get; set; }
        [Required]
        public string nazwa_miejscowosci { get; set; }
        [Required]
        public string kraj_miejscowosci { get; set; }

        public virtual ICollection<Wyniki> Wyniki { get; set; }

    }
}
