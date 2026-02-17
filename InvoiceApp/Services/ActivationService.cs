using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using InvoiceLib.Contracts;
using InvoiceLib.Services;
using Microsoft.UI.Xaml;
using InvoiceApp.Views;

namespace InvoiceApp.Services;

public class ActivationService
{
    IConfigService ConfigService { get; }

    TokenService TokenService { get; }

    HomePage HomePage { get; }

    public ActivationService(IConfigService configService, TokenService tokenService, HomePage homePage)
    {
        ConfigService = configService;
        TokenService = tokenService;
        HomePage = homePage;
    }

    public async Task ActivateAsync(LaunchActivatedEventArgs args)
    {
        // Show main window
        App.MainWindow.Content = HomePage;
        App.MainWindow.ExtendsContentIntoTitleBar = true;
        App.MainWindow.SetTitleBar(HomePage.AppTitleBar);
        App.MainWindow.Activate();

        // Do init works
        await ConfigService.LoadConfigAsync();
        await TokenService.UpdateTokenAsync();
        Console.WriteLine($"ID: {TokenService.ID}{Environment.NewLine}secret: {TokenService.Secret}{Environment.NewLine}token: {TokenService.Token}");
        await ConfigService.SaveConfigAsync();
    }
}
