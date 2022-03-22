using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public class Odrzava
    {
        [Key]
        public int ID { get; set; }


       // [JsonIgnore]
        public virtual Vrtic Vrtic {get; set;}

        public virtual Aktivnost Aktivnost{get; set;}
        
    }
}