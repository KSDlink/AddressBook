using Microsoft.EntityFrameworkCore;

namespace ContactList.Models
{
    public class ContactsContext : DbContext
    {
        public DbSet<Contact>? Contacts { get; set; } //cs8618

        public ContactsContext(DbContextOptions<ContactsContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
