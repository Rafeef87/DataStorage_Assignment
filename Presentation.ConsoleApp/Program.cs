using Business.Interfaces;
using Business.Services;
using Data.Contexts;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Presentation.ConsoleApp.Dialogs;

var serviceProvider = new ServiceCollection()
    .AddDbContext<DataContext>(x => x.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=E:\\ECUtbildning\\Datalagring\\DataStorage_Assignment\\Data\\Databases\\Local_Database.mdf;Integrated Security=True;Connect Timeout=30"))
    .AddSingleton<IProjectRepository, ProjectRepository>()
    .AddSingleton<IProjectService, ProjectService>()
    .AddSingleton<ICustomerRepository, CustomerRepository>()
    .AddSingleton<ICustomerService, CustomerService>()
    .AddSingleton<IProductRepository, ProductRepository>()
    .AddSingleton<IProductService, ProductService>()
    .AddSingleton<IStatusTypeRepository, StatusTypeRepository>()
    .AddSingleton<IStatusTypeService, StatusTypeService>()
    .AddSingleton<IUserRepository, UserRepository>()
    .AddSingleton<IUserService, UserService>()

    .AddTransient<MenuDialog>()

.BuildServiceProvider();

var menuDialog = serviceProvider.GetRequiredService<MenuDialog>();
await menuDialog.ShowMenu();
