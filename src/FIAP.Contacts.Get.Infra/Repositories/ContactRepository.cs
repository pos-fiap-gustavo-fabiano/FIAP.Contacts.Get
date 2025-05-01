using FIAP.Contacts.Get.Domain.ContactAggregate;
using FIAP.Contacts.Get.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace FIAP.Contacts.Get.Infra.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly ApplicationDbContext _context;

        public ContactRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Contact?> GetById(Guid id, CancellationToken ct) =>
            await _context.Contacts.Include(c => c.Phone)
                                    .Include(c => c.Address)
                                    .FirstOrDefaultAsync(c => c.Id == id, ct);

        public async Task<(IEnumerable<Contact> Items, int Total)> GetAll(
            int page, 
            int limit, 
            CancellationToken ct,
            int? ddd = null)
        {
            var query = _context.Contacts
                .Include(c => c.Phone)
                .Include(c => c.Address)
                .AsQueryable();
                    
            if (ddd.HasValue)
                query = query.Where(x => x.Phone.DDD == ddd);

            var total = await query.CountAsync(ct);

            query = query
                .Skip((page - 1) * limit)
                .Take(limit);

            return (await query.ToListAsync(ct), total);
        }
    }
}
