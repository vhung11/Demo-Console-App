using ManageAccountApp.Data;
using ManageAccountApp.Services;
using ManageAccountApp.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ManageAccountApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Đọc cấu hình từ appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Cấu hình Dependency Injection
            var serviceProvider = new ServiceCollection()
                .AddDbContext<ApplicationDbContext>(options =>
                    options.UseOracle(configuration.GetConnectionString("OracleConnection")))
                .AddScoped<AccountService>()
                .BuildServiceProvider();

            // Khởi tạo database và áp dụng migrations
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                
                // Tạo database nếu chưa tồn tại
                
                // Khởi tạo dữ liệu mẫu
                var accountService = scope.ServiceProvider.GetRequiredService<AccountService>();
                accountService.InitializeSampleData();
            }

            // Chạy ứng dụng
            using (var scope = serviceProvider.CreateScope())
            {
                var accountService = scope.ServiceProvider.GetRequiredService<AccountService>();
                var functionsUI = new AccountFunctionsUI(accountService);
                var consoleUI = new ConsoleUI(functionsUI);
                consoleUI.Run();
            }
        }
    }
}