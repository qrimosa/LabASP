namespace Lab0.Models;

public class IContactServiceInMemory : IContactInterface
{
    private static Dictionary<int, Contact> _contacts = new()
    {
        {1, new Contact()
        {
            Id = 1,
            Name = "John Doe",
            Email = "johndoe@gmail.com",
            BirthDate = DateOnly.FromDateTime(new DateTime(1992, 05, 01))
        }},
        {2, new Contact()
        {
            Id = 2,
            Name = "Sigma Sigmovich",
            Email = "sigma@gmail.com",
            BirthDate = DateOnly.FromDateTime(new DateTime(1967, 06, 07))
        }},
    };

    private int _i = 2;

    public List<Contact> GetContacts()
    {
        return _contacts.Values.ToList();
    }

    public Contact? GetContactById(int id)
    {
        if (_contacts.ContainsKey(id))
        {
            return _contacts[id];
        }
        return null;
    }

    public void CreateContact(Contact contact)
    {
        contact.Id = ++_i;
        _contacts.Add(contact.Id, contact);
    }

    public bool UpdateContact(Contact contact)
    {
        if (_contacts.ContainsKey(contact.Id))
        {
            _contacts[contact.Id] = contact;
            return true;
        }

        return false;
    }

    

    public bool DeleteContact(int id)
    {
        if (_contacts.ContainsKey(id))
        {
            _contacts.Remove(id);
            return true;
        }

        return false;
    }
}