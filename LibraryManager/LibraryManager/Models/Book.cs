using LibraryManager.Enums;

namespace LibraryManager.Models;

public class Book
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public int Year { get; set; }
    public BookStatus Status { get; set; }
}