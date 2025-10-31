using LibraryManager.Models;

namespace LibraryManager.Interfaces;

public interface IHighloadSimulator
{
    public Task RunAsync(int taskCounter);
}