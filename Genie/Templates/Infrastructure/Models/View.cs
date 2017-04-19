﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 14.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace Genie.Templates.Infrastructure.Models
{
    using Genie.Base;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "14.0.0.0")]
    public partial class View : ViewBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("using System;\r\nusing System.Collections.Generic;\r\nusing System.Data;\r\nusing Indic" +
                    "o.DataAccess.Dapper;\r\nusing ");
            
            #line 7 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GenerationContext.BaseNamespace));
            
            #line default
            #line hidden
            this.Write(".Infrastructure.Repositories;\r\nusing ");
            
            #line 8 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GenerationContext.BaseNamespace));
            
            #line default
            #line hidden
            this.Write(".Infrastructure.Filters;\r\n\r\nnamespace ");
            
            #line 10 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GenerationContext.BaseNamespace));
            
            #line default
            #line hidden
            this.Write(".Infrastructure.Models\r\n{\r\n    [Table(\"[dbo].[");
            
            #line 12 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_view.Name));
            
            #line default
            #line hidden
            this.Write("]\")]\r\n    public class ");
            
            #line 13 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_view.Name));
            
            #line default
            #line hidden
            this.Write(" \r\n    {\r\n");
            
            #line 15 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
foreach(var atd in _view.Attributes){
            
            #line default
            #line hidden
            this.Write("\t\tpublic ");
            
            #line 16 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(atd.DataType));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 16 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(atd.Name));
            
            #line default
            #line hidden
            this.Write(" { get; set; } \r\n");
            
            #line 17 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
}
            
            #line default
            #line hidden
            this.Write("    }\r\n\r\n    public class ");
            
            #line 20 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_view.Name));
            
            #line default
            #line hidden
            this.Write("QueryContext \r\n\t{\r\n\t\tprivate ");
            
            #line 22 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_view.Name));
            
            #line default
            #line hidden
            this.Write("FilterContext _where; \r\n\t    private ");
            
            #line 23 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_view.Name));
            
            #line default
            #line hidden
            this.Write("OrderContext _order;\r\n\t\tprivate readonly ");
            
            #line 24 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_view.Name));
            
            #line default
            #line hidden
            this.Write("Repository _repo;\r\n\t\tinternal ");
            
            #line 25 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_view.Name));
            
            #line default
            #line hidden
            this.Write("QueryContext(");
            
            #line 25 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_view.Name));
            
            #line default
            #line hidden
            this.Write("Repository repo) { _repo = repo; }\r\n\t\tpublic ");
            
            #line 26 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_view.Name));
            
            #line default
            #line hidden
            this.Write("FilterContext Where { get { return _where ?? (_where = new ");
            
            #line 26 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_view.Name));
            
            #line default
            #line hidden
            this.Write("FilterContext(_repo, this)); }}\r\n        public ");
            
            #line 27 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_view.Name));
            
            #line default
            #line hidden
            this.Write("OrderContext OrderBy { get { return _order ?? (_order = new ");
            
            #line 27 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_view.Name));
            
            #line default
            #line hidden
            this.Write("OrderContext(_repo, this)); } }\r\n        private int? _page;\r\n\t    private int? _" +
                    "pageSize;\r\n\t    private int? _limit;\r\n\r\n        public ");
            
            #line 32 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_view.Name));
            
            #line default
            #line hidden
            this.Write("QueryContext Page(int pageSize, int page)\r\n        {\r\n            _page = page;\r\n" +
                    "            _pageSize = pageSize;\r\n            return this;\r\n        }\r\n\r\n      " +
                    "  public ");
            
            #line 39 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_view.Name));
            
            #line default
            #line hidden
            this.Write("QueryContext Top(int limit)\r\n        {\r\n            _limit = limit;\r\n            " +
                    "return this;\r\n        }\r\n\r\n\t    public IEnumerable<");
            
            #line 45 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_view.Name));
            
            #line default
            #line hidden
            this.Write("> Query(IDbTransaction transaction = null)\r\n\t    {\r\n\t        return _repo.Get(\"[d" +
                    "bo].[");
            
            #line 47 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_view.Name));
            
            #line default
            #line hidden
            this.Write("]\", _where == null ? null : _where.GetFilterExpressions(), _order == null ? null " +
                    ": _order.GetOrderExpressions(), _pageSize, _page, _limit, transaction);\r\n\t    }\r" +
                    "\n\t}\r\n\r\n\tpublic class ");
            
            #line 51 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_view.Name));
            
            #line default
            #line hidden
            this.Write("FilterContext : BaseFilterContext\r\n\t{\r\n\t\tprivate ");
            
            #line 53 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_view.Name));
            
            #line default
            #line hidden
            this.Write("Repository _repo;\r\n\t\tprivate readonly ");
            
            #line 54 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_view.Name));
            
            #line default
            #line hidden
            this.Write("QueryContext  _queryContext;\r\n\t\tinternal ");
            
            #line 55 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_view.Name));
            
            #line default
            #line hidden
            this.Write("FilterContext(");
            
            #line 55 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_view.Name));
            
            #line default
            #line hidden
            this.Write("Repository repo, ");
            
            #line 55 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_view.Name));
            
            #line default
            #line hidden
            this.Write("QueryContext context) { _repo = repo; _queryContext = context; }\r\n        \r\n");
            
            #line 57 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
