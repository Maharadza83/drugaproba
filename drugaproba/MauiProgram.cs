using System;
using System.Net.Http;
using Microsoft.Maui;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using Microsoft.AspNetCore.Components.WebView.Maui;  // <- tu
using Microsoft.Extensions.DependencyInjection;      // <- AddAuthorizationCore
using drugaproba.Services;

namespace drugaproba;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
          .UseMauiApp<App>()
          .UseMauiBlazorWebView()  // <- extension z Microsoft.Maui.Controls.Hosting
          .ConfigureFonts(fonts =>
          {
              fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
          });

        // HTTP client
        builder.Services.AddSingleton(sp => new HttpClient
        {
            BaseAddress = DeviceInfo.Platform == DevicePlatform.Android
                ? new Uri("http://10.0.2.2:5244")
                : new Uri("http://localhost:5244")
        });

        // Serwisy
        builder.Services.AddSingleton<AuthService>();
        builder.Services.AddSingleton<ChatService>();

        // Blazor Authorization
        builder.Services.AddAuthorizationCore();  // <- wymaga using Microsoft.Extensions.DependencyInjection

        return builder.Build();
    }
}
