using MyFinance.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Repositories
{
    public class AppRepository : IAppRepository
    {
        private AppDbContext _context;

        public AppRepository(AppDbContext context)
        {
            _context = context;
        }
        public bool Commit()
        {
            if(_context.SaveChanges()>0)
            {
                return true;
            }
            return false;
        }
    }
}
