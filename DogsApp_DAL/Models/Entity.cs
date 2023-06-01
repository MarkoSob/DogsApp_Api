using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DogsApp_DAL.Entities
{
    public abstract class Entity
    {
        [Key]
        [JsonIgnore]
        public Guid Id { get; set; }
    }
}
