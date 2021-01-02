using System;
using System.Collections.Generic;

namespace RecordDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Record1 r1a = new("FirstName1", "LastName1");
            Record1 r2a = new("FirstName1", "LastName1");
            Record1 r3a = new("FirstName3", "LastName3");

            class1 c1a = new class1("ClassFirstName1", "ClassLastName1");
            class1 c2a = new class1("ClassFirstName1", "ClassLastName1");
            class1 c3a = new class1("ClassFirstName3", "ClassLastName3");

            Console.WriteLine("Record Type:");
            Console.WriteLine($"To String : {r1a} ");
            Console.WriteLine($"Are the two objects equal? {Equals(r1a,r2a)}");
            Console.WriteLine($"Are the two references equal? {ReferenceEquals(r1a, r2a)}");
            Console.WriteLine($"Are the two objects ==? {r1a == r2a}");
            Console.WriteLine($"Has code of Record 1: {r1a.GetHashCode() }");
            Console.WriteLine($"Has code of Record 2: {r2a.GetHashCode() }");
            Console.WriteLine($"Has code of Record 3: {r3a.GetHashCode() }");

            Console.WriteLine("**************************************************");
            Console.WriteLine("Class Type:");
            Console.WriteLine($"To String : {c1a} ");
            Console.WriteLine($"Are the two objects equal? {Equals(c1a, c2a)}");
            Console.WriteLine($"Are the two references equal? {ReferenceEquals(c1a, c2a)}");
            Console.WriteLine($"Are the two objects ==? {c1a == c2a}");
            Console.WriteLine($"Has code of Class 1: {c1a.GetHashCode() }");
            Console.WriteLine($"Has code of Class 2: {c2a.GetHashCode() }");
            Console.WriteLine($"Has code of Class 3: {c3a.GetHashCode() }");
            Console.WriteLine();

            // Anonymous tuples
            var (fn, ln) = r1a;
            Console.WriteLine($"The value of fn is {fn} and the value of ln is {ln}"); // Deconstructor of record used

            Record1 r4a = r1a with
            {
                FirstName = "NewFirstName1"
            };
            Console.WriteLine($"Updated Record1: {r4a}");
            Console.WriteLine();
            RecordB rb1 = new RecordB("NewFirstName1", "NewLastName2");
            Console.WriteLine($"Record B Instance 1 value: {rb1}");
            //Console.WriteLine($"Record B Instance 1 value - expanded: {rb1.FirstName} : {rb1.LastName} - Full Name : {rb1.FullName}");
            Console.WriteLine(rb1.SayHello());
            Console.ReadLine();

        }
    }

    // Record is an enhanced class - it is reference type, behaving like value type
    // Immutable
    public record Record1(string FirstName, string LastName);
    
    //Inheritance
    public record User1(int id, string FirstName, string LastName) : Record1(FirstName, LastName);

    // Get user data online and make manipulation
    public class UserDiscovery
    {
        // record used inside class
        public User1 LookUpUser { get; set; }
        public int IncidentsFound { get; set; }
        public List<string> Incidents { get; set; }
    }

    public record RecordB(string FirstName, string LastName)
    {
        //public string FirstName { get; init; } = FirstName;
        private string _firstName =FirstName;

        public string FirstName
        {
            // Need to fix this = it is not working
            //get { return _firstName =FirstName.Substring(1,1); }
            get { return _firstName; }
            init { }
        }

        public string FullName { get => $"{FirstName} {LastName}"; }

        public string SayHello()
        {
            return $"Hello {FirstName}";
        }
    }

    // Record is similar to the below
    public class class1
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public class1(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        //Deconstructor
        public class1(out string FirstName, out string LastName)
        {
            FirstName = this.FirstName;
            LastName = this.LastName;
        }
    }
}


/* Summary of Records
 * Pros:
 * Easy to setup
 * Thread-safe
 * Easy/safe to share - as it is Read-Only
 * 
 * Usage:
 * Capture external data that does not change - probably for Displaying reference data
 * API calls - get data from API and send it to DB
 * Processing data - Business Logic that uses this data to make decision - use both class and records together
 * Any instance where we need to work with Read only data
 * 
 * When Not to use:
 * When data needs to be changed (Entity Framework - has the concept of change tracking) - will not work with EF. Works with Dapper though * 
  */

//**********************
// NOT RECOMMENDED
//**********************

public record Record3 // No constructor => no deconstructor
{
    // Records expected to work with init and NOT set (it makes the record mutable)
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
// Do not clone records all over - this will be a shallow copy - not memory efficient
// Use class when mutability is needed - not record