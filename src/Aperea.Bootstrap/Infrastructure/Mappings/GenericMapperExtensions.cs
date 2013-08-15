using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Aperea.Infrastructure.Mappings
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public static class GenericMapperExtensions
    {
        public static TDestination MapTo<TDestination>(this object source) where TDestination : class
        {
            return Mapper.Map<TDestination>(source);
        }

        public static void MapTo<TSource, TDestination>(this TSource source, TDestination destination) where TDestination : class
        {
            Mapper.Map(source, destination);
        }

        public static IEnumerable<TDestination> MapTo<TDestination>(this IEnumerable<object> source) where TDestination : class
        {
            return Mapper.Map<IEnumerable<TDestination>>(source);
        }

        public static IProjectionExpression Project<TDestination>(this IQueryable<TDestination> source) where TDestination : class
        {
            return AutoMapper.QueryableExtensions.Extensions.Project(source);
        }

        public static IEnumerable<TDestination> ToEnumerable<TDestination>(this IProjectionExpression projectionExpression) where TDestination : class
        {
            return projectionExpression.To<TDestination>().ToList();
        }

        public static IList<TDestination> ToList<TDestination>(this IProjectionExpression projectionExpression) where TDestination : class
        {
            return projectionExpression.To<TDestination>().ToList();
        }

        public static TDestination[] ToArray<TDestination>(this IProjectionExpression projectionExpression) where TDestination : class
        {
            return projectionExpression.To<TDestination>().ToArray();
        }

        public static TDestination ToSingle<TDestination>(this IProjectionExpression projectionExpression) where TDestination : class
        {
            return projectionExpression.To<TDestination>().Single();
        }

        public static TDestination ToSingleOrDefault<TDestination>(this IProjectionExpression projectionExpression) where TDestination : class
        {
            return projectionExpression.To<TDestination>().SingleOrDefault();
        }
    }
}