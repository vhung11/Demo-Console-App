using ManageAccountApp.Mappers;
using ManageAccountApp.Models;
using ManageAccountApp.Models.DTO;

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

        public List<AccountDTO> GetAllAccounts()
        {
            return AccountMapper.ToDTOList(_accounts);
        }

        // 9. Xếp hạng account theo tổng số dư (giảm dần)
        public List<AccountDTO> GetAccountsRankedByBalance()
        {
            var query = from account in _accounts
                        orderby account.GetTotalBalance() descending, account.Id
                        select account;
            
            return AccountMapper.ToDTOList(query);
        }

        // 10. Danh sách account có tổng số dư nhỏ hơn ngưỡng cho trước
        public List<AccountDTO> GetAccountsBelowBalance(decimal threshold)
        {
            var query = from account in _accounts
                        where account.GetTotalBalance() < threshold
                        orderby account.GetTotalBalance(), account.Id
                        select account;
            
            return AccountMapper.ToDTOList(query);
        }

        // 11. Top N account có số dư tài khoản thanh toán lớn nhất
        public List<AccountDTO> GetTopCheckingAccounts(int top)
        {
            if (top <= 0) return new List<AccountDTO>();

            var query = (from account in _accounts
                         orderby account.CheckingAccount.Balance descending, account.Id
                         select account).Take(top);
            
            return AccountMapper.ToDTOList(query);
        }

        // 12. Tổng số dư tài khoản đầu tư (quy ước dùng tài khoản tiết kiệm hiện có)
        public decimal GetTotalInvestmentBalance()
        {
            var total = (from account in _accounts
                         select account.SavingsAccount.Balance).Sum();
            
            return total;
        }

        public AccountDTO? GetAccountInfoById(int id)
        {
            var account = FindById(id);
            if (account == null) return null;

            return AccountMapper.ToDTO(account);
        }

        private Account? FindById(int id)
        {
            var account = (from a in _accounts
                          where a.Id == id
                          select a).FirstOrDefault();
            
            return account;
        }

        /// <summary>
        /// Khởi tạo dữ liệu mẫu
        /// </summary>
        public void InitializeSampleData()
        {
            // Tạo các tài khoản mẫu với số dư khác nhau
            AddAccount("Nguyễn Văn An", 10000000);      // 10 triệu
            AddAccount("Trần Thị Bình", 25000000);      // 25 triệu
            AddAccount("Lê Văn Cường", 500000);        // 5 triệu
            AddAccount("Phạm Thị Dung", 50000000);      // 50 triệu
            AddAccount("Hoàng Văn Em", 15000000);       // 15 triệu
            AddAccount("Vũ Thị Phương", 30000000);      // 30 triệu
            AddAccount("Đỗ Văn Giang", 8000000);        // 8 triệu
            AddAccount("Bùi Thị Hoa", 450000);        // 45 triệu
            AddAccount("Nguyễn Thị Lan", 12000000);     // 12 triệu
            AddAccount("Trần Văn Minh", 7000000);       // 7 triệu
            AddAccount("Phạm Văn Nam", 2000000);        // 2 triệu
            AddAccount("Lê Thị Oanh", 600000);         // 6 triệu
            AddAccount("Hoàng Thị Phương", 350000);   // 35 triệu
            AddAccount("Vũ Văn Quang", 4000000);        // 4 triệu
            AddAccount("Đỗ Thị Thu", 10000000);         // 10 triệu
            AddAccount("Bùi Văn Sơn", 8000000);         // 8 triệu
            AddAccount("Nguyễn Thị Trang", 22000000);   // 22 triệu
            AddAccount("Trần Văn Uy", 90000);         // 9 triệu
            AddAccount("Phạm Thị Vân", 3000000);        // 3 triệu
            AddAccount("Lê Văn Xinh", 40000000);        // 40 triệu
        }
    }
}