foreach(var atd in _view.Attributes){
            
            #line default
            #line hidden
            
            #line 58 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
if(atd.DataType == "string"){
            
            #line default
            #line hidden
            this.Write("\t\tprivate StringFilter<");
            
            #line 59 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_view.Name));
            
            #line default
            #line hidden
            this.Write("FilterContext,");
            
            #line 59 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_view.Name));
            
            #line default
            #line hidden
            this.Write("QueryContext> ");
            
            #line 59 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(atd.FieldName));
            
            #line default
            #line hidden
            this.Write(";\r\n\t\tpublic StringFilter<");
            
            #line 60 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_view.Name));
            
            #line default
            #line hidden
            this.Write("FilterContext,");
            
            #line 60 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_view.Name));
            
            #line default
            #line hidden
            this.Write("QueryContext> ");
            
            #line 60 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(atd.Name));
            
            #line default
            #line hidden
            this.Write(" { get { return ");
            
            #line 60 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(atd.FieldName));
            
            #line default
            #line hidden
            this.Write(" ?? ( ");
            
            #line 60 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(atd.FieldName));
            
            #line default
            #line hidden
            this.Write(" = new StringFilter<");
            
            #line 60 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_view.Name));
            
            #line default
            #line hidden
            this.Write("FilterContext,");
            
            #line 60 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_view.Name));
            
            #line default
            #line hidden
            this.Write("QueryContext>(\"");
            
            #line 60 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(atd.Name));
            
            #line default
            #line hidden
            this.Write("\", this, _queryContext)); } }\r\n");
            
            #line 61 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
} else if(atd.DataType == "int" || atd.DataType == "int?" || atd.DataType == "double" || atd.DataType == "double?" || atd.DataType == "decimal" || atd.DataType == "decimal?" || atd.DataType == "long" || atd.DataType == "long?" ){
            
            #line default
            #line hidden
            this.Write("\t\tprivate NumberFilter<");
            
            #line 62 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_view.Name));
            
            #line default
            #line hidden
            this.Write("FilterContext,");
            
            #line 62 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_view.Name));
            
            #line default
            #line hidden
            this.Write("QueryContext> ");
            
            #line 62 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(atd.FieldName));
            
            #line default
            #line hidden
            this.Write(";\r\n\t\tpublic NumberFilter<");
            
            #line 63 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_view.Name));
            
            #line default
            #line hidden
            this.Write("FilterContext,");
            
            #line 63 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_view.Name));
            
            #line default
            #line hidden
            this.Write("QueryContext> ");
            
            #line 63 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(atd.Name));
            
            #line default
            #line hidden
            this.Write(" { get { return ");
            
            #line 63 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(atd.FieldName));
            
            #line default
            #line hidden
            this.Write(" ?? ( ");
            
            #line 63 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(atd.FieldName));
            
            #line default
            #line hidden
            this.Write(" = new NumberFilter<");
            
            #line 63 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_view.Name));
            
            #line default
            #line hidden
            this.Write("FilterContext,");
            
            #line 63 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_view.Name));
            
            #line default
            #line hidden
            this.Write("QueryContext>(\"");
            
            #line 63 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(atd.Name));
            
            #line default
            #line hidden
            this.Write("\", this, _queryContext)); } }\r\n");
            
            #line 64 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
}else if(atd.DataType == "DateTime" || atd.DataType == "DateTime?"){
            
            #line default
            #line hidden
            this.Write("    \tprivate DateFilter<");
            
            #line 65 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_view.Name));
            
            #line default
            #line hidden
            this.Write("FilterContext,");
            
            #line 65 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_view.Name));
            
            #line default
            #line hidden
            this.Write("QueryContext> ");
            
            #line 65 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(atd.FieldName));
            
            #line default
            #line hidden
            this.Write(";\r\n\t\tpublic DateFilter<");
            
            #line 66 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_view.Name));
            
            #line default
            #line hidden
            this.Write("FilterContext,");
            
            #line 66 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_view.Name));
            
            #line default
            #line hidden
            this.Write("QueryContext> ");
            
            #line 66 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(atd.Name));
            
            #line default
            #line hidden
            this.Write(" { get { return ");
            
            #line 66 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(atd.FieldName));
            
            #line default
            #line hidden
            this.Write(" ?? ( ");
            
            #line 66 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(atd.FieldName));
            
            #line default
            #line hidden
            this.Write(" = new DateFilter<");
            
            #line 66 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_view.Name));
            
            #line default
            #line hidden
            this.Write("FilterContext,");
            
            #line 66 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_view.Name));
            
            #line default
            #line hidden
            this.Write("QueryContext>(\"");
            
            #line 66 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(atd.Name));
            
            #line default
            #line hidden
            this.Write("\", this, _queryContext)); } }\r\n");
            
            #line 67 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
}
            
            #line default
            #line hidden
            
            #line 68 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
}
            
            #line default
            #line hidden
            this.Write("    }\r\n\r\n    public class  ");
            
            #line 71 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_view.Name));
            
            #line default
            #line hidden
            this.Write("OrderContext : BaseOrderContext\r\n    {\r\n\t\tprivate ");
            
            #line 73 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_view.Name));
            
            #line default
            #line hidden
            this.Write("Repository _repo;\r\n\t\tprivate readonly ");
            
            #line 74 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_view.Name));
            
            #line default
            #line hidden
            this.Write("QueryContext  _queryContext;\r\n\t\tinternal ");
            
            #line 75 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_view.Name));
            
            #line default
            #line hidden
            this.Write("OrderContext(");
            
            #line 75 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_view.Name));
            
            #line default
            #line hidden
            this.Write("Repository repo, ");
            
            #line 75 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_view.Name));
            
            #line default
            #line hidden
            this.Write("QueryContext context) { _repo = repo; _queryContext = context; }\r\n\r\n");
            
            #line 77 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
