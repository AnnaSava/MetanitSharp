using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetanitSharp
{
    class ObservableCollectionDemo
    {
        public static void Display()
        {
            ObservableCollection<User> users = new ObservableCollection<User>
            {
                new User { Name = "Bill"},
                new User { Name = "Tom"},
                new User { Name = "Alice"}
            };

            users.CollectionChanged += Users_CollectionChanged;

            users.Add(new User { Name = "Bob" });
            users.RemoveAt(1);
            users[0] = new User { Name = "Anders" };

            foreach (User user in users)
            {
                Console.WriteLine(user.Name);
            }
        }

        private static void Users_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add: // если добавление
                    User newUser = e.NewItems[0] as User;
                    Console.WriteLine("Добавлен новый объект: {0}", newUser.Name);
                    break;
                case NotifyCollectionChangedAction.Remove: // если удаление
                    User oldUser = e.OldItems[0] as User;
                    Console.WriteLine("Удален объект: {0}", oldUser.Name);
                    break;
                case NotifyCollectionChangedAction.Replace: // если замена
                    User replacedUser = e.OldItems[0] as User;
                    User replacingUser = e.NewItems[0] as User;
                    Console.WriteLine("Объект {0} заменен объектом {1}",
                                        replacedUser.Name, replacingUser.Name);
                    break;
            }
        }

        class User
        {
            public string Name { get; set; }
        }
    }

    class EnumeratorDemo
    {
        public static void Display()
        {
            int[] numbers = { 0, 2, 4, 6, 8, 10 };

            IEnumerator ie = numbers.GetEnumerator(); // получаем IEnumerator
            while (ie.MoveNext())   // пока не будет возвращено false
            {
                int item = (int)ie.Current;     // берем элемент на текущей позиции
                Console.WriteLine(item);
            }
            ie.Reset(); // сбрасываем указатель в начало массива

            Rainbow rainbow = new Rainbow();
            foreach(var color in rainbow)
            {
                Console.WriteLine(color);
            }

            Week week = new Week();
            foreach (var day in week)
            {
                Console.WriteLine(day);
            }

            Year year = new Year();
            foreach(var month in year)
            {
                Console.WriteLine(month);
            }
        }

        class Rainbow : IEnumerable
        {
            string[] colors = { "Red", "Orange", "Yellow", "Green",
                         "Cyan", "Blue", "Violet" };

            public IEnumerator GetEnumerator()
            {
                return colors.GetEnumerator();
            }
        }

        class Week : IEnumerable
        {
            string[] days = { "Monday", "Tuesday", "Wednesday", "Thursday",
                         "Friday", "Saturday", "Sunday" };

            public IEnumerator GetEnumerator()
            {
                return new WeekEnumerator(days);
            }
        }

        class WeekEnumerator : IEnumerator
        {
            string[] days;
            int position = -1;
            public WeekEnumerator(string[] days)
            {
                this.days = days;
            }
            public object Current
            {
                get
                {
                    if (position == -1 || position >= days.Length)
                        throw new InvalidOperationException();
                    return days[position];
                }
            }

            public bool MoveNext()
            {
                if (position < days.Length - 1)
                {
                    position++;
                    return true;
                }
                else
                    return false;
            }

            public void Reset()
            {
                position = -1;
            }
        }

        class Year
        {
            string[] months = { "January", "February", "March", "April",
                        "May", "June", "July", "August", "September", "October",
                        "November", "December"};

            public IEnumerator<string> GetEnumerator()
            {
                return new YearEnumerator(months);
            }
        }

        class YearEnumerator : IEnumerator<string>
        {
            string[] months;
            int position = -1;
            public YearEnumerator(string[] months)
            {
                this.months = months;
            }

            public string Current
            {
                get
                {
                    if (position == -1 || position >= months.Length)
                        throw new InvalidOperationException();
                    return months[position];
                }
            }

            object IEnumerator.Current => throw new NotImplementedException();

            public bool MoveNext()
            {
                if (position < months.Length - 1)
                {
                    position++;
                    return true;
                }
                else
                    return false;
            }

            public void Reset()
            {
                position = -1;
            }
            public void Dispose() { }
        }
    }

    class YieldDemo
    {
        public static void Display()
        {
            Numbers numbers = new Numbers();
            foreach (int n in numbers)
            {
                Console.WriteLine(n);
            }

            Library library = new Library();
            foreach (Book b in library)
            {
                Console.WriteLine(b.Name);
            }

            LibraryYields library2 = new LibraryYields();
            foreach (Book b in library2)
            {
                Console.WriteLine(b.Name);
            }

            LibraryNamed library3 = new LibraryNamed();
            foreach (Book b in library3.GetBooks(5))
            {
                Console.WriteLine(b.Name);
            }
        }

        class Numbers
        {
            public IEnumerator GetEnumerator()
            {
                for (int i = 0; i < 6; i++)
                {
                    yield return i * i;
                }
            }
        }

        class Book
        {
            public Book(string name)
            {
                this.Name = name;
            }
            public string Name { get; set; }
        }

        class Library
        {
            private Book[] books;

            public Library()
            {
                books = new Book[] { new Book("Отцы и дети"), new Book("Война и мир"),
                new Book("Евгений Онегин") };
            }

            public int Length
            {
                get { return books.Length; }
            }

            public IEnumerator GetEnumerator()
            {
                for (int i = 0; i < books.Length; i++)
                {
                    yield return books[i];
                }
            }
        }

        class LibraryYields
        {
            private Book[] books;

            public LibraryYields()
            {
                books = new Book[] { new Book("Отцы и дети"), new Book("Война и мир"),
                new Book("Евгений Онегин") };
            }

            public int Length
            {
                get { return books.Length; }
            }

            public IEnumerator GetEnumerator()
            {
                yield return books[0];
                yield return books[1];
                yield return books[2];
            }
        }

        class LibraryNamed
        {
            private Book[] books;

            public LibraryNamed()
            {
                books = new Book[] { new Book("Отцы и дети"), new Book("Война и мир"),
            new Book("Евгений Онегин") };
            }

            public int Length
            {
                get { return books.Length; }
            }

            public IEnumerable GetBooks(int max)
            {
                for (int i = 0; i < max; i++)
                {
                    if (i == books.Length)
                    {
                        yield break;
                    }
                    else
                    {
                        yield return books[i];
                    }
                }
            }
        }
    }
}
