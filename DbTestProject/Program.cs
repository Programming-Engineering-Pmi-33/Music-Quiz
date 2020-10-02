using System;

namespace DbTestProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            using (var context = new TestDbContext())
            {
                context.SaveChanges();
            }
        }
    }
}
