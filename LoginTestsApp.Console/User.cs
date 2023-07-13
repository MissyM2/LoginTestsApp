namespace LoginTestsApp.Console
{
    public class User
    {
        public string Email { get; }
        public string Password { get; }

        public User(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
