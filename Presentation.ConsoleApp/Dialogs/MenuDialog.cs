using System;
using Business.Dtos;
using Business.Interfaces;
using Business.Models;
using Business.Services;


namespace Presentation.ConsoleApp.Dialogs;

public class MenuDialog(IProjectService projectService, ICustomerService customerService, IProductService productService)
{
    private readonly IProjectService _projectService= projectService;
    private readonly ICustomerService _customerService = customerService;
    private readonly IProductService _productService = productService;

    public async Task ShowMenu()
    {
        var isRunning = true;
        do
        {
            Console.Clear();
            Console.WriteLine("----- Main Menu -----");

            Console.WriteLine("¤¤¤ PROJECT MENU ¤¤¤");
            Console.WriteLine("1. ADD NEW PROJECT ");
            Console.WriteLine("2. VIEW ALL PROJECT ");
            Console.WriteLine("3. UPDATE PROJECT ");
            Console.WriteLine("4. DELELTE PROJECT ");

            Console.WriteLine("¤¤¤ CUSTOMER MENU ¤¤¤");
            Console.WriteLine("5. CREATE NEW CUSTOMER ");
            Console.WriteLine("6. VIEW ALL CUSTOMER ");
            Console.WriteLine("7. UPDATE CUSTOMER ");
            Console.WriteLine("8. DELELTE CUSTOMER ");

            Console.WriteLine("¤¤¤ PRODUCT MENU ¤¤¤");
            Console.WriteLine("9. CREATE NEW PRODUCT ");
            Console.WriteLine("10. VIEW ALL PRODUCT ");
            Console.WriteLine("11. UPDATE PRODUCT ");
            Console.WriteLine("12. DELELTE PRODUCT ");

            Console.WriteLine("q EXIT APPLICATION");
            Console.WriteLine("-----------------------------");
            Console.Write("SELSCT YOUR OPTION: ");

            string option = Console.ReadLine()!;

            switch (option.ToLower())
            {
                case "1":
                    await CreateProjectDialog();
                    break;
                case "2":
                    await GetAllProjectsDialog();
                    break;

                case "3":
                    await UpdateProjectDialog();
                    break;
                case "4":
                    await DeleteProjectDialog();
                    break;
                 case "5":
                     await CreateCustomerDialog();
                    break;
                case "6":
                    await GetAllCustomersDialog();
                    break;
                case "7":
                    await UpdateCustomerDialog();
                    break;
                case "8":
                    await DeleteCustomerDialog();
                    break;
                case "9":
                    await CreateProductDialog();
                    break;
                case "10":
                    await GetAllProductsDialog();
                    break;
                case "11":
                    await UpdateProductDialog();
                    break;
                case "12":
                    await DeleteProductDialog();
                    break;
                case "q":
                    Console.WriteLine("PRESS ANY KEY TO EXIT.");
                    Console.ReadKey();
                    isRunning = false;
                    break;
                 default:
                    Console.WriteLine("INVALID OPTION. PLEASE TRY AGAIN.");
                    Console.ReadKey();
                    break;
            }
        } while (isRunning);
    }

    #region CustomerDialog
    private async Task CreateCustomerDialog()
    {
        Console.Clear();
        // Create a new RegistrationForm object
        var customerRegistrationForm = new CustomerRegistrationForm();

        Console.WriteLine("¤¤¤ CREATE CUSTOMER ¤¤¤");

        Console.Write("ENTER CUSTOMER NAME:");
        customerRegistrationForm.CustomerName = Console.ReadLine()!;

        // Send the completed form to the service repository
        var result = await _customerService.CreateCustomerAsync(customerRegistrationForm);
        if (result != null)
        {
            Console.Write("CUSTOMER WAS SUCCESSFULLY CREATED");
        }
        else
        {
            Console.WriteLine("CUSTOMER WAS NOT CREATE");
        }
        Console.ReadKey();
    }
    private async Task GetAllCustomersDialog()
    {
        Console.Clear();
        Console.WriteLine("-------- ALL CUSTOMERS -------");
        var customers = await _customerService.GetAllCustomersAsync();

        foreach (var customer in customers)
        {
            Console.WriteLine($"[{customer.Id},{customer.CustomerName}]");
        }
        Console.ReadKey();
    }
    private async Task UpdateCustomerDialog()
    {
        Console.Clear();
        var customerUpdateForm = new CustomerUpdateForm();

        Console.WriteLine("¤¤¤ UPDATE CUSTOMER ¤¤¤");

        Console.Write("CUSTOMER NUMBER: ");
        customerUpdateForm.Id = int.Parse(Console.ReadLine()!);

        Console.Write("NEW CUSTOMER NAME: ");
        customerUpdateForm.CustomerName = Console.ReadLine()!;

        var result = await _customerService.UpdateCustomerAsync(customerUpdateForm);
        if (result != null)
        {
            Console.Write("CUSTOMER WAS UPDATED SUCCESSFULLY");
        }
        else
        {
            Console.WriteLine("CUSTOMER WAS NOT UPDATE");
        }
        Console.ReadKey();
    }
    private async Task DeleteCustomerDialog()
    {
        Console.Clear();

        Console.WriteLine("¤¤¤ DELETE CUSTOMER ¤¤¤");

        Console.WriteLine("ENTER CUSTOMER ID TO DELETE :");

        if (int.TryParse(Console.ReadLine(), out int customerId))
        {
            Console.WriteLine("ARE YOU SURE YOU WANT TO DELETE THIS CUSTOMER? (YES/NO): ");

            var option = Console.ReadLine()?.Trim().ToLower();
            if (option == "yes")
            {
                var result = await _customerService.DeleteCustomerAsync(customerId);
                if (result)
                {
                    Console.WriteLine("CUSTOMER WAS DELETED SUCCESSFULLY");
                }
            }
            else
            {
                Console.WriteLine("CUSTOMER WAS NOT DELETED");
            }
            Console.ReadKey();
        }
    }
    #endregion

