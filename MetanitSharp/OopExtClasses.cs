using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace MetanitSharp
{
    namespace AccountSpace
    {
        class Account
        {
            public int Id { get; private set; }
            public Account(int _id)
            {
                Id = _id;
            }

            public void Display()
            {
                Console.WriteLine($"Account Id={Id}");
            }
        }

        namespace ClientSpace
        {
            class Client
            {
                public int Id { get; private set; }
                public Client(int _id)
                {
                    Id = _id;
                }

                public void Display()
                {
                    Console.WriteLine($"Client Id={Id}");
                }
            }

            namespace VipSpace
            {
                class VipClient
                {
                    public int Id { get; private set; }
                    public VipClient(int _id)
                    {
                        Id = _id;
                    }

                    public static void DisplayVip(VipClient client)
                    {
                        Console.WriteLine($"VipClient Id={client.Id}");
                    }
                }
            }
        }
    }

    namespace StaticSpace
    {
        class Message
        {
            public static void ShowMyMessage(String text)
            {
                Console.WriteLine(text);
            }
        }
    }

    class Geometry
    {
        public static double GetArea(double radius)
        {
            return PI * radius * radius; // Math.PI
        }
    }

    namespace ExtentionMethodDemo
    {
        public static class StringExtension
        {
            public static int CharCount(this string str, char c)
            {
                int counter = 0;
                for (int i = 0; i < str.Length; i++)
                {
                    if (str[i] == c)
                        counter++;
                }
                return counter;
            }

            public static T To<T>(this string text)
            {
                try
                {
                    return (T)Convert.ChangeType(text, typeof(T));
                }
                catch
                {
                    return default(T);
                }
            }

        }
    }
}
