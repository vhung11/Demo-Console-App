using System;
using System.Text;
using ManageAccountApp.Services;
using ManageAccountApp.Helpers;

namespace ManageAccountApp.UI
{
    public class ConsoleUI
    {
        private readonly AccountService _accountService;

        public ConsoleUI()
        {
            _accountService = new AccountService();
        }

        public void Run()
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;

            bool isRunning = true;

            while (isRunning)
            {
                ShowMenu();
                string choice = Console.ReadLine() ?? "";

                Console.Clear();

                switch (choice)
                {
                    case "1":
                        ShowAllAccounts();
                        break;

                    case "2":
                        AddAccount();
                        break;

                    case "3":
                        DeleteAccount();
                        break;

                    case "4":
                        Deposit();
                        break;

                    case "5":
                        Withdraw();
                        break;

                    case "0":
                        isRunning = false;
                        Console.WriteLine("Thoát chương trình...");
                        continue;

                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ!");
                        break;
                }

                Pause();
            }
        }

        private void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("=== QUẢN LÝ TÀI KHOẢN ===");
            Console.WriteLine("1. Xem danh sách tài khoản");
            Console.WriteLine("2. Thêm tài khoản mới");
            Console.WriteLine("3. Xóa tài khoản");
            Console.WriteLine("4. Nộp tiền");
            Console.WriteLine("5. Rút tiền");
            Console.WriteLine("0. Thoát");
            Console.Write("Chọn chức năng (0-5): ");
        }

        private void ShowAllAccounts()
        {
            var accounts = _accountService.GetAllAccounts();

            Console.WriteLine("=== DANH SÁCH TÀI KHOẢN ===");

            if (accounts.Count == 0)
            {
                Console.WriteLine("Chưa có tài khoản nào.");
                return;
            }

            foreach (var acc in accounts)
            {
                Console.WriteLine($"ID: {acc.Id} | Tên: {acc.Name} | Số dư: {acc.Balance:N0} VND");
            }
        }

        private void AddAccount()
        {
            Console.WriteLine("=== THÊM TÀI KHOẢN ===");

            while (true)
            {
                int id = InputHelper.ReadInt("Nhập ID: ");
                string name = InputHelper.ReadString("Nhập tên chủ tài khoản: ");
                decimal balance = InputHelper.ReadDecimal("Nhập số dư ban đầu: ");

                bool result = _accountService.AddAccount(id, name, balance);

                if (result)
                {
                    Console.WriteLine("Thêm tài khoản thành công!");
                    break; // Thoát vòng lặp khi thành công
                }
                else
                {
                    Console.WriteLine("ID đã tồn tại! Vui lòng nhập lại.\n");
                }
            }
        }

        private void DeleteAccount()
        {
            Console.WriteLine("=== XÓA TÀI KHOẢN ===");

            int id = InputHelper.ReadInt("Nhập ID tài khoản cần xóa: ");

            if (_accountService.DeleteAccount(id))
                Console.WriteLine("Xóa tài khoản thành công!");
            else
                Console.WriteLine("Không tìm thấy tài khoản với ID này.");
        }

        private void Deposit()
        {
            Console.WriteLine("=== NỘP TIỀN ===");

            int id = InputHelper.ReadInt("Nhập ID: ");
            decimal amount = InputHelper.ReadDecimal("Nhập số tiền nộp: ");

            if (_accountService.Deposit(id, amount))
                Console.WriteLine("Nộp tiền thành công!");
            else
                Console.WriteLine("Giao dịch thất bại.");
        }

        private void Withdraw()
        {
            Console.WriteLine("=== RÚT TIỀN ===");

            int id = InputHelper.ReadInt("Nhập ID: ");
            decimal amount = InputHelper.ReadDecimal("Nhập số tiền rút: ");

            if (_accountService.Withdraw(id, amount))
                Console.WriteLine("Rút tiền thành công!");
            else
                Console.WriteLine("Giao dịch thất bại (ID sai hoặc số dư không đủ).");
        }

        private void Pause()
        {
            Console.WriteLine("\nNhấn phím bất kỳ để quay lại menu...");
            Console.ReadKey();
        }
    }
}