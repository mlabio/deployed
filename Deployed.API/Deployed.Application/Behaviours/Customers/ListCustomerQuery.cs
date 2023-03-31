using AutoMapper;
using Deployed.Data.Services.Database;
using Deployed.Data.Services.Database.Models;
using Deployed.Helper;
using Deployed.Models.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deployed.Application.Commands
{
    public class ListCustomerQuery : IRequest<List<CustomerResponseModel>>
    {
    }
}

namespace Deployed.Application.Commands.Behaviours
{
    public class ListCustomerQueryHandler : IRequestHandler<ListCustomerQuery, List<CustomerResponseModel>>
    {
        private readonly DatabaseContext _database;
        private readonly IMapper _mapper;

        public ListCustomerQueryHandler(DatabaseContext database, IMapper mapper)
        {
            _database = database;
            _mapper = mapper;
        }

        public async Task<List<CustomerResponseModel>> Handle(ListCustomerQuery request, CancellationToken cancellationToken)
        {
            var customers = await _database.Customers
                .Include(e => e.Dependents)
                    .ThenInclude(e => e.DependentDetails)
                .Include(e => e.CustomerDetails)
                .Where(e => e.IsActive)
                .ToListAsync();

            var response = new List<CustomerResponseModel>();

            customers.ForEach(c =>
            {
                var customerDetails = c.CustomerDetails.Any() ? DeployedHelper.ConvertDictionaryTo<CustomerResponseModel>(c.CustomerDetails) : new CustomerResponseModel();
                var customer = _mapper.Map(c, customerDetails);

                customer.Dependents = new List<DependentResponseModel>();
                c.Dependents.ToList().ForEach(d =>
                {
                    if(d.IsActive)
                    {
                        var dependentDetails = d.DependentDetails.Any() ? DeployedHelper.ConvertDictionaryTo<DependentResponseModel>(d.DependentDetails) : new DependentResponseModel();
                        var dependent = _mapper.Map(d, dependentDetails);

                        customer.Dependents.Add(dependent);
                    }
                });
                response.Add(customer);
            });

            return response;
        }

    }
}


