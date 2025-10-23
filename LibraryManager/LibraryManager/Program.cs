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

        Console.WriteLine("Welcome to the Library Manager!");

        try
        {
            ConsoleHelper.ShowConsoleMenu(service);
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