    #region ProjectDialog
    private async Task CreateProjectDialog()
    {
        Console.Clear ();
        // Create a new ProjectRegistrationForm object
        var projectRegistrationForm = new ProjectRegistrationForm();

        Console.WriteLine("¤¤¤ CREATE PROJECT ¤¤¤");

        Console.Write("ENTER PROJECT NAME:");
        projectRegistrationForm.ProjectName = Console.ReadLine()!;

        Console.Write("ENTER START DATE (yyyy-MM-dd): ");
        projectRegistrationForm.StartDate = DateTime.Parse(Console.ReadLine()!);

        Console.Write("ENTER END DATE (yyyy-MM-dd): ");
        projectRegistrationForm.EndDate = DateTime.Parse(Console.ReadLine()!);

        Console.Write("ENTER CUSTOMER Id:");
        projectRegistrationForm.CustomerId = int.Parse(Console.ReadLine()!);

        Console.Write("ENTER STATUS Id: ");
        projectRegistrationForm.StatusId = int.Parse(Console.ReadLine()!);

        Console.Write("ENTER USER Id: ");
        projectRegistrationForm.UserId = int.Parse(Console.ReadLine()!);

        Console.Write("ENTER PRODUCT Id: ");
        projectRegistrationForm.ProductId = int.Parse(Console.ReadLine()!);

        // Send the completed form to the service repository
        var result = await _projectService.CreateProjectAsync(projectRegistrationForm);
        if (result != null)
        {
            Console.Write("PROJECT WAS SUCCESSFULLY CREATED");

        }
        else
        {
            Console.WriteLine("PROJECT WAS NOT CREATE");
        }
        Console.ReadKey();
    }
    private async Task GetAllProjectsDialog()
    {
        Console.Clear();
        Console.WriteLine("-------- ALL PROJECTS -------");

        var projects = await _projectService.GetAllProjectsAsyncFK();

        foreach (var project in projects)
        {
            Console.WriteLine($"Project: {project.ProjectName}");
            Console.WriteLine($"Start Date: {project.StartDate:yyyy-MM-dd}");
            Console.WriteLine($"End Date: {project.EndDate:yyyy-MM-dd}");
            Console.WriteLine($"Customer: {project.CustomerName}");
            Console.WriteLine($"Status: {project.StatusName}");
            Console.WriteLine($"User: {project.UserName}");
            Console.WriteLine($"Product: {project.ProductName}");
            Console.WriteLine("-------------------------------");
        }

        Console.ReadKey();
    }
    //private async Task GetAllProjectDialog()
    //{
    //    Console.Clear();
    //    Console.WriteLine("-------- ALL PROJECTS -------");
    //    var projects = await _projectService.GetAllProjectsAsync();

