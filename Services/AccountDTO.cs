namespace ManageAccountApp.Services
{
    /// <summary>
    /// Data Transfer Object - chứa thông tin tài khoản để truyền từ Service đến UI
    /// </summary>
    public class AccountDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal SavingsBalance { get; set; }
        public decimal CheckingBalance { get; set; }
        public decimal TotalBalance { get; set; }

        public AccountDTO(int id, string name, decimal savingsBalance, decimal checkingBalance, decimal totalBalance)
        {
            Id = id;
            Name = name;
            SavingsBalance = savingsBalance;
            CheckingBalance = checkingBalance;
            TotalBalance = totalBalance;
        }
    }
}
