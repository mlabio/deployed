using Deployed.Application.Infrastracture.Interfaces;
using Deployed.Data.Services.Database;
using Deployed.Data.Services.Database.Models;
using Deployed.Helper;
using Deployed.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deployed.Application.Infrastracture.Services
{
    public class CustomerDependentService : ICustomerDependentService
    {
        private readonly DatabaseContext _database;

        public CustomerDependentService(DatabaseContext database)
        {
            _database = database;
        }

        public async Task AddCustomerDetails(int customerId, CustomerDetailModel model)
        {
            var dictionary = DeployedHelper.ConvertModelToDictionary(model);

            _database.CustomerDetails.AddRange(
                dictionary.Select(e => new CustomerDetail
                {
                    MetaKey = e.Key,
                    MetaValue = e.Value,
                    CustomerId = customerId,
                })
                .ToList()
            );

            await _database.SaveChangesAsync();
        }

        public void AddDependents(int customerId, List<DependentModel> model)
        {
            model.ForEach(m =>
            {
                var dependent = new Dependent
                {
                    CustomerId = customerId,
                    IsActive = m.IsActive,
                    CreatedDateTime = DateTime.Now
                };

                _database.Dependents.Add(dependent);
                _database.SaveChanges();

                AddDependentDetails(dependent.DependentId, m.DependentDetail);
            });
        }

        public void AddDependentDetails(int dependentId, DependentDetailModel model)
        {
            var dictionary = DeployedHelper.ConvertModelToDictionary(model);

            _database.DependentDetails.AddRange(
                dictionary.Select(e => new DependentDetail
                {
                    MetaKey = e.Key,
                    MetaValue = e.Value,
                    DependentId = dependentId
                })
            );

            _database.SaveChanges();
        }

        public async Task RemoveCustomerDetails(int customerId)
        {
            await _database.CustomerDetails
                .Where(e => e.CustomerId == customerId)
                .ExecuteDeleteAsync();
        }

        public async Task RemoveDependents(int customerId)
        {
            await _database.Dependents
                .Where(e => e.CustomerId == customerId)
                .ExecuteUpdateAsync(e => e.SetProperty(
                        e => e.IsActive,
                        e => false
                    ));
        }
    }
}
