namespace Lab0.Models;

public interface IContactInterface
{
    List<Contact> GetContacts();
    Contact? GetContactById(int id);
    void CreateContact(Contact contact);
    bool UpdateContact(Contact contact);
    bool DeleteContact(int id);
}