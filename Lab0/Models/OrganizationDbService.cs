using Microsoft.EntityFrameworkCore;

namespace Lab0.Models
{
    public class OrganizationDbService : IOrganizationService
    {
        private readonly AppDbContext _context;

        public OrganizationDbService(AppDbContext context)
        {
            _context = context;
        }

        public List<Organization> GetOrganizations()
        {
            return _context.Organization
                .Include(o => o.Contacts)
                .ToList();
        }

        public Organization? GetOrganizationById(int id)
        {
            return _context.Organization
                .Include(o => o.Contacts)
                .FirstOrDefault(o => o.Id == id);
        }
    }
}