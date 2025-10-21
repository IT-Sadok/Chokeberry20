using LibraryManager.Models;

namespace LibraryManager.Validators;

public class InputValidators
{
    public static bool AreBookPropertiesValid(Book book)
    {
        if (book.Year !> DateTime.Now.Year && book.Year !< 0)
        {
            throw new Exception("Invalid year!");
        }
        else if (!IsStringValid(book.Author))
        {
            throw new Exception("Invalid author!");
        }
        else if (!IsStringValid(book.Author))
        {
            throw new Exception("Invalid title!");
        }

        return true;
    }

    public static bool IsStringValid(string stringToValidate)
    {
        if (stringToValidate != null && stringToValidate != String.Empty)
        {
            return true;
        }
        
        return false;
    }
}