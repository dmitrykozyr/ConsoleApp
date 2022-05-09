namespace JWTToken.Models
{
    public class UserConstants
    {
        public static List<UserModel> Users = new List<UserModel>()
        {
            new UserModel() {
                UserName = "User1",
                EmailAddress = "email1@email.com",
                Password = "pass_1",
                GivenName = "Name1",
                Surname = "Surname1",
                Role = "Administrator"
            },

            new UserModel() {
                UserName = "User2",
                EmailAddress = "email2@email.com",
                Password = "pass_2",
                GivenName = "Name2",
                Surname = "Surname2",
                Role = "Seller"
            }
        };
    }
}
