using System;
using System.Linq;
using System.Collections.Generic;
using LINQ.Entities;

namespace LINQ
{
    class Program
    {
        static void Print<T>(string message, IEnumerable<T> collections)
        {
            Console.WriteLine(message);
            foreach (T obj in collections)
            {
                Console.WriteLine(obj);
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            #region Primeiro Exemplo de LINQ
            ////Specify the date source
            //int[] numbers = new int[] { 2, 3, 4, 5, 6, 7, 8, 9 };

            ////Define the query expression
            //IEnumerable<int> result = numbers //aki eu estou convertendo o conteúdo do vetor para uma lista genérica
            //    .Where(x => x % 2 == 0)
            //    .Select(x => x * 10);

            ////Execute the query

            //foreach (int x in result)
            //{
            //    Console.WriteLine(x);
            //}

            //Console.ReadLine();
            #endregion

            Category c1 = new Category() { Id = 1, Name = "Tools", Tier = 2 };
            Category c2 = new Category() { Id = 2, Name = "Computers", Tier = 1 };
            Category c3 = new Category() { Id = 3, Name = "Eletronics", Tier = 1 };

            List<Product> products = new List<Product>()
            {
                new Product() { Id = 1, Name = "Computer", Price = 1100.0, Category = c2 },
                new Product() { Id = 2, Name = "Hammer", Price = 90.0, Category = c1 },
                new Product() { Id = 3, Name = "TV", Price = 1700.0, Category = c3 },
                new Product() { Id = 4, Name = "Notebook", Price = 1300.0, Category = c2 },
                new Product() { Id = 5, Name = "Saw", Price = 80.0, Category = c1 },
                new Product() { Id = 6, Name = "Tablet", Price = 700.0, Category = c2 },
                new Product() { Id = 7, Name = "Camera", Price = 700.0, Category = c3 },
                new Product() { Id = 8, Name = "Printer", Price = 350.0, Category = c3 },
                new Product() { Id = 9, Name = "MacBook", Price = 1800.0, Category = c2 },
                new Product() { Id = 10, Name = "Sound Bar", Price = 700.0, Category = c3 },
                new Product() { Id = 11, Name = "Level", Price = 70.0, Category = c1 }
            };

            var r1 = products.Where(p => p.Category.Tier == 1 && p.Price < 900.00);
            Print("Tier 1 and Price < 900", r1);

            var r2 = products.Where(p => p.Category.Name == "Tools").Select(p => p.Name);
            Print("Names of Product from Tools:", r2);

            var r3 = products.Where(p => p.Name[0] == 'C')
                .Select(p => new { p.Name, p.Price, CategoryName = p.Category.Name }); //objeto anonimo que não está declarado na classe
            Print("Names stared with 'C' and anonymous object: ", r3);

            var r4 = products.Where(p => p.Category.Tier == 1)
                .OrderBy(p => p.Price)
                .ThenBy(p => p.Name);
            Print("Tier 1 order by Price then by Name:", r4);

            var r5 = r4.Skip(2).Take(4);
            Print("Tier 1 order by Price then by Name Skip 2 Take 4: ", r5);

            var r6 = products.FirstOrDefault();
            Console.WriteLine("First of Defalut test 1: " + r6);

            var r7 = products.Where(p => p.Price > 3000.00).FirstOrDefault();
            Console.WriteLine("\nFirst of Default test 2: ", r7);

            var r8 = products.Where(p => p.Id == 3).SingleOrDefault();
            Console.WriteLine("\nSingle of Default test 1: " + r8);

            var r9 = products.Where(p => p.Id == 30).SingleOrDefault();
            Console.WriteLine("\nSingle of Default test 1: " + r9);

            var r10 = products.Max(p => p.Price);
            Console.WriteLine("\nMax Price: " + r10);

            var r11 = products.Min(p => p.Price);
            Console.WriteLine("\nMax Price: " + r11);

            var r12 = products.Where(p => p.Category.Id == 1).Sum(p => p.Price);
            Console.WriteLine("\nCategory 1 Sum prices: " + r12);

            var r13 = products.Where(p => p.Category.Id == 1).Average(p => p.Price);
            Console.WriteLine("\nCategory 1 Average prices: " + r13);

            var r14 = products.Where(p => p.Category.Id == 5)
                .Select(p => p.Price)
                .DefaultIfEmpty(0.0)
                .Average();
            Console.WriteLine("\nCategory 5 Average prices: " + r14);

            var r15 = products.Where(p => p.Category.Id == 1).Select(p => p.Price).Aggregate((x, y) => x + y);
            //O Select().Agrregate() permite definir um cálculo próprio
            Console.WriteLine("\nCategory 1 aggregate Sum: " + r15);

            //var r15b = products.Where(p => p.Category.Id == 5).Select(p => p.Price).Aggregate(0.0, (x, y) => x + y);
            //Console.WriteLine("Category 1 aggregate Sum: " + r15b);// tratando a exeção caso o Id for nulo

            Console.WriteLine();

            var r16 = products.GroupBy(p => p.Category);
            foreach (IGrouping<Category, Product> group in r16)
            {
                Console.WriteLine("Category " + group.Key.Name + ": ");
                foreach (Product p in group)
                {
                    Console.WriteLine(p);
                }
                Console.WriteLine();
            }
            Console.Read();

        }
    }
}
