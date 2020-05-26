using ReviewCore.Models.Interfaces;
using Services.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IAccountRepository : IGenericRepository<Account>
    {
        Task<Account> Login(Account account);
    }
}
