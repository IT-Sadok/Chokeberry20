using LibraryManager.Models;

namespace LibraryManager.Interfaces;

public interface ILibraryManagementService
{
    public List<Book> GetBooks();
    public List<Book> GetAvailableBooks();
    public Book GetBookByAuthor(string author);
    public Book GetBookByTitle(string title);
    public bool AddBook(Book book);
    public bool UpdateBook(int id, Book book);
    public bool DeleteBookById(int id);
    public bool BorrowBook(int id);
    public bool ReturnBook(int id);
}