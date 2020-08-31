using HotChocolate.Pagination.Abstract;
using HotChocolate.Types;

namespace HotChocolate.Pagination.Types
{
    public class PageInfoType
        : ObjectType<IPageInfo>
    {
        protected override void Configure(IObjectTypeDescriptor<IPageInfo> descriptor)
        {
            descriptor.Description("Information about pagination in a connection.");

            descriptor.BindFields(BindingBehavior.Explicit);

            descriptor.Field(t => t.Limit)
                .Type<NonNullType<IntType>>()
                .Name("limit")
                .Description(
                    "Indicates whether more edges exist following the set defined by the clients arguments.");

            descriptor.Field(t => t.PageNumber)
                .Type<NonNullType<IntType>>()
                .Name("pageNumber")
                .Description("Indicates whether more edges exist prior the set defined by the clients arguments.");

            descriptor.Field(t => t.TotalCount)
                .Type<LongType>()
                .Name("totalCount")
                .Description("Total number of records.");

            descriptor.Field(t => t.HasNextPage)
                .Type<BooleanType>()
                .Name("hasNextPage")
                .Description("Indicates whether more edges exist following the set defined by the clients arguments.");
        }
    }
}