using AutoMapper;
using Deployed.Application.Infrastracture.Interfaces;
using Deployed.Data.Services.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deployed.Application.Commands
{
    public class DeleteCustomerCommand : IRequest<bool>
    {
        public int CustomerId { get; set; }
    }
}


namespace Deployed.Application.Commands.Behaviours
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, bool>
    {
        private readonly DatabaseContext _database;
        private readonly IMapper _mapper;
        private readonly ICustomerDependentService _service;

        public DeleteCustomerCommandHandler(DatabaseContext database, IMapper mapper, ICustomerDependentService service)
        {
            _database = database;
            _mapper = mapper;
            _service = service;
        }

        public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            await _service.RemoveDependents(request.CustomerId);
            await _service.RemoveCustomerDetails(request.CustomerId);

            await _database.Customers
                .Where(e => e.CustomerId == request.CustomerId)
                .ExecuteUpdateAsync(e => e.SetProperty(
                    e => e.IsActive,
                    e => false
                    ));

            return true;
        }
    }
}