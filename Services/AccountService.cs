using ManageAccountApp.Models;

namespace ManageAccountApp.Services
{
    public class AccountService
    {
        // Danh sách lưu trữ tạm thời trong bộ nhớ (RAM)
        private readonly List<Account> _accounts = new List<Account>();
        private int _nextId = 1;

        // 1. Thêm tài khoản
        public int AddAccount(string name, decimal balance)
        {
            if (string.IsNullOrWhiteSpace(name) || balance < 0)
                return 0;

            int newId = _nextId;
            _accounts.Add(new Account(newId, name, balance));
            _nextId++;

            return newId;
        }

        // 2. Xóa tài khoản
        public bool DeleteAccount(int id)
        {
            var account = FindById(id);
            if (account != null)
            {
                _accounts.Remove(account);
                return true;
            }
            return false;
        }

        // 3. Nạp tiền vào tài khoản tiết kiệm
        public bool DepositToSavings(int id, decimal amount)
        {
            var account = FindById(id);
            if (account == null) return false;

            return account.SavingsAccount.Deposit(amount);
        }

        // 4. Nạp tiền vào tài khoản thanh toán
        public bool DepositToChecking(int id, decimal amount)
        {
            var account = FindById(id);
            if (account == null) return false;

            return account.CheckingAccount.Deposit(amount);
        }

        // 5. Rút tiền từ tài khoản tiết kiệm
        public bool WithdrawFromSavings(int id, decimal amount)
        {
            var account = FindById(id);
            if (account == null) return false;

            return account.SavingsAccount.Withdraw(amount);
        }

        // 6. Rút tiền từ tài khoản thanh toán
        public bool WithdrawFromChecking(int id, decimal amount)
        {
            var account = FindById(id);
            if (account == null) return false;

            return account.CheckingAccount.Withdraw(amount);
        }

        // Giữ lại để tương thích ngược: mặc định nạp vào tài khoản thanh toán
        public bool Deposit(int id, decimal amount)
        {
            return DepositToChecking(id, amount);
        }

        // Giữ lại để tương thích ngược: mặc định rút từ tài khoản thanh toán
        public bool Withdraw(int id, decimal amount)
        {
            return WithdrawFromChecking(id, amount);
        }

        // 7. Áp dụng lãi suất cho tất cả tài khoản
        public void ApplyInterestToAllAccounts()
        {
            foreach (var account in _accounts)
            {
                account.ApplyInterestToAll();
            }
        }

        // 8. Lấy danh sách DTOs (để UI hiển thị - không expo Model)
        public List<AccountDTO> GetAllAccounts()
        {
            return _accounts.Select(a => new AccountDTO(
                a.Id,
                a.Name,
                a.SavingsAccount.Balance,
                a.CheckingAccount.Balance,
                a.GetTotalBalance()
            )).ToList();
        }

        // 9. Lấy thông tin chi tiết một tài khoản (DTO)
        public AccountDTO? GetAccountInfoById(int id)
        {
            var account = FindById(id);
            if (account == null) return null;

            return new AccountDTO(
                account.Id,
                account.Name,
                account.SavingsAccount.Balance,
                account.CheckingAccount.Balance,
                account.GetTotalBalance()
            );
        }

        // Hàm phụ trợ tìm kiếm tài khoản
        private Account? FindById(int id)
        {
            return _accounts.FirstOrDefault(a => a.Id == id);
        }
    }
}