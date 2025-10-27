using LibraryManager.Interfaces;
using LibraryManager.Models;

namespace LibraryManager.Services;

public class HighLoadSimulator : IHighloadSimulator
{
    private readonly ILibraryManagementService _libraryService;
    
    public HighLoadSimulator(ILibraryManagementService libraryManagementService)
    { 
        _libraryService = libraryManagementService;   
    }

    public async Task AddBookHighLoad(int counter, Book newBook)
    {
        var tasks = new List<Task>();
        var lockObject = new object();

        for (int i = 0; i < counter; i++)
        {
            var bookToAdd = new Book()
            {
                Title = newBook.Title,
                Author = newBook.Author,
                Year = newBook.Year
            };
            tasks.Add(Task.Run(() =>
            {
                lock (lockObject)
                {
                    _libraryService.AddBook(bookToAdd);
                }
            }));
        }

        await Task.WhenAll(tasks);
    }

    public async Task UpdateBookByIdHighLoad(int counter, int id, Book updatedBook)
    {
        var tasks = new List<Task>();
        var lockObject = new object();

        for (int i = 0; i < counter; i++)
        {
            tasks.Add(Task.Run(() =>
            {
                lock (lockObject)
                {
                    _libraryService.UpdateBookById(id, updatedBook);
                }
            }));
        }

        await Task.WhenAll(tasks);
    }
    
    public async Task DeleteBookHighLoad(int counter,  int id)
    {
        var tasks = new List<Task>();
        var lockObject = new object();

        for (int i = 0; i < counter; i++)
        {
            tasks.Add(Task.Run(() =>
            {
                lock (lockObject)
                {
                    _libraryService.DeleteBookById(id);
                }
            }));
        }

        await Task.WhenAll(tasks);
    }
    
    public async Task BorrowBookHighLoad(int counter, int id)
    {
        var tasks = new List<Task>();
        var lockObject = new object();

        for (int i = 0; i < counter; i++)
        {
            tasks.Add(Task.Run(() =>
            {
                lock (lockObject)
                {
                    _libraryService.BorrowBook(id);
                }
            }));
        }

        await Task.WhenAll(tasks);
    }
    
    public async Task ReturnBookHighLoad(int counter, int id)
    {
        var tasks = new List<Task>();
        var lockObject = new object();

        for (int i = 0; i < counter; i++)
        {
            tasks.Add(Task.Run(() =>
            {
                lock (lockObject)
                {
                    _libraryService.ReturnBook(id);
                }
            }));
        }

        await Task.WhenAll(tasks);
    }
}