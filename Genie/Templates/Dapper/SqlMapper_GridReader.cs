namespace Genie.Templates.Dapper
{
    using Genie.Base;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "D:\Projects\Genie\Genie\Templates\Dapper\SqlMapper_GridReader.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "14.0.0.0")]
    public partial class SqlMapper_GridReader : SqlMapper_GridReaderBase
    {
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("using System;\r\nusing System.Collections.Generic;\r\nusing System.Data;\r\nusing Syste" +
                    "m.Globalization;\r\nusing System.Linq;\r\n\r\nnamespace ");
            
            #line 9 "D:\Projects\Genie\Genie\Templates\Dapper\SqlMapper_GridReader.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GenerationContext.BaseNamespace));
            
            #line default
            #line hidden
            this.Write(".Dapper\r\n{\r\n    partial class SqlMapper\r\n    {\r\n        /// <summary>\r\n        //" +
                    "/ The grid reader provides interfaces for reading multiple result sets from a Da" +
                    "pper query\r\n        /// </summary>\r\n        public partial class GridReader : ID" +
                    "isposable\r\n        {\r\n            private IDataReader reader;\r\n            priva" +
                    "te Identity identity;\r\n            private bool addToCache;\r\n\r\n            inter" +
                    "nal GridReader(IDbCommand command, IDataReader reader, Identity identity, IParam" +
                    "eterCallbacks callbacks, bool addToCache)\r\n            {\r\n                Comman" +
                    "d = command;\r\n                this.reader = reader;\r\n                this.identi" +
                    "ty = identity;\r\n                this.callbacks = callbacks;\r\n                thi" +
                    "s.addToCache = addToCache;\r\n            }\r\n\r\n            /// <summary>\r\n        " +
                    "    /// Read the next grid of results, returned as a dynamic object\r\n           " +
                    " /// </summary>\r\n            /// <remarks>Note: each row can be accessed via \"dy" +
                    "namic\", or by casting to an IDictionary&lt;string,object&gt;</remarks>\r\n        " +
                    "    public IEnumerable<dynamic> Read(bool buffered = true)\r\n            {\r\n     " +
                    "           return ReadImpl<dynamic>(typeof(DapperRow), buffered);\r\n            }" +
                    "\r\n\r\n            /// <summary>\r\n            /// Read an individual row of the nex" +
                    "t grid of results, returned as a dynamic object\r\n            /// </summary>\r\n   " +
                    "         /// <remarks>Note: the row can be accessed via \"dynamic\", or by casting" +
                    " to an IDictionary&lt;string,object&gt;</remarks>\r\n            public dynamic Re" +
                    "adFirst()\r\n            {\r\n                return ReadRow<dynamic>(typeof(DapperR" +
                    "ow), Row.First);\r\n            }\r\n            /// <summary>\r\n            /// Read" +
                    " an individual row of the next grid of results, returned as a dynamic object\r\n  " +
                    "          /// </summary>\r\n            /// <remarks>Note: the row can be accessed" +
                    " via \"dynamic\", or by casting to an IDictionary&lt;string,object&gt;</remarks>\r\n" +
                    "            public dynamic ReadFirstOrDefault()\r\n            {\r\n                " +
                    "return ReadRow<dynamic>(typeof(DapperRow), Row.FirstOrDefault);\r\n            }\r\n" +
                    "            /// <summary>\r\n            /// Read an individual row of the next gr" +
                    "id of results, returned as a dynamic object\r\n            /// </summary>\r\n       " +
                    "     /// <remarks>Note: the row can be accessed via \"dynamic\", or by casting to " +
                    "an IDictionary&lt;string,object&gt;</remarks>\r\n            public dynamic ReadSi" +
                    "ngle()\r\n            {\r\n                return ReadRow<dynamic>(typeof(DapperRow)" +
                    ", Row.Single);\r\n            }\r\n            /// <summary>\r\n            /// Read a" +
                    "n individual row of the next grid of results, returned as a dynamic object\r\n    " +
                    "        /// </summary>\r\n            /// <remarks>Note: the row can be accessed v" +
                    "ia \"dynamic\", or by casting to an IDictionary&lt;string,object&gt;</remarks>\r\n  " +
                    "          public dynamic ReadSingleOrDefault()\r\n            {\r\n                r" +
                    "eturn ReadRow<dynamic>(typeof(DapperRow), Row.SingleOrDefault);\r\n            }\r\n" +
                    "\r\n            /// <summary>\r\n            /// Read the next grid of results\r\n    " +
                    "        /// </summary>\r\n            public IEnumerable<T> Read<T>(bool buffered " +
                    "= true)\r\n            {\r\n                return ReadImpl<T>(typeof(T), buffered);" +
                    "\r\n            }\r\n\r\n            /// <summary>\r\n            /// Read an individual" +
                    " row of the next grid of results\r\n            /// </summary>\r\n            public" +
                    " T ReadFirst<T>()\r\n            {\r\n                return ReadRow<T>(typeof(T), R" +
                    "ow.First);\r\n            }\r\n            /// <summary>\r\n            /// Read an in" +
                    "dividual row of the next grid of results\r\n            /// </summary>\r\n          " +
                    "  public T ReadFirstOrDefault<T>()\r\n            {\r\n                return ReadRo" +
                    "w<T>(typeof(T), Row.FirstOrDefault);\r\n            }\r\n            /// <summary>\r\n" +
                    "            /// Read an individual row of the next grid of results\r\n            " +
                    "/// </summary>\r\n            public T ReadSingle<T>()\r\n            {\r\n           " +
                    "     return ReadRow<T>(typeof(T), Row.Single);\r\n            }\r\n            /// <" +
                    "summary>\r\n            /// Read an individual row of the next grid of results\r\n  " +
                    "          /// </summary>\r\n            public T ReadSingleOrDefault<T>()\r\n       " +
                    "     {\r\n                return ReadRow<T>(typeof(T), Row.SingleOrDefault);\r\n    " +
                    "        }\r\n\r\n            /// <summary>\r\n            /// Read the next grid of re" +
                    "sults\r\n            /// </summary>\r\n            public IEnumerable<object> Read(T" +
                    "ype type, bool buffered = true)\r\n            {\r\n                if (type == null" +
                    ") throw new ArgumentNullException(nameof(type));\r\n                return ReadImp" +
                    "l<object>(type, buffered);\r\n            }\r\n\r\n            /// <summary>\r\n        " +
                    "    /// Read an individual row of the next grid of results\r\n            /// </su" +
                    "mmary>\r\n            public object ReadFirst(Type type)\r\n            {\r\n         " +
                    "       if (type == null) throw new ArgumentNullException(nameof(type));\r\n       " +
                    "         return ReadRow<object>(type, Row.First);\r\n            }\r\n            //" +
                    "/ <summary>\r\n            /// Read an individual row of the next grid of results\r" +
                    "\n            /// </summary>\r\n            public object ReadFirstOrDefault(Type t" +
                    "ype)\r\n            {\r\n                if (type == null) throw new ArgumentNullExc" +
                    "eption(nameof(type));\r\n                return ReadRow<object>(type, Row.FirstOrD" +
                    "efault);\r\n            }\r\n            /// <summary>\r\n            /// Read an indi" +
                    "vidual row of the next grid of results\r\n            /// </summary>\r\n            " +
                    "public object ReadSingle(Type type)\r\n            {\r\n                if (type == " +
                    "null) throw new ArgumentNullException(nameof(type));\r\n                return Rea" +
                    "dRow<object>(type, Row.Single);\r\n            }\r\n            /// <summary>\r\n     " +
                    "       /// Read an individual row of the next grid of results\r\n            /// <" +
                    "/summary>\r\n            public object ReadSingleOrDefault(Type type)\r\n           " +
                    " {\r\n                if (type == null) throw new ArgumentNullException(nameof(typ" +
                    "e));\r\n                return ReadRow<object>(type, Row.SingleOrDefault);\r\n      " +
                    "      }\r\n\r\n            private IEnumerable<T> ReadImpl<T>(Type type, bool buffer" +
                    "ed)\r\n            {\r\n                if (reader == null) throw new ObjectDisposed" +
                    "Exception(GetType().FullName, \"The reader has been disposed; this can happen aft" +
                    "er all data has been consumed\");\r\n                if (IsConsumed) throw new Inva" +
                    "lidOperationException(\"Query results must be consumed in the correct order, and " +
                    "each result can only be consumed once\");\r\n                var typedIdentity = id" +
                    "entity.ForGrid(type, gridIndex);\r\n                CacheInfo cache = GetCacheInfo" +
                    "(typedIdentity, null, addToCache);\r\n                var deserializer = cache.Des" +
                    "erializer;\r\n\r\n                int hash = GetColumnHash(reader);\r\n               " +
                    " if (deserializer.Func == null || deserializer.Hash != hash)\r\n                {\r" +
                    "\n                    deserializer = new DeserializerState(hash, GetDeserializer(" +
                    "type, reader, 0, -1, false));\r\n                    cache.Deserializer = deserial" +
                    "izer;\r\n                }\r\n                IsConsumed = true;\r\n                va" +
                    "r result = ReadDeferred<T>(gridIndex, deserializer.Func, typedIdentity, type);\r\n" +
                    "                return buffered ? result.ToList() : result;\r\n            }\r\n\r\n  " +
                    "          private T ReadRow<T>(Type type, Row row)\r\n            {\r\n             " +
                    "   if (reader == null) throw new ObjectDisposedException(GetType().FullName, \"Th" +
                    "e reader has been disposed; this can happen after all data has been consumed\");\r" +
                    "\n                if (IsConsumed) throw new InvalidOperationException(\"Query resu" +
                    "lts must be consumed in the correct order, and each result can only be consumed " +
                    "once\");\r\n                IsConsumed = true;\r\n\r\n                T result = defaul" +
                    "t(T);\r\n                if (reader.Read() && reader.FieldCount != 0)\r\n           " +
                    "     {\r\n                    var typedIdentity = identity.ForGrid(type, gridIndex" +
                    ");\r\n                    CacheInfo cache = GetCacheInfo(typedIdentity, null, addT" +
                    "oCache);\r\n                    var deserializer = cache.Deserializer;\r\n\r\n        " +
                    "            int hash = GetColumnHash(reader);\r\n                    if (deseriali" +
                    "zer.Func == null || deserializer.Hash != hash)\r\n                    {\r\n         " +
                    "               deserializer = new DeserializerState(hash, GetDeserializer(type, " +
                    "reader, 0, -1, false));\r\n                        cache.Deserializer = deserializ" +
                    "er;\r\n                    }\r\n                    object val = deserializer.Func(r" +
                    "eader);\r\n                    if (val == null || val is T)\r\n                    {" +
                    "\r\n                        result = (T)val;\r\n                    }\r\n             " +
                    "       else\r\n                    {\r\n                        var convertToType = " +
                    "Nullable.GetUnderlyingType(type) ?? type;\r\n                        result = (T)C" +
                    "onvert.ChangeType(val, convertToType, CultureInfo.InvariantCulture);\r\n          " +
                    "          }\r\n                    if ((row & Row.Single) != 0 && reader.Read()) T" +
                    "hrowMultipleRows(row);\r\n                    while (reader.Read()) { }\r\n         " +
                    "       }\r\n                else if ((row & Row.FirstOrDefault) == 0) // demanding" +
                    " a row, and don\'t have one\r\n                {\r\n                    ThrowZeroRows" +
                    "(row);\r\n                }\r\n                NextResult();\r\n                return" +
                    " result;\r\n            }\r\n\r\n\r\n            private IEnumerable<TReturn> MultiReadI" +
                    "nternal<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(Del" +
                    "egate func, string splitOn)\r\n            {\r\n                var identity = this." +
                    "identity.ForGrid(typeof(TReturn), new Type[] {\r\n                    typeof(TFirs" +
                    "t),\r\n                    typeof(TSecond),\r\n                    typeof(TThird),\r\n" +
                    "                    typeof(TFourth),\r\n                    typeof(TFifth),\r\n     " +
                    "               typeof(TSixth),\r\n                    typeof(TSeventh)\r\n          " +
                    "      }, gridIndex);\r\n                try\r\n                {\r\n                  " +
                    "  foreach (var r in MultiMapImpl<TFirst, TSecond, TThird, TFourth, TFifth, TSixt" +
                    "h, TSeventh, TReturn>(null, default(CommandDefinition), func, splitOn, reader, i" +
                    "dentity, false))\r\n                    {\r\n                        yield return r;" +
                    "\r\n                    }\r\n                }\r\n                finally\r\n           " +
                    "     {\r\n                    NextResult();\r\n                }\r\n            }\r\n\r\n " +
                    "           private IEnumerable<TReturn> MultiReadInternal<TReturn>(Type[] types," +
                    " Func<object[], TReturn> map, string splitOn)\r\n            {\r\n                va" +
                    "r identity = this.identity.ForGrid(typeof(TReturn), types, gridIndex);\r\n        " +
                    "        try\r\n                {\r\n                    foreach (var r in MultiMapIm" +
                    "pl<TReturn>(null, default(CommandDefinition), types, map, splitOn, reader, ident" +
                    "ity, false))\r\n                    {\r\n                        yield return r;\r\n  " +
                    "                  }\r\n                }\r\n                finally\r\n               " +
                    " {\r\n                    NextResult();\r\n                }\r\n            }\r\n\r\n     " +
                    "       /// <summary>\r\n            /// Read multiple objects from a single record" +
                    " set on the grid\r\n            /// </summary>\r\n            public IEnumerable<TRe" +
                    "turn> Read<TFirst, TSecond, TReturn>(Func<TFirst, TSecond, TReturn> func, string" +
                    " splitOn = \"id\", bool buffered = true)\r\n            {\r\n                var resul" +
                    "t = MultiReadInternal<TFirst, TSecond, DontMap, DontMap, DontMap, DontMap, DontM" +
                    "ap, TReturn>(func, splitOn);\r\n                return buffered ? result.ToList() " +
                    ": result;\r\n            }\r\n\r\n            /// <summary>\r\n            /// Read mult" +
                    "iple objects from a single record set on the grid\r\n            /// </summary>\r\n " +
                    "           public IEnumerable<TReturn> Read<TFirst, TSecond, TThird, TReturn>(Fu" +
                    "nc<TFirst, TSecond, TThird, TReturn> func, string splitOn = \"id\", bool buffered " +
                    "= true)\r\n            {\r\n                var result = MultiReadInternal<TFirst, T" +
                    "Second, TThird, DontMap, DontMap, DontMap, DontMap, TReturn>(func, splitOn);\r\n  " +
                    "              return buffered ? result.ToList() : result;\r\n            }\r\n\r\n    " +
                    "        /// <summary>\r\n            /// Read multiple objects from a single recor" +
                    "d set on the grid\r\n            /// </summary>\r\n            public IEnumerable<TR" +
                    "eturn> Read<TFirst, TSecond, TThird, TFourth, TReturn>(Func<TFirst, TSecond, TTh" +
                    "ird, TFourth, TReturn> func, string splitOn = \"id\", bool buffered = true)\r\n     " +
                    "       {\r\n                var result = MultiReadInternal<TFirst, TSecond, TThird" +
                    ", TFourth, DontMap, DontMap, DontMap, TReturn>(func, splitOn);\r\n                " +
                    "return buffered ? result.ToList() : result;\r\n            }\r\n\r\n            /// <s" +
                    "ummary>\r\n            /// Read multiple objects from a single record set on the g" +
                    "rid\r\n            /// </summary>\r\n            public IEnumerable<TReturn> Read<TF" +
                    "irst, TSecond, TThird, TFourth, TFifth, TReturn>(Func<TFirst, TSecond, TThird, T" +
                    "Fourth, TFifth, TReturn> func, string splitOn = \"id\", bool buffered = true)\r\n   " +
                    "         {\r\n                var result = MultiReadInternal<TFirst, TSecond, TThi" +
                    "rd, TFourth, TFifth, DontMap, DontMap, TReturn>(func, splitOn);\r\n               " +
                    " return buffered ? result.ToList() : result;\r\n            }\r\n            /// <su" +
                    "mmary>\r\n            /// Read multiple objects from a single record set on the gr" +
                    "id\r\n            /// </summary>\r\n            public IEnumerable<TReturn> Read<TFi" +
                    "rst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(Func<TFirst, TSecond, TT" +
                    "hird, TFourth, TFifth, TSixth, TReturn> func, string splitOn = \"id\", bool buffer" +
                    "ed = true)\r\n            {\r\n                var result = MultiReadInternal<TFirst" +
                    ", TSecond, TThird, TFourth, TFifth, TSixth, DontMap, TReturn>(func, splitOn);\r\n " +
                    "               return buffered ? result.ToList() : result;\r\n            }\r\n     " +
                    "       /// <summary>\r\n            /// Read multiple objects from a single record" +
                    " set on the grid\r\n            /// </summary>\r\n            public IEnumerable<TRe" +
                    "turn> Read<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(" +
                    "Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> func, " +
                    "string splitOn = \"id\", bool buffered = true)\r\n            {\r\n                var" +
                    " result = MultiReadInternal<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TS" +
                    "eventh, TReturn>(func, splitOn);\r\n                return buffered ? result.ToLis" +
                    "t() : result;\r\n            }\r\n\r\n            /// <summary>\r\n            /// Read " +
                    "multiple objects from a single record set on the grid\r\n            /// </summary" +
                    ">\r\n            public IEnumerable<TReturn> Read<TReturn>(Type[] types, Func<obje" +
                    "ct[], TReturn> map, string splitOn = \"id\", bool buffered = true)\r\n            {\r" +
                    "\n                var result = MultiReadInternal<TReturn>(types, map, splitOn);\r\n" +
                    "                return buffered ? result.ToList() : result;\r\n            }\r\n\r\n  " +
                    "          private IEnumerable<T> ReadDeferred<T>(int index, Func<IDataReader, ob" +
                    "ject> deserializer, Identity typedIdentity, Type effectiveType)\r\n            {\r\n" +
                    "                try\r\n                {\r\n                    var convertToType = " +
                    "Nullable.GetUnderlyingType(effectiveType) ?? effectiveType;\r\n                   " +
                    " while (index == gridIndex && reader.Read())\r\n                    {\r\n           " +
                    "             object val = deserializer(reader);\r\n                        if (val" +
                    " == null || val is T)\r\n                        {\r\n                            yi" +
                    "eld return (T)val;\r\n                        }\r\n                        else\r\n   " +
                    "                     {\r\n                            yield return (T)Convert.Chan" +
                    "geType(val, convertToType, CultureInfo.InvariantCulture);\r\n                     " +
                    "   }\r\n                    }\r\n                }\r\n                finally // final" +
                    "ly so that First etc progresses things even when multiple rows\r\n                " +
                    "{\r\n                    if (index == gridIndex)\r\n                    {\r\n         " +
                    "               NextResult();\r\n                    }\r\n                }\r\n        " +
                    "    }\r\n            private int gridIndex, readCount;\r\n            private IParam" +
                    "eterCallbacks callbacks;\r\n\r\n            /// <summary>\r\n            /// Has the u" +
                    "nderlying reader been consumed?\r\n            /// </summary>\r\n            public " +
                    "bool IsConsumed { get; private set; }\r\n\r\n            /// <summary>\r\n            " +
                    "/// The command associated with the reader\r\n            /// </summary>\r\n        " +
                    "    public IDbCommand Command { get; set; }\r\n\r\n            private void NextResu" +
                    "lt()\r\n            {\r\n                if (reader.NextResult())\r\n                {" +
                    "\r\n                    readCount++;\r\n                    gridIndex++;\r\n          " +
                    "          IsConsumed = false;\r\n                }\r\n                else\r\n        " +
                    "        {\r\n                    // happy path; close the reader cleanly - no\r\n   " +
                    "                 // need for \"Cancel\" etc\r\n                    reader.Dispose();" +
                    "\r\n                    reader = null;\r\n                    callbacks?.OnCompleted" +
                    "();\r\n                    Dispose();\r\n                }\r\n            }\r\n         " +
                    "   /// <summary>\r\n            /// Dispose the grid, closing and disposing both t" +
                    "he underlying reader and command.\r\n            /// </summary>\r\n            publi" +
                    "c void Dispose()\r\n            {\r\n                if (reader != null)\r\n          " +
                    "      {\r\n                    if (!reader.IsClosed) Command?.Cancel();\r\n         " +
                    "           reader.Dispose();\r\n                    reader = null;\r\n              " +
                    "  }\r\n                if (Command != null)\r\n                {\r\n                  " +
                    "  Command.Dispose();\r\n                    Command = null;\r\n                }\r\n  " +
                    "          }\r\n        }\r\n    }\r\n}\r\n");
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
    public class SqlMapper_GridReaderBase
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
