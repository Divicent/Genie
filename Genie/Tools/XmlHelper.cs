using System.IO;
using System.Text;
using System.Xml;

namespace Genie.Core.Tools
{
    internal static class XmlHelper
    {
        internal static XmlDocument GetDocument(string filePath)
        {
            var document = new XmlDocument();
            document.LoadXml(File.ReadAllText(filePath));

            return document;
        }


        internal static XmlAttribute CreateAttribute(XmlDocument document, string name, string value)
        {
            var attribute = document.CreateAttribute(name, null);
            attribute.Value = value;
            return attribute;
        }

        internal static void Save(XmlDocument document, string targetPath)
        {
            var ws = new XmlWriterSettings { Indent = true };
            var sbb = new StringBuilder();
            using (var xw = XmlWriter.Create(sbb, ws))
            {
                document.WriteContentTo(xw);
            }

            File.WriteAllText(targetPath, sbb.Replace("xmlns=\"\"", "").ToString());
        }
    }
}
