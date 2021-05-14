using System;
using System.Collections.Generic;
using AutoFixture.Kernel;
using Google.Protobuf.Collections;

namespace Tests.Greeter
{
    public class MapFieldCustomization : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (request is not Type { IsGenericType: true } requestType)
            {
                return new NoSpecimen();
            }

            if (requestType.GetGenericTypeDefinition() == typeof(MapField<,>))
            {
                var keyType = requestType.GenericTypeArguments[0];

                var valueType = requestType.GenericTypeArguments[1];

                var dictionaryType = typeof(IDictionary<,>).MakeGenericType(keyType, valueType);

                var dictionary = context.Resolve(dictionaryType);

                var mapFieldType = typeof(MapField<,>).MakeGenericType(keyType, valueType);

                var mapField = Activator.CreateInstance(mapFieldType);

                var method = mapFieldType.GetMethod("Add", new[] { typeof(IDictionary<,>).MakeGenericType(keyType, valueType) });

                method?.Invoke(mapField, new[] { dictionary });

                return mapField;
            }

            return new NoSpecimen();
        }
    }
}
