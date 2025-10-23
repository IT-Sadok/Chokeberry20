namespace LibraryManager.Exceptions;

public class BookNotBorrowedException : Exception
{
    public BookNotBorrowedException(string  message) : base(message) { }
}