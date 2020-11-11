using Exercicio1.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Exercicio1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Employee> employees = new List<Employee>();

            Console.Write("Enter full file path: ");
            string path = Console.ReadLine();

            try
            {
                Console.Write("Enter salary: ");
                double salary = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

                using (StreamReader stream = File.OpenText(path))
                {
                    while (!stream.EndOfStream)
                    {
                        string[] vs = stream.ReadLine().Split(',');
                        employees.Add(new Employee(vs[0], vs[1], double.Parse(vs[2], CultureInfo.InvariantCulture)));
                    }
                }

                Console.WriteLine("Email of people whose salary is more than " + salary.ToString("F2", CultureInfo.InvariantCulture) + ":");

                var peopleEmails = employees.Where(p => p.Salary > salary).OrderBy(p => p.Name).Select(p => p.Email);
                foreach (string email in peopleEmails)
                {
                    Console.WriteLine(email);
                }

                double peopleSalary = employees.Where(p => p.Name[0] == 'M').Select(p => p.Salary).Sum();

                Console.WriteLine("Sum of salary of people whose name starts with 'M': " + peopleSalary.ToString("F2", CultureInfo.InvariantCulture));
            } catch (IOException e)
            {
                Console.WriteLine("Unexpected Error: " + e.Message);
            }
        }
    }
}
