namespace Lab0.Models;

public class ContactMemoryService: IContactService
{
    private Dictionary<int, Contact> _contacts = new(){
        { 1, new Contact() {Id = 1, Email = "ewa@wsei.edu.pl", Name = "ewa"} },
        { 2, new Contact() {Id = 2, Email = "adam@wsei.edu.pl", Name = "adaś"} }
    };

    private int i = 2;
    
    public List<Contact> GetContacts()
    {
        return _contacts.Values.ToList();
    }

    public void AddContact(Contact contact)
    {
        contact.Id = i++;
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

    public bool DeleteContactById(int id)
    {
        if (_contacts.ContainsKey(id))
        {
            _contacts.Remove(id);
            return true;
        }
        return false;
    }

    public Contact? GetContactById(int id)
    {
        if (_contacts.ContainsKey(id))
        {
            return _contacts[id];
        }
        return null;
    }
}