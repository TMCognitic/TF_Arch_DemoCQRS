using Tools.Cqrs.Command;
using Tools.Cqrs.Queries;

namespace Tools.Cqrs
{
    public interface IDispatcher
    {
        Result Dispatch(ICommand command);
        T Dispatch<T>(IQuery<T> query);
    }
}