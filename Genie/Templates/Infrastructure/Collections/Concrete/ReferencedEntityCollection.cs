#region Usings

#endregion

using Genie.Core.Base.Generating;

namespace Genie.Core.Templates.Infrastructure.Collections.Concrete
{
    public class ReferencedEntityCollectionTemplate : GenieTemplate
    {
        public ReferencedEntityCollectionTemplate(string path) : base(path)
        {
        }

        public override string Generate()
        {
            L($@"

using System;
using System.Collections.Generic;
using {GenerationContext.BaseNamespace}.Infrastructure.Models.Concrete;
using {GenerationContext.BaseNamespace}.Infrastructure.Collections.Abstract;
using System.Collections;
using System.Linq;
using {GenerationContext.BaseNamespace}.Infrastructure.Actions.Abstract;
using {GenerationContext.BaseNamespace}.Infrastructure.Actions.Concrete;


namespace {GenerationContext.BaseNamespace}.Infrastructure.Collections.Concrete
{{
    internal class ReferencedEntityCollection<T> : IReferencedEntityCollection<T> where T: BaseModel
	{{
		private readonly List<T> _collection;
		private readonly Action<object> _addAction;
        private readonly BaseModel _creator;

		internal ReferencedEntityCollection(IEnumerable<T> collection, Action<object> addAction, BaseModel creator)
		{{
			_collection = collection.ToList();
			_addAction = addAction;
            _creator = creator;
		}}

		public void Add(T entityToAdd) 
		{{
            if (entityToAdd == null)
                return;
            switch (_creator.DatabaseModelStatus)
            {{
                case ModelStatus.Retrieved:
                    _addAction(entityToAdd);
                    break;
                case ModelStatus.ToAdd:
                    if(_creator.ActionsToRunWhenAdding == null)
                        _creator.ActionsToRunWhenAdding = new List<IAddAction>();
                    _creator.ActionsToRunWhenAdding.Add(new AddAction(_addAction, entityToAdd));
                    break;
            }}    
			_collection.Add(entityToAdd);
		}}

        public IEnumerator<T> GetEnumerator()
        {{
            return _collection.GetEnumerator();
        }}

        IEnumerator IEnumerable.GetEnumerator()
        {{
            return _collection.GetEnumerator();
        }}
    }}
}} 

");

            return E();
        }
    }
}