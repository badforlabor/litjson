using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Drawing;

namespace SHGame
{
    using LitJson;
    using System;

    public class Person
    {
        // C# 3.0 auto-implemented properties
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime Birthday { get; set; }
    }
    public class Person2
    {
        // ��ȻҲ֧����ͨ����
        public string Name;
        public int Age;
        public DateTime Birthday;
    }
    public class PersonGroup
    {
        public Dictionary<int, Person> Dict;
        public List<Person> Group;

        // ���ֲ�֧�֣�Dictionary��Key�����ǻ������͵Ĳſ��ԣ�
        public Dictionary<Person, Person> Dict2;
    }

    public class JsonSample
    {
        public static void Main()
        {
            PersonToJson();
            JsonToPerson();

            PersonGroupToJson();
            JsonToPersonGroup();

            ComplexClassToJson();

            Console.WriteLine("�����������...");
            Console.ReadKey();
        }
        public static void PersonGroupToJson()
        {
            Person bill = new Person();

            bill.Name = "William Shakespeare";
            bill.Age = 51;
            bill.Birthday = new DateTime(1564, 4, 26);

            PersonGroup pg = new PersonGroup();
            pg.Dict = new Dictionary<int, Person>();
            pg.Dict.Add(10, bill);
            pg.Group = new List<Person>();
            pg.Group.Add(bill);
            
            JsonHelper.saveObjectToJsonFile(pg, "./dict-test.txt");
        }
        public static void JsonToPersonGroup()
        {
            PersonGroup pg = JsonHelper.loadObjectFromJsonFile<PersonGroup>("./dict-test.txt");
            JsonHelper.saveObjectToJsonFile(pg, "./dict-test2.txt");
        }

        public static void PersonToJson()
        {
            Person bill = new Person();

            bill.Name = "William Shakespeare";
            bill.Age = 51;
            bill.Birthday = new DateTime(1564, 4, 26);

            string json_bill = JsonMapper.ToJson(bill);

            Console.WriteLine(json_bill);

            JsonHelper.saveObjectToJsonFile(bill, "./test.txt");
        }

        public static void JsonToPerson()
        {
            string json = @"
            {
                ""Name""     : ""Thomas More"",
                ""Age""      : 57,
                ""Birthday"" : ""02/07/1478 00:00:00""
            }";

            Person thomas2 = JsonMapper.ToObject<Person>(json);

            Person thomas = JsonHelper.loadObjectFromJsonFile<Person>("./test.txt");

            Console.WriteLine("Thomas' age: {0}", thomas.Age);
        }

        public static void ComplexClassToJson()
        {
            // case 1
            {
                // �Զ��嵼������
                JsonMapper.AddCustomTypeProperties(typeof(Rectangle), "X", "Y", "Width", "Height");

                Rectangle rect = new Rectangle(5, 10, 20, 30);
                JsonHelper.saveObjectToJsonFile(rect, "./rect.txt");
                Rectangle rect2 = JsonHelper.loadObjectFromJsonFile<Rectangle>("./rect.txt");
                JsonHelper.saveObjectToJsonFile(rect2, "./rect.txt");
            }

            // case 2
            {
                Person2 p2 = new Person2();
                p2.Name = "p2";
                p2.Age = 13;
                p2.Birthday = DateTime.Now;
                JsonHelper.saveObjectToJsonFile(p2, "./p2.txt");
                Person2 p22 = JsonHelper.loadObjectFromJsonFile<Person2>("./p2.txt");
                JsonHelper.saveObjectToJsonFile(p22, "./p2.txt");
            }
        }
    }
}