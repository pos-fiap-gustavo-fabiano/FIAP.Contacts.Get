namespace FIAP.Contacts.Get.Application.Handlers.Queries.GetContactById;

public class GetContactByIdRequestDto : IRequest<GetContactByIdResponseDto>
{
    public Guid Id { get; set; }
}
