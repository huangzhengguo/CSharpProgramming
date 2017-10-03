using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wrox.ProCSharp.LINQ;

namespace Chapter11
{
    class Program
    {
        static void Main(string[] args)
        {
            // LinqQuery();
            // ExtensionMethods();
            // DelayQueryMethod();
            OpteratorSampleMethod();
            Console.ReadLine();
        }

        // 1.使用Linq查询
        private static void LinqQuery()
        {
            // 查询来自巴西的世界冠军
            var query = from r in Formula1.GetChampions()
                        where r.Country == "Brazil"
                        orderby r.Wins descending
                        select r;
            foreach (Racer r in query)
            {
                Console.WriteLine("{0:A}", r);
            }
        }

        // 2.使用Linq扩展方法
        private static void ExtensionMethods()
        {
            var champions = new List<Racer>(Formula1.GetChampions());
            IEnumerable<Racer> brazilChampions = champions.Where(r => r.Country == "Brazil").
                                                 OrderByDescending(r => r.Wins).
                                                 Select(r => r);
            foreach(Racer r in brazilChampions)
            {
                Console.WriteLine("{0:A}", r);
            }
        }

        // 3.推迟查询的执行
        static void DelayQueryMethod()
        {
            var names = new List<String> { "Nina", "Aiberto", "Juan", "Mike", "Phil" };
            var namesWithJ = from n in names
                             where n.StartsWith("J")
                             orderby n
                             select n;
            Console.WriteLine("First interation");
            foreach (string name in namesWithJ)
            {
                Console.WriteLine(name);
            }

            names.Add("John");
            names.Add("Jim");
            names.Add("Jack");
            names.Add("Denny");

            Console.WriteLine("Second interation");
            foreach (string name in namesWithJ)
            {
                Console.WriteLine(name);
            }
        }

