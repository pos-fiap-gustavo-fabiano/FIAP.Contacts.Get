using FIAP.Contacts.Get.Application.Handlers.Queries.GetAllContacts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FIAP.Contacts.Get
{
    public class GetAllContactsFunction
    {
        private readonly ILogger<GetAllContactsFunction> _logger;
        private readonly IMediator _mediator;

        public GetAllContactsFunction(
            ILogger<GetAllContactsFunction> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [Function(nameof(GetAllContactsFunction))]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "contacts")] HttpRequest req)
        {
            var ct = req.HttpContext.RequestAborted;

            var (pageParam, limitParam, dddParam) = GetParams(req);

            var response = await _mediator.Send(
                new GetAllContactsRequestDto { Page = pageParam, Limit = limitParam, DDD = dddParam }, ct);

            return new OkObjectResult(response.Contacts);
        }

        private static (int, int, int?) GetParams(HttpRequest req)
        {
            req.Query.TryGetValue("page", out var pageValue);
            req.Query.TryGetValue("limit", out var limitValue);
            req.Query.TryGetValue("ddd", out var dddValue);

            return (
                int.TryParse(pageValue, out int page) ? page : 1,
                int.TryParse(limitValue, out int limit) ? limit : 10,
                int.TryParse(dddValue, out int ddd) ? ddd : (int?)null);
        }
    }
}
