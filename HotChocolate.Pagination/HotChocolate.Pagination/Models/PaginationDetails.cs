using Newtonsoft.Json;
using System.Collections.Generic;

namespace HotChocolate.Pagination.Models
{
    public class PaginationDetails
    {
        private int? _pageNumber = 1;

        private int? _limit = 10;

        [JsonIgnore]
        public int Offset
        {
            get
            {
                if (!PageNumber.HasValue || !Limit.HasValue)
                    return 0;

                return Limit.Value * (PageNumber.Value - 1);
            }
        }

        [JsonIgnore]
        public IDictionary<string, object> Properties { get; set; }

        /// <summary>
        /// Page number
        /// </summary>
        public int? PageNumber
        {
            get { return _pageNumber; }
            set { if (value > 0) _pageNumber = value; }
        }

        /// <summary>
        /// Number of records shown per page default 10
        /// </summary>
        public int? Limit
        {
            get { return _limit; }
            set { if (value > 0) _limit = value; }
        }
    }
}