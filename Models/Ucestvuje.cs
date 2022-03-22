using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public class Ucestvuje
    {
        [Key]
        public int ID { get; set; }

       // [Required]
        //public int trenBrojMesta { get; set; }

        public virtual Dete Dete { get; set;}

        [JsonIgnore]
        public virtual Aktivnost Aktivnost { get; set; }

    }
}