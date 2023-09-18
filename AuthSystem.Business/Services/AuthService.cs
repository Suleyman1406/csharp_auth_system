using System;
using System.Text.RegularExpressions;
using AuthSystem.Core.Entities;
using AuthSystem.DataAccess.Context;

namespace AuthSystem.Business.Services
{
	public class AuthService
	{

		public const string USERNAME_REGEX = @"^(?=.{8,20}$)(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$";
        public const string EMAIL_REGEX = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
        public const string PASSWORD_REGEX = @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$";


        public User? Login()
		{
            Console.Write("Email veya username'i girin: ");
        emailOrUsernameLabel:
            string? emailOrUsername = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(emailOrUsername))
            {
                Console.Write("Email veya Username'i dogru girin: ");
                goto emailOrUsernameLabel;
            }
            Console.Write("Password'u girin: ");
        passwordLabel:
            string? password = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(password))
            {
                Console.Write("Password'u dogru girin: ");
                goto passwordLabel;
            }
            User? user = FindUser(emailOrUsername);

            if((user is null) || user.Password != password )
            {
                Console.WriteLine("Email veya password yanlisdir!");
                return null;
            }

            Console.WriteLine("Giris edildi!");
            return user;
        }

		public void Register(UserService userService)
		{
			Console.Write("Full name'i girin: ");
		fullnameInputLabel:
			string? fullName = Console.ReadLine();
			if (string.IsNullOrWhiteSpace(fullName))
			{
				Console.Write("Full name'i dogru girin: ");
				goto fullnameInputLabel;
            }
            Console.Write("Username'i girin: ");
        usernameInputLabel:
            string? username = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(username) || !ControlStrWithRegex(username, USERNAME_REGEX))
            {
				Console.WriteLine("en az 8 karakterli olmalidir.");
                Console.Write("Username'i dogru girin: ");
                goto usernameInputLabel;
            }
			if (IsAlreadyExist(username))
			{
				Console.WriteLine("Bu username daha once istifade edilib.");
				Console.Write("Username'i dogru girin: ");
                goto usernameInputLabel;
            }
            Console.Write("Email'i girin: ");
        emailInputLabel:
            string? email = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(email) || !ControlStrWithRegex(email, EMAIL_REGEX))
            {
                Console.WriteLine("Email strukturuna uygun edin.");
                Console.Write("Email'i dogru girin: ");
                goto emailInputLabel;
            }
            if (IsAlreadyExist(email))
            {
                Console.WriteLine("Bu email daha once istifade edilib.");
                Console.Write("Email'i dogru girin: ");
                goto emailInputLabel;
            }
            Console.Write("Password'u girin: ");
        passwordInputLabel:
            string? password = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(password) || !ControlStrWithRegex(password, PASSWORD_REGEX))
            {
                Console.WriteLine("Password strukturuna uygun edin.");
                Console.Write("Password'u dogru girin: ");
                goto passwordInputLabel;
            }

            userService.AddUser(fullName, username, email, password);
        }

        public bool IsAlreadyExist(string value)
		{
			foreach(User user in AppDbContext.users)
			{
				if(user.Email == value || user.Username == value)
				{
					return true;
				}
			}
			return false;
		}

        public bool ControlStrWithRegex(string value, string pattern)
		{
			return Regex.IsMatch(value,pattern);
		}

        private User? FindUser(string emailOrUsername)
        {
            User? foundUser = null;

            foreach(User user in AppDbContext.users)
            {
                if(user.Email==emailOrUsername || user.Username == emailOrUsername)
                {
                    foundUser = user;
                    break;
                }
            }

            return foundUser;
        }
	}
}

