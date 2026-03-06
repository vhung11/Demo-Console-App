# ManageAccountApp

Console app quản lý tài khoản ngân hàng viết bằng C# (.NET 10.0) cho phép quản lý tài khoản với 2 loại tài khoản con: Tài khoản tiết kiệm và Tài khoản thanh toán.

## Chức năng chính:

- **Thêm tài khoản mới** - Tạo tài khoản với số dư ban đầu được chia đều cho 2 tài khoản con
- **Xóa tài khoản** - Xóa tài khoản theo ID
- **Xem danh sách tài khoản** - Hiển thị tất cả tài khoản và thông tin chi tiết
- **Nộp tiền** - Nạp tiền vào tài khoản tiết kiệm hoặc thanh toán
- **Rút tiền** - Rút tiền từ tài khoản tiết kiệm hoặc thanh toán
- **Tính lãi suất** - Áp dụng lãi suất cho tất cả tài khoản
  - Tài khoản tiết kiệm: **4.7%**
  - Tài khoản thanh toán: **5.1%**

## Các tính chất OOP đã áp dụng

### 1. **Encapsulation (Đóng gói)**

**Mục đích:** Ngăn chặn truy cập trực tiếp từ bên ngoài.

**Áp dụng trong project:**

```csharp
// AccountService.cs
public class AccountService
{
    // Private fields - chỉ truy cập được trong class
    private readonly List<Account> _accounts = new List<Account>();
    private int _nextId = 1;
    
    // Public methods - interface để tương tác với dữ liệu
    public int AddAccount(string name, decimal balance) { ... }
    public bool DeleteAccount(int id) { ... }
}

// SubAccount.cs
public abstract class SubAccount
{
    // Protected - cho phép class con truy cập nhưng bên ngoài không thể
    protected set { ... }
    public decimal Balance { get; protected set; }
    public decimal InterestRate { get; protected set; }
}
```

### 2. **Inheritance (Kế thừa)**

**Mục đích:** Tái sử dụng code

**Áp dụng trong project:**

```csharp
// SubAccount.cs - Class cha
public abstract class SubAccount
{
    public decimal Balance { get; protected set; }
    public virtual bool Deposit(decimal amount) { ... }
    public virtual bool Withdraw(decimal amount) { ... }
}

// SavingsAccount.cs - Class con kế thừa
public class SavingsAccount : SubAccount
{
    private const decimal SAVINGS_INTEREST_RATE = 4.7m;
    
    public SavingsAccount(decimal initialBalance = 0) 
        : base("Tài khoản tiết kiệm", initialBalance, SAVINGS_INTEREST_RATE)
    { }
}

// CheckingAccount.cs - Class con kế thừa
public class CheckingAccount : SubAccount
{
    private const decimal CHECKING_INTEREST_RATE = 5.1m;
    
    public CheckingAccount(decimal initialBalance = 0) 
        : base("Tài khoản thanh toán", initialBalance, CHECKING_INTEREST_RATE)
    { }
}
```

### 3. **Polymorphism (Đa hình)**

**Áp dụng trong project:**

```csharp
// SubAccount.cs - Định nghĩa virtual methods
public abstract class SubAccount
{
    public virtual bool Deposit(decimal amount) { ... }
    public virtual bool Withdraw(decimal amount) { ... }
    public virtual void ApplyInterest() { ... }
}

// Các class con có thể override để custom behavior
public class CheckingAccount : SubAccount
{
    public override bool Withdraw(decimal amount)
    {
        // Có thể thêm logic riêng cho tài khoản thanh toán
        return base.Withdraw(amount);
    }
}

// Account.cs - Sử dụng polymorphism
public class Account
{
    public SavingsAccount SavingsAccount { get; set; }
    public CheckingAccount CheckingAccount { get; set; }
    
    public void ApplyInterestToAll()
    {
        // Gọi cùng một method nhưng mỗi account xử lý khác nhau
        SavingsAccount.ApplyInterest();  // Áp dụng lãi 4.7%
        CheckingAccount.ApplyInterest(); // Áp dụng lãi 5.1%
    }
}
```

### 4. **Abstraction (Trừu tượng)**

**Mục đích:** Ẩn chi tiết phức tạp, chỉ hiển thị những gì cần thiết.

**Áp dụng trong project:**

```csharp
// SubAccount.cs - sử dụng Abstract class
public abstract class SubAccount
{
    // Định nghĩa "contract" - các class con phải có
    protected SubAccount(string type, decimal initialBalance, decimal interestRate)
    { ... }
}

// DTO Pattern - chỉ trả về dữ liệu của đối tượng chứ ko trả về đối tượng đó
public class AccountDTO
{
    // Chỉ expose data cần thiết cho UI
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal SavingsBalance { get; set; }
    public decimal CheckingBalance { get; set; }
    public decimal TotalBalance { get; set; }
}

## Sử dụng LINQ

```csharp
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

```

## 🚀 Cách chạy ứng dụng

### Yêu cầu
- .NET 10.0 SDK hoặc cao hơn

### Chạy ứng dụng

```bash
# Tải code và chạy lệnh
cd ManageAccountApp
dotnet run
```

### Build ứng dụng

```bash
# Build project
dotnet build

```


