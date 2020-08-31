using System;
using System.Collections.Generic;
using System.Linq;

namespace HotChocolate.Pagination.Models
{
    internal class PageableData<T>
    {
        public PageableData(IEnumerable<T> source)
            : this(source, null)
        {
        }

        public PageableData(
            IEnumerable<T> source,
            IDictionary<string, object> properties)
            : this(source?.AsQueryable(), properties)
        {
        }

        public PageableData(IQueryable<T> source)
            : this(source, null)
        {
        }

        public PageableData(
            IQueryable<T> source,
            IDictionary<string, object> properties)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
            Properties = properties;
        }

        /// <summary>
        /// Source
        /// </summary>
        public IQueryable<T> Source { get; }

        /// <summary>
        /// Properties
        /// </summary>
        public IDictionary<string, object> Properties { get; }
    }
}