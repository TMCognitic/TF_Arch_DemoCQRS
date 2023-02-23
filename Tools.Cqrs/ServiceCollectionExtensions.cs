using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Tools.Cqrs.Command;
using Tools.Cqrs.Queries;

namespace Tools.Cqrs
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            Assembly mainAssembly = Assembly.GetEntryAssembly()!;
            IEnumerable<Type> handlers = mainAssembly.GetTypes()
                .Union(mainAssembly.GetReferencedAssemblies().SelectMany(an => Assembly.Load(an).GetTypes()))
                .Where(t => t.GetInterfaces().Any(i => IsHandler(i)) && t.Name.EndsWith("Handler"));

            foreach (Type type in handlers)
            {
                Type interfaceType = type.GetInterfaces().Single(i => IsHandler(i));
                services.AddScoped(interfaceType, type);
            }

            return services;
        }

        private static bool IsHandler(Type type)
        {
            if (!type.IsGenericType)
                return false;

            Type[] cqrsHandlers = new[] { typeof(ICommandHandler<>), typeof(IQueryHandler<,>) };

            Type typeDefinition = type.GetGenericTypeDefinition();
            return cqrsHandlers.Contains(typeDefinition);
        }
    }
}
