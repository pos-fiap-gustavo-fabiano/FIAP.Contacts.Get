using FIAP.Contacts.Get.Application.Dto;

namespace FIAP.Contacts.Get.Application.Handlers.Queries.GetContactById;

public class GetContactByIdResponseDto
{
    public required ContactDto Contact { get; set; }
}
