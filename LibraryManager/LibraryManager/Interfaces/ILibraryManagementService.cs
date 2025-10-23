using LibraryManager.Enums;
using LibraryManager.Models;

namespace LibraryManager.Interfaces;

public interface ILibraryManagementService
{
    public List<Book> GetBooks(BookStatus? status = null);
    public Book GetBookByAuthor(string author);
    public Book GetBookByTitle(string title);
    public void AddBook(Book book);
    public void UpdateBookById(int id, Book book);
    public void DeleteBookById(int id);
    public void BorrowBook(int id);
    public void ReturnBook(int id);
}