foreach(var atd in _view.Attributes){
            
            #line default
            #line hidden
            this.Write("        private OrderElement<");
            
            #line 78 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_view.Name));
            
            #line default
            #line hidden
            this.Write("OrderContext,");
            
            #line 78 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_view.Name));
            
            #line default
            #line hidden
            this.Write("QueryContext> ");
            
            #line 78 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(atd.FieldName));
            
            #line default
            #line hidden
            this.Write(";\r\n\t\tpublic OrderElement<");
            
            #line 79 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_view.Name));
            
            #line default
            #line hidden
            this.Write("OrderContext,");
            
            #line 79 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_view.Name));
            
            #line default
            #line hidden
            this.Write("QueryContext> ");
            
            #line 79 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(atd.Name));
            
            #line default
            #line hidden
            this.Write(" { get { return ");
            
            #line 79 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(atd.FieldName));
            
            #line default
            #line hidden
            this.Write(" ?? ( ");
            
            #line 79 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(atd.FieldName));
            
            #line default
            #line hidden
            this.Write(" = new OrderElement<");
            
            #line 79 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_view.Name));
            
            #line default
            #line hidden
            this.Write("OrderContext,");
            
            #line 79 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_view.Name));
            
            #line default
            #line hidden
            this.Write("QueryContext>(\"");
            
            #line 79 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(atd.Name));
            
            #line default
            #line hidden
            this.Write("\", this, _queryContext)); } }\r\n");
            
            #line 80 "D:\Projects\Genie\Genie\Templates\Infrastructure\Models\View.tt"
}
            
            #line default
            #line hidden
            this.Write("    }\r\n}\r\n");
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
    public class ViewBase
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
