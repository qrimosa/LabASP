using Microsoft.EntityFrameworkCore;

namespace Lab0.Models;

public class ContactDbService(AppDbContext context, ILogger<ContactDbService> logger) : IContactService
{
    public List<Contact> GetContacts()
    {
        return context.Contacts
            .Include(c => c.Organization)
            .ToList();
    }

    public void AddContact(Contact contact)
    {
        context.Contacts.Add(contact);
        context.SaveChanges();
    }

    public bool UpdateContact(Contact contact)
    {
        try
        {
            context.Contacts.Update(contact);
            context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException e)
        {
            logger.LogError(e.Message);
            return false;
        }

        return true;
    }

    public bool DeleteContactById(int id)
    {
        var deleted = context.Contacts.Find(id);
        if (deleted == null)
        {
            return false;
        }

        context.Contacts.Remove(deleted);
        context.SaveChanges();
        return true;
    }

    public Contact? GetContactById(int id)
    {
        return context.Contacts
            .Include(c => c.Organization)
            .FirstOrDefault(c => c.Id == id);
    }
}