    using System;
using System.Xml;
using System.IO;

namespace Lab6
{
    class Program
    {

        static public int DisplayMenu()
        {
            Console.WriteLine("\n\nРабота с Xml-файлом");
            Console.WriteLine();
            Console.WriteLine("1. Поиск информации, содержащейся в документе");
            Console.WriteLine("2. Доступ к содержимому узлов");
            Console.WriteLine("3. Изменение документа");
            Console.WriteLine("4. Выход из программы");
            Console.WriteLine("Выберите опцию: ");
            var result = Console.ReadLine();
            return Convert.ToInt32(result);
        }
        static void Main(string[] args)
        {
            //int choice = DisplayMenu();
            // 1. Reading document
            XmlDocument inputXML = new XmlDocument();
            inputXML.Load("groups.xml");
            while(true)
            {
                int choice = DisplayMenu();
                switch (choice)
                {
                    case 1:
                        //2. Finding information 
                        Console.Write("This names were found in the document:\r\n");
                        XmlNodeList contents = inputXML.GetElementsByTagName("Name");
                        for (int i = 0; i < contents.Count; i++)
                            Console.Write(contents[i].ChildNodes[0].Value + "\r\n");

                        Console.Write("\n\nThis group has id = 11:\r\n");
                        XmlElement groupID = inputXML.GetElementById("11");
                        //Console.WriteLine(groupID.OuterXml);
                        Console.Write(groupID.ChildNodes[0].ChildNodes[0].Value + "\t");
                        Console.Write(groupID.ChildNodes[1].ChildNodes[0].Value + "\r\n");

                        Console.Write("\n\nGroups from Ireland are:\r\n");
                        XmlNodeList name = inputXML.SelectNodes("//Group/Name/text()[../../Country/text()='Ireland']");
                        for (int i = 0; i < name.Count; i++)
                            Console.Write(name[i].Value + "\r\n");

                        Console.Write("\n\n First group from Ireland is:\r\n");
                        XmlNode firstName = inputXML.SelectSingleNode("//Group/Name/text()[../../Country/text()='Ireland']");
                        Console.Write(firstName.Value + "\r\n");

                        break;
                    case 2:
                        // 3. Access to nodes
                        /*Console.Write("\n" + inputXML.DocumentElement.InnerXml + "\r\n");

                        Console.Write("\n\nNames of groups: \n");
                        XmlNodeList group = inputXML.GetElementsByTagName("Group");
                        for (int i = 0; i < group.Count; i++)
                        {
                            Console.Write(group[i].ChildNodes[0].InnerText + "\n");
                        }
                        */
                        //вывод любого комментария

                        // Console.Write("\nComment:\n");
                        //Console.Write(group[0].ChildNodes[2].InnerText + "\n");

                        foreach (XmlNode com in inputXML.SelectNodes("//comment()"))
                             Console.WriteLine(com.Value);

                       /* if (inputXML.FirstChild is XmlProcessingInstruction)
                        {
                            XmlProcessingInstruction processInfo = (XmlProcessingInstruction)inputXML.FirstChild;
                            Console.WriteLine(processInfo.Data);
                            Console.WriteLine(processInfo.Name);
                        }

                        Console.Write("\n\nIDs of groups: \n");
                        for (int i = 0; i < group.Count; i++)
                            Console.Write(group[i].ChildNodes[0].InnerText + " : " + group[i].Attributes[0].Value + "\r\n");
                            */
                        break;
                    case 3:
                        //3. Changes file
                        XmlNodeList change = inputXML.GetElementsByTagName("Group");

                        XmlElement pcElement = (XmlElement)inputXML.GetElementsByTagName("Name")[1];
                        change[1].RemoveChild(pcElement);
                        Console.Write("\n Delete the second groups's name..." + "\r\n");
                        inputXML.Save("groups-deleted.xml");

                        XmlNodeList nameValues = inputXML.SelectNodes("//Group/Name/text()");
                        for (int i = 0; i < nameValues.Count; i++)
                            nameValues[i].Value = nameValues[i].Value + "::?:?:";
                        Console.Write("\n Change format of name..." + "\r\n");
                        inputXML.Save("groups-chg.xml");

                         XmlElement GroupElement = inputXML.CreateElement("Group");
                         XmlElement nameElement = inputXML.CreateElement("Name");
                         XmlElement countryElement = inputXML.CreateElement("Country");


                         XmlText nameText = inputXML.CreateTextNode("DAJE DVA");
                         XmlText country = inputXML.CreateTextNode("Russia");

                         nameElement.AppendChild(nameText);
                         countryElement.AppendChild(country);

                         GroupElement.AppendChild(nameElement);
                         GroupElement.AppendChild(countryElement);
                         GroupElement.SetAttribute("Id", "16565");

                         inputXML.DocumentElement.AppendChild(GroupElement);
                         inputXML.Save("groups-new.xml");
                        
                        break;

                    case 4:
                        System.Environment.Exit(0);
                        break;
                }
            }
        }
    }
}
