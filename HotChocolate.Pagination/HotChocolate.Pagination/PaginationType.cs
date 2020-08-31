using System;
using System.Linq;
using HotChocolate.Types;

namespace HotChocolate.Pagination
{
    public class PaginationType<T> : ObjectType<Abstract.IConnection> where T : class, IOutputType
    {
        public PaginationType() : base(descriptor => Configure(descriptor)) { }

        public PaginationType(
            Action<IObjectTypeDescriptor<Abstract.IConnection>> configure) : base(descriptor =>
            {
                Configure(descriptor);
                configure?.Invoke(descriptor);
            })
        { }

        protected new static void Configure(
            IObjectTypeDescriptor<Abstract.IConnection> descriptor)
        {
            descriptor.Name(dependency => dependency.Name + "Connection")
                .DependsOn<T>();

            descriptor.Description("A connection to a list of items.");

            descriptor.BindFields(BindingBehavior.Explicit);

            descriptor.Field(x => x.PageInfo)
                .Description("Information to aid in pagination.")
                .Type<PageInfoType>()
                .Resolver(ctx =>
                   ctx.Parent<Abstract.IConnection>().PageInfo);

            descriptor.Field("nodes")
                .Description("A flattened list of the items.")
                .Type<ListType<T>>()
                .Resolver(ctx =>
                   ctx.Parent<Abstract.IConnection>().Edges.Select(t => t.Node));
        }
    }
}