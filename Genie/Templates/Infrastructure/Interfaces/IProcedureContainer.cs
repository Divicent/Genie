#region Usings

using System.Text;
using Genie.Core.Base.Generating;
using Genie.Core.Base.Reading.Abstract;

#endregion

namespace Genie.Core.Templates.Infrastructure.Interfaces
{
    internal class IProcedureContainerTemplate : GenieTemplate
    {
        private readonly IDatabaseSchema _schema;

        public IProcedureContainerTemplate(string path, IDatabaseSchema schema) : base(path)
        {
            _schema = schema;
        }

        public override string Generate()
        {
            var sps = new StringBuilder();
            foreach (var sp in _schema.Procedures)
            {
                var parametersCommentBuilder = new StringBuilder();
                foreach(var parameter in sp.Parameters) 
                {
                    parametersCommentBuilder.AppendLine($@"
        /// <param name=""{parameter.Name.Replace("@", "")}"">Value to pass to the procedure's parameter '{parameter.Name.Replace("@", "")}'</param>");
                }
                sps.AppendLine($@"
        /// <summary>
        /// This will execute {sp.Name} and try to map the result to a <typeparamref name=""T""/> collection{parametersCommentBuilder}
        /// <returns>Collection of <typeparamref name=""T""/> </returns>
		IEnumerable<T> {sp.Name}_List<T>({sp.ParamString});
");
                sps.AppendLine($@"
        /// <summary>
        /// This will execute {sp.Name} and try to map map the result as <typeparamref name=""T""/>
        /// </summary>
        /// <typeparam name=""T"">Type to map the result</typeparam>{parametersCommentBuilder}
        /// <returns>Procedure result as <typeparamref name=""T""/></returns>
		T {sp.Name}_Single<T>({sp.ParamString});
");
                sps.AppendLine($@"
        /// <summary>
        /// This will execute {sp.Name} and will not expect a result{parametersCommentBuilder}
        /// </summary>
		void {sp.Name}_Void({sp.ParamString});
");
            }
            L($@"
using System;
using System.Collections.Generic;

namespace {GenerationContext.BaseNamespace}.Infrastructure.Interfaces
{{
    /// <summary>
    /// Contains methods for invoke all stored procedures in the data store.
    /// these methods will directly invoke without any side effects. all parameters are optional in all methods.
    /// 
    /// for each stored procedure there are 3 methods, because the generation process cannot identify the result at the time the code is generated.
    /// void methods are for execute procedures without expecting a result which are suffixed as _Void.
    /// list methods are to execute procedures and expect a collection which are suffixed with as _List
    /// single methods are to execute a procedures and expect a single object. object can be any type these methods are suffixed as _Single
    /// </summary>
	  public interface IProcedureContainer
    {{

{sps}

    }}
}}
");

            return E();
        }
    }
}