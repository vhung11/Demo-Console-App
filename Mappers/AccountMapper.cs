using ManageAccountApp.Models;
using ManageAccountApp.Models.DTO;
namespace ManageAccountApp.Mappers
{
    public static class AccountMapper
    {
        public static AccountDTO ToDTO(Account account)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account), "Account không được null");

            return new AccountDTO(
                id: account.Id,
                name: account.Name,
                savingsBalance: account.SavingsAccount.Balance,
                checkingBalance: account.CheckingAccount.Balance,
                totalBalance: account.GetTotalBalance()
            );
        }

        public static List<AccountDTO> ToDTOList(IEnumerable<Account> accounts)
        {
            if (accounts == null)
                throw new ArgumentNullException(nameof(accounts), "Danh sách accounts không được null");

            var dtoList = (from account in accounts
                          select ToDTO(account)).ToList();
            
            return dtoList;
        }
    }
}