        // 4.操作符实例
        static void OpteratorSampleMethod()
        {
            // 1.使用where子句，可以合并多个表达式
            var racers = from r in Formula1.GetChampions()
                         where r.Wins > 15 && (r.Country == "Brazil" || r.Country == "Austria")
                         select r;

            DisplayRacers(racers);

            // 2.使用索引查询
            racers = Formula1.GetChampions().
                     Where((r, index) => r.LastName.StartsWith("A") && index % 2 != 0);
            DisplayRacers(racers);

            // 3.类型筛选
            Console.WriteLine("类型筛选");
            object[] data = { "one", 2, 3, "four", "five", 6 };
            var query = data.OfType<string>();
            foreach (var s in query)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine();

            // 4.复合的from子句
            Console.WriteLine("复合的from子句");
            var ferrariDrivers = from r in Formula1.GetChampions()
                                 from c in r.Cars
                                 where c == "Ferrari"
                                 orderby r.LastName
                                 select r.FirstName + " " + r.LastName;
            DisplayRacers(ferrariDrivers);

            // 5.排序
            Console.WriteLine("排序");
            racers = (from r in Formula1.GetChampions()
                      orderby r.Country, r.LastName, r.FirstName
                      select r).Take(10);
            DisplayRacers(racers);

            // 6.分组
            Console.WriteLine("分组");
            var countries = from r in Formula1.GetChampions()
                            group r by r.Country into g
                            orderby g.Count() descending, g.Key
                            where g.Count() >= 2
                            select new
                            {
                                Country = g.Key,
                                Count = g.Count()
                            };
            foreach (var item in countries)
            {
                Console.WriteLine("{0,-10} {1}", item.Country, item.Count);
            }
            Console.WriteLine();

            // 7.对嵌套的对象分组
            Console.WriteLine("对嵌套内的对象分组");
            var mcountries = from r in Formula1.GetChampions()
                        group r by r.Country into g
                        orderby g.Count() descending, g.Key
                        where g.Count() >= 2
                        select new
                        {
                            Country = g.Key,
                            Count = g.Count(),
                            Racers = from r1 in g
                                     orderby r1.LastName
                                     select r1.FirstName + " " + r1.LastName
                            };
            foreach (var item in mcountries)
            {
                Console.WriteLine("{0,-10} {1}", item.Country, item.Count);
                foreach (var name in item.Racers)
                {
                    Console.WriteLine("{0}: ", name);
                }

                Console.WriteLine();
            }


            // 8.内连接：使用join子句可以根据特定的条件合并两个数据源，但之前要获得两个连接的列表
            var racers1 = from r in Formula1.GetChampions()
                          from y in r.Years
                          select new
                          {
                              Year = y,
                              Name = r.FirstName + " " + r.LastName
                          };

            var teams = from t in Formula1.GetContructorChampions()
                        from y in t.Years
                        select new
                        {
                            Year = y,
                            Name = t.Name
                        };

            var racersAndTeams = from r in racers1
                                 join t in teams on r.Year equals t.Year
                                 select new
                                 {
                                     r.Year,
                                     Champion = r.Name,
                                     Constructor = t.Name
                                 };
            Console.WriteLine("Year World Champion\t Constructor Title");
            foreach (var item in racersAndTeams)
            {
                Console.WriteLine("{0}: {1,-20} {2}", item.Year, item.Champion, item.Constructor);
            }

            Console.WriteLine();

            // 9.组连接
            Console.WriteLine("组连接");
            var racers2 = Formula1.GetChampionships()
                .SelectMany(cs => new List<RacerInfo>()
                {
                    new RacerInfo
                    {
                        Year=cs.Year,
                        Position=1,
                        FirstName=cs.First.FirstName(),
                        LastName=cs.First.LastName()
                    },
                    new RacerInfo
                    {
                        Year=cs.Year,
                        Position=2,
                        FirstName=cs.Second.FirstName(),
                        LastName=cs.Second.LastName()
                    },
                    new RacerInfo
                    {
                        Year=cs.Year,
                        Position=3,
                        FirstName=cs.Third.FirstName(),
                        LastName=cs.Third.LastName()
                    }
                });

            var q = (from r in Formula1.GetChampions()
                     join r2 in racers2 on
                     new
                     {
                         FirstName = r.FirstName,
                         LastName = r.LastName
                     }
                     equals
                     new
                     {
                         FirstName = r2.FirstName,
                         LastName = r2.LastName
                     }
                     into yearResults
                     select new
                     {
                         FirstName = r.FirstName,
                         LastName = r.LastName,
                         Wins = r.Wins,
                         Starts = r.Starts,
                         Results = yearResults
                     });
            foreach (var r in q)
            {
                Console.WriteLine("{0} {1}", r.FirstName, r.LastName);
                foreach (var results in r.Results)
                {
                    Console.WriteLine("{0} {1}", results.Year, results.Position);
                }
            }

            // 10.集合操作
            // 定义委托
            Console.WriteLine("集合操作");
            Func<string, IEnumerable<Racer>> racerByCar = car => from r in Formula1.GetChampions()
                                                                 from c in r.Cars
                                                                 where c == car
                                                                 orderby r.LastName
                                                                 select r;
            Console.WriteLine("World champion with ferrati and McLaren");
            foreach (var racer in racerByCar("Ferrari").Intersect(racerByCar("McLaren")))
            {
                Console.WriteLine(racer);
            }

            // 11.合并
            Console.WriteLine("合并操作");
            var racerName = from r in Formula1.GetChampions()
                            where r.Country == "Italy"
                            orderby r.Wins descending
                            select new
                            {
                                Name = r.FirstName + " " + r.LastName
                            };
            var racerNamesAndStarts = from r in Formula1.GetChampions()
                                      where r.Country == "Italy"
                                      orderby r.Wins descending
                                      select new
                                      {
                                          LastName = r.LastName,
                                          Starts = r.Starts
                                      };
            var racers3 = racerName.Zip(racerNamesAndStarts, (first, second) => first.Name + ", starts" + second);
            foreach (var r in racers)
            {
                Console.WriteLine(r);
            }

            Console.WriteLine();

        }

        static void DisplayRacers(IEnumerable<Racer> racers)
        {
            foreach (var r in racers)
            {
                Console.WriteLine("{0:A}", r);
            }

            Console.WriteLine();
        }

        static void DisplayRacers(IEnumerable<String> racers)
        {
            foreach (var r in racers)
            {
                Console.WriteLine("{0:A}", r);
            }

            Console.WriteLine();
        }
    }

    public static class StringExtension
    {
        // 扩展方法，第一个参数是要扩展的类型：扩展方法必须定义在静态类中
        public static void Foo(this string s)
        {
            Console.WriteLine(s);
        }

        public static string FirstName(this string name)
        {
            int ix = name.LastIndexOf(' ');
            return name.Substring(0, ix);
        }

        public static string LastName(this string name)
        {
            int ix = name.LastIndexOf(' ');
            return name.Substring(ix + 1);
        }
    }
}
