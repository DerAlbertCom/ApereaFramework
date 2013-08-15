using System;
using System.Linq.Expressions;
using AutoMapper;

namespace Aperea.Infrastructure.Mappings
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public static class AutoMapperConfigurationExtensions
    {
        public static IMappingExpression<TSource, TDestination> IgnoreMember<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> expression,
            Expression<Func<TDestination, object>> destinationMember)
        {
            return expression.ForMember(destinationMember, c => c.Ignore());
        }
    }
}