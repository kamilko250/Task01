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
            XmlTextReader reader;
            
            if (args.Count() > 0 && args[0] != "null")
                reader = new XmlTextReader(args[0]);
            else
                reader = new XmlTextReader("data.xml");



            int sum = 0;
            int has_atr_ignore_it = 0;
            int bfr_sum = 0;

            
            string value = Environment.GetEnvironmentVariable("PROGRAM_VERBOSE");
            Console.WriteLine(value);
            
            while (reader.Read())
            {

                if (reader.NodeType == XmlNodeType.Element &&(reader.Name == "ns1:number" || reader.Name == "number"))
                {
                    string atr_ignore = "false";
                    string ns_ignore = "NULL";
                    string num_base = "NULL";
                    string enc = "NULL";

                    if (reader.HasAttributes)
                    {
                        atr_ignore = reader.GetAttribute("ignore-it");
                        num_base = reader.GetAttribute("base");
                        enc = reader.GetAttribute("enc");
                        
                    }
                    if (reader.Namespaces)
                        ns_ignore = reader.NamespaceURI;

                    if (value == "1")
                    { 
                        Console.WriteLine("attribute \"ignore-it\": " + atr_ignore);
                        Console.WriteLine("namespace: " + ns_ignore);
                    }

                    string s1 = reader.ReadElementContentAsString();
                    
                    if (has_atr_ignore_it % 2 == 0 && ns_ignore != "http://ignore/it" && atr_ignore != "true")
                    {
                        if (enc == "base64" && num_base == "2")
                        {
                            byte[] data = Convert.FromBase64String(s1);
                            string decoded = ASCIIEncoding.ASCII.GetString(data);
                            bfr_sum = Convert.ToInt32(decoded, 2);
                        }
                        else
                        {
                            if (num_base == "16")
                            {
                                bfr_sum = Convert.ToSByte(s1, 16);
                            }
                            else if (num_base == "2")
                            {
                                bfr_sum = Convert.ToSByte(s1, 2);
                            }

                            else
                            {
                                int.TryParse(s1, out bfr_sum);
                            }
                            sum += bfr_sum;
                        }
                    }
                    if (value == "1")
                    {
                        Console.WriteLine("value: " + s1);
                    }
                }
                else if (reader.Name == "ignore-it")
                {
                    has_atr_ignore_it += 1;
                }
                      

            }
            Console.WriteLine("sum = "+sum);       
            Console.ReadKey();
        }
    }
   
}
