namespace ManageAccountApp.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }

        public Account(int id, string name, decimal initialBalance)
        {
            Id = id;
            Name = name;
            Balance = initialBalance;
        }
    }
}