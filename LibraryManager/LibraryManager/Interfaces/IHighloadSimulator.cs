using LibraryManager.Models;

namespace LibraryManager.Interfaces;

public interface IHighloadSimulator
{
    public Task AddBookHighLoad(int counter, Book book);

    public Task UpdateBookByIdHighLoad(int counter, int id, Book updatedBook);

    public Task DeleteBookHighLoad(int counter, int id);

    public Task BorrowBookHighLoad(int counter, int id);

    public Task ReturnBookHighLoad(int counter, int id);
}