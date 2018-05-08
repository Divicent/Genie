#region Usings

using System.Collections.Generic;
using DotLiquid;
using Genie.Core.Templates.Abstract;

#endregion

namespace Genie.Core.Templates
{
    public abstract class GenieTemplate : ITemplate
    {
        private static readonly Dictionary<string, Template> TemplateCache = new Dictionary<string, Template>();

        protected GenieTemplate(string path)
        {
            Path = path;
        }

        public string Path { get; }

        public abstract string Generate();
        
        public bool External { get; set; }

        protected string Process(string templateName, string template, object data)
        {
            if (TemplateCache.TryGetValue(templateName, out var parsed))
                return parsed.Render(Hash.FromAnonymousObject(data));

            parsed = Template.Parse(template);
            TemplateCache[templateName] = parsed;
            return parsed.Render(Hash.FromAnonymousObject(data));
        }
    }
}