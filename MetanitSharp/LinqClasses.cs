using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetanitSharp
{
    class LinqFilter
    {
        public static void Display()
        {
            filter();
            filterObjects();
            complexFilter();
        }

        static void filter()
        {
            int[] numbers = { 1, 2, 3, 4, 10, 34, 55, 66, 77, 88 };

            IEnumerable<int> evens = from i in numbers
                                     where i % 2 == 0 && i > 10   //все четные элементы, которые больше 10
                                     select i;
            foreach (int i in evens)
                Console.WriteLine(i);

            IEnumerable<int> evensExt = numbers.Where(i => i % 2 == 0 && i > 10);
        }

        static void filterObjects()
        {
            List<User> users = new List<User>
            {
                new User {Name="Том", Age=23, Languages = new List<string> {"английский", "немецкий" }},
                new User {Name="Боб", Age=27, Languages = new List<string> {"английский", "французский" }},
                new User {Name="Джон", Age=29, Languages = new List<string> {"английский", "испанский" }},
                new User {Name="Элис", Age=24, Languages = new List<string> {"испанский", "немецкий" }}
            };

            var selectedUsers = from user in users
                                where user.Age > 25
                                select user;
            foreach (User user in selectedUsers)
                Console.WriteLine("{0} - {1}", user.Name, user.Age);


            var selectedUsersExt = users.Where(u => u.Age > 25);
        }

        static void complexFilter()
        {
            List<User> users = new List<User>
            {
                new User {Name="Том", Age=23, Languages = new List<string> {"английский", "немецкий" }},
                new User {Name="Боб", Age=27, Languages = new List<string> {"английский", "французский" }},
                new User {Name="Джон", Age=29, Languages = new List<string> {"английский", "испанский" }},
                new User {Name="Элис", Age=24, Languages = new List<string> {"испанский", "немецкий" }}
            };

            var selectedUsers = from user in users
                                from lang in user.Languages
                                where user.Age < 28
                                where lang == "английский"
                                select user;

            foreach (User user in selectedUsers)
                Console.WriteLine("{0} - {1}", user.Name, user.Age);

            var selectedUsersExt = users.SelectMany(u => u.Languages,
                            (u, l) => new { User = u, Lang = l })
                          .Where(u => u.Lang == "английский" && u.User.Age < 28)
                          .Select(u => u.User);
        }

        class User
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public List<string> Languages { get; set; }
            public User()
            {
                Languages = new List<string>();
            }
        }
    }

    class LinqProjection
    {
        public static void Display()
        {
            selectName();
            selectAnonymous();
            letOperator();
            differentSources();
        }

        static void selectName()
        {
            List<User> users = new List<User>();
            users.Add(new User { Name = "Sam", Age = 43 });
            users.Add(new User { Name = "Tom", Age = 33 });

            var names = from u in users select u.Name;

            foreach (string n in names)
                Console.WriteLine(n);

            // выборка имен
            var namesExt = users.Select(u => u.Name);
        }

        static void selectAnonymous()
        {
            List<User> users = new List<User>();
            users.Add(new User { Name = "Sam", Age = 43 });
            users.Add(new User { Name = "Tom", Age = 33 });

            var items = from u in users
                        select new
                        {
                            FirstName = u.Name,
                            DateOfBirth = DateTime.Now.Year - u.Age
                        };

            foreach (var n in items)
                Console.WriteLine("{0} - {1}", n.FirstName, n.DateOfBirth);

            // выборка объектов анонимного типа
            var itemsExt = users.Select(u => new
            {
                FirstName = u.Name,
                DateOfBirth = DateTime.Now.Year - u.Age
            });
        }

        static void letOperator()
        {
            List<User> users = new List<User>()
            {
                new User { Name = "Sam", Age = 43 },
                new User { Name = "Tom", Age = 33 }
            };

            var people = from u in users
                         let name = "Mr. " + u.Name
                         select new
                         {
                             Name = name,
                             Age = u.Age
                         };

            foreach (var p in people)
                Console.WriteLine("{0} - {1}", p.Name, p.Age);
        }

        static void differentSources()
        {
            List<User> users = new List<User>()
            {
                new User { Name = "Sam", Age = 43 },
                new User { Name = "Tom", Age = 33 }
            };
            List<Phone> phones = new List<Phone>()
            {
                new Phone {Name="Lumia 630", Company="Microsoft" },
                new Phone {Name="iPhone 6", Company="Apple"},
            };

            var people = from user in users
                         from phone in phones
                         select new { Name = user.Name, Phone = phone.Name };

            foreach (var p in people)
                Console.WriteLine("{0} - {1}", p.Name, p.Phone);
        }

        class User
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }

        class Phone
        {
            public string Name { get; set; }
            public string Company { get; set; }
        }
    }

    class LinqSort
    {
        public static void Display()
        {
            sortNums();
            sortObjects();
            multiSort();
        }

        static void sortNums()
        {
            int[] numbers = { 3, 12, 4, 10, 34, 20, 55, -66, 77, 88, 4 };
            var orderedNumbers = from i in numbers
                                 orderby i
                                 select i;
            foreach (int i in orderedNumbers)
                Console.Write(i + " ");
            Console.WriteLine();
        }

        static void sortObjects()
        {
            List<User> users = new List<User>()
            {
                new User { Name = "Tom", Age = 33 },
                new User { Name = "Bob", Age = 30 },
                new User { Name = "Tom", Age = 21 },
                new User { Name = "Sam", Age = 43 }
            };

            var sortedUsers = from u in users
                              orderby u.Name
                              select u;

            foreach (User u in sortedUsers)
                Console.WriteLine(u.Name);

            var sortedUsersExt = users.OrderBy(u => u.Name);
        }

        static void multiSort()
        {
            List<User> users = new List<User>()
            {
                new User { Name = "Tom", Age = 33 },
                new User { Name = "Bob", Age = 30 },
                new User { Name = "Tom", Age = 21 },
                new User { Name = "Alice", Age = 28 },
                new User { Name = "Sam", Age = 43 }
            };
            var result = from user in users
                         orderby user.Name, user.Age, user.Name.Length
                         select user;
            foreach (User u in result)
                Console.WriteLine("{0} - {1}", u.Name, u.Age);

            var resultExt = users.OrderBy(u => u.Name).ThenBy(u => u.Age).ThenBy(u => u.Name.Length);
        }

        class User
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }
    }

    class LinqAggr
    {
        public static void Display()
        {
            aggregate();
            count();
            sumMinMaxAvg();
        }

        static void aggregate()
        {
            int[] numbers = { 1, 2, 3, 4, 5 };

            int query = numbers.Aggregate((x, y) => x - y);
            Console.WriteLine(query);

            int query2 = numbers.Aggregate((x, y) => x + y); // аналогично 1 + 2 + 3 + 4 + 5
            Console.WriteLine(query2);

            int factorial = numbers.Aggregate((x, y) => x * y);
            Console.WriteLine(factorial);

            string[] chars = { "h", "e", "l", "l", "o" };
            string concat = chars.Aggregate((x, y) => x + y);
            Console.WriteLine(concat);
        }

        static void count()
        {
            int[] numbers = { 1, 2, 3, 4, 10, 34, 55, 66, 77, 88 };
            int size = (from i in numbers where i % 2 == 0 && i > 10 select i).Count();
            Console.WriteLine(size);

            size = numbers.Count(i => i % 2 == 0 && i > 10);
            Console.WriteLine(size);
        }

        static void sumMinMaxAvg()
        {
            int[] numbers = { 1, 2, 3, 4, 10, 34, 55, 66, 77, 88 };
            List<User> users = new List<User>()
            {
                new User { Name = "Tom", Age = 23 },
                new User { Name = "Sam", Age = 43 },
                new User { Name = "Bill", Age = 35 }
            };

            int sum1 = numbers.Sum();
            decimal sum2 = users.Sum(n => n.Age);

            int min1 = numbers.Min();
            int min2 = users.Min(n => n.Age); // минимальный возраст

            int max1 = numbers.Max();
            int max2 = users.Max(n => n.Age); // максимальный возраст

            double avr1 = numbers.Average();
            double avr2 = users.Average(n => n.Age); //средний возраст

            Console.WriteLine($"numbers:\tsum={sum1} min={min1} max={max1} avg={avr1}");
            Console.WriteLine($"user ages:\tsum={sum2} min={min2} max={max2} avg={avr2}");
        }

        class User
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }
    }

    class LinqGroup
    {
        public static void Display()
        {
            groupPhones();
            groupPhonesCount();
            groupNested();
        }

        static List<Phone> phones = new List<Phone>
        {
            new Phone {Name="Lumia 430", Company="Microsoft" },
            new Phone {Name="Mi 5", Company="Xiaomi" },
            new Phone {Name="LG G 3", Company="LG" },
            new Phone {Name="iPhone 5", Company="Apple" },
            new Phone {Name="Lumia 930", Company="Microsoft" },
            new Phone {Name="iPhone 6", Company="Apple" },
            new Phone {Name="Lumia 630", Company="Microsoft" },
            new Phone {Name="LG G 4", Company="LG" }
        };

        static void groupPhones()
        {
            var phoneGroups = from phone in phones
                              group phone by phone.Company;

            foreach (IGrouping<string, Phone> g in phoneGroups)
            {
                Console.WriteLine(g.Key);
                foreach (var t in g)
                    Console.WriteLine(t.Name);
                Console.WriteLine();
            }

            var phoneGroupsExt = phones.GroupBy(p => p.Company);
        }

        static void groupPhonesCount()
        {
            var phoneGroups = from phone in phones
                              group phone by phone.Company into g
                              select new { Name = g.Key, Count = g.Count() };
            foreach (var group in phoneGroups)
                Console.WriteLine("{0} : {1}", group.Name, group.Count);

            var phoneGroupsExt = phones.GroupBy(p => p.Company)
                        .Select(g => new { Name = g.Key, Count = g.Count() });
        }

        static void groupNested()
        {
            var phoneGroups = from phone in phones
                              group phone by phone.Company into g
                              select new
                              {
                                  Name = g.Key,
                                  Count = g.Count(),
                                  Phones = from p in g select p
                              };
            foreach (var group in phoneGroups)
            {
                Console.WriteLine("{0} : {1}", group.Name, group.Count);
                foreach (Phone phone in group.Phones)
                    Console.WriteLine(phone.Name);
                Console.WriteLine();
            }

            var phoneGroupsExt = phones.GroupBy(p => p.Company)
                        .Select(g => new
                        {
                            Name = g.Key,
                            Count = g.Count(),
                            Phones = g.Select(p => p)
                        });
        }

        class Phone
        {
            public string Name { get; set; }
            public string Company { get; set; }
        }
    }

    class LinqJoin
    {
        public static void Display()
        {
            join();
            groupJoin();
            zip();
        }

        static List<Team> teams = new List<Team>()
        {
            new Team { Name = "Бавария", Country ="Германия" },
            new Team { Name = "Барселона", Country ="Испания" }
        };
        static List<Player> players = new List<Player>()
        {
            new Player {Name="Месси", Team="Барселона"},
            new Player {Name="Неймар", Team="Барселона"},
            new Player {Name="Роббен", Team="Бавария"}
        };

        static void join()
        {
            var result = from pl in players
                         join t in teams on pl.Team equals t.Name
                         select new { Name = pl.Name, Team = pl.Team, Country = t.Country };

            foreach (var item in result)
                Console.WriteLine("{0} - {1} ({2})", item.Name, item.Team, item.Country);

            var resultExt = players.Join(teams, // второй набор
             p => p.Team, // свойство-селектор объекта из первого набора
             t => t.Name, // свойство-селектор объекта из второго набора
             (p, t) => new { Name = p.Name, Team = p.Team, Country = t.Country }); // результат
        }

        static void groupJoin()
        {
            var result = teams.GroupJoin(
                        players, // второй набор
                        t => t.Name, // свойство-селектор объекта из первого набора
                        pl => pl.Team, // свойство-селектор объекта из второго набора
                        (team, pls) => new  // результирующий объект
                        {
                            Name = team.Name,
                            Country = team.Country,
                            Players = pls.Select(p => p.Name)
                        });

            foreach (var team in result)
            {
                Console.WriteLine(team.Name);
                foreach (string player in team.Players)
                {
                    Console.WriteLine(player);
                }
                Console.WriteLine();
            }
        }

        static void zip()
        {
            List<Team> teams = new List<Team>()
            {
                new Team { Name = "Бавария", Country ="Германия" },
                new Team { Name = "Барселона", Country ="Испания" },
                new Team { Name = "Ювентус", Country ="Италия" }
            };
            List<Player> players = new List<Player>()
            {
                new Player {Name="Роббен", Team="Бавария"},
                new Player {Name="Неймар", Team="Барселона"},
                new Player {Name="Буффон", Team="Ювентус"}
            };
            var result2 = players.Zip(teams,
                                      (player, team) => new
                                      {
                                          Name = player.Name,
                                          Team = team.Name,
                                          Country = team.Country
                                      });
            foreach (var player in result2)
            {
                Console.WriteLine("{0} - {1} ({2})", player.Name, player.Team, player.Country);
            }
        }

        class Player
        {
            public string Name { get; set; }
            public string Team { get; set; }
        }
        class Team
        {
            public string Name { get; set; }
            public string Country { get; set; }
        }
    }

    class LinqAllAny
    {
        public static void Display()
        {
            all();
            any();
        }

        static List<User> users = new List<User>()
        {
            new User { Name = "Tom", Age = 23 },
            new User { Name = "Sam", Age = 43 },
            new User { Name = "Bill", Age = 35 }
        };

        static void all()
        {
            bool result1 = users.All(u => u.Age > 20); // true
            if (result1)
                Console.WriteLine("У всех пользователей возраст больше 20");
            else
                Console.WriteLine("Есть пользователи с возрастом меньше 20");

            bool result2 = users.All(u => u.Name.StartsWith("T")); //false
            if (result2)
                Console.WriteLine("У всех пользователей имя начинается с T");
            else
                Console.WriteLine("Не у всех пользователей имя начинается с T");
        }

        static void any()
        {
            bool result1 = users.Any(u => u.Age < 20); //false
            if (result1)
                Console.WriteLine("Есть пользователи с возрастом меньше 20");
            else
                Console.WriteLine("У всех пользователей возраст больше 20");

            bool result2 = users.Any(u => u.Name.StartsWith("T")); //true
            if (result2)
                Console.WriteLine("Есть пользователи, у которых имя начинается с T");
            else
                Console.WriteLine("Отсутствуют пользователи, у которых имя начинается с T");
        }

        class User
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }
    }
}
