#region Usings

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Genie.Core.Base.Exceptions;
using Genie.Core.Base.ProcessOutput.Abstract;

#endregion

namespace Genie.Core.Base.ProjectFileManaging
{
    /// <summary>
    ///     Manages items of an existing project file
    /// </summary>
    internal static class CSharpProjectItemManager
    {
        /// <summary>
        ///     Process the project file using given items
        /// </summary>
        /// <param name="projectFilePath">Path to the project file</param>
        /// <param name="files">Files to include</param>
        /// <param name="output">A process output</param>
        public static void Process(string projectFilePath, List<string> files, IProcessOutput output)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(projectFilePath) || !File.Exists(projectFilePath))
                {
                    return;
                }

                output.WriteInformation("Processing project file.");
                var document = new XmlDocument();
                document.LoadXml(File.ReadAllText(projectFilePath));
                var compiles = document.GetElementsByTagName("Compile");
                var torRemove = (from XmlNode compile in compiles
                    where
                    compile?.Attributes != null
                    let include = compile.Attributes["Include"]?.Value
                    where !string.IsNullOrWhiteSpace(include) &&
                          (include.StartsWith("Dapper") || include.StartsWith("Infrastructure"))
                    select compile).ToList();

                foreach (var tr in torRemove)
                {
                    var parent = tr.ParentNode;
                    parent.RemoveChild(tr);
                    if (parent.ChildNodes.Count < 1)
                    {
                        parent.ParentNode.RemoveChild(parent);
                    }
                }

                var root = document.CreateElement("ItemGroup", null);
                foreach (var file in files)
                {
                    var node = document.CreateElement("Compile", null);
                    var include = document.CreateAttribute("Include");
                    include.Value = file.Replace("/", "\\");
                    node.Attributes.Append(include);
                    root.AppendChild(node);
                }

                document.DocumentElement.AppendChild(root);

                var xmlWriterSettings = new XmlWriterSettings {Indent = true};
                var sb = new StringBuilder();
                using (var xw = XmlWriter.Create(sb, xmlWriterSettings))
                {
                    document.WriteContentTo(xw);
                }
                File.WriteAllText(projectFilePath, sb.Replace("xmlns=\"\"", "").ToString());
            }
            catch (Exception e)
            {
                throw new GenieException("Unable to process file.", e);
            }
            output.WriteSuccess("Project file processed.");
        }
    }
}