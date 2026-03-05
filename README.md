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
