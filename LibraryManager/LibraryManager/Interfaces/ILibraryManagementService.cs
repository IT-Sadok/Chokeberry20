using LibraryManager.Models;

namespace LibraryManager.Interfaces;

public interface ILibraryManagementService
{
    public List<Book> GetBooks();
    public List<Book> GetAvailableBooks();
    public Book GetBookById(int id);
    public Book GetBookByTitle(string bookName);
    public bool AddBook(Book book);
    public bool UpdateBook(int id, Book book);
    public bool DeleteBookById(int id);
    public bool BorrowBook(int id);
    public bool ReturnBook(int id);
}