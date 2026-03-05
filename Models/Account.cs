namespace ManageAccountApp.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        // Tài khoản con: Tiết kiệm (4.7%) và Thanh toán (5.1%)
        public SavingsAccount SavingsAccount { get; set; }
        public CheckingAccount CheckingAccount { get; set; }

        public Account(int id, string name, decimal initialBalance)
        {
            Id = id;
            Name = name;
            
            // Khởi tạo 2 tài khoản con với số dư ban đầu
            SavingsAccount = new SavingsAccount(initialBalance / 2);
            CheckingAccount = new CheckingAccount(initialBalance / 2);
        }

        /// <summary>
        /// Tính tổng số dư của cả 2 tài khoản con
        /// </summary>
        public decimal GetTotalBalance()
        {
            return SavingsAccount.Balance + CheckingAccount.Balance;
        }

        /// <summary>
        /// Áp dụng lãi suất cho cả 2 tài khoản con
        /// </summary>
        public void ApplyInterestToAll()
        {
            SavingsAccount.ApplyInterest();
            CheckingAccount.ApplyInterest();
        }
    }
}