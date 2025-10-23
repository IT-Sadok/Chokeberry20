using LibraryManager.Enums;
using LibraryManager.Exceptions;
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

    public Book GetBookById(int id)
    {
        var book = _books.FirstOrDefault(b => b.Id == id);

        if (book == null)
        {
            throw new BookNotFoundException($"Book with id {id} not found!");
        }

        return book;
    }
    
    public Book GetBookByAuthor(string author)
    {
        var book = _books.FirstOrDefault(b => b.Author == author);

        if (book == null)
        {
            throw new BookNotFoundException($"Book with author {author} not found!");
        }

        return book;
    }
    
    public Book GetBookByTitle(string title)
    {
        var book = _books.FirstOrDefault(b => b.Title == title);

        if (book == null)
        {
            throw new BookNotFoundException($"Book with title {title} not found!");
        }

        return book;
    }
    public void AddBook(Book book)
    {
        book.Id = CreateUniqueId();
            
        if (InputValidators.AreBookPropertiesValid(book))
        {
            _books.Add(book);
        }
    }

    public void UpdateBookById(int id, Book updatedBook)
    {
        if (InputValidators.AreBookPropertiesValid(updatedBook))
        { 
            var oldBook = _books.FirstOrDefault(b => b.Id == id);
        
            if (oldBook == null)
            {
                throw new BookNotFoundException($"Book with id {id} not found!");
            }
            
            oldBook.Title = updatedBook.Title;
            oldBook.Author = updatedBook.Author;
            oldBook.Year = updatedBook.Year;
            oldBook.Status = updatedBook.Status;
        }
    }

    public void DeleteBookById(int id)
    {
        var oldBook = GetBookById(id);

        if (oldBook == null)
        {
            throw new BookNotFoundException($"Book with id {id} not found!");
        }
        
        _books.RemoveAll(b => b.Id == id);
    }

    public void BorrowBook(int id)
    {
        var borrowedBook = GetBookById(id);

        if (borrowedBook.Status == BookStatus.Borrowed)
        {
            throw new BookAlreadyBorrowedException($"Book with id {id} is already borrowed!");
        }
        
        borrowedBook.Status = BookStatus.Borrowed;
        UpdateBookById(id, borrowedBook);
    }
    
    public void ReturnBook(int id)
    {
        var borrowedBook = GetBookById(id);

        if (borrowedBook.Status == BookStatus.Available)
        {
            throw new BookNotBorrowedException($"Book with id {id} is not borrowed yet!");
        }
        
        borrowedBook.Status = BookStatus.Available;
        UpdateBookById(id, borrowedBook);
    }

    public void SaveChanges()
    {
        DataStorage.AddBooksToJsonFile(_books, _filePath);
    }

    private int CreateUniqueId()
    {
        if (_books.Count == 0)
        {
            return 1;
        }
        
        int newId = _books.Max(b => b.Id) + 1;

        return newId;
    }
}