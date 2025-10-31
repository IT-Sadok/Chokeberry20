using LibraryManager.Exceptions;
using LibraryManager.Models;

namespace LibraryManager.Validators;

public class InputValidators
{
    public static bool AreBookPropertiesValid(Book book)
    {
        if (book.Year !> DateTime.Now.Year && book.Year !< 0)
        {
            throw new InvalidValueException("Invalid year!");
        }
        else if (!IsStringValid(book.Author))
        {
            throw new InvalidValueException("Invalid author!");
        }
        else if (!IsStringValid(book.Author))
        {
            throw new InvalidValueException("Invalid title!");
        }

        return true;
    }

    public static bool IsStringValid(string stringToValidate)
    {
        if (string.IsNullOrEmpty(stringToValidate) || string.IsNullOrWhiteSpace(stringToValidate))
        {
            return false;
        }
        
        return true;
    }
}