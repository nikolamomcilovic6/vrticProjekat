using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public class Vrtic
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Naziv { get; set; }  

        [JsonIgnore]
        public virtual List<Odrzava> VrticAktivnost {get;set;}

        public virtual List<Dete> Deca { get; set; }

        public virtual List<Vaspitac> Vaspitaci {get; set; }

        public virtual Administrator Administrator { get; set; }

    }
}