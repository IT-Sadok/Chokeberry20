using LibraryManager.Enums;
using LibraryManager.Models;

namespace LibraryManager.Interfaces;

public interface ILibraryManagementService
{
    public List<Book> GetBooks(BookStatus? status = null);
    public Book GetBookByAuthor(string author);
    public Book GetBookByTitle(string title);
    public void AddBook(Book book);
    public void UpdateBookByTitle(string title, Book book);
    public void DeleteBookByTitle(string title);
    public void BorrowBook(string title);
    public void ReturnBook(string title);
}