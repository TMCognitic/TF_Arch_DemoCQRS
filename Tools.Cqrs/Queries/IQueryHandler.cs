using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Cqrs.Queries
{
    public interface IQueryHandler<TQuery, TResult>
        where TQuery : class, IQuery
    {
        TResult Execute(TQuery query);
    }
}
