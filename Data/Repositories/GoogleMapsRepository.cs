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

        public List<MapsCoordinates> GetCoordinates()
        {
            return _dbContext.MapsCoordinates.ToList();
        }
    }
}
