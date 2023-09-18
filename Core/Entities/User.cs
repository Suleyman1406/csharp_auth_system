using System;
namespace AuthSystem.Core.Entities
{
	public class User:BaseEntity
	{
		public string Fullname { get; set; }
		public string Username { get; set; }
		public string Email { get; set; }
        public string Password { get; set; }
        private static int count = 0;

        public User(string fullname, string username, string email, string password)
        {
            Fullname = fullname;
            Username = username;
            Password = password;
            Email = email;
            Id = ++count;
        }

        public void PrintUserInfo()
        {
            Console.WriteLine($"Name: {Fullname}, username: {Username}, email: {Email}");
        }
    }
}

