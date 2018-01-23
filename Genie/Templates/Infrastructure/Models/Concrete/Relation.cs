#region Usings

using System.Linq;
using System.Text;
using Genie.Core.Base.Configuration.Abstract;
using Genie.Core.Base.Generating;
using Genie.Core.Extensions;
using Genie.Core.Models.Abstract;
using Genie.Core.Tools;

#endregion

namespace Genie.Core.Templates.Infrastructure.Models.Concrete
{
    public class RelationTemplate : GenieTemplate
    {
        private readonly IConfiguration _configuration;
        private readonly IEnum _enum;
        private readonly IRelation _relation;

        public RelationTemplate(string path, IRelation relation, IEnum @enum, IConfiguration configuration) : base(path)
        {
            _relation = relation;
            _enum = @enum;
            _configuration = configuration;
        }

        public override string Generate()
        {
            var entity = _relation;
            var name = _relation.Name;
            var quote = FormatHelper.GetDbmsSpecificQuoter(_configuration);

            var enm = new StringBuilder();
            if (_enum != null && _enum.Values.Count > 0)
            {
                enm.AppendLine($@"	public sealed class {_enum.Name}
	{{
		private readonly {_enum.Type} _value;
		private {_enum.Name}({_enum.Type} value)
	    {{
	        _value = value;
	    }}

		public static implicit operator {_enum.Type}({_enum.Name} @enum)
	    {{
	        return @enum._value;
	    }}

");
                foreach (var key in _enum.Values) enm.AppendLine($@"		private static {_enum.Name} {key.FieldName};");

                foreach (var key in _enum.Values)
                    enm.AppendLine(
                        $@"		public static {_enum.Name} {key.Name} => {key.FieldName} ?? ( {key.FieldName} = new {
                                _enum.Name
                            }({key.Value}));");

                enm.AppendLine($@"	}}");
            }

            var fields = new StringBuilder();
            foreach (var atd in entity.Attributes) fields.AppendLine($@"		private {atd.DataType} {atd.FieldName};");

            var fkFields = new StringBuilder();
            foreach (var atd in entity.ForeignKeyAttributes)
                fkFields.AppendLine(
                    $@"		private {atd.ReferencingRelationName} {atd.ReferencingNonForeignKeyAttribute.FieldName}Obj;");

            var attrProperties = new StringBuilder();

            foreach (var atd in entity.Attributes)
            {
                attrProperties.AppendLine();
                if (!string.IsNullOrWhiteSpace(atd.Comment))
                    attrProperties.AppendLine($@"		/// <summary>
		/// {atd.Comment}
		/// </summary>");

                if (atd.IsKey) attrProperties.AppendLine($@"		[Key]");

                if (atd.IsIdentity) attrProperties.AppendLine($@"		[Identity]");

                var rpn = atd.RefPropName != null ? atd.RefPropName + " = null;" : "";
                attrProperties.AppendLine(
                    $@"		public {atd.DataType} {atd.Name} {{ get {{ return {atd.FieldName}; }} set {{ if({
                            atd.FieldName
                        } == value ) {{ return; }}  {atd.FieldName} = value; __Updated(""{atd.Name}""); {rpn} }} }}");
            }

