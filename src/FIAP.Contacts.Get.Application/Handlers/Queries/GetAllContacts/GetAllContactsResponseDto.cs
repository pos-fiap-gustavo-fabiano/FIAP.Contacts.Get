using FIAP.Contacts.Get.Application.Dto;

namespace FIAP.Contacts.Get.Application.Handlers.Queries.GetAllContacts;

public class GetAllContactsResponseDto
{
    public required PaginationDto<ContactWithIdDto> Contacts { get; set; }
}
