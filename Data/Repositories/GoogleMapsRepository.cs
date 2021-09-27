using HealthAndBeauty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthAndBeauty.Data.Repositories
{
    public class GoogleMapsRepository
    {
        ApplicationDbContext _dbContext;
        public GoogleMapsRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Address> GetCoordinates()
        {
            return _dbContext.Addresses.ToList();
        }

        public List<string> GetAddresses()
        {
            return _dbContext.Addresses.Select(address=>address.AddressName).ToList();
        }

        internal void DeleteAddress(int id)
        {
            _dbContext.Addresses.Remove(_dbContext.Addresses.Where(address => address.Id == id).SingleOrDefault());
            _dbContext.SaveChanges();
        }

        internal void AddAddress(Address address)
        {
            _dbContext.Addresses.Add(address);
            _dbContext.SaveChanges();

        }
    }
}
