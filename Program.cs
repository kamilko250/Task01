using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ReadFileXML
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlTextReader reader = new XmlTextReader("data.xml");
            int sum = 0;
            int scanOrNo = 0;
            while (reader.Read())
            {

                if (reader.NodeType == XmlNodeType.Element && reader.Name == "number" )
                {
                    string s1 = reader.ReadElementString();
                    if (scanOrNo % 2 == 0 && reader.GetAttribute("number") != "igonre-it")
                        sum += int.Parse(s1);

                    Console.WriteLine(s1);
                }
                else if (reader.Name == "ignore-it")
                {
                    scanOrNo += 1;
                }
                      
            }
            Console.WriteLine("sum = "+sum);       
            Console.ReadKey();
        }
    }
   
}
