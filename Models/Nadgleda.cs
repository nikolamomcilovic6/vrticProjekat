using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Nadgleda
    {
        [Key]
        public int ID { get; set; }

        public virtual Vaspitac Vaspitac {get; set; }

        public virtual Aktivnost Aktivnost{ get; set; }

    }
}