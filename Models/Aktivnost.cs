using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public class Aktivnost
    {
        [Key]
        public int ID { get; set; } 

        [Required]
        [MaxLength(30)]
        public string Naziv { get; set; }
        
        public virtual List<Ucestvuje> AktivnostDete { get; set; }

        //[JsonIgnore]
        public virtual List<Odrzava> AktivnostVrtic { get; set; }

        public virtual List<Nadgleda> AktivnostVaspitac{ get; set; }

       // public virtual List<Nadgleda> AktivnostVaspitac{ get; set; }


           
    }
}