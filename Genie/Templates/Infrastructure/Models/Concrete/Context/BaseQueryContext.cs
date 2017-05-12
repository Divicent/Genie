﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 14.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace Genie.Templates.Infrastructure.Models.Concrete.Context
{
    using Genie.Base;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\Concrete\Context\BaseQueryContext.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "14.0.0.0")]
    public partial class BaseQueryContext : BaseQueryContextBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("using System.Collections.Generic;\r\nusing System.Linq;\r\nusing ");
            
            #line 5 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\Concrete\Context\BaseQueryContext.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GenerationContext.BaseNamespace));
            
            #line default
            #line hidden
            this.Write(".Infrastructure.Filters.Abstract;\r\nusing ");
            
            #line 6 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\Concrete\Context\BaseQueryContext.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GenerationContext.BaseNamespace));
            
            #line default
            #line hidden
            this.Write(".Infrastructure.Filters.Concrete;\r\n\r\nnamespace ");
            
            #line 8 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\Concrete\Context\BaseQueryContext.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GenerationContext.BaseNamespace));
            
            #line default
            #line hidden
            this.Write(".Infrastructure.Models.Concrete.Context\r\n{\r\n    public abstract class BaseQueryCo" +
                    "ntext\r\n    {\r\n        protected int? _page;\r\n        protected int? _pageSize;\r\n" +
                    "        protected int? _limit;\r\n        protected int? _skip;\r\n        protected" +
                    " int? _take;\r\n\r\n\t\tprotected abstract bool? IsQuoted(ref string propertyName);\r\n\t" +
                    "\t\r\n        protected void ProcessFilter(Queue<string> queue, IEnumerable<IProper" +
                    "tyFilter> f)  \r\n\t\t{\r\n\t\t    var filters = f?.ToList();\r\n            if(filters ==" +
                    " null)\r\n                return;\r\n            if(filters.Count < 1)\r\n            " +
                    "    return;\r\n\r\n\t\t    foreach (var propertyFilter in filters)\r\n\t\t    {\r\n\t\t       " +
                    " var propName = propertyFilter.PropertyName;\r\n                var qotd = IsQuote" +
                    "d(ref propName);\r\n                if(qotd == null)\r\n                    continue" +
                    ";\r\n\t\t        \r\n                queue.Enqueue(\"and\");\r\n\t\t        queue.Enqueue(Ge" +
                    "tExpression(propertyFilter.Type, propName, propertyFilter.Value,\r\n\t\t            " +
                    "qotd.GetValueOrDefault()));\r\n\t\t    }\r\n\t\t}\r\n\r\n        private static string GetEx" +
                    "pression(string type, string propName, object value, bool quoted )\r\n        {\r\n " +
                    "           switch (type.ToLower())\r\n            {\r\n                case \"equals\"" +
                    ":\r\n                case \"eq\":\r\n                    return QueryMaker.EqualsTo(pr" +
                    "opName, value, quoted);\r\n                case \"notequals\":\r\n                case" +
                    " \"neq\":\r\n                case \"ne\":\r\n                    return QueryMaker.NotEq" +
                    "uals(propName, value, quoted);\r\n                case \"contains\":\r\n              " +
                    "  case \"c\":\r\n                    return QueryMaker.Contains(propName, value);\r\n " +
                    "               case \"notcontains\":\r\n                case \"nc\":\r\n                " +
                    "    return QueryMaker.NotContains(propName, value);\r\n                case \"start" +
                    "swith\":\r\n                case \"sw\":\r\n                    return QueryMaker.Start" +
                    "sWith(propName, value);\r\n                case \"notstartswith\":\r\n                " +
                    "case \"nsw\":\r\n                    return QueryMaker.NotStartsWith(propName, value" +
                    ");\r\n                case \"endswith\":\r\n                case \"ew\":\r\n              " +
                    "      return QueryMaker.EndsWith(propName, value);\r\n                case \"notend" +
                    "swith\":\r\n                case \"new\":\r\n                    return QueryMaker.NotE" +
                    "ndsWith(propName, value);\r\n                case \"isempty\":\r\n                case" +
                    " \"ie\":\r\n                    return QueryMaker.IsEmpty(propName);\r\n              " +
                    "  case \"isnotempty\":\r\n                case \"ino\":\r\n                    return Qu" +
                    "eryMaker.IsNotEmpty(propName);\r\n                case \"isnull\":\r\n                " +
                    "case \"in\":\r\n                    return QueryMaker.IsNull(propName);\r\n           " +
                    "     case \"isnotnull\":\r\n                case \"inn\":\r\n                    return " +
                    "QueryMaker.IsNotNull(propName);\r\n                case \"greaterthan\":\r\n          " +
                    "      case \"gt\":\r\n                    return QueryMaker.GreaterThan(propName, va" +
                    "lue, quoted);\r\n                case \"lessthan\":\r\n                case \"lt\":\r\n   " +
                    "                 return QueryMaker.LessThan(propName, value, quoted);\r\n         " +
                    "       case \"greaterthanorequals\":\r\n                case \"gtoe\":\r\n              " +
                    "  case \"gte\":\r\n                    return QueryMaker.GreaterThanOrEquals(propNam" +
                    "e, value, quoted);\r\n                case \"lessthanorequals\":\r\n                ca" +
                    "se \"ltoe\":\r\n                case \"lte\":\r\n                    return QueryMaker.L" +
                    "essThanOrEquals(propName, value, quoted);\r\n                case \"istrue\":\r\n     " +
                    "           case \"it\":\r\n                    return QueryMaker.IsTrue(propName);\r\n" +
                    "                case \"isfalse\":\r\n                case \"if\":\r\n                   " +
                    " return QueryMaker.IsFalse(propName);\r\n                default:\r\n               " +
                    "     return \"\";\r\n            }\r\n        }\r\n    }\r\n}\r\n");
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "14.0.0.0")]
    public class BaseQueryContextBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        /// <summary>
        /// Helper to produce culture-oriented representation of an object as a string
        /// </summary>
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
    }
    #endregion
}
