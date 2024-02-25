using System;
using System.Reflection;

namespace ConsoleApp4
{
    internal class Program
    {
        public class Person
        {
            private string name;
            public int age;
            protected double height;
            internal bool isStudent;
            protected internal string country;

            public Person(string name, int age, double height, bool isStudent, string country)
            {
                this.name = name;
                this.age = age;
                this.height = height;
                this.isStudent = isStudent;
                this.country = country;
            }

            public void Show()
            {
                Console.WriteLine($"{name} {age} {height}m {(isStudent ? "Student" : "not Student")} from {country}");
            }

            public void Birthday()
            {
                age++;
                Console.WriteLine($"Happy Birthday! Now {name} is {age} years old.");
            }

            public bool IsAdult()
            {
                return age >= 18;
            }
            public void Say(string phrase)
            {
                Console.WriteLine($"{name} says:{phrase}");
            }
        }
        static void Main(string[] args)
        {
            Type personType = typeof(Person);
            Console.WriteLine($"Type:{ personType}");

            //TypeInfo
            Console.WriteLine("\n-------------");
            Console.WriteLine("TypeInfo");
            Console.WriteLine("-------------");
            TypeInfo personTypeInfo = personType.GetTypeInfo();
            Console.WriteLine("Is interface: " + personTypeInfo.IsInterface);
            Console.WriteLine("Is class: " + personTypeInfo.IsClass);
            Console.WriteLine("Is class abstract: " + personTypeInfo.IsAbstract);
            Console.WriteLine("Is class sealed: " + personTypeInfo.IsSealed);

            //MemberInfo
            Console.WriteLine("\n-------------");
            Console.WriteLine("MemberInfo");
            Console.WriteLine("-------------");
            MemberInfo[] members = personType.GetMembers();
            Console.WriteLine("Members:");
            foreach (MemberInfo member in members)
            {
                Console.WriteLine($"{member.MemberType}: {member.Name}");
            }

            //FieldInfo
            Console.WriteLine("\n-------------");
            Console.WriteLine("FieldInfo");
            Console.WriteLine("-------------");
            FieldInfo[] fields = personType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            Console.WriteLine("Fields:");
            foreach (FieldInfo field in fields)
            {
                Console.WriteLine($"{field.FieldType} {field.Name}");
            }

            //MethodInfo
            Console.WriteLine("\n-------------");
            Console.WriteLine("MethodInfo");
            Console.WriteLine("-------------");
            Console.WriteLine("Methods in Person class:");
            foreach (MethodInfo method in personType.GetMethods())
            {
                Console.WriteLine(method.Name);
            }

            //Reflection
            Console.WriteLine("\n-------------");
            Console.WriteLine("Reflection");
            Console.WriteLine("-------------");

            var person = new Person("Mike", 25, 1.93, true, "USA");

            MethodInfo methodShow = personType.GetMethod("Show");
            if (methodShow != null)
            {
                methodShow.Invoke(person, null);
            }

            MethodInfo methodIsAdult = personType.GetMethod("IsAdult");
            if (methodIsAdult != null)
            {
                bool isAdult = (bool)methodIsAdult.Invoke(person, null);
                Console.WriteLine($"Is Adult: {isAdult}");
            }

            MethodInfo methodSay = personType.GetMethod("Say");
            if (methodSay != null)
            {
                methodSay.Invoke(person, new object[] { "Hello" });
            }
            Console.ReadLine();
        }
    }
}
