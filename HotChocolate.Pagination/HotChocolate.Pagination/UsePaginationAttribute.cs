using HotChocolate.Types;
using HotChocolate.Types.Descriptors;
using HotChocolate.Utilities;
using System;
using System.Linq;
using System.Reflection;

#nullable enable

namespace HotChocolate.Pagination
{
    public class UsePaginationAttribute : DescriptorAttribute
    {
        private static readonly MethodInfo _off = typeof(PaginationObjectFieldDescriptorExtensions)
      .GetMethods(BindingFlags.Public | BindingFlags.Static)
      .Single(m => m.Name.Equals(
          nameof(PaginationObjectFieldDescriptorExtensions.UsePaging),
          StringComparison.Ordinal)
          && m.GetGenericArguments().Length == 1
          && m.GetParameters().Length == 1
          && m.GetParameters()[0].ParameterType == typeof(IObjectFieldDescriptor));

        private static readonly MethodInfo _iff = typeof(PaginationObjectFieldDescriptorExtensions)
            .GetMethods(BindingFlags.Public | BindingFlags.Static)
            .Single(m => m.Name.Equals(
                nameof(PaginationObjectFieldDescriptorExtensions.UsePaging),
                StringComparison.Ordinal)
                && m.GetGenericArguments().Length == 1
                && m.GetParameters().Length == 1
                && m.GetParameters()[0].ParameterType == typeof(IInterfaceFieldDescriptor));

        public Type? SchemaType { get; set; }

        protected override void TryConfigure(
            IDescriptorContext context,
            IDescriptor descriptor,
            ICustomAttributeProvider element)
        {
            if (element is MemberInfo m)
            {
                Type schemaType = GetSchemaType(context, m);
                if (descriptor is IObjectFieldDescriptor ofd)
                {
                    _off.MakeGenericMethod(schemaType).Invoke(null, new[] { ofd });
                }
                else if (descriptor is IInterfaceFieldDescriptor ifd)
                {
                    _iff.MakeGenericMethod(schemaType).Invoke(null, new[] { ifd });
                }
            }
        }

        private Type GetSchemaType(
            IDescriptorContext context,
            MemberInfo member)
        {
            Type? type = SchemaType;

            ITypeReference returnType = context.Inspector.GetReturnType(
                member, TypeContext.Output);

            if (type is null
                && returnType is ClrTypeReference clr
                && TypeInspector.Default.TryCreate(clr.Type, out var typeInfo))
            {
                if (BaseTypes.IsSchemaType(typeInfo.ClrType))
                {
                    type = typeInfo.ClrType;
                }
            }

            if (type is null || !typeof(IType).IsAssignableFrom(type))
            {
                throw new SchemaException(
                    SchemaErrorBuilder.New()
                        .SetMessage("The UsePaging attribute needs a valid node schema type.")
                        .SetCode("ATTR_USEPAGING_SCHEMATYPE_INVALID")
                        .Build());
            }

            return type;
        }
    }
}