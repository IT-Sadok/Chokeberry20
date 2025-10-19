using LibraryManager.ConsoleHelpers;
using LibraryManager.Services;

namespace LibraryManager;

class Program
{
    static void Main(string[] args)
    {
        LibraryManagementService  service = new LibraryManagementService();
        
        const string PATH = @"..\..\..\Data\booksData.json";
        
        if (File.Exists(PATH))
        {
            Console.WriteLine("File exists");
        }
        else
        {
            Console.WriteLine("File does not exist, creating...");
            File.WriteAllText(PATH, "");
            Console.WriteLine("File created");
        }

        Console.WriteLine("Welcome to the Library Manager!");
        ConsoleHelper.ShowConsoleMenu(service);
    }
}