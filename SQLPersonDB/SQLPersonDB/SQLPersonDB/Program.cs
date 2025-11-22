using Microsoft.Extensions.Configuration;
using DataAccessLibrary;
using DataAccessLibrary.Models;

namespace SQLPersonDB;

public class Program
{
    static void Main(string[] args)
    {
        SqlCrud sql = new SqlCrud(GetConnectionString());

        Menu(sql);

        Console.WriteLine("\nDone processing...");
        Console.ReadKey();
    }

    private static void Menu(SqlCrud sql)
    {
        string? input = "";

        do
        {
            Console.WriteLine("\nWelcome to Personal Information System");
            Console.WriteLine("------------------------------------------");

            Console.WriteLine("1 - Create a new person");
            Console.WriteLine("2a - Retrieve an existing person");
            Console.WriteLine("2b - Retrieve all existing person");
            Console.WriteLine("3 - Update an existing person");
            Console.WriteLine("4 - Delete an existing person");
            Console.WriteLine("exit - To close the program");
            input = Console.ReadLine().ToLower();

            switch (input)
            {
                case "1":
                    Console.Clear();
                    CreateNewPerson(sql);
                    break;
                case "2a":
                    Console.Clear();
                    int contactId = Helper.AskUserForInt("What is the ID: ");
                    ReadPerson(sql, contactId);
                    break;
                case "2b":
                    Console.Clear();
                    ReadAllContacts(sql);
                    break;
                case "3":
                    Console.Clear();
                    UpdatePerson(sql);
                    break;
                case "4":
                    Console.Clear();
                    DeletePerson(sql);
                    break;
                case "exit":
                    Console.Clear();
                    Console.WriteLine("Thanks for using the program!");
                    break;
            };
        } while (input != "exit");


    }


    private static void ReadAllContacts(SqlCrud sql)
    {
        var rows = sql.GetAllContacts();

        Console.WriteLine($"Person List (Count: {rows.Count} as of {DateTime.Now})");
        foreach (var row in rows)
        {
            Console.WriteLine($"{row.FirstName}: {row.FirstName} {row.LastName}");
        }
    }

    private static void CreateNewPerson(SqlCrud sql)
    {
        string? firstName = Helper.AskUser("What is your first name: ");
        string? lastName = Helper.AskUser("What is your last name: ");

        //BasicPersonModel person = new BasicPersonModel
        //{
        //    FirstName = firstName,
        //    LastName = lastName
        //};

        FullPersonModel person = new FullPersonModel
        {
            BasicPerson = new BasicPersonModel
            {
                FirstName = firstName,
                LastName = lastName
            }
        };

        sql.CreatePerson(person);
    }
    private static void ReadPerson(SqlCrud sql, int id)
    {
        var person = sql.ReadPersonById(id);

        Console.WriteLine($"\n[{person.BasicPerson.Id}: {person.BasicPerson.FirstName} {person.BasicPerson.LastName}]");
    }

    private static void UpdatePerson(SqlCrud sql)
    {
        BasicPersonModel person = new BasicPersonModel();

        person.Id = Helper.AskUserForInt("What is the ID you wish to edit: ");

        Console.WriteLine("\nYou are editing...");
        ReadPerson(sql, person.Id);
        Console.WriteLine("--------------------------------------");

        person.FirstName = Helper.AskUser("What will be the new first name: ");
        person.LastName = Helper.AskUser("What will be the new last name: ");

        sql.UpdatePersonName(person);
    }
    private static void DeletePerson(SqlCrud sql)
    {
        int id = Helper.AskUserForInt("What is the ID you wish to delete: ");

        Console.WriteLine("\nYou are deleting...");
        ReadPerson(sql, id);
        Console.WriteLine("-------------------------------------\n");
        
        string input = Helper.AskUser("Are you sure you want to delete this person? (Y/N)");

        if (input == "y") sql.DeletePerson(id);
    }

    private static string GetConnectionString(string connectionStringName = "Default")
    {
        string output = "";

        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

        var config = builder.Build();

        output = config.GetConnectionString(connectionStringName);

        return output;
    }
}

