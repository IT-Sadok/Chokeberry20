using System.Text.Json;
using LibraryManager.Models;

namespace LibraryManager.JsonHelpers;

public class JsonListConverter
{
    public static void AddBooksToJsonFile(List<Book> books, string filePath)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };
        
        var json = JsonSerializer.Serialize(books, options);
            
        File.WriteAllText(filePath, json);
    }
    public static List<Book> GetAllBooksFromJsonFile(string filePath)
    {
        string json = File.ReadAllText(filePath);
        
        var listOfBooks = new List<Book>();

        if (json.Length == 0)
        {
            return listOfBooks;
        }
        
        var books = JsonSerializer.Deserialize<List<Book>>(json);
        listOfBooks.AddRange(books);
        return listOfBooks;
    }
}