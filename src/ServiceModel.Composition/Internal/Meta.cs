namespace ServiceModel.Composition.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    internal class Meta<T>
        where T : new()
    {
        private const string ContractTypeIdentity = "ContractTypeIdentity";
        private const string ExportTypeIdentity = System.ComponentModel.Composition.Hosting.CompositionConstants.ExportTypeIdentityMetadataName;
        private static Func<Dictionary<string, object>, T> _processMetadataItem;

        private IEnumerable<T> _metadata;
        private IDictionary<string, object> _metadataDictionary;

        public Meta(IDictionary<string, object> metadataDictionary)
        {
            _metadataDictionary = metadataDictionary;
        }

        public IEnumerable<T> View
        {
            get
            {
                if (_metadata == null)
                {
                    ProcessMetadata();
                }

                return _metadata;
            }
        }

        private static T ProcessMetadataItem(Dictionary<string, object> metadataItem)
        {
            if (_processMetadataItem == null)
            {
                List<Expression> assignemns = new List<Expression>();

                var instanceType = typeof(T);

                var dictParameter = Expression.Parameter(typeof(Dictionary<string, object>), "dictionary");

                // T instance;
                var resultVariable = Expression.Variable(instanceType, "instance");

                // A label expression of the <T> type that is the target for Expression.Return(target, value, type).
                LabelTarget returnTarget = Expression.Label(instanceType);

                // instance = new T()
                assignemns.Add(Expression.Assign(resultVariable, Expression.New(instanceType)));
                foreach (var item in metadataItem)
                {
                    var propertyInfo = instanceType.GetProperty(item.Key);

                    // dictionary["Key"]
                    var getValue = Expression.Call(dictParameter, dictParameter.Type.GetMethod("get_Item"), Expression.Constant(item.Key, typeof(string)));

                    // instance.Key = (propertyType) dictionary["Key"];
                    var assignment = Expression.Assign(
                       Expression.Property(resultVariable, propertyInfo),
                       Expression.Convert(getValue, propertyInfo.PropertyType));
                    assignemns.Add(assignment);
                }

                // return instance;
                assignemns.Add(Expression.Return(returnTarget, resultVariable, instanceType));
                assignemns.Add(Expression.Label(returnTarget, resultVariable));
                var block = Expression.Block(instanceType, new[] { resultVariable }, assignemns);

                var lambdaExpresion = Expression.Lambda<Func<Dictionary<string, object>, T>>(block, dictParameter);
                _processMetadataItem = lambdaExpresion.Compile();
            }

            return _processMetadataItem(metadataItem);
        }

        private void ProcessMetadata()
        {
            var metadata = new List<T>();

            if (_metadataDictionary[ExportTypeIdentity] == _metadataDictionary[ContractTypeIdentity])
            {
                var metadataItem = new Dictionary<string, object>(_metadataDictionary);
                metadataItem.Remove(ExportTypeIdentity);
                metadata.Add(ProcessMetadataItem(metadataItem));
            }
            else
            {
                var exportTypeIdentity = (string)_metadataDictionary[ExportTypeIdentity];
                var conctractTypeIdentities = (string[])_metadataDictionary[ContractTypeIdentity];

                var keys = _metadataDictionary.Keys
                    .Where(x => x != ExportTypeIdentity);
                var values = new Dictionary<string, object[]>();
                foreach (var key in keys)
                {
                    values[key] = (object[])_metadataDictionary[key];
                }

                for (var i = 0; i < conctractTypeIdentities.Length; i++)
                {
                    if (exportTypeIdentity == conctractTypeIdentities[i])
                    {
                        var metadataItem = keys.ToDictionary(x => x, x => values[x][i]);
                        metadata.Add(ProcessMetadataItem(metadataItem));
                    }
                }
            }

            _metadata = metadata;
        }
    }
}