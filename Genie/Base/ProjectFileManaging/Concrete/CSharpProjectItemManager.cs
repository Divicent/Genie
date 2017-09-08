using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using Genie.Base.Exceptions;
using Genie.Base.ProcessOutput.Abstract;
using Genie.Base.ProjectFileManaging.Abstract;

namespace Genie.Base.ProjectFileManaging.Concrete
{
  internal class CSharpProjectItemManager : IProjectItemManager
  {
    public void Process(string projectFilePath, List<string> files, IProcessOutput output)
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
        var torRemove = new List<XmlNode>();
        foreach (XmlNode compile in compiles)
        {
          if (compile != null && compile.Attributes != null)
          {
            var include = compile.Attributes["Include"]?.Value;
            if (!string.IsNullOrWhiteSpace(include) && (include.StartsWith("Dapper") || include.StartsWith("Infrastructure")))
            {
              torRemove.Add(compile);
            }
          }
        }

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
          include.Value = file;
          node.Attributes.Append(include);
          root.AppendChild(node);
        }

        document.DocumentElement.AppendChild(root);

        var xmlWriterSettings = new XmlWriterSettings { Indent = true };
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
