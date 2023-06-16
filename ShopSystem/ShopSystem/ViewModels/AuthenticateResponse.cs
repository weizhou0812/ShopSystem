using ShopSystem.Models;

namespace ShopSystem.ViewModels
{
    public class AuthenticateResponse
    {
        private List<Users> user;
        private object token;

        public AuthenticateResponse(Users user,string token) {
            id = user.UNumber;
            Token = token;
            uname = user.UName;
            password = user.UPassword;
        }

        public int id { get; set; }
        public string Token { get; set; }
        public string uname { get; set; }
        public string password { get; set; }
    }
}
