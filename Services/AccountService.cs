using ManageAccountApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace ManageAccountApp.Services
{
    public class AccountService
    {
        // Danh sách lưu trữ tạm thời trong bộ nhớ (RAM)
        private List<Account> _accounts = new List<Account>();

        // 1. Thêm tài khoản
        public bool AddAccount(int id, string name, decimal balance)
        {
            if (_accounts.Any(a => a.Id == id)) return false; // ID đã tồn tại
            
            _accounts.Add(new Account(id, name, balance));
            return true;
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

        // 3. Nạp tiền
        public bool Deposit(int id, decimal amount)
        {
            if (amount <= 0) return false;
            var account = FindById(id);
            if (account != null)
            {
                account.Balance += amount;
                return true;
            }
            return false;
        }

        // 4. Rút tiền
        public bool Withdraw(int id, decimal amount)
        {
            var account = FindById(id);
            if (account != null && amount > 0 && account.Balance >= amount)
            {
                account.Balance -= amount;
                return true;
            }
            return false;
        }

        // 5. Lấy danh sách (để ConsoleUI hiển thị)
        public List<Account> GetAllAccounts() => _accounts;

        // Hàm phụ trợ tìm kiếm tài khoản
        private Account? FindById(int id)
        {
            return _accounts.FirstOrDefault(a => a.Id == id);
        }
    }
}