#region Usings

using System.Text;
using Genie.Core.Templates.Abstract;

#endregion

namespace Genie.Core.Templates
{
    internal abstract class GenieTemplate : ITemplate
    {
        private readonly StringBuilder _stringBuilder = new StringBuilder();

        public GenieTemplate(string path)
        {
            Path = path;
        }

        public string Path { get; }

        public abstract string Generate();

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
    }
}