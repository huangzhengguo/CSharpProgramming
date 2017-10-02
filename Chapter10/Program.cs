using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chapter10.QueueSample;
using System.Threading;
using Chapter10.LinkedListSample;
using Chapter10.DictionarySample;

namespace Chapter10
{
    class Program
    {
        static void Main(string[] args)
        {
            // MyListMethod();
            // MyQueueMethod();
            // MyLinkedListMethod();
            MyDictionaryMethod();

            Console.ReadLine();
        }

        // 1.list 列表
        static void MyListMethod()
        {
            List<Racer> racers = new List<Racer>
            {
                Capacity = 40
            };

            var graham = new Racer(7, "Graham", "Hill", "UK", 14);
            var emerson = new Racer(13, "Emerson", "Fittipaldi", "Brazil", 14);
            var mario = new Racer(16, "Mario", "Andretti", "USA", 12);

            racers.Add(graham);
            racers.Add(emerson);
            racers.Add(mario);

            for (int i= 0;i < racers.Count; i++){
                Console.WriteLine("{0,10:A}", racers[i]);
            }

            racers.ForEach(Console.WriteLine);

            int index2 = racers.FindIndex(new FindCountry("Brazil").FindCountryPredicate);

            List<Racer> bigWinners = racers.FindAll(r => r.Wins > 20);
            racers.TrimExcess();
        }

        // 2.队列
        static void MyQueueMethod()
        {
            var dm = new DocumentManager();

            ProcessDocuments.Start(dm);

            for(int i = 0; i < 1000; i++)
            {
                var doc = new Document("Doc " + i.ToString(), "content");

                dm.AddDocument(doc);

                Console.WriteLine("Added document {0}", doc.Title);
                Thread.Sleep(new Random().Next(20));
            }
        }

        // 2.链表:linked list
        static void MyLinkedListMethod()
        {
            var pdm = new PriorityDocumentManager();

            pdm.AddDocument(new Documents("one", "sample", 8));
            pdm.AddDocument(new Documents("two", "sample", 3));
            pdm.AddDocument(new Documents("three", "sample", 4));
            pdm.AddDocument(new Documents("four", "sample", 8));
            pdm.AddDocument(new Documents("five", "sample", 1));
            pdm.AddDocument(new Documents("six", "sample", 9));
            pdm.AddDocument(new Documents("seven", "sample", 1));
            pdm.AddDocument(new Documents("eight", "sample", 1));

            pdm.DisplayAllNodes();
        }

        // 3.字典
        static void MyDictionaryMethod()
        {
            var employees = new Dictionary<EmployeeId, Employee>(31);
            var idTony = new EmployeeId("C3755");
            var tony = new Employee(idTony, "Tony Stewart", 379025.00m);

            employees.Add(idTony, tony);
            Console.WriteLine(tony);

            var idCarl = new EmployeeId("F3574");
            var carl = new Employee(idCarl, "Carl Edwards", 403466.00m);

            employees.Add(idCarl, carl);
            Console.WriteLine(carl);

            var idKevin = new EmployeeId("C3386");
            var kevin = new Employee(idKevin, "Kevin Harwick", 415261.00m);

            employees.Add(idKevin, kevin);
            Console.WriteLine(kevin);

            while (true)
            {
                Console.WriteLine("Enter id (X to exit) >");
                var userInput = Console.ReadLine();

                if (userInput == "X")
                {
                    break;
                }

                EmployeeId id;
                try
                {
                    id = new EmployeeId(userInput);

                    Employee employee;
                    if (!employees.TryGetValue(id,out employee))
                    {
                        Console.WriteLine("Employee with id {0} does exist.", id);
                    }
                    else
                    {
                        Console.WriteLine(employee);
                    }
                }catch(EmployeeIdException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
