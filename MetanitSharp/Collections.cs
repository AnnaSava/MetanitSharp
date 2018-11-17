using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetanitSharp
{
    class Collections
    {
        public static void Run()
        {
            char key;

            while (true)
            {
                printMenu();

                key = Console.ReadKey().KeyChar;

                Console.WriteLine();
                switch (key)
                {
                    case 'c':
                        usingCollections();
                        arrayList();
                        break;
                    case 'g':
                        genericList();
                        break;
                    case 'l':
                        linkedList();
                        break;
                    case 'q':
                        queue();
                        break;
                    case 's':
                        stack();
                        break;
                    case 'd':
                        dictionary();
                        break;
                    case 'o':
                        ObservableCollectionDemo.Display();
                        break;
                    case 'e':
                        EnumeratorDemo.Display();
                        break;
                    case 'y':
                        YieldDemo.Display();
                        break;
                    case 'x': return;
                }
                Console.ReadKey();
            }
        }

        static void printMenu()
        {
            Console.WriteLine("Нажмите клавишу для вывода информации");
            Console.WriteLine("C - пример использования коллекций");
            Console.WriteLine("G - обобщенный список List<T>");
            Console.WriteLine("L - двусвязный список LinkedList<T>");
            Console.WriteLine("Q - очередь Queue<T>");
            Console.WriteLine("S - стек Stack<T>");
            Console.WriteLine("D - словарь Dictionary<TKey, TValue>");
            Console.WriteLine("O - класс ObservableCollection");
            Console.WriteLine("E - перечислители");
            Console.WriteLine("Y - итераторы и yield");
            Console.WriteLine("X - выход из раздела");
        }

        static void usingCollections()
        {
            // необобщенная коллекция ArrayList
            ArrayList objectList = new ArrayList() { 1, 2, "string", 'c', 2.0f };

            object obj = 45.8;

            objectList.Add(obj);
            objectList.Add("string2");
            objectList.RemoveAt(0); // удаление первого элемента
            foreach (object o in objectList)
            {
                Console.WriteLine(o);
            }
            Console.WriteLine("Общее число элементов коллекции: {0}", objectList.Count);

            // обобщенная коллекция List
            List<string> countries = new List<string>() { "Россия", "США", "Великобритания", "Китай" };
            countries.Add("Франция");
            countries.RemoveAt(1); // удаление второго элемента
            foreach (string s in countries)
            {
                Console.WriteLine(s);
            }
        }

        static void arrayList()
        {
            ArrayList list = new ArrayList();
            list.Add(2.3); // заносим в список объект типа double
            list.Add(55); // заносим в список объект типа int
            list.AddRange(new string[] { "Hello", "world" }); // заносим в список строковый массив

            // перебор значений
            foreach (object o in list)
            {
                Console.WriteLine(o);
            }

            // удаляем первый элемент
            list.RemoveAt(0);
            // переворачиваем список
            list.Reverse();
            // получение элемента по индексу
            Console.WriteLine(list[0]);
            // перебор значений
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine(list[i]);
            }
        }

        static void genericList()
        {
            List<int> numbers = new List<int>() { 1, 2, 3, 4, 45 };
            numbers.Add(6); // добавление элемента

            numbers.AddRange(new int[] { 7, 8, 9 });

            numbers.Insert(0, 666); // вставляем на первое место в списке число 666

            numbers.RemoveAt(1); //  удаляем второй элемент

            foreach (int i in numbers)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();

            numbers.Sort();

            foreach (int i in numbers)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();

            int found = numbers.BinarySearch(7);
            Console.WriteLine($"found index={found}");

            List<Person> people = new List<Person>(3);
            people.Add(new Person() { Name = "Том", LastName = "Иванов" });
            people.Add(new Person() { Name = "Билл", LastName = "Петров" });
            people.Add(new Person() { Name = "Боб", LastName = "Сидоров" });

            foreach (Person p in people)
            {
                Console.WriteLine($"{p.Name} {p.LastName}");
            }
        }

        static void linkedList()
        {
            LinkedList<int> numbers = new LinkedList<int>();

            numbers.AddLast(1); // вставляем узел со значением 1 на последнее место
            // так как в списке нет узлов, то последнее будет также и первым
            numbers.AddFirst(2); // вставляем узел со значением 2 на первое место
            numbers.AddAfter(numbers.Last, 3); // вставляем после последнего узла новый узел со значением 3
            // теперь у нас список имеет следующую последовательность: 2, 1, 3
            foreach (int i in numbers)
            {
                Console.WriteLine(i);
            }

            LinkedList<Person> persons = new LinkedList<Person>();

            // добавляем persona в список и получим объект LinkedListNode<Person>, в котором хранится имя Tom
            LinkedListNode<Person> tom = persons.AddLast(new Person() { Name = "Tom" });
            persons.AddLast(new Person() { Name = "John" });
            persons.AddFirst(new Person() { Name = "Bill" });

            Console.WriteLine(tom.Previous.Value.Name); // получаем узел перед томом и его значение
            Console.WriteLine(tom.Next.Value.Name); // получаем узел после тома и его значение

            LinkedListNode<Person> artur = new LinkedListNode<Person>(new Person() { Name = "Artur" });
            persons.AddAfter(tom.Previous, artur);

            foreach (Person p in persons)
            {
                Console.WriteLine($"{p.Name} {p.LastName}");
            }
        }

        static void queue()
        {
            Queue<int> numbers = new Queue<int>();

            numbers.Enqueue(3); // очередь 3
            numbers.Enqueue(5); // очередь 3, 5
            numbers.Enqueue(8); // очередь 3, 5, 8

            // получаем первый элемент очереди
            int queueElement = numbers.Dequeue(); //теперь очередь 5, 8
            Console.WriteLine(queueElement);

            Queue<Person> persons = new Queue<Person>();
            persons.Enqueue(new Person() { Name = "Tom" });
            persons.Enqueue(new Person() { Name = "Bill" });
            persons.Enqueue(new Person() { Name = "John" });

            // получаем первый элемент без его извлечения
            Person pp = persons.Peek();
            Console.WriteLine(pp.Name);

            Console.WriteLine("Сейчас в очереди {0} человек", persons.Count);

            // теперь в очереди Tom, Bill, John
            foreach (Person p in persons)
            {
                Console.WriteLine(p.Name);
            }

            // Извлекаем первый элемент в очереди - Tom
            Person person = persons.Dequeue(); // теперь в очереди Bill, John
            Console.WriteLine(person.Name);
        }

        static void stack()
        {
            Stack<int> numbers = new Stack<int>();

            numbers.Push(3); // в стеке 3
            numbers.Push(5); // в стеке 5, 3
            numbers.Push(8); // в стеке 8, 5, 3

            // так как вверху стека будет находиться число 8, то оно и извлекается
            int stackElement = numbers.Pop(); // в стеке 5, 3
            Console.WriteLine(stackElement);

            Stack<Person> persons = new Stack<Person>();
            persons.Push(new Person() { Name = "Tom" });
            persons.Push(new Person() { Name = "Bill" });
            persons.Push(new Person() { Name = "John" });

            foreach (Person p in persons)
            {
                Console.WriteLine(p.Name);
            }

            // Первый элемент в стеке
            Person person = persons.Pop(); // теперь в стеке Bill, Tom
            Console.WriteLine(person.Name);
        }

        static void dictionary()
        {
            Dictionary<int, string> countries = new Dictionary<int, string>(5);
            countries.Add(1, "Russia");
            countries.Add(3, "Great Britain");
            countries.Add(2, "USA");
            countries.Add(4, "France");
            countries.Add(5, "China");

            foreach (KeyValuePair<int, string> keyValue in countries)
            {
                Console.WriteLine(keyValue.Key + " - " + keyValue.Value);
            }

            // получение элемента по ключу
            string country = countries[4];
            // изменение объекта
            countries[4] = "Spain";
            // удаление по ключу
            countries.Remove(2);

            Dictionary<char, Person> people = new Dictionary<char, Person>();
            people.Add('b', new Person() { Name = "Bill" });
            people.Add('t', new Person() { Name = "Tom" });
            people.Add('j', new Person() { Name = "John" });

            people['a'] = new Person() { Name = "Alice" };

            foreach (KeyValuePair<char, Person> keyValue in people)
            {
                // keyValue.Value представляет класс Person
                Console.WriteLine(keyValue.Key + " - " + keyValue.Value.Name);
            }

            // перебор ключей
            foreach (char c in people.Keys)
            {
                Console.WriteLine(c);
            }

            // перебор по значениям
            foreach (Person p in people.Values)
            {
                Console.WriteLine(p.Name);
            }

            // В C# 5.0 мы могли инициализировать словари следующим образом
            Dictionary<string, string> countriesInit = new Dictionary<string, string>
            {
                {"Франция", "Париж"},
                {"Германия", "Берлин"},
                {"Великобритания", "Лондон"}
            };

            // начиная с C# 6.0 (Visual Studio 2015) доступен также еще один способ инициализации
            Dictionary<string, string> countriesInitVS2015 = new Dictionary<string, string>
            {
                ["Франция"] = "Париж",
                ["Германия"] = "Берлин",
                ["Великобритания"] = "Лондон"
            };
        }

        class Person
        {
            public string Name { get; set; }

            public string LastName { get; set; }
        }
    }
}
