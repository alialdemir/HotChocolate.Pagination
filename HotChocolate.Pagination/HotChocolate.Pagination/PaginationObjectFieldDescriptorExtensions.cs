using HotChocolate.Resolvers;
using HotChocolate.Types;
using System;

namespace HotChocolate.Pagination
{
    public static class PaginationObjectFieldDescriptorExtensions
    {
        public static IObjectFieldDescriptor UsePaging<TSchemaType, TClrType>(
             this IObjectFieldDescriptor descriptor)
             where TSchemaType : class, IOutputType
        {
            return descriptor
                .AddPagingArguments()

                .Type<PaginationType<TSchemaType>>()
                .Use<QueryableConnectionMiddleware<TClrType>>();
        }

        public static IObjectFieldDescriptor UsePaging<TSchemaType>(
          this IObjectFieldDescriptor descriptor)
          where TSchemaType : class, IOutputType
        {
            FieldMiddleware placeholder = next => default(FieldDelegate);
            Type middlewareDefinition = typeof(QueryableConnectionMiddleware<>);

            descriptor
                .AddPagingArguments()
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

        public static IInterfaceFieldDescriptor UsePaging<TSchemaType>(
            this IInterfaceFieldDescriptor descriptor)
            where TSchemaType : class, IOutputType
        {
            descriptor
                .AddPagingArguments()
                .Type<PaginationType<TSchemaType>>();

            return descriptor;
        }

        public static IObjectFieldDescriptor AddPagingArguments(
            this IObjectFieldDescriptor descriptor)
        {
            return descriptor
                .Argument("pageNumber", a => a.Type<IntType>())
                .Argument("limit", a => a.Type<IntType>());
        }

        public static IInterfaceFieldDescriptor AddPagingArguments(
            this IInterfaceFieldDescriptor descriptor)
        {
            return descriptor
                .Argument("pageNumber", a => a.Type<IntType>())
                .Argument("limit", a => a.Type<IntType>());
        }
    }
}