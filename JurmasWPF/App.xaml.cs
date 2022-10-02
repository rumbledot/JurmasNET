using JurmasWPF.Services;
using JurmasWPF.ViewModels;
using JurmasWPF.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace JurmasWPF;

public partial class App : Application
{
	public static IHost? AppHost { get; private set; }

	public App()
	{
		AppHost = Host.CreateDefaultBuilder()
			.ConfigureServices((context, services) => {
				//services.AddDbContext<DBClient>(o =>
				//	o.UseSqlite("D:\\Personal_Projects\\JurmasNET\\JurmasWPF\\JurmasDB.sqlite3")
				//	);
				services.AddSingleton<FakeData>();

                services.AddSingleton<MainWindow>();
                services.AddTransient<AddRecipeeWindow>();
                services.AddTransient<CreateRecipeeViewModel>();
            })
			.Build();
	}

	protected override async void OnStartup(StartupEventArgs e)
	{
		await AppHost!.StartAsync();

		var startupWindow = AppHost!.Services.GetRequiredService<MainWindow>();
		startupWindow.Show();

		base.OnStartup(e);
	}

	protected override async void OnExit(ExitEventArgs e)
	{
		await AppHost!.StopAsync();

		base.OnExit(e);
	}
}
