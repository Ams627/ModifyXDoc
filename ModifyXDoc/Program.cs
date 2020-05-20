using System;
using System.IO;
using System.Xml.Linq;

namespace ModifyXDoc
{
    class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                var doc = new XDocument(new XElement("PhaseMappings", new XElement("Engine", new XElement("Widget", new XElement("Frog", "tadpole")))));
                XNamespace xmlns = "http://schemas.datacontract.org/2004/07/Widgets";
                ChangeNS(doc, xmlns);
                Console.WriteLine($"{doc}");
            }
            catch (Exception ex)
            {
                var fullname = System.Reflection.Assembly.GetEntryAssembly().Location;
                var progname = Path.GetFileNameWithoutExtension(fullname);
                Console.Error.WriteLine(progname + ": Error: " + ex.Message);
            }

        }

        /// <summary>
        /// Change the namespace of the whole document. There is no other easy way to do this
        /// </summary>
        /// <param name="doc">doc</param>
        /// <param name="ns">namespace</param>
        private static void ChangeNS(XDocument doc, XNamespace ns)
        {
            foreach (XElement el in doc.Descendants())
            {
                if (el.Name.NamespaceName == string.Empty)
                {
                    el.Name = ns + el.Name.LocalName;
                }
            }
        }
    }
}
