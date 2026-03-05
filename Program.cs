using ManageAccountApp.Services;
using ManageAccountApp.UI;

namespace ManageAccountApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var accountService = new AccountService();
            var functionsUI = new AccountFunctionsUI(accountService);
            var consoleUI = new ConsoleUI(functionsUI);
            consoleUI.Run();
        }
    }
}