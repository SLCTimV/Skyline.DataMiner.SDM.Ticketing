// Ignore Spelling: SDM

namespace Skyline.DataMiner.SDM.Ticketing
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Skyline.DataMiner.Net.Messages.SLDataGateway;

    using SLDataGateway.API.Querying;
    using SLDataGateway.API.Types.Querying;

    public static class QueryExtensions
    {
        public static IQuery<T> Limit<T>(this IQuery<T> query, int limit) where T : class
        {
            if (!query.Limit.Equals((object)LimitBy.Default))
                throw new ArgumentException("Query already contains a limit. Can only have one.", nameof(query));

            return query.WithLimit(LimitBy.Default.WithLimit(limit));
        }

        public static IQuery<T> OrderBy<T>(this IQuery<T> query, FieldExposer exposer)
        {
            if (exposer.FilterType != typeof(T))
                throw new ArgumentException("Impossible to order by a field exposer of type " + exposer.FilterType.Name + " on a filter element of type " + typeof(T).Name + ".", nameof(exposer));

            return query.WithOrder(
                SLDataGateway.API.Querying.OrderBy.Default.SingleConcat(
                    OrderByElement.Default.WithFieldExposer(exposer).WithSortOrder(SortOrder.Ascending)));
        }

        public static IQuery<T> OrderBy<T>(this IQuery<T> query, Exposer<T, string> exposer, bool naturalSort)
        {
            if (exposer.FilterType != typeof(T))
                throw new ArgumentException("Impossible to order by a field exposer of type " + exposer.FilterType.Name + " on a filter element of type " + typeof(T).Name + ".", nameof(exposer));

            return query.WithOrder(
                SLDataGateway.API.Querying.OrderBy.Default.SingleConcat(
                    OrderByElement.Default.WithFieldExposer(exposer).WithSortOrder(SortOrder.Ascending).WithNaturalSort(naturalSort)));
        }

        public static IQuery<T> OrderByDescending<T>(this IQuery<T> query, FieldExposer exposer)
        {
            if (exposer.FilterType != typeof(T))
                throw new ArgumentException("Impossible to order by a field exposer of type " + exposer.FilterType.Name + " on a filter element of type " + typeof(T).Name + ".", nameof(exposer));

            return query.WithOrder(
                SLDataGateway.API.Querying.OrderBy.Default.SingleConcat(
                    OrderByElement.Default.WithFieldExposer(exposer).WithSortOrder(SortOrder.Descending)));
        }

        public static IQuery<T> OrderByDescending<T>(this IQuery<T> query, Exposer<T, string> exposer, bool naturalSort)
        {
            if (exposer.FilterType != typeof(T))
                throw new ArgumentException("Impossible to order by a field exposer of type " + exposer.FilterType.Name + " on a filter element of type " + typeof(T).Name + ".", nameof(exposer));

            return query.WithOrder(
                SLDataGateway.API.Querying.OrderBy.Default.SingleConcat(
                    OrderByElement.Default.WithFieldExposer(exposer).WithSortOrder(SortOrder.Ascending).WithNaturalSort(naturalSort)));
        }

        public static IQuery<T> ThenBy<T>(this IQuery<T> query, FieldExposer exposer)
        {
            if (exposer.FilterType != typeof(T))
                throw new ArgumentException("Impossible to order by a field exposer of type " + exposer.FilterType.Name + " on a filter element of type " + typeof(T).Name + ".", nameof(exposer));

            return query.WithOrder(
                query.Order.SingleConcat(
                    OrderByElement.Default.WithFieldExposer(exposer).WithSortOrder(SortOrder.Ascending)));
        }

        public static IQuery<T> ThenBy<T>(this IQuery<T> query, Exposer<T, string> exposer, bool naturalSort)
        {
            if (exposer.FilterType != typeof(T))
                throw new ArgumentException("Impossible to order by a field exposer of type " + exposer.FilterType.Name + " on a filter element of type " + typeof(T).Name + ".", nameof(exposer));

            return query.WithOrder(
                query.Order.SingleConcat(
                    OrderByElement.Default.WithFieldExposer(exposer).WithSortOrder(SortOrder.Ascending).WithNaturalSort(naturalSort)));
        }

        public static IQuery<T> ThenByDescending<T>(this IQuery<T> query, FieldExposer exposer)
        {
            if (exposer.FilterType != typeof(T))
                throw new ArgumentException("Impossible to order by a field exposer of type " + exposer.FilterType.Name + " on a filter element of type " + typeof(T).Name + ".", nameof(exposer));

            return query.WithOrder(
                query.Order.SingleConcat(
                    OrderByElement.Default.WithFieldExposer(exposer).WithSortOrder(SortOrder.Descending)));
        }

        public static IQuery<T> ThenByDescending<T>(this IQuery<T> query, Exposer<T, string> exposer, bool naturalSort)
        {
            if (exposer.FilterType != typeof(T))
                throw new ArgumentException("Impossible to order by a field exposer of type " + exposer.FilterType.Name + " on a filter element of type " + typeof(T).Name + ".", nameof(exposer));

            return query.WithOrder(
                query.Order.SingleConcat(
                    OrderByElement.Default.WithFieldExposer(exposer).WithSortOrder(SortOrder.Ascending).WithNaturalSort(naturalSort)));
        }
    }
}
