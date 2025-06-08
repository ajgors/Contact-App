using Microsoft.AspNetCore.Mvc;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using backend.dto;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ContactController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "contact")]
        public async Task<ActionResult<IEnumerable<Contact>>> GetContacts()
        {
            var contacts = await _context.Contacts.ToListAsync();
            return Ok(contacts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> GetContact(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null) return NotFound();
            return contact;
        }

        [Authorize]
        [HttpPost (Name = "contact")]
        public async Task<ActionResult<Contact>> PostContact(Contact contact)
        {
            if (!ModelState.IsValid)
                return Ok(false);

            //check if contact with the same email already exists
            if (await _context.Contacts.AnyAsync(c => c.Email == contact.Email))
            {
                return Ok(false);
            }

            //check if contact with the same phone already exists
            if (await _context.ContactDetails.AnyAsync(cd => cd.Phone == contact.ContactDetails.Phone))
            {
                return Ok(false);
            }

            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetContacts), true);
        }

        [Authorize]
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchContact(int id, ContactPatchDto patchDto)
        {
            if (patchDto == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var Contact = await _context.Contacts
                .Include(c => c.ContactDetails)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (Contact == null)
            {
                return NotFound();
            }

            if(patchDto.Name != null) Contact.Name = patchDto.Name;
            if(patchDto.Surname != null) Contact.Surname = patchDto.Surname;
            if(patchDto.Email != null) Contact.Email = patchDto.Email;
            if(patchDto.Category != null) Contact.ContactDetails.Category = patchDto.Category.Value;
            if(patchDto.Subcategory != null) Contact.ContactDetails.Subcategory = patchDto.Subcategory;
            if(patchDto.Phone != null) Contact.ContactDetails.Phone = patchDto.Phone;
            if(patchDto.BirthDay != null) Contact.ContactDetails.BirthDay = patchDto.BirthDay;
           
            await _context.SaveChangesAsync();

            return Ok(true);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var Contact = await _context.Contacts.FindAsync(id);
            if (Contact == null)
            {
                return NotFound();
            }

           _context.Contacts.Remove(Contact);
            await _context.SaveChangesAsync();

            return Ok(true);
        }

        [HttpGet("{id}/details")]
        public async Task<ActionResult<ContactDetails>> GetContactDetails(int id)
        {
            var details = await _context.ContactDetails
                .FirstOrDefaultAsync(cd => cd.ContactId == id);

            if (details == null) return NotFound();

            return details;
        }
    }
}
