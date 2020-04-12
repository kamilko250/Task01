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
	    //Chcę żeby ścieżka do tego pliku była podawana z linii poleceń, np. Program.exe C:\\test\data.xml
            XmlTextReader reader = new XmlTextReader("data.xml");
            int sum = 0;
            int has_atr_ignore_it = 0;
            int bfr_sum = 0;
            while (reader.Read())
            {

                if (reader.NodeType == XmlNodeType.Element &&(reader.Name == "ns1:number" || reader.Name == "number"))
                {
                    string a = "false";
                    string b = "NULL";
                    if (reader.HasAttributes)
                        a = reader.GetAttribute("ignore-it");
                    if (reader.Namespaces)
                        b = reader.NamespaceURI;
                    //Extra 
		    //Chcę żeby te diagnostyczne linie wyświetlały się w zależności od tego czy ustawiona jest zmienna środowiskowa PROGRAM_VERBOSE=1 
                    //Console.WriteLine("attribute \"ignore-it\": " + a);
                    //Console.WriteLine("namespace: " + b);
                    
                    string s1 = reader.ReadElementContentAsString();

                    if (has_atr_ignore_it % 2 == 0 && b != "http://ignore/it" && a != "true")
                    {
                        int.TryParse(s1, out bfr_sum);
                        sum += bfr_sum;
                    }
                    //Console.WriteLine("value: " + s1);
                }
                else if (reader.Name == "ignore-it")
                {
                    has_atr_ignore_it += 1;
                }
                      

            }
            Console.WriteLine("sum = "+sum);       
            //Console.ReadKey();
        }
    }
   
}
