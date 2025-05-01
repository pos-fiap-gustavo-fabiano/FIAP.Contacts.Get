namespace FIAP.Contacts.Get.Domain.ContactAggregate;

public interface IContactRepository
{
    Task<Contact?> GetById(Guid id, CancellationToken ct);
    Task<(IEnumerable<Contact> Items, int Total)> GetAll(int page, int limit, CancellationToken ct, int? ddd = null);
}
