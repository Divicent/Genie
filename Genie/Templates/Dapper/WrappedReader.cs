namespace Genie.Templates.Dapper
{
    using Genie.Base;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "D:\Projects\Genie\Genie\Templates\Dapper\WrappedReader.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "14.0.0.0")]
    public partial class WrappedReader : WrappedReaderBase
    {
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("using System;\r\nusing System.Data;\r\n\r\nnamespace ");
            
            #line 6 "D:\Projects\Genie\Genie\Templates\Dapper\WrappedReader.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GenerationContext.BaseNamespace));
            
            #line default
            #line hidden
            this.Write(".Dapper\r\n{\r\n    internal class WrappedReader : IDataReader, IWrappedDataReader\r\n " +
                    "   {\r\n        private IDataReader reader;\r\n        private IDbCommand cmd;\r\n\r\n  " +
                    "      public IDataReader Reader\r\n        {\r\n            get\r\n            {\r\n    " +
                    "            var tmp = reader;\r\n                if (tmp == null) throw new Object" +
                    "DisposedException(GetType().Name);\r\n                return tmp;\r\n            }\r\n" +
                    "        }\r\n        IDbCommand IWrappedDataReader.Command\r\n        {\r\n           " +
                    " get\r\n            {\r\n                var tmp = cmd;\r\n                if (tmp == " +
                    "null) throw new ObjectDisposedException(GetType().Name);\r\n                return" +
                    " tmp;\r\n            }\r\n        }\r\n        public WrappedReader(IDbCommand cmd, ID" +
                    "ataReader reader)\r\n        { \r\n            this.cmd = cmd;\r\n            this.rea" +
                    "der = reader;\r\n        }\r\n\r\n        void IDataReader.Close()\r\n        {\r\n       " +
                    "     reader?.Close();\r\n        }\r\n\r\n        int IDataReader.Depth => Reader.Dept" +
                    "h;\r\n\r\n        DataTable IDataReader.GetSchemaTable()\r\n        {\r\n            ret" +
                    "urn Reader.GetSchemaTable();\r\n        }\r\n\r\n        bool IDataReader.IsClosed => " +
                    "reader?.IsClosed ?? true;\r\n\r\n        bool IDataReader.NextResult()\r\n        {\r\n " +
                    "           return Reader.NextResult();\r\n        }\r\n\r\n        bool IDataReader.Re" +
                    "ad()\r\n        {\r\n            return Reader.Read();\r\n        }\r\n\r\n        int IDa" +
                    "taReader.RecordsAffected => Reader.RecordsAffected;\r\n\r\n        void IDisposable." +
                    "Dispose()\r\n        {\r\n            reader?.Close();\r\n            reader?.Dispose(" +
                    ");\r\n            reader = null;\r\n            cmd?.Dispose();\r\n            cmd = n" +
                    "ull;\r\n        }\r\n\r\n        int IDataRecord.FieldCount => Reader.FieldCount;\r\n\r\n " +
                    "       bool IDataRecord.GetBoolean(int i)\r\n        {\r\n            return Reader." +
                    "GetBoolean(i);\r\n        }\r\n\r\n        byte IDataRecord.GetByte(int i)\r\n        {\r" +
                    "\n            return Reader.GetByte(i);\r\n        }\r\n\r\n        long IDataRecord.Ge" +
                    "tBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)\r\n  " +
                    "      {\r\n            return Reader.GetBytes(i, fieldOffset, buffer, bufferoffset" +
                    ", length);\r\n        }\r\n\r\n        char IDataRecord.GetChar(int i)\r\n        {\r\n   " +
                    "         return Reader.GetChar(i);\r\n        }\r\n\r\n        long IDataRecord.GetCha" +
                    "rs(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)\r\n      " +
                    "  {\r\n            return Reader.GetChars(i, fieldoffset, buffer, bufferoffset, le" +
                    "ngth);\r\n        }\r\n\r\n        IDataReader IDataRecord.GetData(int i)\r\n        {\r\n" +
                    "            return Reader.GetData(i);\r\n        }\r\n\r\n        string IDataRecord.G" +
                    "etDataTypeName(int i)\r\n        {\r\n            return Reader.GetDataTypeName(i);\r" +
                    "\n        }\r\n\r\n        DateTime IDataRecord.GetDateTime(int i)\r\n        {\r\n      " +
                    "      return Reader.GetDateTime(i);\r\n        }\r\n\r\n        decimal IDataRecord.Ge" +
                    "tDecimal(int i)\r\n        {\r\n            return Reader.GetDecimal(i);\r\n        }\r" +
                    "\n\r\n        double IDataRecord.GetDouble(int i)\r\n        {\r\n            return Re" +
                    "ader.GetDouble(i);\r\n        }\r\n\r\n        Type IDataRecord.GetFieldType(int i)\r\n " +
                    "       {\r\n            return Reader.GetFieldType(i);\r\n        }\r\n\r\n        float" +
                    " IDataRecord.GetFloat(int i)\r\n        {\r\n            return Reader.GetFloat(i);\r" +
                    "\n        }\r\n\r\n        Guid IDataRecord.GetGuid(int i)\r\n        {\r\n            re" +
                    "turn Reader.GetGuid(i);\r\n        }\r\n\r\n        short IDataRecord.GetInt16(int i)\r" +
                    "\n        {\r\n            return Reader.GetInt16(i);\r\n        }\r\n\r\n        int IDa" +
                    "taRecord.GetInt32(int i)\r\n        {\r\n            return Reader.GetInt32(i);\r\n   " +
                    "     }\r\n\r\n        long IDataRecord.GetInt64(int i)\r\n        {\r\n            retur" +
                    "n Reader.GetInt64(i);\r\n        }\r\n\r\n        string IDataRecord.GetName(int i)\r\n " +
                    "       {\r\n            return Reader.GetName(i);\r\n        }\r\n\r\n        int IDataR" +
                    "ecord.GetOrdinal(string name)\r\n        {\r\n            return Reader.GetOrdinal(n" +
                    "ame);\r\n        }\r\n\r\n        string IDataRecord.GetString(int i)\r\n        {\r\n    " +
                    "        return Reader.GetString(i);\r\n        }\r\n\r\n        object IDataRecord.Get" +
                    "Value(int i)\r\n        {\r\n            return Reader.GetValue(i);\r\n        }\r\n\r\n  " +
                    "      int IDataRecord.GetValues(object[] values)\r\n        {\r\n            return " +
                    "Reader.GetValues(values);\r\n        }\r\n\r\n        bool IDataRecord.IsDBNull(int i)" +
                    "\r\n        {\r\n            return Reader.IsDBNull(i);\r\n        }\r\n\r\n        object" +
                    " IDataRecord.this[string name] => Reader[name];\r\n\r\n        object IDataRecord.th" +
                    "is[int i] => Reader[i];\r\n    }\r\n}\r\n");
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
    public class WrappedReaderBase
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
