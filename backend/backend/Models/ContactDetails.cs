using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace backend.Models
{
    public enum ContactCategory
    {
        Business,
        Private,
        Other
    }

    public enum ContactSubcategory
    {
        Boss,
        Client,
        Colleague
    }

    public class ContactDetails
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Contact")]
        public int ContactId { get; set; }

        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]

        public ContactCategory Category { get; set; }

        public string? Subcategory { get; set; }

        [Phone]
        public string Phone { get; set; }

        [DataType(DataType.Date)]
        public string BirthDay { get; set; }

        [JsonIgnore]
        public Contact? Contact { get; set; }
    }
}
