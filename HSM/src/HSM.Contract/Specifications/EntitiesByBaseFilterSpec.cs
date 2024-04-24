using Ardalis.Specification;
using HSM.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSM.Application.Specifications
{

    public class EntitiesByBaseFilterSpec<T, TResult> : Specification<T, TResult>
    {
        protected EntitiesByBaseFilterSpec(BaseFilter filter) =>
            Query.SearchBy(filter);
    }

    public class EntitiesByBaseFilterSpec<T> : Specification<T>
    {
        protected EntitiesByBaseFilterSpec(BaseFilter filter) =>
            Query.SearchBy(filter);
    }
}
