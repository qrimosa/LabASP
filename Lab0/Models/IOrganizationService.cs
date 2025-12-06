namespace Lab0.Models
{
    public interface IOrganizationService
    {
        List<Organization> GetOrganizations();
        Organization? GetOrganizationById(int id);
    }
}