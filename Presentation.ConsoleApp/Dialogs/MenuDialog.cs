using System;
using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;

namespace Presentation.ConsoleApp.Dialogs;

public class MenuDialog(IProjectService projectService)
{
    private readonly IProjectService _projectService= projectService;

    public async Task ShowMenu()
    {
        var isRunning = true;
        do
        {
            Console.Clear();
            Console.WriteLine("----- MainMenu -----");
            Console.WriteLine("1. ADD NEW PROJECT ");
            Console.WriteLine("2. VIEW ALL PROJECT ");
            Console.WriteLine("3. UPDATE PROJECT ");
            Console.WriteLine("4. DELELTE PROJECT ");
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
                    await GetAllProjectDialog();
                    break;

                case "3":
                    await UpdateProjectDialog();
                    break;
                case "4":
                    await DeleteProjectDialog();
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

    private async Task CreateProjectDialog()
    {
        Console.Clear ();

        ProjectRegistrationForm projectRegistrationForm = ProjectFactory.Create();

        Console.WriteLine("¤¤¤ CREATE PROJECT ¤¤¤");

        Console.Write("ENTER PROJECT NAME:");
        var projectName = Console.ReadLine()!;

        Console.Write("ENTER START DATE");
        var startDate = Console.ReadLine()!;

        Console.Write("ENTER END DATE");
        var endDate = Console.ReadLine()!;

        Console.Write("");
        var customerId = Console.ReadLine()!;

        Console.Write("");
        var statusId = Console.ReadLine()!;

        Console.Write("");
        var userId = Console.ReadLine()!;

        Console.Write("");
       var productId= Console.ReadLine()!;

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
    private async Task GetAllProjectDialog()
    {
        Console.Clear();
        Console.WriteLine("-------- ALL PROJECTS -------");
        var projects = await _projectService.GetAllProjectsAsync();

        foreach (var project in projects)
        {
            Console.WriteLine($"[{project.Id},{project.ProjectName},{project.StartDate},{project.EndDate},{project.CustomerId},{project.StatusId},{project.UserId},{project.ProductId}]");
        }
        Console.ReadKey();
    }

    private async Task UpdateProjectDialog()
    {
        Console.Clear();
        ProjectUpdateForm projectUpdateForm = new ProjectUpdateForm();

        Console.WriteLine("¤¤¤ UPDATE PROJECT ¤¤¤");

        Console.Write("NEW PROJECT NAME:");
        var projectName = Console.ReadLine()!;

        Console.Write("NEW START DATE");
        var startDate = Console.ReadLine()!;

        Console.Write("NEW END DATE");
        var endDate = Console.ReadLine()!;

        Console.Write("");
        var customerId = Console.ReadLine()!;

        Console.Write("");
        var statusId = Console.ReadLine()!;

        Console.Write("");
        var userId = Console.ReadLine()!;

        Console.Write("");
        var productId = Console.ReadLine()!;

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

}
