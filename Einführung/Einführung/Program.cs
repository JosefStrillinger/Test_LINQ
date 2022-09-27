using Einführung.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Einführung {
    class Program {
        static void Main(string[] args) {

            // LINQ ... Language Integrated Query (für das Abfragen von listenartigen Strukturen (Liste, Array, ...)
            // LINQ to Objects ... für Abfragen im RAM (Listen, Arrays, ...)
            // LINQ to XML ... für das Abfragen von XML-Dokumenten
            // LINQ to Entities ... für das Abfragen von DB-Tabellen

            // LINQ-Abfragemöglichektiten: Abfragesyntax (SQL ähnlich) und
            //           Erweiterungsmethodensyntax (Lambda-Ausdrücke)

            // Delegates: eine anonyme Methode
            // vordefinierte generische Delegates:
            //              Action<...> ... für Methoden ohne Ruckgabendatentyp und 1 bis 16 Parameter
            //              Action<T1>, Action<T1, T2>, ..., Action<T1, ..., T16>
            //              Func<...> ... für Methoden mit Rückgabetyp und 1 bis 16 Parameter
            //              Func<T1, R>, Func<T1, T2, R>, ....          ==> R ist Rückgabedatentyp
            //              Predicate<T1> ... Rückgabedatentyp ist immer bool, nur ein Parameter

            //Lambda-Ausdruck:ist eine Methodendefinition, die Methode hat allerdings keinen Namen
            //              kurze Schreibweise für eine Methidendefinition

            List<Customer> customers = CreateTestdata();

            //LINQ-Abfragen:

            // alle männlichen Personen ermitteln - Abfragesyntax
            // select * from customers where gender = 0
            var customersMale_AS = (from c in customers where c.Gender == Gender.male select c).ToList<Customer>();

            Console.WriteLine("Customers - male - AS mit foreach");
            foreach(Customer c in customersMale_AS)
            {
                Console.WriteLine(c);
            }

            //Ausgabe mit der Erweiterungsmethode Foreach + Lambda-Ausdruck
            Console.WriteLine("Customers - male  AS mit EM ForEach");
            //Lambda-Ausdruck: links vom => befindet sich die Parameterliste der Methode
            //                 rechts von => befindet sich der Programmcode der Methode

            //customers.ForEach((Customer c) => { Console.WriteLine(c); });
            //kurze Schreibweise
            customersMale_AS.ForEach(c => Console.WriteLine(c));



            //Beispiel 1 - mit Erweiterungsmethodensyntax
            //var customersMale_EMS = customers.Where(c => { return c.Gender == Gender.male; });
            var customersMale_EMS = customers.Where(c => c.Gender == Gender.male ).ToList<Customer>();
            Console.WriteLine("\nCustomers - male - AS mit EM ForEach");
            customersMale_EMS.ForEach(c => Console.WriteLine(c));
            
            //die Abfragesyntax wird vom Compiler immer in die Erweiterungsmethodensyntx umgewandelt
            // nicht aller Erweiterungsmethoden-Befehle von LINQ sind als Abfragesyntax-Befehle vorhanden
            
           // Abfrage 2
           // Alle Customers mit Geburtsdatum 2003 oder jünger
           // sortiert nach dem Nachnamen
           // die erste Person überspringen und die nächsten beiden Personen auswählen

           // Abfragesyntax

            var customersBirthdate_AS = (from c in customers
                                        where c.Birthdate.Year >= 2003
                                        orderby c.Lastname descending
                                        select c)
                                        .Skip(1)
                                        .Take(2)
                                        .ToList<Customer>();
            Console.WriteLine("\nCustomers Birthdate - AS");
            customersBirthdate_AS.ForEach(c => Console.WriteLine(c));

            //EM - Syntax
            var customerBirthdate_EMS = customers.Where(c => c.Birthdate.Year >= 2003)
                                                 .OrderBy(c => c.Lastname)
                                                 .Skip(1)
                                                 .Take(2)
                                                 .ToList<Customer>();
            Console.WriteLine("\nCustomers Birthdate - AS - EMS");
            customerBirthdate_EMS.ForEach(c => Console.WriteLine(c));


            //Projektion -> nur bestimmte Felder (DB: Spalten) werden ausgewählt
            // SQL select id, lastname from customers ...
            // Beispiel: alle Personen mit Gehalt zwischen 2000 und 2700
            //          nur die Felder Lastname und Salary auswählen
            var customersProj_AS = from c in customers
                                   where c.Salary >= 2000 && c.Salary <= 2700
                                   // new {...} -> Projektion
                                   // es wird vom Compiler eine Klasse erzeugt (mit einem und
                                   //           unbekannten Namen) mit den Feldern Name und Gehalt
                                   select new {
                                       Name = c.Lastname + " " + c.Firstname,
                                       Gehalt = c.Salary
                                   };

            Console.WriteLine("\n Projektion");
            foreach(var cust in customersProj_AS) {
                Console.WriteLine(cust.Name + " -> " +cust.Gehalt);
            }

        }

        private static List<Customer> CreateTestdata() {
            Address a1 = new Address(100, "6020", "Innsbruck", "Anichstraße", "17b");
            Address a2 = new Address(101, "6020", "Innsbruck", "Museumsstraße", "183c");
            Address a3 = new Address(102, "6500", "Landeck", "Brixnerstraße", "110");
            Address a4 = new Address(103, "6176", "Völs", "Angerweg", "6a");
            Address a5 = new Address(104, "6210", "Wiesing", "Rofansiedlung", "403c");//Fabi ==> send nukes

            //Initilisierersyntax
            List<Customer> customers = new List<Customer>() {
                //Initialisierersyntax
                new Customer() {
                    Id = 1000, Firstname = "Niklas", Lastname = "Sillaber", Gender = Gender.male, Birthdate = new DateTime(2003, 9, 18), Salary = 2081.90m
                },
                //ctor verwenden
                new Customer(1001, "Fabian", "Steinlechner", new DateTime(2004, 06, 21), 2081.90m, Gender.male),
                new Customer(1002, "Josef", "Strillinger", new DateTime(2004, 07, 08), 2081.90m, Gender.male),
                new Customer(1003, "Petra", "Sowieso", new DateTime(1996, 1, 28), 2081.90m, Gender.female),
                new Customer(1004, "Karin", "Gutkauf", new DateTime(1999, 12, 13), 2081.90m, Gender.female),
                new Customer(1004, "Timo", "Strobl", new DateTime(2009, 9, 21), 981.90m, Gender.male),
            };

            customers[0].AdAddresses(a1);
            customers[1].AdAddresses(a5);
            customers[1].AdAddresses(a2);
            customers[2].AdAddresses(a3);
            customers[3].AdAddresses(a4);
            customers[5].AdAddresses(a2);

            return customers;


        }
    }
}
