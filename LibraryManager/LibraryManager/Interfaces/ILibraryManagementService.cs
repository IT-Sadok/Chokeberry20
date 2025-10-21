using LibraryManager.Models;

namespace LibraryManager.Interfaces;

public interface ILibraryManagementService
{
    public List<Book> GetBooks();
    public List<Book> GetAvailableBooks();
    public Book GetBookByAuthor(string author);
    public Book GetBookByTitle(string title);
    public void AddBook(Book book);
    public void UpdateBook(string title, Book book);
    public void DeleteBookByTitle(string title);
    public void BorrowBook(string title);
    public void ReturnBook(string title);
}