    //    foreach (var project in projects)
    //    {
    //        Console.WriteLine($"[{project.Id},{project.ProjectName},{project.StartDate},{project.EndDate},{project.CustomerId},{project.StatusId},{project.UserId},{project.ProductId}]");
    //    }
    //    Console.ReadKey();
    //}
    private async Task UpdateProjectDialog()
    {
        Console.Clear();
        var projectUpdateForm = new ProjectUpdateForm();

        Console.WriteLine("¤¤¤ UPDATE PROJECT ¤¤¤");

        Console.Write("PROJECT NUMBER: ");
        projectUpdateForm.Id = int.Parse(Console.ReadLine()!);

        Console.Write("NEW PROJECT NAME: ");
        projectUpdateForm.ProjectName = Console.ReadLine()!;

        Console.Write("NEW START DATE (yyyy-MM-dd): ");
        if (!DateTime.TryParse(Console.ReadLine(), out var startDate))
        {
            Console.WriteLine("Invalid date format.");
            return;
        }
        projectUpdateForm.StartDate = startDate;

        Console.Write("NEW END DATE (yyyy-MM-dd): ");
        if (!DateTime.TryParse(Console.ReadLine(), out var endDate))
        {
            Console.WriteLine("Invalid date format.");
            return;
        }
        projectUpdateForm.EndDate = endDate;

        Console.Write("NEW CUSTOMER Id: ");
        projectUpdateForm.CustomerId = int.Parse(Console.ReadLine()!);

        Console.Write("NEW STATUS Id: ");
        projectUpdateForm.StatusId = int.Parse(Console.ReadLine()!);

        Console.Write("NEW USER Id: ");
        projectUpdateForm.UserId = int.Parse(Console.ReadLine()!);

        Console.Write("NEW PRODUCT Id: ");
        projectUpdateForm.ProductId = int.Parse(Console.ReadLine()!);

        var result = await _projectService.UpdateProjectAsync(projectUpdateForm);
        if (result != null)
        {
            Console.Write("PROJECT WAS UPDATED SUCCESSFULLY");
        }
        else
        {
            Console.WriteLine("PROJECT WAS NOT UPDATE");
        }

        Console.ReadKey();
    }
    private async Task DeleteProjectDialog()
    {
        Console.Clear();

        Console.WriteLine("¤¤¤ DELETE PROJECT ¤¤¤");

        Console.WriteLine("ENTER PROJECT ID TO DELETE :");
       
        if (int.TryParse(Console.ReadLine(), out int projectId))
        {
            Console.WriteLine("ARE YOU SURE YOU WANT TO DELETE THIS PROJECT? (YES/NO): ");

            var option = Console.ReadLine()?.Trim().ToLower();
            if (option == "yes")
            {
                var result = await _projectService.DeleteProjectAsync(projectId);
                if (result)
                {
                    Console.WriteLine("PROJECT WAS DELETED SUCCESSFULLY");
                }
            }
            else
            {
                Console.WriteLine("PROJECT WAS NOT DELETED");
            }
            Console.ReadKey();
        }
    }
    #endregion

    #region ProductDialog
    private async Task CreateProductDialog()
    {
        Console.Clear();
        // Create a new RegistrationForm object
        var productRegistrationForm = new ProductRegistrationForm();

        Console.WriteLine("¤¤¤ CREATE PRODUCT ¤¤¤");

        Console.Write("ENTER PRODUCT NAME:");
        productRegistrationForm.ProductName = Console.ReadLine()!;
        Console.Write("ENTER PRODUCT PRICE:");
        productRegistrationForm.Price = decimal.Parse(Console.ReadLine()!);

        // Send the completed form to the service repository
        var result = await _productService.CreateProductAsync(productRegistrationForm);
        if (result != null)
        {
            Console.Write("PRODUCT WAS SUCCESSFULLY CREATED");
        }
        else
        {
            Console.WriteLine("PRODUCT WAS NOT CREATE");
        }
        Console.ReadKey();
    }
    private async Task GetAllProductsDialog()
    {
        Console.Clear();
        Console.WriteLine("-------- ALL PRODUCTS -------");
        var products = await _productService.GetAllProductsAsync();

        foreach (var product in products)
        {
            Console.WriteLine($"[{product.Id},{product.ProductName}, {product.Price}]");
        }
        Console.ReadKey();
    }
    private async Task UpdateProductDialog()
    {
        Console.Clear();
        var productUpdateForm = new ProductUpdateForm();

        Console.WriteLine("¤¤¤ UPDATE PRODUCT ¤¤¤");

        Console.Write("PRODUCT NUMBER: ");
        productUpdateForm.Id = int.Parse(Console.ReadLine()!);

        Console.Write("NEW PRODUCT NAME: ");
        productUpdateForm.ProductName = Console.ReadLine()!;
        Console.Write("NEW PRODUCT PRICE:");
        productUpdateForm.Price = decimal.Parse(Console.ReadLine()!);

        var result = await _productService.UpdateProductAsync(productUpdateForm);
        if (result != null)
        {
            Console.Write("PRODUCT WAS UPDATED SUCCESSFULLY");
        }
        else
        {
            Console.WriteLine("PRODUCT WAS NOT UPDATE");
        }

        Console.ReadKey();
    }
    private async Task DeleteProductDialog()
    {
        Console.Clear();

        Console.WriteLine("¤¤¤ DELETE PRODUCT ¤¤¤");

        Console.WriteLine("ENTER PROJECT ID TO DELETE :");

        if (int.TryParse(Console.ReadLine(), out int productId))
        {
            Console.WriteLine("ARE YOU SURE YOU WANT TO DELETE THIS PRODUCT? (YES/NO): ");

            var option = Console.ReadLine()?.Trim().ToLower();
            if (option == "yes")
            {
                var result = await _productService.DeleteProductAsync(productId);
                if (result)
                {
                    Console.WriteLine("PRODUCT WAS DELETED SUCCESSFULLY");
                }
            }
            else
            {
                Console.WriteLine("PRODUCT WAS NOT DELETED");
            }
            Console.ReadKey();
        }
    }

    #endregion
}
