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
    using Genie.Base;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "D:\Projects\Genie\Genie\Templates\Dapper\CommandDefinition.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "14.0.0.0")]
    public partial class CommandDefinition : CommandDefinitionBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("using System;\r\nusing System.Data;\r\nusing System.Reflection;\r\nusing System.Reflect" +
                    "ion.Emit;\r\n\r\nnamespace ");
            
            #line 8 "D:\Projects\Genie\Genie\Templates\Dapper\CommandDefinition.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GenerationContext.BaseNamespace));
            
            #line default
            #line hidden
            this.Write(".Dapper\r\n{\r\n    /// <summary>\r\n    /// Represents the key aspects of a sql operat" +
                    "ion\r\n    /// </summary>\r\n    public struct CommandDefinition\r\n    {\r\n        int" +
                    "ernal static CommandDefinition ForCallback(object parameters)\r\n        {\r\n      " +
                    "      if (parameters is DynamicParameters)\r\n            {\r\n                retur" +
                    "n new CommandDefinition(parameters);\r\n            }\r\n            else\r\n         " +
                    "   {\r\n                return default(CommandDefinition);\r\n            }\r\n       " +
                    " }\r\n\r\n        internal void OnCompleted()\r\n        {\r\n            (Parameters as" +
                    " SqlMapper.IParameterCallbacks)?.OnCompleted();\r\n        }\r\n\r\n        /// <summa" +
                    "ry>\r\n        /// The command (sql or a stored-procedure name) to execute\r\n      " +
                    "  /// </summary>\r\n        public string CommandText { get; }\r\n\r\n        /// <sum" +
                    "mary>\r\n        /// The parameters associated with the command\r\n        /// </sum" +
                    "mary>\r\n        public object Parameters { get; }\r\n\r\n        /// <summary>\r\n     " +
                    "   /// The active transaction for the command\r\n        /// </summary>\r\n        p" +
                    "ublic IDbTransaction Transaction { get; }\r\n\r\n        /// <summary>\r\n        /// " +
                    "The effective timeout for the command\r\n        /// </summary>\r\n        public in" +
                    "t? CommandTimeout { get; }\r\n\r\n        /// <summary>\r\n        /// The type of com" +
                    "mand that the command-text represents\r\n        /// </summary>\r\n        public Co" +
                    "mmandType? CommandType { get; }\r\n\r\n        /// <summary>\r\n        /// Should dat" +
                    "a be buffered before returning?\r\n        /// </summary>\r\n        public bool Buf" +
                    "fered => (Flags & CommandFlags.Buffered) != 0;\r\n\r\n        /// <summary>\r\n       " +
                    " /// Should the plan for this query be cached?\r\n        /// </summary>\r\n        " +
                    "internal bool AddToCache => (Flags & CommandFlags.NoCache) == 0;\r\n\r\n        /// " +
                    "<summary>\r\n        /// Additional state flags against this command\r\n        /// " +
                    "</summary>\r\n        public CommandFlags Flags { get; }\r\n\r\n        /// <summary>\r" +
                    "\n        /// Can async queries be pipelined?\r\n        /// </summary>\r\n        pu" +
                    "blic bool Pipelined => (Flags & CommandFlags.Pipelined) != 0;\r\n\r\n        /// <su" +
                    "mmary>\r\n        /// Initialize the command definition\r\n        /// </summary>\r\n " +
                    "       public CommandDefinition(string commandText, object parameters = null, ID" +
                    "bTransaction transaction = null, int? commandTimeout = null,\r\n                  " +
                    "               CommandType? commandType = null, CommandFlags flags = CommandFlag" +
                    "s.Buffered\r\n#if ASYNC\r\n                                 , CancellationToken canc" +
                    "ellationToken = default(CancellationToken)\r\n#endif\r\n            )\r\n        {\r\n  " +
                    "          CommandText = commandText;\r\n            Parameters = parameters;\r\n    " +
                    "        Transaction = transaction;\r\n            CommandTimeout = commandTimeout;" +
                    "\r\n            CommandType = commandType;\r\n            Flags = flags;\r\n#if ASYNC\r" +
                    "\n            CancellationToken = cancellationToken;\r\n#endif\r\n        }\r\n\r\n      " +
                    "  private CommandDefinition(object parameters) : this()\r\n        {\r\n            " +
                    "Parameters = parameters;\r\n        }\r\n\r\n#if ASYNC\r\n\r\n        /// <summary>\r\n     " +
                    "   /// For asynchronous operations, the cancellation-token\r\n        /// </summar" +
                    "y>\r\n        public CancellationToken CancellationToken { get; }\r\n#endif\r\n\r\n     " +
                    "   internal IDbCommand SetupCommand(IDbConnection cnn, Action<IDbCommand, object" +
                    "> paramReader)\r\n        {\r\n            var cmd = cnn.CreateCommand();\r\n         " +
                    "   var init = GetInit(cmd.GetType());\r\n            init?.Invoke(cmd);\r\n         " +
                    "   if (Transaction != null)\r\n                cmd.Transaction = Transaction;\r\n   " +
                    "         cmd.CommandText = CommandText;\r\n            if (CommandTimeout.HasValue" +
                    ")\r\n            {\r\n                cmd.CommandTimeout = CommandTimeout.Value;\r\n  " +
                    "          }\r\n            else if (SqlMapper.Settings.CommandTimeout.HasValue)\r\n " +
                    "           {\r\n                cmd.CommandTimeout = SqlMapper.Settings.CommandTim" +
                    "eout.Value;\r\n            }\r\n            if (CommandType.HasValue)\r\n             " +
                    "   cmd.CommandType = CommandType.Value;\r\n            paramReader?.Invoke(cmd, Pa" +
                    "rameters);\r\n            return cmd;\r\n        }\r\n\r\n        private static SqlMapp" +
                    "er.Link<Type, Action<IDbCommand>> commandInitCache;\r\n\r\n        private static Ac" +
                    "tion<IDbCommand> GetInit(Type commandType)\r\n        {\r\n            if (commandTy" +
                    "pe == null)\r\n                return null; // GIGO\r\n            Action<IDbCommand" +
                    "> action;\r\n            if (SqlMapper.Link<Type, Action<IDbCommand>>.TryGet(comma" +
                    "ndInitCache, commandType, out action))\r\n            {\r\n                return ac" +
                    "tion;\r\n            }\r\n            var bindByName = GetBasicPropertySetter(comman" +
                    "dType, \"BindByName\", typeof(bool));\r\n            var initialLongFetchSize = GetB" +
                    "asicPropertySetter(commandType, \"InitialLONGFetchSize\", typeof(int));\r\n\r\n       " +
                    "     action = null;\r\n            if (bindByName != null || initialLongFetchSize " +
                    "!= null)\r\n            {\r\n                var method = new DynamicMethod(commandT" +
                    "ype.Name + \"_init\", null, new Type[] { typeof(IDbCommand) });\r\n                v" +
                    "ar il = method.GetILGenerator();\r\n\r\n                if (bindByName != null)\r\n   " +
                    "             {\r\n                    // .BindByName = true\r\n                    i" +
                    "l.Emit(OpCodes.Ldarg_0);\r\n                    il.Emit(OpCodes.Castclass, command" +
                    "Type);\r\n                    il.Emit(OpCodes.Ldc_I4_1);\r\n                    il.E" +
                    "mitCall(OpCodes.Callvirt, bindByName, null);\r\n                }\r\n               " +
                    " if (initialLongFetchSize != null)\r\n                {\r\n                    // .I" +
                    "nitialLONGFetchSize = -1\r\n                    il.Emit(OpCodes.Ldarg_0);\r\n       " +
                    "             il.Emit(OpCodes.Castclass, commandType);\r\n                    il.Em" +
                    "it(OpCodes.Ldc_I4_M1);\r\n                    il.EmitCall(OpCodes.Callvirt, initia" +
                    "lLongFetchSize, null);\r\n                }\r\n                il.Emit(OpCodes.Ret);" +
                    "\r\n                action = (Action<IDbCommand>)method.CreateDelegate(typeof(Acti" +
                    "on<IDbCommand>));\r\n            }\r\n            // cache it\r\n            SqlMapper" +
                    ".Link<Type, Action<IDbCommand>>.TryAdd(ref commandInitCache, commandType, ref ac" +
                    "tion);\r\n            return action;\r\n        }\r\n\r\n        private static MethodIn" +
                    "fo GetBasicPropertySetter(Type declaringType, string name, Type expectedType)\r\n " +
                    "       {\r\n            var prop = declaringType.GetProperty(name, BindingFlags.Pu" +
                    "blic | BindingFlags.Instance);\r\n            if (prop != null && prop.CanWrite &&" +
                    " prop.PropertyType == expectedType && prop.GetIndexParameters().Length == 0)\r\n  " +
                    "          {\r\n                return prop.GetSetMethod();\r\n            }\r\n       " +
                    "     return null;\r\n        }\r\n    }\r\n}");
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
    public class CommandDefinitionBase
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
