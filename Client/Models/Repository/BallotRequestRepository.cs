using Client.Models.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Supports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Models.Repository
{
    public class BallotRequestRepository : IBallotRequestRepository
    {
        private string BASE_URL = "https://localhost:44318/api/ballot/";

        public int Create(BallotRequest ballotRequest, string token)
        {
            string result = CallAPI.MethodPOST(BASE_URL + "create", ballotRequest, token);
            return Convert.ToInt32(result);
        }

        public BallotRequest GetById(int id, string token)
        {
            string result = CallAPI.MethodGET(BASE_URL + "getbyid/" + id, token);
            return JsonConvert.DeserializeObject<BallotRequest>(result);
        }

        public List<BallotRequest> GetByIdUser(int userid, string token)
        {
            string result = CallAPI.MethodGET(BASE_URL + "getbyuserid/" + userid, token);
            return JsonConvert.DeserializeObject<List<BallotRequest>>(result);
        }

        public Dictionary<int, List<BallotRequest>> GetData(int page, int rows, string token)
        {
            string result = CallAPI.MethodGET($"{BASE_URL}getdata/{page}/{rows}", token);
            List<BallotRequest> data = JsonConvert.DeserializeObject<List<BallotRequest>>(JObject.Parse(result)["data"].ToString());
            int count = (int)JObject.Parse(result)["count"];
            return new Dictionary<int, List<BallotRequest>> { { count, data } };
        }

        public int Modify(BallotRequest ballotRequest, string token)
        {
            string result = CallAPI.MethodPUT(BASE_URL + "modify", ballotRequest, token);
            return Convert.ToInt32(result);
        }

        public bool Remove(int id, string token)
        {
            return CallAPI.MethodDELETE(BASE_URL + "remove/" + id, token);
        }

        public bool SetStatus(BallotRequest ballotRequest, string token)
        {
            string result = CallAPI.MethodPUT(BASE_URL + "setstatus", ballotRequest, token);
            return Convert.ToBoolean(result);
        }
    }
}
