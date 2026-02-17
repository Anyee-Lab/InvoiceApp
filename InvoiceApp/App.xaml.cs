using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using InvoiceLib.Services;
using InvoiceLib.Contracts;
using InvoiceApp.Services;
using InvoiceApp.Views;
using System.Threading.Tasks;
using System;
using InvoiceApp.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace InvoiceApp;

/// <summary>
/// Provides application-specific behavior to supplement the default Application class.
/// </summary>
public partial class App : Application
{
    public static T? GetService<T>() where T : class => (Current as App)?.AppHost.Services.GetService<T>();

    public IHost AppHost { get; }

    public static Window MainWindow { get; }= new MainWindow();

    /// <summary>
    /// Initializes the singleton application object.  This is the first line of authored code
    /// executed, and as such is the logical equivalent of main() or WinMain().
    /// </summary>
    public App()
    {
        InitializeComponent();

        var builder = Host.CreateApplicationBuilder();

        // Core services
        builder.Services.AddSingleton<IConfigService, ConfigService>();
        builder.Services.AddSingleton<TokenService>();
        builder.Services.AddSingleton<InvoiceService>();

        // Shell services
        builder.Services.AddSingleton<ActivationService>();
        builder.Services.AddTransient<ObservableInvoiceWorker>();

        // Views and view models
        builder.Services.AddTransient<HomePage>();
        builder.Services.AddTransient<HomeViewModel>();

        AppHost = builder.Build();
    }

    protected override async void OnLaunched(LaunchActivatedEventArgs args)
    {
        base.OnLaunched(args);
        // Invoke when the app started
        if (GetService<ActivationService>() is ActivationService activationService)
            await activationService.ActivateAsync(args);
    }
}
