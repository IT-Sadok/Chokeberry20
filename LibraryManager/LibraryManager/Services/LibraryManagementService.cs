using LibraryManager.Interfaces;
using LibraryManager.JsonHelpers;
using LibraryManager.Models;

namespace LibraryManager.Services;

public class LibraryManagementService : ILibraryManagementService
{
    const string PATH = @"..\..\..\Data\booksData.json";
    public List<Book> GetBooks()
    {
        var books = JsonConverter.GetAllBooksFromJsonFile(PATH);

        return books;
    }

    public List<Book> GetAvailableBooks()
    {
        var books = GetBooks();
        var availableBooks = books.Where(b => b.IsAvailable).ToList();
        
        return availableBooks;
    }

    public Book GetBookById(int id)
    {
        var list = JsonConverter.GetAllBooksFromJsonFile(PATH);
        var book = list.Where(b => b.Id == id).FirstOrDefault();

        if (book == null)
        {
            Console.WriteLine("Book not found!"); 
        }

        return book;
    }
    
    public Book GetBookByAuthor(string title)
    {
        var list = JsonConverter.GetAllBooksFromJsonFile(PATH);
        var book = list.Where(b => b.Author == title).FirstOrDefault();

        if (book == null)
        {
            Console.WriteLine("Book not found!");
        }

        return book;
    }
    
    public Book GetBookByTitle(string title)
    {
        var list = JsonConverter.GetAllBooksFromJsonFile(PATH);
        var book = list.Where(b => b.Title == title).FirstOrDefault();

        if (book == null)
        {
            Console.WriteLine("Book not found!");
        }

        return book;
    }
    public bool AddBook(Book book)
    {
        var existingBooks = JsonConverter.GetAllBooksFromJsonFile(PATH);

        if (!existingBooks.All(b => b.Id != book.Id))
        {
            Console.WriteLine("Book with same ID has already been added!");
            return false;
        }
        existingBooks.Add(book);
        JsonConverter.AddBooksToJsonFile(existingBooks, PATH);

        Console.WriteLine("Successfully added new book!");
        return true;
    }

    public bool UpdateBook(int id, Book updatedBook)
    {
        var books = GetBooks();
        var oldBook = GetBookById(id);

        if (oldBook == null)
        {
            return false;
        }
        
        updatedBook.Id = oldBook.Id;
        
        books.RemoveAll(b => b.Id == id);
        books.Add(updatedBook);
        
        JsonConverter.AddBooksToJsonFile(books, PATH);

        Console.WriteLine("Successfully updated book with ID: " + id);
        return true;
    }

    public bool DeleteBookById(int id)
    {
        var books = GetBooks();
        var oldBook = GetBookById(id);

        if (oldBook == null)
        {
            return false;
        }
        
        books.RemoveAll(b => b.Id == id);
        
        JsonConverter.AddBooksToJsonFile(books, PATH);

        Console.WriteLine("Successfully deleted book with ID: " + id);
        return true;
    }

    public bool BorrowBook(int id)
    {
        var books = GetBooks();
        var borrowedBook = GetBookById(id);

        if (!borrowedBook.IsAvailable)
        {
            Console.WriteLine("Unfortunately this book is already borrowed!");
            return false;
        }
        
        borrowedBook.IsAvailable = false;
        UpdateBook(id, borrowedBook);

        Console.WriteLine("Successfully borrowed book with ID: " + id);
        return true;
    }
    
    public bool ReturnBook(int id)
    {
        var books = GetBooks();
        var borrowedBook = GetBookById(id);

        if (borrowedBook.IsAvailable)
        {
            Console.WriteLine("This book is not borrowed yet!");
            return false;
        }
        
        borrowedBook.IsAvailable = true;
        UpdateBook(id, borrowedBook);

        Console.WriteLine("Successfully returned book with ID: " + id);
        return true;
    }
}