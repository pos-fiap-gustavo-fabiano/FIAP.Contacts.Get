using FIAP.Contacts.Get.Application.Handlers.Queries.GetContactById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FIAP.Contacts.Get
{
    public class GetContactByIdFunction
    {
        private readonly ILogger<GetAllContactsFunction> _logger;
        private readonly IMediator _mediator;

        public GetContactByIdFunction(
            ILogger<GetAllContactsFunction> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [Function(nameof(GetContactByIdFunction))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, 
            "get", Route = "contacts/{id:guid}")] HttpRequest req,
            Guid id)
        {
            var ct = req.HttpContext.RequestAborted;

            var response = await _mediator.Send(
                new GetContactByIdRequestDto { Id = id }, ct);

            if (response is null)
                return new NotFoundResult();

            return new OkObjectResult(response.Contact);
        }
    }
}
