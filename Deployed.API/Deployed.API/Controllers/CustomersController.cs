using Deployed.Application.Commands;
using Deployed.Models.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Deployed.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> ListCustomers() =>
             Ok(await _mediator.Send(new ListCustomerQuery { }));

        [HttpPost]
        public async Task<ActionResult> CreateCustomer([FromBody] CustomerModel model) =>
             Ok(await _mediator.Send(new CreateCustomerCommand { Model = model }));

        [HttpPut("{customerId}")]
        public async Task<ActionResult> UpdateCustomer(int customerId, [FromBody] CustomerModel model) =>
            Ok(await _mediator.Send(new UpdateCustomerCommand { CustomerId = customerId, Model = model }));

        [HttpDelete("{customerId}")]
        public async Task<ActionResult> DeleteCustomer(int customerId) =>
            Ok(await _mediator.Send(new DeleteCustomerCommand { CustomerId = customerId }));
    }
}
