using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public class Vaspitac
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(30)]
        public string Ime {get;set;}
        
        [Required]
        [MaxLength(50)]
        public string Prezime { get; set; }
        
        [Required]
        [MaxLength(13)]
        [MinLength(13)]
        [RegularExpression("\\d+$")]
        public string JMBG { get; set; }

        [JsonIgnore]
        public virtual Vrtic Vrtic { get; set; }

        public virtual List<Nadgleda> VaspitacAktivnost {get; set;}

       // public virtual List<Nadgleda> VaspitacAktivnost {get; set; }


    }
}