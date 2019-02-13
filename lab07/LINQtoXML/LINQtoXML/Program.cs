using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace LINQtoXML
{
    class Program
    {
        static void Main(string[] args)
        {
            int status = -1;            
            while (status == -1)
            {
                Console.WriteLine("1.Извлечь данные\n2. Вывести первый элемент\n3. Обновить XML\n4. Записать новые строки в XML\n0. Выход");
                Console.WriteLine("Выбор: ");
                status = Convert.ToInt32(Console.ReadLine());
                switch (status)
                {
                    case 1:
                        ReadXML();
                        status = -1;
                        break;
                    case 2:
                        ReadOneElement();
                        status = -1;
                        break;
                    case 3:
                        UpdateXML();
                        status = -1;
                        break;
                    case 4:
                        WriteXML();
                        status = -1;
                        break;
                    case 0:
                        Console.WriteLine("Завершение");
                        break;
                    default:
                        Console.WriteLine("Неизвестный пункт меню");
                        break;
                }
            }          
        }

        // Извлечение данных
        static void ReadXML()
        {
            XDocument xdoc = XDocument.Load(@"XML.xml");
            var result = from groups in xdoc.Descendants("Group")
                         select groups.Element("Album").Value;
            Console.WriteLine("Найдено {0} групп", result.Count());
            for (int i = 0; i < result.Count(); i++)
            {
                Console.WriteLine("имя: " + result.ElementAt(i) + "\n");
            }
            Console.WriteLine();
    
        }
        // Чтение
        static void ReadOneElement()
        {
            XDocument xdoc = XDocument.Load(@"XML.xml");
            XElement xgroup = xdoc.Element("GroupName").Element("Group");
            XAttribute nameAttribute = xgroup.Attribute("Name");
            XElement albumElement = xgroup.Element("Album");

            Console.WriteLine("\n\nID: {0}", nameAttribute.Value);
            Console.WriteLine("Имя: {0}", albumElement.Value);
        }
        // Обновление
        static void UpdateXML()
        {
            XDocument xdoc = XDocument.Load(@"XML.xml");
            IEnumerable<XElement> answerElements = xdoc.Descendants("Group");
            XElement toChange = answerElements.ElementAtOrDefault(1);

            Console.WriteLine("\n\nИзмененный элемент\n");

            Console.WriteLine("До:");
            Console.WriteLine(toChange.Element("Album").Value);

            toChange.SetElementValue("Album", "Wow new album");
            xdoc.Save("updated.xml");
            Console.WriteLine("После:");
            Console.WriteLine(toChange.Element("Album").Value);
        }

        // Запись
        static void WriteXML()
        {
            XDocument xdoc = XDocument.Load(@"XML.xml");
            XElement root = new XElement("GroupName");
            XElement newgroup = new XElement("Group");
            XAttribute id = new XAttribute("Name", "101");
            XElement name = new XElement("Album", "505");
            

            Console.WriteLine("\nСоздан XML-файл\n");

            newgroup.Add(id, name);
            xdoc.Element("GroupName").Add(newgroup);
            xdoc.Save("written.xml");

            /*Console.WriteLine("Добавленные элементы:\n");*/
            
        }
    }
}
