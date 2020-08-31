using HotChocolate.Pagination.Types;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using System;

namespace HotChocolate.Pagination
{
    public static class PaginationObjectFieldDescriptorExtensions
    {
        public static IObjectFieldDescriptor UsePagination<TSchemaType, TClrType>(this IObjectFieldDescriptor descriptor) where TSchemaType : class, IOutputType
        {
            return descriptor
                .AddPaginationArguments()
                .Type<PaginationType<TSchemaType>>()
                .Use<QueryableConnectionMiddleware<TClrType>>();
        }

        public static IObjectFieldDescriptor UsePagination<TSchemaType>(this IObjectFieldDescriptor descriptor) where TSchemaType : class, IOutputType
        {
            FieldMiddleware placeholder = next => default(FieldDelegate);
            Type middlewareDefinition = typeof(QueryableConnectionMiddleware<>);

            descriptor
                .AddPaginationArguments()
                .Type<PaginationType<TSchemaType>>()
                .Use(placeholder)
                .Extend()
                .OnBeforeCompletion((context, defintion) =>
                {
                    var reference = typeof(TSchemaType)?.BaseType?.GenericTypeArguments?[0];
                    if (reference != null)
                    {
                        Type middlewareType = middlewareDefinition.MakeGenericType(reference);
                        FieldMiddleware middleware = FieldClassMiddlewareFactory.Create(
                            middlewareType);
                        int index = defintion.MiddlewareComponents.IndexOf(placeholder);
                        defintion.MiddlewareComponents[index] = middleware;
                    }
                })
                .DependsOn<TSchemaType>();

            return descriptor;
        }

        public static IInterfaceFieldDescriptor UsePagination<TSchemaType>(this IInterfaceFieldDescriptor descriptor) where TSchemaType : class, IOutputType
        {
            descriptor
                .AddPaginationArguments()
                .Type<PaginationType<TSchemaType>>();

            return descriptor;
        }

        public static IObjectFieldDescriptor AddPaginationArguments(this IObjectFieldDescriptor descriptor)
        {
            return descriptor
                .Argument("pageNumber", a => a.Type<IntType>())
                .Argument("limit", a => a.Type<IntType>());
        }

        public static IInterfaceFieldDescriptor AddPaginationArguments(this IInterfaceFieldDescriptor descriptor)
        {
            return descriptor
                .Argument("pageNumber", a => a.Type<IntType>())
                .Argument("limit", a => a.Type<IntType>());
        }
    }
}