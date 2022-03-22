using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public class Administrator
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Ime { get; set; }  
        [Required]
        [StringLength(13)]
        public string JMBG { get; set; }

        [Required]
        [MaxLength(8)]
        [MinLength(5)]
        public string Sifra {get; set; }  

        [JsonIgnore]
       public virtual List<Vrtic> Vrtici { get; set; }

    }
}