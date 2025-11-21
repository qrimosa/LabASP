namespace Lab0.Models;

public interface IContactService
{
    List<Contact> GetContacts();
    void AddContact(Contact contact);
    bool UpdateContact(Contact contact);
    bool DeleteContactById(int id);
    Contact? GetContactById(int id);
}