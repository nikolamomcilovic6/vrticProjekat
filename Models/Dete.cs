using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public class Dete
    {
        [Key]
        public int ID { get; set; }


        [Required]
        [MaxLength(30)]
        public string  Ime { get; set; }
        [Required]
        [MaxLength(50)]
        public string Prezime { get; set; }

        [Required]
        [StringLength(13)]
        [RegularExpression("\\d+$")]
        public string JMBG {get; set;}

        [Required]
        [Phone]
        [RegularExpression("\\d+$")]
        public string brojRoditelja { get; set; }


        public virtual List<Ucestvuje> DeteAktivnost{get; set;}
        [JsonIgnore]
        public virtual Vrtic Vrtic {get; set; }

    }
}