using drugaproba.Services;
using Microsoft.AspNetCore.Components.WebView.Maui;  // <- tu
using Microsoft.Extensions.DependencyInjection;      // <- AddAuthorizationCore
using Microsoft.Extensions.Logging;
using Microsoft.Maui;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using System;
using System.Net.Http;

namespace drugaproba;

public static MauiApp CreateMauiApp()
{
    var builder = MauiApp.CreateBuilder();
    builder
        .UseMauiApp<App>()
        .ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
        });

    // Poprawna rejestracja dla .NET 8
    builder.Services.AddMauiBlazorWebView();

    // Dodaj to, aby ułatwić debugowanie
#if DEBUG
    builder.Services.AddBlazorWebViewDeveloperTools();
    builder.Logging.AddDebug();
#endif

    // Reszta Twojego kodu...
    builder.Services.AddSingleton(sp => new HttpClient
    {
        BaseAddress = DeviceInfo.Platform == DevicePlatform.Android
            ? new Uri("http://10.0.2.2:5244")
            : new Uri("http://localhost:5244")
    });

    builder.Services.AddSingleton<AuthService>();
    builder.Services.AddSingleton<ChatService>();
    builder.Services.AddAuthorizationCore();

    return builder.Build();
}
