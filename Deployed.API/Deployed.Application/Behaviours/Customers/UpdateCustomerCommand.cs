using AutoMapper;
using Deployed.Application.Infrastracture.Interfaces;
using Deployed.Data.Services.Database;
using Deployed.Models.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deployed.Application.Commands
{
    public class UpdateCustomerCommand : IRequest<bool>
    {
        public int CustomerId { get; set; }
        public CustomerModel Model { get; set; }
    }
}

namespace Deployed.Application.Commands.Behaviours
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, bool>
    {
        private readonly DatabaseContext _database;
        private readonly IMapper _mapper;
        private readonly ICustomerDependentService _service;

        public UpdateCustomerCommandHandler(DatabaseContext database, IMapper mapper, ICustomerDependentService service)
        {
            _database = database;
            _mapper = mapper;
             _service = service;
        }

        public async Task<bool> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var model = request.Model;
            var customer = await _database.Customers.FirstOrDefaultAsync(e => e.CustomerId == request.CustomerId);
            
            _mapper.Map(model, customer);
            _database.Customers.Update(customer);
            
            await _database.SaveChangesAsync();
            await _service.RemoveDependents(customer.CustomerId);
            await _service.RemoveCustomerDetails(customer.CustomerId);
            await _service.AddCustomerDetails(customer.CustomerId, model.CustomerDetail);
            _service.AddDependents(customer.CustomerId, model.Dependents);

            return true;
        }
    }
}
