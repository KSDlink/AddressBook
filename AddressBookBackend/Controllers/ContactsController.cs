using ContactList.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        readonly ContactsContext db;

        public ContactsController(ContactsContext context)
        {
            db = context;
            if(!db.Contacts.Any())
            {
                db.Contacts.Add(new Contact { FirstName = "Test", LastName = "Testify", Email = "@", Gender = "Male", Birthday = "25.0.164", PhoneNumber = "+79999994466" });
                db.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> Get()
        {
            return await db.Contacts.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> Get(int id)
        {
            Contact contact = await db.Contacts.FirstOrDefaultAsync(x => x.Id == id);
            if (contact == null)
                return NotFound();
            return new ObjectResult(contact);
        }

        [HttpPost]
        public async Task<ActionResult<Contact>> Post(Contact contact)
        {
            if (contact == null)
            {
                return BadRequest();
            }

            db.Contacts.Add(contact);
            await db.SaveChangesAsync();
            return Ok(contact);
        }

        [HttpPut]
        public async Task<ActionResult<Contact>> Put(Contact contact)
        {
            if (contact == null)
            {
                return BadRequest();
            }
            if (!db.Contacts.Any(x => x.Id == contact.Id))
            {
                return NotFound();
            }

            db.Update(contact);
            await db.SaveChangesAsync();
            return Ok(contact);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Contact>> Delete(int id)
        {
            Contact contact = db.Contacts.FirstOrDefault(x => x.Id == id);
            if (contact == null)
            {
                return NotFound();
            }
            db.Contacts.Remove(contact);
            await db.SaveChangesAsync();
            return Ok(contact);
        }
    }
}
