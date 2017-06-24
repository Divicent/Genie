﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 14.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace Genie.Templates.Dapper
{
    using Genie.Base.Generating.Concrete;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "F:\Projects\Genie\Genie\Templates\Dapper\SqlMapper_Identity.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "14.0.0.0")]
    public partial class SqlMapper_Identity : SqlMapper_IdentityBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("using System;\r\nusing System.Data;\r\n \r\nnamespace ");
            
            #line 6 "F:\Projects\Genie\Genie\Templates\Dapper\SqlMapper_Identity.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GenerationContext.BaseNamespace));
            
            #line default
            #line hidden
            this.Write(".Dapper\r\n{\r\n    partial class SqlMapper\r\n    {\r\n        /// <summary>\r\n        //" +
                    "/ Identity of a cached query in Dapper, used for extensibility\r\n        /// </su" +
                    "mmary>\r\n        public class Identity : IEquatable<Identity>\r\n        {\r\n       " +
                    "     internal Identity ForGrid(Type primaryType, int gridIndex)\r\n            {\r\n" +
                    "                return new Identity(sql, commandType, connectionString, primaryT" +
                    "ype, parametersType, null, gridIndex);\r\n            }\r\n\r\n            internal Id" +
                    "entity ForGrid(Type primaryType, Type[] otherTypes, int gridIndex)\r\n            " +
                    "{\r\n                return new Identity(sql, commandType, connectionString, prima" +
                    "ryType, parametersType, otherTypes, gridIndex);\r\n            }\r\n            /// " +
                    "<summary>\r\n            /// Create an identity for use with DynamicParameters, in" +
                    "ternal use only\r\n            /// </summary>\r\n            /// <param name=\"type\">" +
                    "</param>\r\n            /// <returns></returns>\r\n            public Identity ForDy" +
                    "namicParameters(Type type)\r\n            {\r\n                return new Identity(s" +
                    "ql, commandType, connectionString, this.type, type, null, -1);\r\n            }\r\n\r" +
                    "\n            internal Identity(string sql, CommandType? commandType, IDbConnecti" +
                    "on connection, Type type, Type parametersType, Type[] otherTypes)\r\n             " +
                    "   : this(sql, commandType, connection.ConnectionString, type, parametersType, o" +
                    "therTypes, 0)\r\n            { }\r\n            private Identity(string sql, Command" +
                    "Type? commandType, string connectionString, Type type, Type parametersType, Type" +
                    "[] otherTypes, int gridIndex)\r\n            {\r\n                this.sql = sql;\r\n " +
                    "               this.commandType = commandType;\r\n                this.connectionS" +
                    "tring = connectionString;\r\n                this.type = type;\r\n                th" +
                    "is.parametersType = parametersType;\r\n                this.gridIndex = gridIndex;" +
                    "\r\n                unchecked\r\n                {\r\n                    hashCode = 1" +
                    "7; // we *know* we are using this in a dictionary, so pre-compute this\r\n        " +
                    "            hashCode = hashCode * 23 + commandType.GetHashCode();\r\n             " +
                    "       hashCode = hashCode * 23 + gridIndex.GetHashCode();\r\n                    " +
                    "hashCode = hashCode * 23 + (sql?.GetHashCode() ?? 0);\r\n                    hashC" +
                    "ode = hashCode * 23 + (type?.GetHashCode() ?? 0);\r\n                    if (other" +
                    "Types != null)\r\n                    {\r\n                        foreach (var t in" +
                    " otherTypes)\r\n                        {\r\n                            hashCode = " +
                    "hashCode * 23 + (t?.GetHashCode() ?? 0);\r\n                        }\r\n           " +
                    "         }\r\n                    hashCode = hashCode * 23 + (connectionString == " +
                    "null ? 0 : connectionStringComparer.GetHashCode(connectionString));\r\n           " +
                    "         hashCode = hashCode * 23 + (parametersType?.GetHashCode() ?? 0);\r\n     " +
                    "           }\r\n            }\r\n\r\n            /// <summary>\r\n            ///\r\n     " +
                    "       /// </summary>\r\n            /// <param name=\"obj\"></param>\r\n            /" +
                    "// <returns></returns>\r\n            public override bool Equals(object obj)\r\n   " +
                    "         {\r\n                return Equals(obj as Identity);\r\n            }\r\n    " +
                    "        /// <summary>\r\n            /// The sql\r\n            /// </summary>\r\n    " +
                    "        public readonly string sql;\r\n            /// <summary>\r\n            /// " +
                    "The command type\r\n            /// </summary>\r\n            public readonly Comman" +
                    "dType? commandType;\r\n\r\n            /// <summary>\r\n            ///\r\n            /" +
                    "// </summary>\r\n            public readonly int hashCode, gridIndex;\r\n           " +
                    " /// <summary>\r\n            ///\r\n            /// </summary>\r\n            public " +
                    "readonly Type type;\r\n            /// <summary>\r\n            ///\r\n            ///" +
                    " </summary>\r\n            public readonly string connectionString;\r\n            /" +
                    "// <summary>\r\n            ///\r\n            /// </summary>\r\n            public re" +
                    "adonly Type parametersType;\r\n            /// <summary>\r\n            ///\r\n       " +
                    "     /// </summary>\r\n            /// <returns></returns>\r\n            public ove" +
                    "rride int GetHashCode()\r\n            {\r\n                return hashCode;\r\n      " +
                    "      }\r\n            /// <summary>\r\n            /// Compare 2 Identity objects\r\n" +
                    "            /// </summary>\r\n            /// <param name=\"other\"></param>\r\n      " +
                    "      /// <returns></returns>\r\n            public bool Equals(Identity other)\r\n " +
                    "           {\r\n                return\r\n                    other != null &&\r\n    " +
                    "                gridIndex == other.gridIndex &&\r\n                    type == oth" +
                    "er.type &&\r\n                    sql == other.sql &&\r\n                    command" +
                    "Type == other.commandType &&\r\n                    connectionStringComparer.Equal" +
                    "s(connectionString, other.connectionString) &&\r\n                    parametersTy" +
                    "pe == other.parametersType;\r\n            }\r\n        }\r\n    }\r\n}\r\n");
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
    public class SqlMapper_IdentityBase
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
