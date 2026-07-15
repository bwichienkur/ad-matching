using EDDY.IS.AdMatching.Domain.ChainResponsability;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EDDY.IS.AdMatching.Core.ChainResponsability
{
    public static class ChainConfiguratorExtension
    {
        public static IChainConfigurator<T> Chain<T>(this IServiceCollection services) where T : class
        {
            return new ChainConfigurator<T>(services);
        }
        private class ChainConfigurator<T> : IChainConfigurator<T> where T : class
        {
            private readonly IServiceCollection _services;
            private List<Type> _types;
            private Type _interfaceType;

            public ChainConfigurator(IServiceCollection services)
            {
                _services = services;
                _types = new List<Type>();
                _interfaceType = typeof(T);
            }

            public IChainConfigurator<T> Add<TImplementation>() where TImplementation : T
            {
                var type = typeof(TImplementation);

                _types.Add(type);

                return this;
            }

            public void Configure()
            {
                if (_types.Count == 0)
                    throw new InvalidOperationException($"No implementation register for {_interfaceType.Name}");

                foreach (var type in _types)
                {
                    ConfigureType(type);
                }
            }

            private void ConfigureType(Type currentType)
            {
                var nextType = _types.SkipWhile(x => x != currentType).SkipWhile(x => x == currentType).FirstOrDefault();
                var parameter = Expression.Parameter(typeof(IServiceProvider), "x");


                var constructor = currentType.GetConstructors().OrderByDescending(x => x.GetParameters().Count()).First();
                var costructorParameters = constructor.GetParameters().Select(p =>
                {
                    if (_interfaceType.IsAssignableFrom(p.ParameterType))
                    {
                        if (nextType is null)
                            return Expression.Constant(null, _interfaceType);
                        else
                            return Expression.Call(typeof(ServiceProviderServiceExtensions), "GetRequiredService", new Type[] { nextType }, parameter);
                    }
                    return (Expression)Expression.Call(typeof(ServiceProviderServiceExtensions), "GetRequiredService", new Type[] { p.ParameterType }, parameter);
                });

                var body = Expression.New(constructor, costructorParameters.ToArray());

                var first = _types[0] == currentType;
                var resolveType = first ? _interfaceType : currentType;
                var expressionType = Expression.GetFuncType(typeof(IServiceProvider), resolveType);

                var expression = Expression.Lambda(expressionType, body, parameter);
                var compiledExpression = (Func<IServiceProvider, object>)expression.Compile();

                _services.AddScoped(resolveType, compiledExpression);
            }
        }
    }
}
