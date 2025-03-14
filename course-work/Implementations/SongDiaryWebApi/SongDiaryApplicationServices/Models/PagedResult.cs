using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongDiaryApplicationServices.Models
{
    /// <summary>
    /// Paginated result set
    /// </summary>
    /// <typeparam name="T">The type of the items in the paginated list</typeparam>
    public class PagedResult<T>
    {
        /// <summary>
        /// The list of items in the current page
        /// </summary>
        public List<T> Items { get; set; } = new List<T>();

        /// <summary>
        /// The total number of items in all pages
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// The current page number
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// The number of items per page
        /// </summary>
        public int PageSize { get; set; }
    }
}
