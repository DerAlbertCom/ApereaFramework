using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Aperea.Mappings
{
    public static class GenericMapperExtensions
    {
        public static TResult MapTo<TResult>(this object value)
        {
            return Mapper.Map<TResult>(value);
        }

        public static IProjectionExpression Project<TSource>(this IQueryable<TSource> source)
        {
            return AutoMapper.QueryableExtensions.Extensions.Project(source);
        }

        public static IEnumerable<TResult> ToList<TResult>(this IProjectionExpression projectionExpression)
        {
            return projectionExpression.To<TResult>().ToList();
        }

        public static TResult ToSingle<TResult>(this IProjectionExpression projectionExpression)
        {
            return projectionExpression.To<TResult>().Single();
        }


        public static TResult ToSingleOrDefault<TResult>(this IProjectionExpression projectionExpression)
        {
            return projectionExpression.To<TResult>().SingleOrDefault();
        }
    }
}