using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSM.Application.Common.Models
{

    public class PaginationFilter : BaseFilter
    {
        private int _pageNumber;

        private int _pageSize;

        public int PageNumber
        {
            get
            {
                if (_pageNumber <= 0)
                {
                    return 1;
                }

                return _pageNumber;
            }
            set
            {
                _pageNumber = value;
            }
        }

        public int PageSize
        {
            get
            {
                if (_pageSize <= 0)
                {
                    return 10;
                }

                return _pageSize;
            }
            set
            {
                _pageSize = value == 0 ? int.MaxValue : value;
            }
        }

        public string[]? OrderBy { get; set; }
    }

    public static class PaginationFilterExtensions
    {
        public static bool HasOrderBy(this PaginationFilter filter) =>
            filter.OrderBy?.Any() is true;
    }
}
