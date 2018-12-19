using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace MetanitSharp
{
    class XmlWork
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
                    case 'r':
                        readXml();
                        readUsers();
                        break;
                    case 'e':
                        addElement();
                        removeElement();
                        break;
                    case 'p':
                        usingXPath();
                        break;
                    case 'q':
                        linqToXml();
                        linqToXmlShort();
                        readLinq();
                        readPhones();
                        break;
                    case 'd':
                        editLinq();
                        break;
                    case 'x': return;
                }
                Console.ReadKey();
            }
        }

        static void printMenu()
        {
            Console.WriteLine("Нажмите клавишу для вывода информации");
            Console.WriteLine("R - чтение xml-файла");
            Console.WriteLine("E - редактирование xml-файла");
            Console.WriteLine("P - XPath");
            Console.WriteLine("Q - Linq to Xml");
            Console.WriteLine("D - изменение документа Linq to Xml");
            Console.WriteLine("X - выход из раздела");
        }

        static void readXml()
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(@"C:\Metanit\users.xml");
            // получим корневой элемент
            XmlElement xRoot = xDoc.DocumentElement;
            // обход всех узлов в корневом элементе
            foreach (XmlNode xnode in xRoot)
            {
                // получаем атрибут name
                if (xnode.Attributes.Count > 0)
                {
                    XmlNode attr = xnode.Attributes.GetNamedItem("name");
                    if (attr != null)
                        Console.WriteLine(attr.Value);
                }
                // обходим все дочерние узлы элемента user
                foreach (XmlNode childnode in xnode.ChildNodes)
                {
                    // если узел - company
                    if (childnode.Name == "company")
                    {
                        Console.WriteLine("Компания: {0}", childnode.InnerText);
                    }
                    // если узел age
                    if (childnode.Name == "age")
                    {
                        Console.WriteLine("Возраст: {0}", childnode.InnerText);
                    }
                }
                Console.WriteLine();
            }
        }

        static void readUsers()
        {
            List<User> users = new List<User>();

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(@"C:\Metanit\users.xml");
            XmlElement xRoot = xDoc.DocumentElement;
            foreach (XmlElement xnode in xRoot)
            {
                User user = new User();
                XmlNode attr = xnode.Attributes.GetNamedItem("name");
                if (attr != null)
                    user.Name = attr.Value;

                foreach (XmlNode childnode in xnode.ChildNodes)
                {
                    if (childnode.Name == "company")
                        user.Company = childnode.InnerText;

                    if (childnode.Name == "age")
                        user.Age = Int32.Parse(childnode.InnerText);
                }
                users.Add(user);
            }
            foreach (User u in users)
                Console.WriteLine("{0} ({1}) - {2}", u.Name, u.Company, u.Age);
        }

        static void addElement()
        {
            string fileName = @"C:\Metanit\users.xml";

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(fileName);
            XmlElement xRoot = xDoc.DocumentElement;
            // создаем новый элемент user
            XmlElement userElem = xDoc.CreateElement("user");
            // создаем атрибут name
            XmlAttribute nameAttr = xDoc.CreateAttribute("name");
            // создаем элементы company и age
            XmlElement companyElem = xDoc.CreateElement("company");
            XmlElement ageElem = xDoc.CreateElement("age");
            // создаем текстовые значения для элементов и атрибута
            XmlText nameText = xDoc.CreateTextNode("Mark Zuckerberg");
            XmlText companyText = xDoc.CreateTextNode("Facebook");
            XmlText ageText = xDoc.CreateTextNode("30");

            //добавляем узлы
            nameAttr.AppendChild(nameText);
            companyElem.AppendChild(companyText);
            ageElem.AppendChild(ageText);
            userElem.Attributes.Append(nameAttr);
            userElem.AppendChild(companyElem);
            userElem.AppendChild(ageElem);
            xRoot.AppendChild(userElem);
            xDoc.Save(fileName);

            Console.WriteLine($"File {fileName} saved");

            Console.ReadLine();
        }

        static void removeElement()
        {
            string fileName = @"C:\Metanit\users.xml";

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(fileName);
            XmlElement xRoot = xDoc.DocumentElement;

            XmlNode firstNode = xRoot.FirstChild;
            xRoot.RemoveChild(firstNode);
            xDoc.Save(fileName);

            Console.WriteLine($"File {fileName} saved");
        }

        static void usingXPath()
        {
            string fileName = @"C:\Metanit\users.xml";

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(fileName);
            XmlElement xRoot = xDoc.DocumentElement;

            // выбор всех дочерних узлов
            XmlNodeList childnodes = xRoot.SelectNodes("*");
            foreach (XmlNode n in childnodes)
                Console.WriteLine(n.OuterXml);

            childnodes = xRoot.SelectNodes("user");
            foreach (XmlNode n in childnodes)
                Console.WriteLine(n.SelectSingleNode("@name").Value);

            XmlNode childnode = xRoot.SelectSingleNode("user[@name='Bill Gates']");
            if (childnode != null)
                Console.WriteLine(childnode.OuterXml);

            childnode = xRoot.SelectSingleNode("user[company='Microsoft']");
            if (childnode != null)
                Console.WriteLine(childnode.OuterXml);

            childnodes = xRoot.SelectNodes("//user/company");
            foreach (XmlNode n in childnodes)
                Console.WriteLine(n.InnerText);
        }
        class User
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public string Company { get; set; }
        }

        static void linqToXml()
        {
            var fileName = @"C:\Metanit\phones.xml";

            XDocument xdoc = new XDocument();
            // создаем первый элемент
            XElement iphone6 = new XElement("phone");
            // создаем атрибут
            XAttribute iphoneNameAttr = new XAttribute("name", "iPhone 6");
            XElement iphoneCompanyElem = new XElement("company", "Apple");
            XElement iphonePriceElem = new XElement("price", "40000");
            // добавляем атрибут и элементы в первый элемент
            iphone6.Add(iphoneNameAttr);
            iphone6.Add(iphoneCompanyElem);
            iphone6.Add(iphonePriceElem);

            // создаем второй элемент
            XElement galaxys5 = new XElement("phone");
            XAttribute galaxysNameAttr = new XAttribute("name", "Samsung Galaxy S5");
            XElement galaxysCompanyElem = new XElement("company", "Samsung");
            XElement galaxysPriceElem = new XElement("price", "33000");
            galaxys5.Add(galaxysNameAttr);
            galaxys5.Add(galaxysCompanyElem);
            galaxys5.Add(galaxysPriceElem);
            // создаем корневой элемент
            XElement phones = new XElement("phones");
            // добавляем в корневой элемент
            phones.Add(iphone6);
            phones.Add(galaxys5);
            // добавляем корневой элемент в документ
            xdoc.Add(phones);
            //сохраняем документ
            xdoc.Save(fileName);

            Console.WriteLine($"Файл {fileName} сохранен");
        }
        
        static void linqToXmlShort()
        {
            var fileName = @"C:\Metanit\phones2.xml";

            XDocument xdoc = new XDocument(new XElement("phones",
            new XElement("phone",
                new XAttribute("name", "iPhone 6"),
                new XElement("company", "Apple"),
                new XElement("price", "40000")),
            new XElement("phone",
                new XAttribute("name", "Samsung Galaxy S5"),
                new XElement("company", "Samsung"),
                new XElement("price", "33000"))));
            xdoc.Save(fileName);

            Console.WriteLine($"Файл {fileName} сохранен");
        }

        static void readLinq()
        {
            var fileName = @"C:\Metanit\phones.xml";

            XDocument xdoc = XDocument.Load(fileName);
            foreach (XElement phoneElement in xdoc.Element("phones").Elements("phone"))
            {
                XAttribute nameAttribute = phoneElement.Attribute("name");
                XElement companyElement = phoneElement.Element("company");
                XElement priceElement = phoneElement.Element("price");

                if (nameAttribute != null && companyElement != null && priceElement != null)
                {
                    Console.WriteLine("Смартфон: {0}", nameAttribute.Value);
                    Console.WriteLine("Компания: {0}", companyElement.Value);
                    Console.WriteLine("Цена: {0}", priceElement.Value);
                }
                Console.WriteLine();
            }
        }

        static void readPhones()
        {
            var fileName = @"C:\Metanit\phones.xml";

            XDocument xdoc = XDocument.Load(fileName);
            var items = from xe in xdoc.Element("phones").Elements("phone")
                        where xe.Element("company").Value == "Samsung"
                        select new Phone
                        {
                            Name = xe.Attribute("name").Value,
                            Price = xe.Element("price").Value
                        };

            foreach (var item in items)
                Console.WriteLine("{0} - {1}", item.Name, item.Price);
        }

        static void editLinq()
        {
            var fileName = @"C:\Metanit\phones.xml";
            var resultFileName = @"C:\Metanit\phones-updated.xml";

            XDocument xdoc = XDocument.Load(fileName);
            XElement root = xdoc.Element("phones");

            foreach (XElement xe in root.Elements("phone").ToList())
            {
                // изменяем название и цену
                if (xe.Attribute("name").Value == "Samsung Galaxy S5")
                {
                    xe.Attribute("name").Value = "Samsung Galaxy Note 4";
                    xe.Element("price").Value = "31000";
                }
                //если iphone - удаляем его
                else if (xe.Attribute("name").Value == "iPhone 6")
                {
                    xe.Remove();
                }
            }
            // добавляем новый элемент
            root.Add(new XElement("phone",
                        new XAttribute("name", "Nokia Lumia 930"),
                        new XElement("company", "Nokia"),
                        new XElement("price", "19500")));
            xdoc.Save(resultFileName);
            // выводим xml-документ на консоль
            Console.WriteLine(xdoc);
        }

        class Phone
        {
            public string Name { get; set; }
            public string Price { get; set; }
        }

    }
}
