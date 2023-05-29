using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using PositionManager.Application.Abstractions;
using PositionManager.Application.Services;
using PositionManager.Domain.Abstractions;
using PositionManager.Persisctence.Repository;
using PositionManager.ViewModels;
using PositionManager.Pages;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using PositionManager.Persisctence.Data;
using PositionManager.Domain.Entities;

namespace PositionManager;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        string settingsStream = "PositionManager.appsettings.json";
        var builder = MauiApp.CreateBuilder();
        var a = Assembly.GetExecutingAssembly();
        using var stream = a.GetManifestResourceStream(settingsStream);
        builder.Configuration.AddJsonStream(stream);

        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        AddDbContext(builder);
        SetupServices(builder.Services);
        SeedData(builder.Services);

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
    private static void SetupServices(IServiceCollection services)
    {
        services.AddSingleton<IUnitOfWork, EfUnitOfWork>();
        services.AddSingleton<IResponsibilityService, ResponsibilityService>();
        services.AddSingleton<IEmployeePositionService, EmployeePositionService>();

        services.AddTransient<PositionsViewModel>();
        services.AddTransient<Positions>();

        services.AddTransient<AddPositionPage>();
        services.AddTransient<AddResponsibilityPage>();
        services.AddTransient<AddPositionViewModel>();
        services.AddTransient<AddResponsibilityViewModel>();

        services.AddTransient<ResponsibilityDetails>();
        services.AddTransient<ResponsibilityDetailsViewModel>();

        services.AddTransient<EditResponsibilityPage>();
        services.AddTransient<EditResponsibilityViewModel>();
        
    }

    private static void AddDbContext(MauiAppBuilder builder)
    {
        var connStr = builder.Configuration.GetConnectionString("SqliteConnection");
        string dataDirectory = String.Empty;

#if ANDROID
 dataDirectory = FileSystem.AppDataDirectory+"/";
#endif

        connStr = String.Format(connStr, dataDirectory);
        var options = new DbContextOptionsBuilder<AppDbContext>()
       .UseSqlite(connStr)
       .Options;
        builder.Services.AddSingleton<AppDbContext>((s) => new AppDbContext(options));
    }
    public async static void SeedData(IServiceCollection services)
    {
        using var provider = services.BuildServiceProvider();
        var unitOfWork = provider.GetService<IUnitOfWork>();

        await unitOfWork.RemoveDatbaseAsync();
        await unitOfWork.CreateDatabaseAsync();
        // Add cources
        IReadOnlyList<EmployeePosition> positions = new List<EmployeePosition>()
        {
            new EmployeePosition {Name = "SEO", Salary=1000, Id = 1},
            new EmployeePosition {Name = "Manager", Salary = 500, Id = 2},
            new EmployeePosition { Name = "Employee", Salary = 100, Id = 3}
        };

        foreach (var pos in positions)
            await unitOfWork.PositionRepository.AddAsync(pos);
        await unitOfWork.SaveAllAsync();
        IReadOnlyList<Responsibility> responsibilities = new List<Responsibility>()
            {
                new Responsibility {Id=1, Name="Name1", Importance=3,Description = "Managing assets", EmployeePositionId = 1 },
                new Responsibility {Id=2, Name="Name2", Importance=1,Description = "Making major decisions", EmployeePositionId = 1 },
                new Responsibility {Id=3, Name="Name3", Importance=1,Description = "Setting goals", EmployeePositionId = 1 },
                new Responsibility {Id=4, Name="Name4", Importance=4,Description = "Monitor company perfomance", EmployeePositionId = 1 },
                new Responsibility {Id=5, Name="Name5", Importance=1,Description = "Setting precedence for the working culture and environment", EmployeePositionId = 1 },
                new Responsibility {Id=6, Name="Name6", Importance=1,Description = "Manage the software development team", EmployeePositionId = 2 },
                new Responsibility {Id=7, Name="Name7", Importance=10,Description = "Control troject timelines", EmployeePositionId = 2 },
                new Responsibility {Id=8, Name="Name8", Importance=1,Description = "Hiring and recruiting", EmployeePositionId = 2 },
                new Responsibility {Id=9, Name="Name9", Importance=2,Description = "Manage the tools", EmployeePositionId = 2 },
                new Responsibility {Id=10,Name="Name10",Importance=1, Description = "Ensure software quality", EmployeePositionId = 2 },
                new Responsibility {Id=11,Name="Name11",Importance=3, Description = "Writing and implementing efficient code", EmployeePositionId = 3 },
                new Responsibility {Id=12,Name="Name12",Importance=11, Description = "Maintaining and upgrading existing systems", EmployeePositionId = 3 },
                new Responsibility {Id=13,Name="Name13",Importance=1, Description = "Testing and evaluating new programs", EmployeePositionId = 3 },
                new Responsibility {Id=14,Name="Name14",Importance=1, Description = "Designing programs", EmployeePositionId = 3 },
                new Responsibility {Id=15,Name="Name15",Importance=1, Description = "Training users", EmployeePositionId = 3 }
            };
        foreach (var resp in responsibilities)
            await unitOfWork.ResponsibilityRepository.AddAsync(resp);
        await unitOfWork.SaveAllAsync();
    }

}
