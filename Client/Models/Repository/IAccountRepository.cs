using Client.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Models.Repository
{
    public interface IAccountRepository
    {
        string Login(Account account);

    }
}
