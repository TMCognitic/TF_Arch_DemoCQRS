using Tools.Cqrs.Command;
using Tools.Cqrs.Queries;

namespace Tools.Cqrs
{
    public class Dispatcher : IDispatcher
    {
        private readonly IServiceProvider _container;

        public Dispatcher(IServiceProvider container)
        {
            _container = container;
        }

        public Result Dispatch(ICommand command)
        {
            Type type = typeof(ICommandHandler<>);
            Type[] typeArgs = { command.GetType() };
            Type handlerType = type.MakeGenericType(typeArgs);

            dynamic handler = _container.GetService(handlerType)!;
            Result result = handler.Execute((dynamic)command);

            return result;
        }

        public T Dispatch<T>(IQuery<T> query)
        {
            Type type = typeof(IQueryHandler<,>);
            Type[] typeArgs = { query.GetType(), typeof(T) };
            Type handlerType = type.MakeGenericType(typeArgs);

            dynamic handler = _container.GetService(handlerType)!;
            T result = handler.Execute((dynamic)query);

            return result;
        }
    }
}
