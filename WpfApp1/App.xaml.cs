using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using WpfApp1.Services;
using WpfApp1.Windows;
using System.Windows;
using WpfApp1.Data;

namespace WpfApp1;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private ServiceProvider? _serviceProvider;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        // Настройка DI контейнера
        var services = new ServiceCollection();

        ConfigureServices(services);
        _serviceProvider = services.BuildServiceProvider();

        // Создание и миграция базы данных
        using (var scope = _serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            try
            {
                context.Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения к базе данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown();

                return;
            }
        }

        // Вызов основного окна
        var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=WpfApp1Db;Trusted_Connection=true;");
        });

        services.AddScoped<IProductService, ProductService>();
        services.AddTransient<MainWindow>();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        _serviceProvider?.Dispose();
        base.OnExit(e);
    }
}
