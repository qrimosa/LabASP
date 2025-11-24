namespace Lab0.Models;

public class Organization
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }

    public List<Contact> Contacts { get; set; } = new();
}