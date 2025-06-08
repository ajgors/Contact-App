using backend.Models;

namespace backend.dto
{
    public class ContactPatchDto
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }

        public ContactCategory? Category { get; set; }

        public string? Subcategory { get; set; }

        public string? Phone { get; set; }

        public string? BirthDay { get; set; }
    }
}
