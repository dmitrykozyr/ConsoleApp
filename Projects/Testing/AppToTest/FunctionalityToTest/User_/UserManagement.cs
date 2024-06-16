namespace xUnit_.FunctionalityToTest.User_
{
    public class UserManagement
    {
        private readonly List<User> _users = new();
        private int idCounter = 1;
        public IEnumerable<User> AllUsers => _users;

        public List<User> GetUsers()
        {
            var users = _users.ToList();
            return users;
        }

        public void AddUser(User user)
        {
            _users.Add(user with { Id = idCounter++ });
        }

        public void UpdatePhone(User user)
        {
            var dbUser = _users.First(z => z.Id == user.Id);
            dbUser.Phone = user.Phone;
        }

        public void VerifyEmail(int userId)
        {
            var dbUser = _users.First(z => z.Id == userId);
            dbUser.verifiedEmail = true;
        }
    }
}
