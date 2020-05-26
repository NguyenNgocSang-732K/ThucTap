using Client.Models.Entities;
using Supports;

namespace Client.Models.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private string BASE_URL = "https://localhost:44318/api/account/";
        public string Login(Account account)
        {
            string tokenString = CallAPI.MethodPOST(BASE_URL + "login", account,null);
            return tokenString;
        }
    }
}
