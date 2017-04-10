using System;
using System.Collections.Generic;
using Genie.Base.Abstract;
using Microsoft.Build.Evaluation;

namespace Genie.Base
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

                var compileItems = project.GetItems("Compile");

                foreach (var projectItem in compileItems)
                    project.RemoveItem(projectItem);

                foreach (var file in files)
                    project.AddItem("Compiled", file);
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
