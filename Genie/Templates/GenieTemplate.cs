#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Genie.Core.Templates.Abstract;

#endregion

namespace Genie.Core.Templates
{
    public abstract class GenieTemplate : ITemplate
    {
        private readonly StringBuilder _stringBuilder = new StringBuilder();

        protected GenieTemplate(string path)
        {
            Path = path;
        }

        public string Path { get; }

        public abstract string Generate();
        
        public bool External { get; set; }

        protected void R(string str)
        {
            _stringBuilder.Append(str);
        }

        protected void L(string str)
        {
            _stringBuilder.AppendLine(str);
        }

        protected string E()
        {
            return _stringBuilder.ToString();
        }
        
        protected static string Lines<T>(IEnumerable<T> collection, Func<T, string> accumilator, string space= "")
        {
            var first = true;
            return collection.Aggregate("", (c, n) =>
            {
                var str = $"{c}{(first ? "": Environment.NewLine)}{space}{accumilator(n).Replace(Environment.NewLine, $"{Environment.NewLine}{space}")}";
                first = false;
                return str;
            });
        }
    }
}