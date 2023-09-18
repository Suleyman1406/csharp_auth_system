using System;
using AuthSystem.Core.Entities;
using AuthSystem.DataAccess.Context;

namespace AuthSystem.Business.Services
{
	public class UserService
	{

		public void AddUser(string fullName, string username, string email, string password)
		{
            User user = new User(fullName, username, email, password);
			Array.Resize(ref AppDbContext.users, AppDbContext.users.Length + 1);
			AppDbContext.users[AppDbContext.users.Length - 1] = user;
        }

		public void UpdateUserInfo(User user, AuthService authService)
		{
			UpdateUserInfoLabel:
			Console.WriteLine("Guncellemek istediyinizi secin");
			Console.WriteLine("1. Fullname");
			Console.WriteLine("2. Email");
			Console.WriteLine("3. Username");
			Console.WriteLine("4. Password");
			Console.WriteLine("5. Cixis");
            int choice;
			int.TryParse(Console.ReadLine(), out choice);


			switch (choice)
			{
				case 1:
					Console.Write("Yeni Fullname'i girin: ");
				fullnameUpdateLabel:
					string? fullname = Console.ReadLine();
					if (string.IsNullOrEmpty(fullname))
					{
						Console.Write("Yeni fulname'i dogru girin: ");
						goto fullnameUpdateLabel;
                    }
					user.Fullname = fullname;
					Console.WriteLine("Fullname guncellendi.");
                    goto UpdateUserInfoLabel;
                case 2:
                    Console.Write("Yeni Email'i girin: ");
                emailUpdateLabel:
                    string? email = Console.ReadLine();
                    if (string.IsNullOrEmpty(email) || !authService.ControlStrWithRegex(email, AuthService.EMAIL_REGEX))
                    {
                        Console.Write("Yeni email'i dogru girin: ");
                        goto emailUpdateLabel;
                    }

					if (authService.IsAlreadyExist(email))
					{
                        Console.WriteLine("Bu username daha once istifade edilib.");
                        Console.Write("Username'i dogru girin: ");
                        goto emailUpdateLabel;
                    }
					user.Email = email;
                    goto UpdateUserInfoLabel;
                case 3:

                    goto UpdateUserInfoLabel;
                case 4:

                    goto UpdateUserInfoLabel;
                case 5:
                    break;
				default:
					goto UpdateUserInfoLabel;
            }
		}
	}
}

