using AutoMapper;
using Deployed.Application.Infrastracture.Interfaces;
using Deployed.Data.Services.Database;
using Deployed.Data.Services.Database.Models;
using Deployed.Helper;
using Deployed.Models.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Deployed.Application.Commands
{
    public class CreateCustomerCommand : IRequest<bool>
    {
        public CustomerModel Model { get; set; }
    }
}

namespace Deployed.Application.Commands.Behaviours
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, bool>
    {
        private readonly DatabaseContext _database;
        private readonly IMapper _mapper;
        private readonly ICustomerDependentService _service;

        public CreateCustomerCommandHandler(DatabaseContext database, IMapper mapper, ICustomerDependentService service)
        {
            _database = database;
            _mapper = mapper;
            _service = service;
        }

        public async Task<bool> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var model = request.Model;
            var customer = new Customer
            {
                CustomerType = model.CustomerType,
                IsActive = model.IsActive,
                CreatedDateTime = DateTime.Now
            };

            _database.Customers.Add(customer);

            await _database.SaveChangesAsync();
            await _service.AddCustomerDetails(customer.CustomerId, model.CustomerDetail);
            _service.AddDependents(customer.CustomerId, model.Dependents);

            return customer.CustomerId > 0 ? true : false;
        }
    }
}
