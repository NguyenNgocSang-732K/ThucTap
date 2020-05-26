using Services.Interfaces;
using Services.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IUnitofWork
    {
        IAccountRepository Account { get; }
        IBallotRequestRepository BallotRequest { get; }
    }
}
