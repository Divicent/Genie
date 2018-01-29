#region Usings

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Genie.Core.Base.Configuration.Abstract;
using Genie.Core.Base.Exceptions;
using Genie.Core.Base.ProcessOutput.Abstract;

#endregion

namespace Genie.Core.Base.ProjectFileManaging
{
    /// <summary>
    /// Manages items of an existing project file
    /// </summary>
    internal static class CSharpProjectItemManager
    {
        /// <summary>
        ///  Process the project file using given items
        /// </summary>
        /// <param name="projectFilePath">Path to the project file</param>
        /// <param name="output"></param>
        public static void Process(string projectFilePath, IProcessOutput output, IConfiguration configuration, IEnumerable<string> files)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(projectFilePath)) return;

                if (configuration.Standard)
                {
                    
                    if (!File.Exists(projectFilePath))
                    {
                        output.WriteInformation("Project file does not exist. creating project file");
                        File.WriteAllText(projectFilePath, $@"<?xml version=""1.0"" encoding=""utf-16""?>
<Project Sdk=""Microsoft.NET.Sdk"">
  <PropertyGroup>
    <TargetFramework>netstandard2.0</TargetFramework>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include=""Microsoft.CSharp"" Version=""4.4.1"" />
    <PackageReference Include=""System.Data.SqlClient"" Version=""4.4.2"" />
    {(configuration.DBMS == "mysql" ? @"\n<PackageReference Include=""MySql.Data"" Version=""6.10.6"" />" : "")}
  </ItemGroup>
</Project>");
                        return;
                    }
    
                    var document = new XmlDocument();
                    document.LoadXml(File.ReadAllText(projectFilePath));
                    var packages = document.GetElementsByTagName("PackageReference");
                    var requiredPackages = new List<(string name, string version)>
                    {
                        (name: "Microsoft.CSharp", version: "4.4.1"),
                        (name: "System.Data.SqlClient", version: "4.4.2"),
                        (name: "System.Reflection.Emit.Lightweight", "4.3.0"),
                        (name: "System.Reflection.TypeExtensions", "4.4.0")
                    };
    
                    if (configuration.DBMS == "mysql")
                    {
                        requiredPackages.Add((name: "MySql.Data", version: "6.10.6"));
                    }
    
                    var existingPackages = new List<(XmlNode node, string requiredVersion)>();
                    var packagesToAdd = new List<XmlNode>();
    
                    foreach (var requiredPackage in requiredPackages)
                    {
                        XmlNode existing = null;
                        foreach (XmlNode package in packages)
                        {
                            if (package.Attributes["Include"].Value == requiredPackage.name)
                            {
                                existing = package;
                            }
                        }
                        if (existing == null)
                        {
                            var node = document.CreateElement("PackageReference", null);
                            var includeAttribute = document.CreateAttribute("Include", null);
                            includeAttribute.Value = requiredPackage.name;
    
                            var versionAttribuete = document.CreateAttribute("Version", null);
                            versionAttribuete.Value = requiredPackage.version;
    
                            node.Attributes.Append(includeAttribute);
                            node.Attributes.Append(versionAttribuete);
                            packagesToAdd.Add(node);
                        }
                        else
                        {
                            existingPackages.Add((existing, requiredPackage.version));
                        }
                    }
                    
                    var nodesToRemove = new HashSet<XmlNode>();
    
                    if (existingPackages.Count > 1)
                    {
                        var nodes = new HashSet<(XmlNode node, string requiredVersion)>();
                        for (var i = 0; i < existingPackages.Count; i++)
                        {
                            var current = existingPackages[i];
                            var removed = false;
                            for (var j = i + 1 ; j < existingPackages.Count; j++)
                            {
                                var next = existingPackages[j];
                                if (current.node.Attributes["Include"].Value != next.node.Attributes["Include"].Value)
                                    continue;
                                nodesToRemove.Add(next.node);
                                removed = true;
                            }
                            if(!removed)
                                nodes.Add(current);
                        }
                        
                        existingPackages = nodes.ToList();
                    }
    
                    
                    foreach (var nodeToRemove in nodesToRemove)
                    {
                        var parent = nodeToRemove.ParentNode;
                        parent.RemoveChild(nodeToRemove);
                        if (parent.ChildNodes.Count < 1) parent.ParentNode.RemoveChild(parent);
                    }
    
                    var parentNode = existingPackages.Any() ? existingPackages.First().node.ParentNode 
                        : document.CreateElement("ItemGroup", null);
    
                    foreach (var expackage in existingPackages)
                        expackage.node.Attributes["Version"].Value = expackage.requiredVersion;
    
                    foreach (var node in packagesToAdd)
                        parentNode.AppendChild(node);
    
                    if (!existingPackages.Any())
                        document.DocumentElement.AppendChild(parentNode);
    
                    var xmlWriterSettings = new XmlWriterSettings {Indent = true};
                    var sb = new StringBuilder();
                    using (var xw = XmlWriter.Create(sb, xmlWriterSettings))
                    {
                        document.WriteContentTo(xw);
                    }
    
                    File.WriteAllText(projectFilePath, sb.Replace("xmlns=\"\"", "").ToString());
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(projectFilePath) || !File.Exists(projectFilePath)) return;

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
                        if (parent.ChildNodes.Count < 1) parent.ParentNode.RemoveChild(parent);
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

            }
            catch (Exception e)
            {
                throw new GenieException("Unable to process file.", e);
            }
        }
    }
}