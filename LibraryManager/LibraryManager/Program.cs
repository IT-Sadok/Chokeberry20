using LibraryManager.ConsoleHelpers;
using LibraryManager.Services;

namespace LibraryManager;

class Program
{
    private static readonly string FileName = "booksData.json";
    static void Main(string[] args)
    {
        if (File.Exists(FileName))
        {
            Console.WriteLine("File exists!");
        }
        else
        {
            Console.WriteLine("File does not exist, creating...");
            File.WriteAllText(FileName, "");
            Console.WriteLine("File created!");
        }

        LibraryManagementService  service = new LibraryManagementService(FileName);
        HighLoadSimulator highLoadSimulator = new HighLoadSimulator(service);

        Console.WriteLine("Welcome to the Library Manager!");

        try
        {
            ConsoleHelper helper = new ConsoleHelper(service, highLoadSimulator);
            helper.ShowModes();
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Ooops! {ex.Message}");
        }
        finally
        {
            service.SaveChanges();
        }  
    }
}