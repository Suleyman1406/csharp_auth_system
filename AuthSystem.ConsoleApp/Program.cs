

using AuthSystem.Business.Services;
using AuthSystem.Core.Entities;
using AuthSystem.Core.Enums;

AuthService authService = new AuthService();
UserService userService = new UserService();
User? loggedUser = null;
bool isContinue = true;

while (isContinue)
{
    if(loggedUser is not null)
    {
        Console.WriteLine("Xos geldiniz " + loggedUser.Fullname);
        loggedOperationsLabel:
        Console.WriteLine("1. Hesab bilgilerini goster.");
        Console.WriteLine("2. Bilgileri guncelle.");
        Console.WriteLine("3. Cixis.");

        int loggedOperationsChoice;
        int.TryParse(Console.ReadLine(), out loggedOperationsChoice);

        switch (loggedOperationsChoice)
        {
            case (int)ConsoleLoggedOperations.PRINT_INFO:
                loggedUser.PrintUserInfo();
                goto loggedOperationsLabel;
            case (int)ConsoleLoggedOperations.UPDATE_INFO:
                userService.UpdateUserInfo(loggedUser, authService);
                goto loggedOperationsLabel;
            case (int)ConsoleLoggedOperations.LOGOUT:
                loggedUser = null;
                break;
            default:
                goto loggedOperationsLabel;
        }
       
    }

    Console.WriteLine("Elktron Bank uygulamasina xos gelmisiniz.");
    Console.WriteLine("1. Giris Edin.");
    Console.WriteLine("2. Hesab yaradin.");
    Console.WriteLine("3. Cixis.");
    int choice;
    int.TryParse(Console.ReadLine(), out choice);

    switch (choice)
    {
        case (int)ConsoleOperations.LOGIN:
            loggedUser = authService.Login();
            break;
        case (int)ConsoleOperations.REGISTER:
            authService.Register(userService);
            break;
        case (int)ConsoleOperations.EXIT:
            isContinue = false;
            break;
        default:
            Console.WriteLine("Dogru girin.");
            break;

    }
}