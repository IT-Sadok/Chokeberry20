using LibraryManager.Enums;
using LibraryManager.Interfaces;
using LibraryManager.JsonHelpers;
using LibraryManager.Models;

namespace LibraryManager.Services;

public class HighLoadSimulator : IHighloadSimulator
{
    private readonly ILibraryManagementService _libraryService;
    
    public HighLoadSimulator(ILibraryManagementService libraryManagementService)
    { 
        _libraryService = libraryManagementService; 
    }

    public async Task RunAsync(int taskCounter)
    {
        var tasks =  new List<Task>();

        for (int i = 0; i < taskCounter; i++)
        {
            int taskId = i;
            tasks.Add(SimulateUserAction(taskId));
            
        }

        await Task.WhenAll(tasks);
    }

    private Task SimulateUserAction(int taskId)
    {
        var option = Random.Shared.Next(1, 8);

        try
        {
            switch (option)
            {
                case 1:
                    _libraryService.GetBooks();
                    break;
                case 2:
                    _libraryService.GetBooks(BookStatus.Available);
                    break;
                case 3:
                    _libraryService.AddBook(new Book()
                    {
                        Title = "Book",
                        Author = "Author",
                        Year = 1000
                    });
                    break;
                case 4:
                    _libraryService.UpdateBookById(Random.Shared.Next(1, 50), new Book()
                    {
                        Title = "Updated Book",
                        Author = "Updated Author",
                        Year = 2222
                    });
                    break;
                case 5:
                    _libraryService.DeleteBookById(Random.Shared.Next(1, 50));
                    break;
                case 6:
                    _libraryService.BorrowBook(Random.Shared.Next(1, 50));
                    break;
                case 7:
                    _libraryService.ReturnBook(Random.Shared.Next(1, 50));
                    break;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message} in task {taskId}!");
        }        
        
        return Task.CompletedTask;
    }
}