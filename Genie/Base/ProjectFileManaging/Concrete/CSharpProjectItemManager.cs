using System;
using System.Collections.Generic;
using System.Linq;
using Genie.Base.ProcessOutput.Abstract;
using Genie.Base.ProjectFileManaging.Abstract;
using Microsoft.Build.Evaluation;

namespace Genie.Base.ProjectFileManaging.Concrete
{
    internal class CSharpProjectItemManager: IProjectItemManager
    {
        public void Process(string projectFilePath, List<string> files, IProcessOutput output)
        {
            output.WriteInformation("Processing project file.");
            try
            {
                var projectCollection = new ProjectCollection();
                var project = projectCollection.LoadProject(projectFilePath);

                foreach (var projectItem in project.GetItems("Compile").ToList())
                {
                    if (projectItem.EvaluatedInclude.StartsWith("Dapper") || projectItem.EvaluatedInclude.StartsWith("Infrastructure"))
                    {
                        project.RemoveItem(projectItem);
                    }
                }
                foreach (var file in files)
                    project.AddItem("Compile", file);

                project.Save();
            }
            catch (Exception e)
            {
                throw new Exception("Unable to process file.", e);
            }
            output.WriteSuccess("Project file processed.");
        }
    }
}
