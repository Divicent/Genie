#region Usings

using System.Linq;
using Genie.Core.Base.Configuration.Abstract;
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


            const string template =
@"
{% if hasEnum %}
            	public sealed class {{enam.Name}}
                {
		            private readonly {{enam.Type}} _value;
		            private {{enam.Name}}({{enam.Type}} value)
	                {
	                    _value = value;
	                }

		            public static implicit operator {{enam.Type}}({{enam.Name}} @enum)
	                {
	                    return @enum._value;
	                }
                    
{% for key in enam.Values %}
                    private static {{enam.Name}} {{key.FieldName}};
{% endfor %}

{% for key in enam.Values %}
                    public static {{enam.Name}} {{key.Name}} => {{key.FieldName}} ?? ( {{key.FieldName}} = new {{enam.Name}}({{key.Value}}));
{% endfor %}

                }
{% endif %}

                [Table(""{{quotedSchema}}.{{quotedName}}"")]
                public class {{name}} : BaseModel {{absImplement}}
                {


{% if abstractModelsEnabled %}
		            public {{name}}() { }
        
                    public {{name}}(I{{name}} model) 
                    {
                        if(model == null) { return; }
                       {% for attribute in relation.Attributes %}
                        {{attribute.Name}} = model.{{attribute.Name}};
                       {% endfor %}
                    }
{% endif %}

{% for atd in relation.Attributes %}
                    private {{atd.DataType}} {{atd.FieldName}};
{% endfor %}

{% for atd in relation.ForeignKeyAttributes %}
		            private {{atd.ReferencingRelationName}} {{atd.ReferencingNonForeignKeyAttribute.FieldName}}Obj;
{% endfor %}

{% for atd in relation.ForeignKeyAttributes %}
            		/// <summary>
		            /// Get {{atd.ReferencingRelationName}} object from {{atd.ReferencingNonForeignKeyAttribute.Name}} value.<para />This object will be cache within this instance.
		            /// </summary>
		            public {{atd.ReferencingRelationName}} Get{{atd.ReferencingNonForeignKeyAttribute.Name}}(IDbTransaction transaction =null)
                    {
                        return __DatabaseUnitOfWork != null ? {{atd.ReferencingNonForeignKeyAttribute.FieldName}}Obj ?? ({{atd.ReferencingNonForeignKeyAttribute.FieldName}}Obj = __DatabaseUnitOfWork.{{atd.ReferencingRelationName}}Repository().Get().Where.{{atd.ReferencingTableColumnName}}.EqualsTo({{atd.ReferencingNonForeignKeyAttribute.FieldName}}{{atd.Fix}}).Filter().Top(1).FirstOrDefault(transaction)) : null;
                    }
        
                    /// <summary>
		            /// Get {{atd.ReferencingRelationName}} object from {{atd.ReferencingNonForeignKeyAttribute.Name}} value asynchronously .<para />This object will be cache within this instance.
		            /// </summary>
		            public async Task<{{atd.ReferencingRelationName}}> Get{{atd.ReferencingNonForeignKeyAttribute.Name}}Async(IDbTransaction transaction =null)
                    {
                        return __DatabaseUnitOfWork != null ? {{atd.ReferencingNonForeignKeyAttribute.FieldName}}Obj ?? ({{atd.ReferencingNonForeignKeyAttribute.FieldName}}Obj = await __DatabaseUnitOfWork.{{atd.ReferencingRelationName}}Repository().Get().Where.{{atd.ReferencingTableColumnName}}.EqualsTo({{atd.ReferencingNonForeignKeyAttribute.FieldName}}{{atd.Fix}}).Filter().Top(1).FirstOrDefaultAsync(transaction)) : null;
                    }
{% endfor %}

{% for atd in relation.Attributes %}
{% if atd.HasComment %}
                    /// <summary>
		            /// {{atd.Comment}}
		            /// </summary>
{% endif %}
{% if atd.IsKey %}
		            [Key]
{% endif %}
{% if atd.IsIdentity %}
		            [Identity]
{% endif %}
		            public {{atd.DataType}} {{atd.Name}} { get { return {{atd.FieldName}}; } set { if({{atd.FieldName}} == value ) { return; }  {{atd.FieldName}} = value; __Updated(""{{atd.Name}}""); {{atd.RefPropNameNull}} } }
{% endfor %}

{% for atd in relation.ForeignKeyAttributes %}
                	/// <summary>
		            /// Set {{atd.ReferencingRelationName}} object for {{atd.ReferencingNonForeignKeyAttribute.Name}} value. <para />This will also change the {{atd.ReferencingNonForeignKeyAttribute.Name}} value.
		            /// </summary>
		            public void Set{{atd.ReferencingNonForeignKeyAttribute.Name}}({{atd.ReferencingRelationName}} entity)
                    {
                        if (entity == null)
                            return;
                        switch (entity.__DatabaseModelStatus)
                        {
                            case ModelStatus.Retrieved:
                                {{atd.ReferencingNonForeignKeyAttribute.Name}} = entity.{{atd.ReferencingTableColumnName}};
                                break;
                            case ModelStatus.ToAdd:
                                if (entity.__ActionsToRunWhenAdding == null)
                                    entity.__ActionsToRunWhenAdding = new List<IAddAction>();
                                entity.__ActionsToRunWhenAdding.Add(new AddAction(i => { {{atd.ReferencingNonForeignKeyAttribute.Name}} = (({{atd.ReferencingRelationName}}) i).{{atd.ReferencingTableColumnName}}; }, entity));
                                break;
                            case ModelStatus.JustInMemory:
	                        case ModelStatus.Deleted:
		                        break;
	                        default:
		                        break;
                        }
                    }
{% endfor %}

{% for list in relation.ReferenceLists %}
		            public IReferencedEntityCollection<{{list.ReferencedRelationName}}> {{list.ReferencedRelationNamePlural}}WhereThisIs{{list.ReferencedPropertyName}}(IDbTransaction transaction = null ){  return new ReferencedEntityCollection<{{list.ReferencedRelationName}}>(__DatabaseUnitOfWork.{{list.ReferencedRelationName}}Repository().Get().Where.{{list.ReferencedPropertyName}}.EqualsTo({{list.ReferencedPropertyOnThisRelation}}).Filter().Query(transaction), (i) => { (({{list.ReferencedRelationName}})i).{{list.ReferencedPropertyName}} = {{list.ReferencedPropertyOnThisRelation}};}, this); }
		            public async Task<IReferencedEntityCollection<{{list.ReferencedRelationName}}>> {{list.ReferencedRelationNamePlural}}WhereThisIs{{list.ReferencedPropertyName}}Async(IDbTransaction transaction = null ){  return new ReferencedEntityCollection<{{list.ReferencedRelationName}}>(await __DatabaseUnitOfWork.{{list.ReferencedRelationName}}Repository().Get().Where.{{list.ReferencedPropertyName}}.EqualsTo({{list.ReferencedPropertyOnThisRelation}}).Filter().QueryAsync(transaction), (i) => { (({{list.ReferencedRelationName}})i).{{list.ReferencedPropertyName}} = {{list.ReferencedPropertyOnThisRelation}}; }, this); }
{% endfor %}

                    public override void SetId(object id)
                    {
{% for k in identities %}
    {% if atd.DataType == 'string' %}
                        {{k.FieldName}} = id as string;
    {% else if atd.DataType contains 'int' %}
                        {{k.FieldName}} = Convert.ToInt32(id);
    {% else if atd.DataType contains 'double' %}
                        {{k.FieldName}} = Convert.ToDouble(id);
    {% else if atd.DataType contains 'decimal' %}
                        {{k.FieldName}} = Convert.ToDecimal(id);
    {% else if atd.DataType contains 'decimal' %}
                        {{k.FieldName}} = Convert.ToDecimal(id);
    {% else if atd.DataType contains 'long' %}
                        {{k.FieldName}} = Convert.ToInt64(id);
    {% else if atd.DataType contains 'DateTime'%}
		                {{k.FieldName}} = Convert.ToDateTime(id);
    {% else if atd.DataType contains 'bool'%}
		                {{k.FieldName}} = Convert.ToBoolean(id);
    {% endif %}
{% endfor %}
                    }
                }
";

            var quote = FormatHelper.GetDbmsSpecificQuoter(_configuration);
            var name = _relation.Name;

            return Process(nameof(RelationTemplate), template, new
            {
                hasEnum = _enum != null && _enum.Values.Count > 0,
                enam = _enum,
                quotedSchema = quote(_configuration.Schema),
                quotedName = quote(name),
                name,
                val = "dsadas",
                absImplement = _configuration.AbstractModelsEnabled ? $", I{name}" : "",
                abstractModelsEnabled = _configuration.AbstractModelsEnabled,
                relation =_relation,
                identities = _relation.Attributes.Where(e => e.IsIdentity).ToList()
            });
        }
    }
}