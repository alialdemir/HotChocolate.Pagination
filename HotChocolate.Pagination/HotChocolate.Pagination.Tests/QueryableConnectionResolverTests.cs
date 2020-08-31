using HotChocolate.Pagination.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HotChocolate.Pagination.Tests
{
    public class QueryableConnectionResolvers
    {
        [Test]
        public async Task Limit()
        {
            // arrange
            var list = new List<string> { "a", "b", "c", "d", "e", "f", "g", };

            var pagingDetails = new PaginationDetails
            {
                Limit = 2,
            };

            var connectionFactory = new QueryableConnectionResolver<string>(list.AsQueryable(), pagingDetails);

            // act
            Connection<string> connection = await connectionFactory.ResolveAsync(CancellationToken.None);

            // assert
            Assert.True(
                connection.PageInfo.Limit == 2,
                "Limit");
        }

        [Test]
        public async Task TotalCount()
        {
            // arrange
            var list = new List<string> { "a", "b", "c", "d", };

            var pagingDetails = new PaginationDetails();

            var connectionFactory = new QueryableConnectionResolver<string>(list.AsQueryable(), pagingDetails);

            // act
            Connection<string> connection = await connectionFactory.ResolveAsync(CancellationToken.None);

            // assert
            Assert.True(
                connection.PageInfo.TotalCount == 4,
                "TotalCount");
        }

        [Test]
        public async Task PageNumber()
        {
            // arrange
            var list = new List<string> { "a", "b", "c", "d", };

            var pagingDetails = new PaginationDetails
            {
                PageNumber = 123
            };

            var connectionFactory = new QueryableConnectionResolver<string>(list.AsQueryable(), pagingDetails);

            // act
            Connection<string> connection = await connectionFactory.ResolveAsync(CancellationToken.None);

            // assert
            Assert.True(
                connection.PageInfo.PageNumber == 123,
                "PageNumber");
        }

        [Test]
        public async Task HasNextPage_True()
        {
            // arrange
            var list = new List<string> { "a", "b", "c", "d", };

            var pagingDetails = new PaginationDetails
            {
                PageNumber = 2,
                Limit = 1
            };

            var connectionFactory = new QueryableConnectionResolver<string>(list.AsQueryable(), pagingDetails);

            // act
            Connection<string> connection = await connectionFactory.ResolveAsync(CancellationToken.None);

            // assert
            Assert.True(
                connection.PageInfo.HasNextPage,
                "HasNextPage_True");
        }

        [Test]
        public async Task HasNextPage_False()
        {
            // arrange
            var list = new List<string> { "a", "b", "c", "d", };

            var pagingDetails = new PaginationDetails
            {
                PageNumber = 2,
                Limit = 10
            };

            var connectionFactory = new QueryableConnectionResolver<string>(list.AsQueryable(), pagingDetails);

            // act
            Connection<string> connection = await connectionFactory.ResolveAsync(CancellationToken.None);

            // assert
            Assert.False(
                connection.PageInfo.HasNextPage,
                "HasNextPage_False");
        }

        [Test]
        public async Task HasPreviousPage_True()
        {
            // arrange
            var list = new List<string> { "a", "b", "c", "d", };

            var pagingDetails = new PaginationDetails
            {
                PageNumber = 2,
                Limit = 1
            };

            var connectionFactory = new QueryableConnectionResolver<string>(list.AsQueryable(), pagingDetails);

            // act
            Connection<string> connection = await connectionFactory.ResolveAsync(CancellationToken.None);

            // assert
            Assert.True(
                connection.PageInfo.HasPreviousPage,
                "HasPreviousPage_True");
        }

        [Test]
        public async Task HasPreviousPage_False()
        {
            // arrange
            var list = new List<string> { "a", "b", "c", "d", };

            var pagingDetails = new PaginationDetails
            {
                PageNumber = 1,
            };

            var connectionFactory = new QueryableConnectionResolver<string>(list.AsQueryable(), pagingDetails);

            // act
            Connection<string> connection = await connectionFactory.ResolveAsync(CancellationToken.None);

            // assert
            Assert.False(
                connection.PageInfo.HasPreviousPage,
                "HasPreviousPage_False");
        }

        [Test]
        public async Task Check_PaginationDetails_Default_Value()
        {
            // arrange
            var list = new List<string> { "a", "b", "c", "d", };

            var pagingDetails = new PaginationDetails();

            var connectionFactory = new QueryableConnectionResolver<string>(list.AsQueryable(), pagingDetails);

            // act
            Connection<string> connection = await connectionFactory.ResolveAsync(CancellationToken.None);

            // assert
            Assert.AreEqual(connection.PageInfo.Limit, 10);

            Assert.AreEqual(connection.PageInfo.PageNumber, 1);

            Assert.False(connection.PageInfo.HasNextPage);

            Assert.False(connection.PageInfo.HasPreviousPage);

            Assert.AreEqual(connection.PageInfo.TotalCount, list.Count);
        }

        [Test]
        public async Task Check_Items()
        {
            // arrange
            var list = new List<string> { "a", "b", "c", "d", };

            var pagingDetails = new PaginationDetails
            {
                Limit = 10
            };

            var connectionFactory = new QueryableConnectionResolver<string>(
                list.AsQueryable(), pagingDetails);

            // act
            Connection<string> connection = await connectionFactory.ResolveAsync(CancellationToken.None);

            // assert
            Assert.AreEqual(connection.Edges[0].Node,
                            list[0],
                            "Check_Items[0]");

            Assert.AreEqual(connection.Edges[1].Node,
                            list[1],
                            "Check_Items[1]");

            Assert.AreEqual(connection.Edges[2].Node,
                            list[2],
                            "Check_Items[2]");

            Assert.AreEqual(connection.Edges[3].Node,
                            list[3],
                            "Check_Items[3]");
        }
    }
}