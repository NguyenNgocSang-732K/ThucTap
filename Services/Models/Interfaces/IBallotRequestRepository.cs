using ReviewCore.Models.Interfaces;
using Services.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Models.Interfaces
{
    public interface IBallotRequestRepository : IGenericRepository<BallotRequest>
    {
        Task<IEnumerable<BallotRequest>> GetByIdUser(int id);
        Task<bool> SetStatus(int id, int status);

        Task<int> GetRowCount();
        Task<int> GetRowCountByUserId(int userId);
    }
}
