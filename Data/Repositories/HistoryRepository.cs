using HealthAndBeauty.Models.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthAndBeauty.Data.Repositories
{
    public class HistoryRepository
    {
        private ApplicationDbContext context;
        public HistoryRepository(ApplicationDbContext _context)
        {
            context = _context;
        }
        internal int AddHistory(History history)
        {
            context.History.Add(history);
            context.SaveChanges();
            return history.Id;
        }

        internal List<History> GetHistoryByUserId(Guid userId) =>
            context.History.Where(history => history.UserId == userId).ToList();

    }
}