            var foreignKeyAttributes = new StringBuilder();
            var fkSetters = new StringBuilder();
            foreach (var atd in entity.ForeignKeyAttributes)
            {
                var fix = atd.ReferencingNonForeignKeyAttribute.DataType.EndsWith("?") ? ".GetValueOrDefault()" : "";
                foreignKeyAttributes.AppendLine($@"		/// <summary>
		/// Get {atd.ReferencingRelationName} object from {atd.ReferencingNonForeignKeyAttribute.Name} value.<para />This object will be cache within this instance.
		/// </summary>
		public {atd.ReferencingRelationName} Get{atd.ReferencingNonForeignKeyAttribute.Name}(IDbTransaction transaction =null)
        {{
            return DatabaseUnitOfWork != null ? {atd.ReferencingNonForeignKeyAttribute.FieldName}Obj ?? ({atd.ReferencingNonForeignKeyAttribute.FieldName}Obj = DatabaseUnitOfWork.{atd.ReferencingRelationName}Repository.Get().Where.{atd.ReferencingTableColumnName}.EqualsTo({atd.ReferencingNonForeignKeyAttribute.FieldName}{fix}).Filter().Top(1).FirstOrDefault(transaction)) : null;
        }}
        
        /// <summary>
		/// Get {atd.ReferencingRelationName} object from {atd.ReferencingNonForeignKeyAttribute.Name} value asynchronously .<para />This object will be cache within this instance.
		/// </summary>
		public async Task<{atd.ReferencingRelationName}> Get{atd.ReferencingNonForeignKeyAttribute.Name}Async(IDbTransaction transaction =null)
        {{
            return DatabaseUnitOfWork != null ? {atd.ReferencingNonForeignKeyAttribute.FieldName}Obj ?? ({atd.ReferencingNonForeignKeyAttribute.FieldName}Obj = await DatabaseUnitOfWork.{atd.ReferencingRelationName}Repository.Get().Where.{atd.ReferencingTableColumnName}.EqualsTo({atd.ReferencingNonForeignKeyAttribute.FieldName}{fix}).Filter().Top(1).FirstOrDefaultAsync(transaction)) : null;
        }}");

                fkSetters.AppendLine($@"		/// <summary>
		/// Set {atd.ReferencingRelationName} object for {atd.ReferencingNonForeignKeyAttribute.Name} value. <para />This will also change the {atd.ReferencingNonForeignKeyAttribute.Name} value.
		/// </summary>
		public void Set{atd.ReferencingNonForeignKeyAttribute.Name}({atd.ReferencingRelationName} entity)
        {{
            if (entity == null)
                return;
            switch (entity.DatabaseModelStatus)
            {{
                case ModelStatus.Retrieved:
                    {atd.ReferencingNonForeignKeyAttribute.Name} = entity.{atd.ReferencingTableColumnName};
                    break;
                case ModelStatus.ToAdd:
                    if (entity.ActionsToRunWhenAdding == null)
                        entity.ActionsToRunWhenAdding = new List<IAddAction>();
                    entity.ActionsToRunWhenAdding.Add(new AddAction(i => {{ {atd.ReferencingNonForeignKeyAttribute.Name
                    } = (({atd.ReferencingRelationName}) i).{atd.ReferencingTableColumnName}; }}, entity));
                    break;
            }}
        }}");
            }

            var referenceLists = new StringBuilder();
            foreach (var list in entity.ReferenceLists)
                referenceLists.AppendLine(
                    $@"		public IReferencedEntityCollection<{list.ReferencedRelationName}> {list.ReferencedRelationName.ToPlural()}WhereThisIs{list.ReferencedPropertyName}(IDbTransaction transaction = null ){{  return new ReferencedEntityCollection<{
                            list.ReferencedRelationName
                        }>(DatabaseUnitOfWork.{list.ReferencedRelationName}Repository.Get().Where.{
                            list.ReferencedPropertyName
                        }.EqualsTo({list.ReferencedPropertyOnThisRelation}).Filter().Query(transaction), (i) => {{ (({
                            list.ReferencedRelationName
                        })i).{list.ReferencedPropertyName} = {list.ReferencedPropertyOnThisRelation};}}, this); }}");

            var keys = entity.Attributes.Where(e => e.IsKey);
            var keysStr = new StringBuilder();
            foreach (var k in keys) keysStr.AppendLine($@"            {k.FieldName} = ({k.DataType})id;");

            var abstractModelsNamespace = _configuration.AbstractModelsEnabled
                ? $"using {_configuration.AbstractModelsNamespace};\n"
                : "";
            var absImplement = _configuration.AbstractModelsEnabled ? $", I{name}" : "";

            L($@"
using System;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using {GenerationContext.BaseNamespace}.Dapper;
using {GenerationContext.BaseNamespace}.Infrastructure.Collections.Concrete;
using {GenerationContext.BaseNamespace}.Infrastructure.Collections.Abstract;
using {GenerationContext.BaseNamespace}.Infrastructure.Actions.Abstract;
using {GenerationContext.BaseNamespace}.Infrastructure.Actions.Concrete;
{abstractModelsNamespace}
namespace {GenerationContext.BaseNamespace}.Infrastructure.Models.Concrete
{{

{enm}

    [Table(""{quote(_configuration.Schema)}.{quote(name)}"")]
    public class {name} : BaseModel {absImplement}
    {{

{fields}

{fkFields}

{foreignKeyAttributes}

{attrProperties}

{fkSetters}

{referenceLists}

        internal override void SetId(object id)
        {{

{keysStr}

        }}
    }}
}}
");

            return E();
        }
    }
}