using HSM.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSM.Application.Specifications
{

    public class EntitiesByPaginationFilterSpec<T, TResult> : EntitiesByBaseFilterSpec<T, TResult>
    {
        protected EntitiesByPaginationFilterSpec(PaginationFilter filter)
            : base(filter)
        {
            if (!filter.HasOrderBy() && typeof(T).GetProperty("CreatedOn") != null)
            {
                filter.OrderBy ??= ["CreatedOn desc"];
            }

            Query.PaginateBy(filter);
        }
    }

    public class EntitiesByPaginationFilterSpec<T> : EntitiesByBaseFilterSpec<T>
    {
        public EntitiesByPaginationFilterSpec(PaginationFilter filter)
            : base(filter) =>
            Query.PaginateBy(filter);
    }
}
