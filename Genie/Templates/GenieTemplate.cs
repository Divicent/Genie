using System.Text;
using Genie.Templates.Abstract;

namespace Genie.Templates 
{
    internal abstract class GenieTemplate :ITemplate
    {
        private StringBuilder _stringBuilder = new StringBuilder();

        public string Path { get; }

        public GenieTemplate(string path) 
        {
            Path = path;
        }
        
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

        public abstract string Generate();
    }
}