using System;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

//логические проверки 
namespace validatorXML
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlSchemaSet schemas = new XmlSchemaSet();
            schemas.Add("", "XML1.xsd");

            XDocument custOrdDoc = XDocument.Load("XML1.xml");
            bool errors = false;
            custOrdDoc.Validate(schemas, (o, e) =>
            {
                    Console.WriteLine(e.Message);
                    errors = true;
            });
            Console.WriteLine("Group {0}", errors ? "did not pass validation" : "passed validation");

            Console.ReadLine();
        }
    }
}
