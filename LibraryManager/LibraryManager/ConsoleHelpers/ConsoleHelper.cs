using LibraryManager.Enums;
using LibraryManager.Interfaces;
using LibraryManager.Models;
using LibraryManager.Services;

namespace LibraryManager.ConsoleHelpers;

public class ConsoleHelper
{
    private readonly ILibraryManagementService _service;
    private readonly IHighloadSimulator _highLoadSimulator;
    private readonly int _taskCounter;

    public ConsoleHelper(ILibraryManagementService service, IHighloadSimulator highLoadSimulator, int taskCounter)
    {
        _service = service;
        _highLoadSimulator = highLoadSimulator;
        _taskCounter = taskCounter;
    }

    public async Task ShowModesAsync()
    {
        Console.WriteLine("-------------------------------------");
        Console.WriteLine("Please choose which mode you want to use:");
        Console.WriteLine("Enter 1 to switch to the default mode:");
        Console.WriteLine("Enter 2 to switch to the stress mode:");
        int mode = Int32.Parse(Console.ReadLine());
        
        switch (mode)
        {
            case 1:
                ShowDefaultConsoleMenu();
                break;
            case 2:
                Console.WriteLine("-------------------------------------");
                Console.WriteLine("Starting load test!");
                await _highLoadSimulator.RunAsync(_taskCounter);
                Console.WriteLine("Load test finished!");
                break;
            default:
                Console.WriteLine("Invalid mode! Restart the program!");
                break;
        }
    }
    
    public void ShowDefaultConsoleMenu()
    {
        var exit = false;

        while (!exit)
        {
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("You are in a default mode!");
            Console.WriteLine("Please choose one of the options:");
            ShowOptions();
            string input = Console.ReadLine();
            
            switch (input)
            {
                case "1":
                    Console.WriteLine("Sure! Here are all the books!");
                    DisplayBooks(_service.GetBooks());
                    break;
                case "2":
                    Console.WriteLine("Sure! Here are all available books!");
                    DisplayBooks(_service.GetBooks(BookStatus.Available));
                    break;
                case "3":
                    Console.WriteLine("Lets create a new book!");
                    var newBook = CreateBookFromInput();
                    _service.AddBook(newBook);
                    break;
                case "4":
                    Console.WriteLine("Lets update a book, enter an id!");
                    int idForUpdate =Int32.Parse(Console.ReadLine());
                    Console.WriteLine("Now enter new value for book!");
                    var updatedBook = CreateBookFromInput();
                    _service.UpdateBookById(idForUpdate, updatedBook);
                    break;
                case "5":
                    Console.WriteLine("No problem, enter id of the book you want to delete!");
                    int idForDelete =Int32.Parse(Console.ReadLine());
                    _service.DeleteBookById(idForDelete);
                    break;
                case "6":
                    Console.WriteLine("Sure! Enter title of the book you are looking for!");
                    string titleForSearch = Console.ReadLine();
                    DisplaySingleSearchResult(_service.GetBookByTitle(titleForSearch));
                    break;
                case "7":
                    Console.WriteLine("Sure! Enter author of the book you are looking for!");
                    string author = Console.ReadLine();
                    DisplaySingleSearchResult(_service.GetBookByAuthor(author));
                    break;
                case "8":
                    Console.WriteLine("Ok! Enter id of the book you want to borrow");
                    int idForBorrowing = Int32.Parse(Console.ReadLine());
                    _service.BorrowBook(idForBorrowing);
                    break;
                case "9":
                    Console.WriteLine("Great! Enter id of the book you want to return");
                    int idForReturning = Int32.Parse(Console.ReadLine());
                    _service.ReturnBook(idForReturning);
                    break;
                case "0":
                    exit = true;
                    break;
            } 
        } 
    }

    private static void ShowOptions()
    {
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("1 - List all books in the library");
        Console.WriteLine("2 - List only available books in the library");
        Console.WriteLine("3 - Add new book to the library");
        Console.WriteLine("4 - Update the book");
        Console.WriteLine("5 - Delete book from the library");
        Console.WriteLine("6 - Find book by title");
        Console.WriteLine("7 - Find book by author");
        Console.WriteLine("8 - Borrow a book from the library");
        Console.WriteLine("9 - Return a book to the library");
        Console.WriteLine("0 - Exit and save changes");
        Console.WriteLine("----------------------------------------");
    }

    private static Book CreateBookFromInput()
    {
        var newBook = new Book();
        
        Console.WriteLine("Please enter a book title!");
        newBook.Title = Console.ReadLine();
            
        Console.WriteLine("Please enter an author!");
        newBook.Author = Console.ReadLine();
            
        Console.WriteLine("Please enter a year!");
        newBook.Year = int.Parse(Console.ReadLine());

        return newBook;
    }

    private static void DisplayBooks(List<Book> books)
    {
        foreach (var book in books)
        {
            Console.WriteLine("-------------------------");
            Console.WriteLine($"Id - {book.Id}");
            Console.WriteLine($"Title - {book.Title}");
        };
    }

    private static void DisplaySingleSearchResult(Book book)
    {
        Console.WriteLine($"- {book.Title}");
    }
}