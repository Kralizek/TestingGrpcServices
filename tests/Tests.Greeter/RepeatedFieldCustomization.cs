using System;
using System.Collections.Generic;
using AutoFixture.Kernel;
using Google.Protobuf.Collections;

namespace Tests.Greeter
{
    public class RepeatedFieldCustomization : ISpecimenBuilder
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

            if (request is not Type { IsGenericType: true } requestType || requestType.GetGenericTypeDefinition() != typeof(RepeatedField<>))
            {
                return new NoSpecimen();
            }

            var argumentType = requestType.GenericTypeArguments[0];

            var listType = typeof(List<>).MakeGenericType(argumentType);

            var list = context.Resolve(listType);

            var repeatedFieldType = typeof(RepeatedField<>).MakeGenericType(argumentType);

            var repeatedField = Activator.CreateInstance(repeatedFieldType);

            var method = repeatedFieldType.GetMethod("AddRange");

            method?.Invoke(repeatedField, new[] { list });

            return repeatedField;
        }
    }
}
