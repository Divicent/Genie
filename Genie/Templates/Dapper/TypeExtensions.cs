namespace Genie.Templates.Dapper
{
    using Genie.Base;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "D:\Projects\Genie\Genie\Templates\Dapper\TypeExtensions.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "14.0.0.0")]
    public partial class TypeExtensions : TypeExtensionsBase
    {
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("using System;\r\nusing System.Reflection;\r\n\r\nnamespace ");
            
            #line 6 "D:\Projects\Genie\Genie\Templates\Dapper\TypeExtensions.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GenerationContext.BaseNamespace));
            
            #line default
            #line hidden
            this.Write(".Dapper\r\n{\r\n    internal static class TypeExtensions\r\n    {\r\n        public stati" +
                    "c string Name(this Type type)\r\n        {\r\n#if COREFX\r\n            return type.Ge" +
                    "tTypeInfo().Name;\r\n#else\r\n            return type.Name;\r\n#endif\r\n        }\r\n\r\n  " +
                    "      public static bool IsValueType(this Type type)\r\n        {\r\n#if COREFX\r\n   " +
                    "         return type.GetTypeInfo().IsValueType;\r\n#else\r\n            return type." +
                    "IsValueType;\r\n#endif\r\n        }\r\n        public static bool IsEnum(this Type typ" +
                    "e)\r\n        {\r\n#if COREFX\r\n            return type.GetTypeInfo().IsEnum;\r\n#else " +
                    "\r\n            return type.IsEnum;\r\n#endif\r\n        }\r\n        public static bool" +
                    " IsGenericType(this Type type)\r\n        {\r\n#if COREFX\r\n            return type.G" +
                    "etTypeInfo().IsGenericType;\r\n#else\r\n            return type.IsGenericType;\r\n#end" +
                    "if\r\n        }\r\n        public static bool IsInterface(this Type type)\r\n        {" +
                    "\r\n#if COREFX\r\n            return type.GetTypeInfo().IsInterface;\r\n#else\r\n       " +
                    "     return type.IsInterface;\r\n#endif\r\n        }\r\n#if COREFX\r\n        public sta" +
                    "tic IEnumerable<Attribute> GetCustomAttributes(this Type type, bool inherit)\r\n  " +
                    "      {\r\n            return type.GetTypeInfo().GetCustomAttributes(inherit);\r\n  " +
                    "      }\r\n\r\n        public static TypeCode GetTypeCode(Type type)\r\n        {\r\n   " +
                    "         if (type == null) return TypeCode.Empty;\r\n            TypeCode result;\r" +
                    "\n            if (typeCodeLookup.TryGetValue(type, out result)) return result;\r\n\r" +
                    "\n            if (type.IsEnum())\r\n            {\r\n                type = Enum.GetU" +
                    "nderlyingType(type);\r\n                if (typeCodeLookup.TryGetValue(type, out r" +
                    "esult)) return result;\r\n            }\r\n            return TypeCode.Object;\r\n    " +
                    "    }\r\n        static readonly Dictionary<Type, TypeCode> typeCodeLookup = new D" +
                    "ictionary<Type, TypeCode>\r\n        {\r\n            {typeof(bool), TypeCode.Boolea" +
                    "n },\r\n            {typeof(byte), TypeCode.Byte },\r\n            {typeof(char), Ty" +
                    "peCode.Char},\r\n            {typeof(DateTime), TypeCode.DateTime},\r\n            {" +
                    "typeof(decimal), TypeCode.Decimal},\r\n            {typeof(double), TypeCode.Doubl" +
                    "e },\r\n            {typeof(short), TypeCode.Int16 },\r\n            {typeof(int), T" +
                    "ypeCode.Int32 },\r\n            {typeof(long), TypeCode.Int64 },\r\n            {typ" +
                    "eof(object), TypeCode.Object},\r\n            {typeof(sbyte), TypeCode.SByte },\r\n " +
                    "           {typeof(float), TypeCode.Single },\r\n            {typeof(string), Type" +
                    "Code.String },\r\n            {typeof(ushort), TypeCode.UInt16 },\r\n            {ty" +
                    "peof(uint), TypeCode.UInt32 },\r\n            {typeof(ulong), TypeCode.UInt64 },\r\n" +
                    "        };\r\n#else\r\n        public static TypeCode GetTypeCode(Type type)\r\n      " +
                    "  {\r\n            return Type.GetTypeCode(type);\r\n        }\r\n#endif\r\n        publ" +
                    "ic static MethodInfo GetPublicInstanceMethod(this Type type, string name, Type[]" +
                    " types)\r\n        {\r\n#if COREFX\r\n            var method = type.GetMethod(name, ty" +
                    "pes);\r\n            return (method != null && method.IsPublic && !method.IsStatic" +
                    ") ? method : null;\r\n#else\r\n            return type.GetMethod(name, BindingFlags." +
                    "Instance | BindingFlags.Public, null, types, null);\r\n#endif\r\n        }\r\n\r\n\r\n    " +
                    "}\r\n}\r\n");
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
    public class TypeExtensionsBase
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
