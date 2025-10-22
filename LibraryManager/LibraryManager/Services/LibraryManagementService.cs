using LibraryManager.Enums;
using LibraryManager.Interfaces;
using LibraryManager.JsonHelpers;
using LibraryManager.Models;
using LibraryManager.Validators;

namespace LibraryManager.Services;

public class LibraryManagementService : ILibraryManagementService
{
    private readonly string _filePath;
    private readonly List<Book> _books;

    public LibraryManagementService(string filePath)
    {
        _filePath = filePath;
        _books = DataStorage.GetAllBooksFromJsonFile(_filePath);
    }

    public List<Book> GetBooks(BookStatus? status = null)
    {
        if (status == BookStatus.Available)
        {
            return _books.Where(b => b.Status == BookStatus.Available).ToList();
        }
        
        return _books;
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
            throw new Exception($"Book with {title} not found!");
        }

        return book;
    }
    
    public Book GetBookByTitle(string title)
    {
        var book = _books.FirstOrDefault(b => b.Title == title);

        if (book == null)
        {
            throw new Exception($"Book with {title} not found!");
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

    public void UpdateBookByTitle(string searchTitle, Book updatedBook)
    {
        if (InputValidators.IsStringValid(searchTitle) || InputValidators.AreBookPropertiesValid(updatedBook))
        { 
            var oldBook = _books.FirstOrDefault(b => b.Title == searchTitle);
        
            if (oldBook == null)
            {
                throw new Exception($"Book with {searchTitle} not found!");
            }
            
            oldBook.Title = updatedBook.Title;
            oldBook.Author = updatedBook.Author;
            oldBook.Year = updatedBook.Year;
            oldBook.Status = updatedBook.Status;
        }
    }

    public void DeleteBookByTitle(string title)
    {
        var oldBook = GetBookByTitle(title);

        if (oldBook == null)
        {
            throw new Exception($"Book with {title} not found!");
        }
        
        _books.RemoveAll(b => b.Title == title);
    }

    public void BorrowBook(string title)
    {
        var borrowedBook = GetBookByTitle(title);

        if (borrowedBook.Status == BookStatus.Borrowed)
        {
            throw new Exception($"Book with {title} is already borrowed!");
        }
        
        borrowedBook.Status = BookStatus.Borrowed;
        UpdateBookByTitle(title, borrowedBook);
    }
    
    public void ReturnBook(string title)
    {
        var borrowedBook = GetBookByTitle(title);

        if (borrowedBook.Status == BookStatus.Available)
        {
            throw new Exception($"Book with {title} is not borrowed yet!");
        }
        
        borrowedBook.Status = BookStatus.Available;
        UpdateBookByTitle(title, borrowedBook);
    }

    public void SaveChanges()
    {
        DataStorage.AddBooksToJsonFile(_books, _filePath);
    }
}