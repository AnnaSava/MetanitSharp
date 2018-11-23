using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MetanitSharp
{
    class SerializationBinary
    {
        public static void Display()
        {
            serializeObject();
            serializeArray();
        }

        static void serializeObject()
        {
            // объект для сериализации
            Person person = new Person("Tom", 29);
            Console.WriteLine("Объект создан");

            // создаем объект BinaryFormatter
            BinaryFormatter formatter = new BinaryFormatter();
            // получаем поток, куда будем записывать сериализованный объект
            using (FileStream fs = new FileStream(@"C:\Metanit\person.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, person);

                Console.WriteLine("Объект сериализован");
            }

            // десериализация из файла people.dat
            using (FileStream fs = new FileStream(@"C:\Metanit\person.dat", FileMode.OpenOrCreate))
            {
                Person newPerson = (Person)formatter.Deserialize(fs);

                Console.WriteLine("Объект десериализован");
                Console.WriteLine("Имя: {0} --- Возраст: {1}", newPerson.Name, newPerson.Age);
            }
        }

        static void serializeArray()
        {
            Person person1 = new Person("Tom", 29);
            Person person2 = new Person("Bill", 25);
            // массив для сериализации
            Person[] people = new Person[] { person1, person2 };

            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream(@"C:\Metanit\people.dat", FileMode.OpenOrCreate))
            {
                // сериализуем весь массив people
                formatter.Serialize(fs, people);

                Console.WriteLine("Объект сериализован");
            }

            // десериализация
            using (FileStream fs = new FileStream(@"C:\Metanit\people.dat", FileMode.OpenOrCreate))
            {
                Person[] deserilizePeople = (Person[])formatter.Deserialize(fs);

                foreach (Person p in deserilizePeople)
                {
                    Console.WriteLine("Имя: {0} --- Возраст: {1}", p.Name, p.Age);
                }
            }

        }

        [Serializable]
        class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }

            public Person(string name, int age)
            {
                Name = name;
                Age = age;
            }
        }
    }

    class SerializationSoap
    {
        public static void Display()
        {
            Person person = new Person("Tom", 29);
            Person person2 = new Person("Bill", 25);
            Person[] people = new Person[] { person, person2 };

            // создаем объект SoapFormatter
            SoapFormatter formatter = new SoapFormatter();
            // получаем поток, куда будем записывать сериализованный объект
            using (FileStream fs = new FileStream(@"C:\Metanit\people.soap", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, people);

                Console.WriteLine("Объект сериализован");
            }

            // десериализация
            using (FileStream fs = new FileStream(@"C:\Metanit\people.soap", FileMode.OpenOrCreate))
            {
                Person[] newPeople = (Person[])formatter.Deserialize(fs);

                Console.WriteLine("Объект десериализован");
                foreach (Person p in newPeople)
                {
                    Console.WriteLine("Имя: {0} --- Возраст: {1}", p.Name, p.Age);
                }
            }
        }

        [Serializable]
        class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }

            public Person(string name, int age)
            {
                Name = name;
                Age = age;
            }
        }
    }

    public class SerializationXml
    {
        public static void Display()
        {
            serializeSingle();
            serializeArr();
            serializeComplex();
        }

        static void serializeSingle()
        {
            // объект для сериализации
            Person person = new Person("Tom", 29);
            Console.WriteLine("Объект создан");

            // передаем в конструктор тип класса
            XmlSerializer formatter = new XmlSerializer(typeof(Person));

            // получаем поток, куда будем записывать сериализованный объект
            using (FileStream fs = new FileStream(@"C:\Metanit\person.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, person);

                Console.WriteLine("Объект сериализован");
            }

            // десериализация
            using (FileStream fs = new FileStream(@"C:\Metanit\person.xml", FileMode.OpenOrCreate))
            {
                Person newPerson = (Person)formatter.Deserialize(fs);

                Console.WriteLine("Объект десериализован");
                Console.WriteLine("Имя: {0} --- Возраст: {1}", newPerson.Name, newPerson.Age);
            }
        }

        static void serializeArr()
        {
            Person person1 = new Person("Tom", 29);
            Person person2 = new Person("Bill", 25);
            Person[] people = new Person[] { person1, person2 };

            XmlSerializer formatter = new XmlSerializer(typeof(Person[]));

            using (FileStream fs = new FileStream(@"C:\Metanit\people.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, people);
            }

            using (FileStream fs = new FileStream(@"C:\Metanit\people.xml", FileMode.OpenOrCreate))
            {
                Person[] newpeople = (Person[])formatter.Deserialize(fs);

                foreach (Person p in newpeople)
                {
                    Console.WriteLine("Имя: {0} --- Возраст: {1}", p.Name, p.Age);
                }
            }
        }

        static void serializeComplex()
        {
            Employee person1 = new Employee("Tom", 29, new Company("Microsoft"));
            Employee person2 = new Employee("Bill", 25, new Company("Apple"));
            Employee[] people = new Employee[] { person1, person2 };

            XmlSerializer formatter = new XmlSerializer(typeof(Employee[]));

            using (FileStream fs = new FileStream(@"C:\Metanit\employees.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, people);
            }

            using (FileStream fs = new FileStream(@"C:\Metanit\employees.xml", FileMode.OpenOrCreate))
            {
                Employee[] newpeople = (Employee[])formatter.Deserialize(fs);

                foreach (Employee p in newpeople)
                {
                    Console.WriteLine("Имя: {0} --- Возраст: {1} --- Компания: {2}", p.Name, p.Age, p.Company.Name);
                }
            }
        }

        // класс и его члены объявлены как public
        [Serializable]
        public class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }

            // стандартный конструктор без параметров
            public Person()
            { }

            public Person(string name, int age)
            {
                Name = name;
                Age = age;
            }
        }

        [Serializable]
        public class Employee
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public Company Company { get; set; }

            public Employee()
            { }

            public Employee(string name, int age, Company comp)
            {
                Name = name;
                Age = age;
                Company = comp;
            }
        }

        [Serializable]
        public class Company
        {
            public string Name { get; set; }

            // стандартный конструктор без параметров
            public Company() { }

            public Company(string name)
            {
                Name = name;
            }
        }
    }

    class SerializationJson
    {
        public static void Display()
        {
            serialize();
            serializeComplex();
        }

        static void serialize()
        {
            // объект для сериализации
            Person person1 = new Person("Tom", 29);
            Person person2 = new Person("Bill", 25);
            Person[] people = new Person[] { person1, person2 };

            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Person[]));

            using (FileStream fs = new FileStream(@"C:\Metanit\people.json", FileMode.OpenOrCreate))
            {
                jsonFormatter.WriteObject(fs, people);
            }

            using (FileStream fs = new FileStream(@"C:\Metanit\people.json", FileMode.OpenOrCreate))
            {
                Person[] newpeople = (Person[])jsonFormatter.ReadObject(fs);

                foreach (Person p in newpeople)
                {
                    Console.WriteLine("Имя: {0} --- Возраст: {1}", p.Name, p.Age);
                }
            }
        }

        static void serializeComplex()
        {
            Employee person1 = new Employee("Tom", 29, new Company("Microsoft"));
            Employee person2 = new Employee("Bill", 25, new Company("Apple"));
            Employee[] people = new Employee[] { person1, person2 };

            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Employee[]));

            using (FileStream fs = new FileStream(@"C:\Metanit\employees.json", FileMode.OpenOrCreate))
            {
                jsonFormatter.WriteObject(fs, people);
            }

            using (FileStream fs = new FileStream(@"C:\Metanit\employees.json", FileMode.OpenOrCreate))
            {
                Employee[] newpeople = (Employee[])jsonFormatter.ReadObject(fs);

                foreach (Employee p in newpeople)
                {
                    Console.WriteLine("Имя: {0} --- Возраст: {1} --- Компания: {2}", p.Name, p.Age, p.Company.Name);
                }
            }
        }

        [DataContract]
        public class Person
        {
            [DataMember]
            public string Name { get; set; }
            [DataMember]
            public int Age { get; set; }

            public Person(string name, int year)
            {
                Name = name;
                Age = year;
            }
        }

        [DataContract]
        public class Employee
        {
            [DataMember]
            public string Name { get; set; }
            [DataMember]
            public int Age { get; set; }
            [DataMember]
            public Company Company { get; set; }

            public Employee()
            { }

            public Employee(string name, int age, Company comp)
            {
                Name = name;
                Age = age;
                Company = comp;
            }
        }

        [DataContract]
        public class Company
        {
            [DataMember]
            public string Name { get; set; }

            public Company() { }

            public Company(string name)
            {
                Name = name;
            }
        }
    }
}
