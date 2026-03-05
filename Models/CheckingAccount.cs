namespace ManageAccountApp.Models
{
    public class CheckingAccount : SubAccount
    {
        // Lãi suất thanh toán: 5.1%
        private const decimal CHECKING_INTEREST_RATE = 5.1m;

        public CheckingAccount(decimal initialBalance = 0) 
            : base("Tài khoản thanh toán", initialBalance, CHECKING_INTEREST_RATE)
        {
        }

        /// <summary>
        /// Override Withdraw - có thể thêm logic riêng cho tài khoản thanh toán nếu cần
        /// </summary>
        public override bool Withdraw(decimal amount)
        {
            return base.Withdraw(amount);
        }
    }
}
