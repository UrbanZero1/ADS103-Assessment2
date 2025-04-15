// ADS103 Assessment 2 Task 2
// Adam Olesh

public class Task2
{
    public static void Main(string[] args)
    {
        Library library = new Library();
        bool continueMenu = true;
        
        // Test data for books
        library.testBooks();
        
        // Main menu loop
        while (continueMenu)
        {
            continueMenu = library.menu();
        }
    }
}


public class Library
{
    List<int> borrowedBooks;
    private Dictionary<int, string> books;

    public Library()
    {
        borrowedBooks = new List<int>();
        books = new Dictionary<int, string>();
    }

    // Menu method to display options and handle user input
    // Added a switch statement to handle user input and call the appropriate method
    // Added a boolean return type to allow the main loop to continue or exit based on user input
    public bool menu()
    {
        Console.Write("What would you like to do?\n" +
                      "1) List all book you have on loan\n" +
                      "2) Return a book\n" +
                      "3) List all books in the library\n" +
                      "4) Borrow a book\n" +
                      "5) Exit\n" +
                      "\nEnter Choice (1-5): ");
        string choice = Console.ReadLine();
        
        switch (choice)
        {
            case "1":
                listLoanedBooks();
                break;
            case "2":
                returnBook();
                break;
            case "3":
                listBooksInLibrary();
                break;
            case "4":
                borrowBook();
                break;
            case "5":
                Console.WriteLine("Exiting...");
                return false;
            default:
                Console.WriteLine("Invalid choice, please try again.");
                break;
        }
        return true;
    }
    
    // Uses a foreach loop to iterate through the borrowedBooks list and print each book
    private void listLoanedBooks()
    {
        Console.WriteLine("\nBooks on loan:");
        foreach (int id in borrowedBooks)
        {
            Console.WriteLine($"{id}: {books[id]}");
        }
        Console.WriteLine();
    }
    
    // Asks the user for the book ID to return, checks if it's valid, and removes it from the borrowedBooks list
    private void returnBook()
    {
        Console.Write("\nBook ID to return: ");
        string input = Console.ReadLine();
        if (int.TryParse(input, out int id))
        {
            if (borrowedBooks.Contains(id))
            {
                borrowedBooks.Remove(id);
                Console.WriteLine($"Book {id} returned successfully.");
            }
            else
            {
                Console.WriteLine($"Book {id} is not on loan.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid book ID.");
        }
        Console.WriteLine();
    }
    
    // Uses a foreach loop to iterate through the books dictionary and print each book
    private void listBooksInLibrary()
    {
        Console.WriteLine("\nBooks in the library:");
        foreach ((int id, string title) in books)
        {
            Console.WriteLine($"{id}: {title}");
        }
        Console.WriteLine();
    }
    
    // Asks the user for the book ID to borrow, checks if it's valid and not already borrowed, and adds it to the borrowedBooks list
    private void borrowBook()
    {
        Console.Write("\nBook ID to borrow: ");
        string input = Console.ReadLine();
        if (int.TryParse(input, out int id))
        {
            if (books.ContainsKey(id))
            {
                if (!borrowedBooks.Contains(id))
                {
                    borrowedBooks.Add(id);
                    Console.WriteLine($"Book {id} borrowed successfully.");
                }
                else
                {
                    Console.WriteLine($"Book {id} is already on loan.");
                }
            }
            else
            {
                Console.WriteLine($"Book {id} does not exist in the library.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid book ID.");
        }
        Console.WriteLine();
    }

    // Test data for books
    public void testBooks()
    {
        books.Add(1, "The Great Gatsby");
        books.Add(2, "To Kill a Mockingbird");
        books.Add(3, "1984");
        books.Add(4, "Pride and Prejudice");
        books.Add(5, "The Catcher in the Rye");
        books.Add(6, "The Lord of the Rings");
        books.Add(7, "The Hobbit");
        books.Add(8, "Fahrenheit 451");
        books.Add(9, "Brave New World");
        books.Add(10, "The Picture of Dorian Gray");
    }
    
}