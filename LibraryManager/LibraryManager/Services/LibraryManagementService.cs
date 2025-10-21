using LibraryManager.Interfaces;
using LibraryManager.JsonHelpers;
using LibraryManager.Models;
using LibraryManager.Validators;

namespace LibraryManager.Services;

public class LibraryManagementService : ILibraryManagementService
{
    private readonly string _path;
    
    private readonly List<Book> _books;

    public LibraryManagementService()
    {
        _path = Path.GetFullPath(Program.FileName);
        _books = JsonListConverter.GetAllBooksFromJsonFile(_path);
    }

    public List<Book> GetBooks()
    {
        return _books;
    }

    public List<Book> GetAvailableBooks()
    {
        return _books.Where(b => b.IsAvailable).ToList();
    }

    public Book GetBookById(Guid id)
    {
        var book = _books.FirstOrDefault(b => b.Id == id);

        if (book == null)
        {
            throw new Exception("Book not found!");
        }

        return book;
    }
    
    public Book GetBookByAuthor(string title)
    {
        var book = _books.FirstOrDefault(b => b.Author == title);

        if (book == null)
        {
            throw new Exception("Book not found!");
        }

        return book;
    }
    
    public Book GetBookByTitle(string title)
    {
        var book = _books.FirstOrDefault(b => b.Title == title);

        if (book == null)
        {
            throw new Exception("Book not found!");
        }

        return book;
    }
    public void AddBook(Book book)
    {
        book.Id = Guid.NewGuid();

        if (InputValidators.AreBookPropertiesValid(book))
        {
            _books.Add(book);
        }
    }

    public void UpdateBook(string title, Book updatedBook)
    {
        if (InputValidators.IsStringValid(title) || InputValidators.AreBookPropertiesValid(updatedBook))
        { 
            var oldBook = _books.FirstOrDefault(b => b.Title == title);
        
            if (oldBook == null)
            {
                throw new Exception("Book not found!");
            }
            
            oldBook.Title = updatedBook.Title;
            oldBook.Author = updatedBook.Author;
            oldBook.Year = updatedBook.Year;
            oldBook.IsAvailable = updatedBook.IsAvailable;
        }
    }

    public void DeleteBookByTitle(string title)
    {
        var oldBook = GetBookByTitle(title);

        if (oldBook == null)
        {
            throw new Exception("Book not found!");
        }
        
        _books.RemoveAll(b => b.Title == title);
    }

    public void BorrowBook(string title)
    {
        var borrowedBook = GetBookByTitle(title);

        if (!borrowedBook.IsAvailable)
        {
            throw new Exception("Book is already borrowed!");
        }
        
        borrowedBook.IsAvailable = false;
        UpdateBook(title, borrowedBook);
    }
    
    public void ReturnBook(string title)
    {
        var borrowedBook = GetBookByTitle(title);

        if (borrowedBook.IsAvailable)
        {
            throw new Exception("Book is not borrowed yet!");
        }
        
        borrowedBook.IsAvailable = true;
        UpdateBook(title, borrowedBook);
    }

    public void SaveChanges()
    {
        JsonListConverter.AddBooksToJsonFile(_books, _path);
    }

    
}