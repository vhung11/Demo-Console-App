namespace ManageAccountApp.Models
{
    public class SavingsAccount : SubAccount
    {
        // Lãi suất tiết kiệm: 4.7%
        private const decimal SAVINGS_INTEREST_RATE = 4.7m;

        public SavingsAccount()
        {
        }

        public SavingsAccount(decimal initialBalance = 0) 
            : base("Tài khoản tiết kiệm", initialBalance, SAVINGS_INTEREST_RATE)
        {
        }
        
        public override bool Withdraw(decimal amount)
        {
            return base.Withdraw(amount);
        }
    }
}
