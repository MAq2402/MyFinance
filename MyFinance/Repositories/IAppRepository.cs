using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Repositories
{
    public interface IAppRepository
    {
        bool Commit();
    }
}
