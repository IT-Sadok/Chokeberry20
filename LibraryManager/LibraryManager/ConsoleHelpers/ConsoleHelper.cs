using LibraryManager.Models;
using LibraryManager.Services;

namespace LibraryManager.ConsoleHelpers;

public class ConsoleHelper
{
    public static void ShowConsoleMenu(LibraryManagementService service)
    {
        var exit = false;

        while (!exit)
        {
            Console.WriteLine("Please choose one of the options:");
            ShowOptions();
            string input = Console.ReadLine();
            
            switch (input)
            {
                case "1":
                    Console.WriteLine("Sure! Here are all the books!");
                    DisplayResult(service.GetBooks());
                    break;
                case "2":
                    Console.WriteLine("Sure! Here are all available books!");
                    DisplayResult(service.GetAvailableBooks());
                    break;
                case "3":
                    Console.WriteLine("Lets create a new book!");
                    var newBook = CreateBookFromInput();
                    service.AddBook(newBook);
                    break;
                case "4":
                    Console.WriteLine("No problem, enter id of the book you want to delete!");
                    int idForDeletion = ValidateInput();
                    service.DeleteBookById(idForDeletion);
                    break;
                case "5":
                    Console.WriteLine("Sure! Enter title of the book you are looking for!");
                    string title = Console.ReadLine();
                    DisplaySingleResult(service.GetBookByTitle(title));
                    break;
                case "6":
                    Console.WriteLine("Sure! Enter author of the book you are looking for!");
                    string author = Console.ReadLine();
                    DisplaySingleResult(service.GetBookByAuthor(author));
                    break;
                case "7":
                    Console.WriteLine("Ok! Enter id of the book you want to borrow");
                    int idForBorrowing = ValidateInput();
                    service.BorrowBook(idForBorrowing);
                    break;
                case "8":
                    Console.WriteLine("Great! Enter id of the book you want to return");
                    int idForReturning = ValidateInput();
                    service.ReturnBook(idForReturning);
                    break;
                case "0":
                    exit = true;
                    break;
            } 
        } 
    }

    public static void ShowOptions()
    {
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("1 - List all books in the library");
        Console.WriteLine("2 - List only available books in the library");
        Console.WriteLine("3 - Add new book to the library");
        Console.WriteLine("4 - Delete book from the library");
        Console.WriteLine("5 - Find book by title");
        Console.WriteLine("6 - Find book by author");
        Console.WriteLine("7 - Borrow a book from the library");
        Console.WriteLine("8 - Return a book to the library");
        Console.WriteLine("0 - Exit the library");
        Console.WriteLine("----------------------------------------");
    }

    public static Book CreateBookFromInput()
    {
        var newBook = new Book();
        var formValid = false;

        while (!formValid)
        {
            try
            {
                Console.WriteLine("Please enter a unique number id!");
                newBook.Id = int.Parse(Console.ReadLine());
            
                Console.WriteLine("Please enter a book title!");
                newBook.Title = Console.ReadLine();
            
                Console.WriteLine("Please enter an author!");
                newBook.Author = Console.ReadLine();
            
                Console.WriteLine("Please enter a year!");
                newBook.Year = int.Parse(Console.ReadLine());
                
                formValid = true;
            }
            catch (Exception e) 
            {
                Console.WriteLine("WITH NUMBERS!!!");
            }
        }

        return newBook;
    }
    
    public static int ValidateInput()
    {
        var inputValid = false;
        int input = 0;
        
        while (!inputValid)
        {
            try
            {
                input =  int.Parse(Console.ReadLine());
                
                inputValid = true;
            }
            catch (Exception e) 
            {
                Console.WriteLine("WITH NUMBERS!!!");
            }
        }

        return input;
    }

    public static void DisplayResult(List<Book> books)
    {
        foreach (var book in books)
        {
            Console.WriteLine($"- {book.Title}");
        };
    }
    
    public static void DisplaySingleResult(Book book)
    {
        Console.WriteLine($"- {book.Title}");
    }
}