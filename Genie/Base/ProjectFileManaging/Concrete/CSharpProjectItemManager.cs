using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using Genie.Base.ProcessOutput.Abstract;
using Genie.Base.ProjectFileManaging.Abstract;

namespace Genie.Base.ProjectFileManaging.Concrete
{
    internal class CSharpProjectItemManager : IProjectItemManager
    {
        public void Process(string projectFilePath, List<string> files, IProcessOutput output)
        {
            output.WriteInformation("Processing project file.");
            try
            {
                if (!File.Exists(projectFilePath))
                {
                    return;
                }


                var document = new XmlDocument();
                document.LoadXml(File.ReadAllText(projectFilePath));

                var compiles = document.GetElementsByTagName("Compile");
                foreach (XmlNode compile in compiles)
                {
                    compile.RemoveAll();
                    var parent = compile.ParentNode;
                    if (parent.ChildNodes.Count < 1)
                    {
                        parent.RemoveAll();
                    }
                }

                var root = document.CreateNode(XmlNodeType.Element, "ItemGroup", null);
                document.AppendChild(root);

                foreach (var file in files)
                {
                    var node = document.CreateNode(XmlNodeType.Element, "Compile", null);
                    node.Value = file;
                    root.AppendChild(node);
                }

                File.WriteAllText(projectFilePath, document.OuterXml);
            }
            catch (Exception e)
            {
                throw new Exception("Unable to process file.", e);
            }
            output.WriteSuccess("Project file processed.");
        }
    }
}
