namespace ManageAccountApp.Models
{
    public abstract class SubAccount
    {
        public int Id { get; set; }
        public int AccountId { get; set; } // Foreign key to Account
        public string Type { get; set; }
        public decimal Balance { get; set; }
        public decimal InterestRate { get; set; }

        // Constructor không tham số cho EF Core
        protected SubAccount()
        {
            Type = string.Empty;
        }

        protected SubAccount(string type, decimal initialBalance, decimal interestRate)
        {
            Type = type;
            Balance = initialBalance;
            InterestRate = interestRate;
        }

        /// <summary>
        /// Nộp tiền vào tài khoản
        /// </summary>
        public virtual bool Deposit(decimal amount)
        {
            if (amount <= 0) return false;
            Balance += amount;
            return true;
        }

        /// <summary>
        /// Rút tiền từ tài khoản
        /// </summary>
        public virtual bool Withdraw(decimal amount)
        {
            if (amount <= 0 || Balance < amount) return false;
            Balance -= amount;
            return true;
        }

        /// <summary>
        /// Tính lãi suất cho tài khoản
        /// </summary>
        public virtual decimal CalculateInterest()
        {
            return Balance * InterestRate / 100;
        }

        /// <summary>
        /// Cộng lãi suất vào balance
        /// </summary>
        public virtual void ApplyInterest()
        {
            Balance += CalculateInterest();
        }

        /// <summary>
        /// Lấy thông tin tài khoản
        /// </summary>
        public override string ToString()
        {
            return $"{Type} | Số dư: {Balance:N0} VND | Lãi suất: {InterestRate}%";
        }
    }
}
