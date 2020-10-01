using System;
using System.Linq;
using DbTestProject.Models;

namespace DbTestProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            using (var context = new TestDbContext())
            {
                context.TestDbSet.Add(new TestModel());
                context.SaveChanges();
                Console.WriteLine(context.TestDbSet.First().Id);  
            }
        }
    }
}
