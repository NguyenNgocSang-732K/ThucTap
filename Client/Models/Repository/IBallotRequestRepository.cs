using Client.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Models.Repository
{
    public interface IBallotRequestRepository
    {
        BallotRequest GetById(int id, string token);

        Dictionary<int, List<BallotRequest>> GetData(int page, int rows, string token);

        int Create(BallotRequest ballotRequest, string token);

        int Modify(BallotRequest ballotRequest, string token);

        bool Remove(int id, string token);

        List<BallotRequest> GetByIdUser(int userid, string token);

        bool SetStatus(BallotRequest ballotRequest, string token);
    }
}
