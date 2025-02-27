﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by the Skyline.DataMiner.SDM.CodeGenerator package.
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace Skyline.DataMiner.SDM.Ticketing.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using DomHelpers.SlcTicketing;

    using Skyline.DataMiner.Net;
    using Skyline.DataMiner.Net.Apps.DataMinerObjectModel;
    using Skyline.DataMiner.Net.ManagerStore;
    using Skyline.DataMiner.Net.Messages.SLDataGateway;
    using Skyline.DataMiner.SDM.Ticketing.Exposers;
    using Skyline.DataMiner.SDM.Ticketing.Storage;

    using SLDataGateway.API.Querying;
    using SLDataGateway.API.Types.Querying;

    public class TicketDomStorageProvider : IStorageProvider<Ticket>
    {
        private readonly IConnection connection;
        private readonly DomHelper ticketHelper;
        public TicketDomStorageProvider(IConnection connection)
        {
            this.connection = connection;
            this.ticketHelper = new DomHelper(connection.HandleMessages, SlcTicketingIds.ModuleId);
        }

        public Ticket Create(Ticket createObject)
        {
            var domTicket = ToInstance(createObject);
            domTicket.Save(ticketHelper);
            createObject = FromInstance(domTicket);
            return createObject;
        }

        public IEnumerable<Ticket> Read(FilterElement<Ticket> filter)
        {
            if (filter == null)
            {
                return Enumerable.Empty<Ticket>();
            }

            var flatFilter = filter.flatten();
            var instances = new List<DomInstance>();
            foreach (var andFilter in flatFilter)
            {
                var domFilters = new List<FilterElement<DomInstance>> { DomInstanceExposers.DomDefinitionId.Equal(SlcTicketingIds.Definitions.Ticket.Id) };
                foreach (var subFilter in andFilter.subFilters)
                {
                    domFilters.Add(TranslateExposer(subFilter));
                }

                if (domFilters.Count < 1)
                {
                    continue;
                }

                instances.AddRange(ticketHelper.DomInstances.Read(new ANDFilterElement<DomInstance>(domFilters.ToArray())));
            }

            return instances.Distinct().Select(x => FromInstance(new TicketInstance(x)));
        }

        public IEnumerable<Ticket> Read(IQuery<Ticket> query)
        {
            if (query == null)
            {
                return Enumerable.Empty<Ticket>();
            }

            var flatFilter = query.Filter.flatten();
            var filters = new List<FilterElement<DomInstance>>();
            foreach (var andFilter in flatFilter)
            {
                var domFilters = new List<FilterElement<DomInstance>> { DomInstanceExposers.DomDefinitionId.Equal(SlcTicketingIds.Definitions.Ticket.Id) };
                foreach (var subFilter in andFilter.subFilters)
                {
                    domFilters.Add(TranslateExposer(subFilter));
                }

                if (domFilters.Count < 1)
                {
                    continue;
                }

                filters.Add(new ANDFilterElement<DomInstance>(domFilters.ToArray()));
            }

            var domOrder = OrderBy.Default;
            foreach (var order in query.Order.Elements)
            {
                domOrder = domOrder.SingleConcat(TranslateOrderBy(order));
            }

            return ticketHelper.DomInstances.Read(new ORFilterElement<DomInstance>(filters.ToArray())
                .ToQuery()
                .WithLimit(query.Limit)
                .WithOrder(domOrder))
                .Distinct()
                .Select(x => FromInstance(new TicketInstance(x)));
        }

        public Ticket Update(Ticket updateObject)
        {
            var domTicket = ToInstance(updateObject);
            domTicket.Save(ticketHelper);
            updateObject = FromInstance(domTicket);
            return updateObject;
        }

        public Ticket Delete(Ticket deleteObject)
        {
            var domTicket = ToInstance(deleteObject);
            domTicket.Delete(ticketHelper);
            return deleteObject;
        }

        private static TicketInstance ToInstance(Ticket ticket)
        {
            var fields = new TicketGeneralSection
            {
                TicketID = ticket.ID,
                TicketDescription = ticket.Description,
                TicketType = ticket.Type,
                TicketPriority = (SlcTicketingIds.Enums.Ticketpriorityenum)(int)ticket.Priority,
                TicketSeverity = (SlcTicketingIds.Enums.Ticketseverityenum)(int)ticket.Severity,
                TicketRequestedResolutionDate = ticket.RequestedResolutionDate,
                TicketExpectedResolutionDate = ticket.ExpectedResolutionDate,
            };
            TicketInstance instance;
            if (ticket.Guid == Guid.Empty)
            {
                instance = new TicketInstance(new DomInstance
                {
                    DomDefinitionId = SlcTicketingIds.Definitions.Ticket
                })
                {
                    TicketGeneral = fields
                };
            }
            else
            {
                instance = new TicketInstance(new DomInstance
                {
                    ID = new DomInstanceId(ticket.Guid),
                    DomDefinitionId = SlcTicketingIds.Definitions.Ticket
                })
                {
                    TicketGeneral = fields
                };
            }

            return instance;
        }

        private static Ticket FromInstance(TicketInstance ticket)
        {
            var rawDom = ticket.ToInstance();
            return new Ticket
            {
                Guid = ticket.ID.Id,
                ID = ticket.TicketGeneral.TicketID,
                Description = ticket.TicketGeneral.TicketDescription,
                Type = ticket.TicketGeneral.TicketType.Value,
                Priority = (TicketPriority)(int)ticket.TicketGeneral.TicketPriority.Value,
                Severity = (TicketSeverity)(int)ticket.TicketGeneral.TicketSeverity.Value,
                RequestedResolutionDate = ticket.TicketGeneral.TicketRequestedResolutionDate.Value,
                ExpectedResolutionDate = ticket.TicketGeneral.TicketExpectedResolutionDate.Value,
                LastModified = ((ITrackLastModified)rawDom).LastModified,
                LastModifiedBy = ((ITrackLastModifiedBy)rawDom).LastModifiedBy,
                CreatedAt = ((ITrackCreatedAt)rawDom).CreatedAt,
                CreatedBy = ((ITrackCreatedBy)rawDom).CreatedBy,
            };
        }

        private static IOrderByElement TranslateOrderBy(IOrderByElement order)
        {
            switch (order.Exposer.fieldName)
            {
                case nameof(Ticket.Guid):
                    return OrderByElement.Default
                        .WithFieldExposer(DomInstanceExposers.Id)
                        .WithSortOrder(order.SortOrder)
                        .WithNaturalSort(order.Options.NaturalSort);

                case nameof(Ticket.ID):
                    return OrderByElement.Default
                        .WithFieldExposer(DomInstanceExposers.FieldValues.DomInstanceField(SlcTicketingIds.Sections.TicketGeneral.TicketID))
                        .WithSortOrder(order.SortOrder)
                        .WithNaturalSort(order.Options.NaturalSort);

                case nameof(Ticket.Description):
                    return OrderByElement.Default
                        .WithFieldExposer(DomInstanceExposers.FieldValues.DomInstanceField(SlcTicketingIds.Sections.TicketGeneral.TicketDescription))
                        .WithSortOrder(order.SortOrder)
                        .WithNaturalSort(order.Options.NaturalSort);

                case nameof(Ticket.Type):
                    return OrderByElement.Default
                        .WithFieldExposer(DomInstanceExposers.FieldValues.DomInstanceField(SlcTicketingIds.Sections.TicketGeneral.TicketType))
                        .WithSortOrder(order.SortOrder)
                        .WithNaturalSort(order.Options.NaturalSort);

                case nameof(Ticket.Priority):
                    return OrderByElement.Default
                        .WithFieldExposer(DomInstanceExposers.FieldValues.DomInstanceField(SlcTicketingIds.Sections.TicketGeneral.TicketPriority))
                        .WithSortOrder(order.SortOrder)
                        .WithNaturalSort(order.Options.NaturalSort);

                case nameof(Ticket.Severity):
                    return OrderByElement.Default
                        .WithFieldExposer(DomInstanceExposers.FieldValues.DomInstanceField(SlcTicketingIds.Sections.TicketGeneral.TicketSeverity))
                        .WithSortOrder(order.SortOrder)
                        .WithNaturalSort(order.Options.NaturalSort);

                case nameof(Ticket.RequestedResolutionDate):
                    return OrderByElement.Default
                        .WithFieldExposer(DomInstanceExposers.FieldValues.DomInstanceField(SlcTicketingIds.Sections.TicketGeneral.TicketRequestedResolutionDate))
                        .WithSortOrder(order.SortOrder)
                        .WithNaturalSort(order.Options.NaturalSort);

                case nameof(Ticket.ExpectedResolutionDate):
                    return OrderByElement.Default
                        .WithFieldExposer(DomInstanceExposers.FieldValues.DomInstanceField(SlcTicketingIds.Sections.TicketGeneral.TicketExpectedResolutionDate))
                        .WithSortOrder(order.SortOrder)
                        .WithNaturalSort(order.Options.NaturalSort);

                case nameof(Ticket.LastModified):
                    return OrderByElement.Default
                        .WithFieldExposer(DomInstanceExposers.LastModified)
                        .WithSortOrder(order.SortOrder)
                        .WithNaturalSort(order.Options.NaturalSort);

                case nameof(Ticket.LastModifiedBy):
                    return OrderByElement.Default
                        .WithFieldExposer(DomInstanceExposers.LastModifiedBy)
                        .WithSortOrder(order.SortOrder)
                        .WithNaturalSort(order.Options.NaturalSort);

                case nameof(Ticket.CreatedAt):
                    var temp = OrderByElement.Default;

                    return OrderByElement.Default
                        .WithFieldExposer(DomInstanceExposers.CreatedAt)
                        .WithSortOrder(order.SortOrder)
                        .WithNaturalSort(order.Options.NaturalSort);

                case nameof(Ticket.CreatedBy):
                    return OrderByElement.Default
                        .WithFieldExposer(DomInstanceExposers.CreatedBy)
                        .WithSortOrder(order.SortOrder)
                        .WithNaturalSort(order.Options.NaturalSort);

                default:
                    throw new NotSupportedException("This comparer option is not supported yet.");
            }
        }

        private static FilterElement<DomInstance> TranslateExposer(FilterElement<Ticket> filter)
        {
            var exposer = filter?.findManagedFilters(TicketExposers.FieldExposers.ToList())?.FirstOrDefault();
            if (exposer == null)
            {
                return new TRUEFilterElement<DomInstance>();
            }

            var fieldName = exposer.getFieldName().fieldName;
            var fieldValue = exposer.getValue();
            var comparer = exposer.getComparer();
            switch (fieldName)
            {
                case nameof(Ticket.Guid):
                    return new ManagedFilter<DomInstance, Guid>(DomInstanceExposers.Id, comparer, (Guid)fieldValue, (value) => value.ID.Id.Equals((Guid)fieldValue));
                case nameof(Ticket.ID):
                    return TranslateFilter(DomInstanceExposers.FieldValues.DomInstanceField(SlcTicketingIds.Sections.TicketGeneral.TicketID), comparer, (String)fieldValue);
                case nameof(Ticket.Description):
                    return TranslateFilter(DomInstanceExposers.FieldValues.DomInstanceField(SlcTicketingIds.Sections.TicketGeneral.TicketDescription), comparer, (String)fieldValue);
                case nameof(Ticket.Type):
                    return TranslateFilter(DomInstanceExposers.FieldValues.DomInstanceField(SlcTicketingIds.Sections.TicketGeneral.TicketType), comparer, (Guid)fieldValue);
                case nameof(Ticket.Priority):
                    return TranslateFilter(DomInstanceExposers.FieldValues.DomInstanceField(SlcTicketingIds.Sections.TicketGeneral.TicketPriority), comparer, (int)fieldValue);
                case nameof(Ticket.Severity):
                    return TranslateFilter(DomInstanceExposers.FieldValues.DomInstanceField(SlcTicketingIds.Sections.TicketGeneral.TicketSeverity), comparer, (int)fieldValue);
                case nameof(Ticket.RequestedResolutionDate):
                    return TranslateFilter(DomInstanceExposers.FieldValues.DomInstanceField(SlcTicketingIds.Sections.TicketGeneral.TicketRequestedResolutionDate), comparer, (DateTime)fieldValue);
                case nameof(Ticket.ExpectedResolutionDate):
                    return TranslateFilter(DomInstanceExposers.FieldValues.DomInstanceField(SlcTicketingIds.Sections.TicketGeneral.TicketExpectedResolutionDate), comparer, (DateTime)fieldValue);
                case nameof(Ticket.LastModified):
                    return new ManagedFilter<DomInstance, DateTime>(DomInstanceExposers.LastModified, comparer, (DateTime)fieldValue, (value) => ((ITrackLastModified)value).LastModified.Equals((DateTime)fieldValue));
                case nameof(Ticket.LastModifiedBy):
                    return new ManagedFilter<DomInstance, String>(DomInstanceExposers.LastModifiedBy, comparer, (String)fieldValue, (value) => ((ITrackLastModifiedBy)value).LastModifiedBy.Equals((String)fieldValue));
                case nameof(Ticket.CreatedAt):
                    return new ManagedFilter<DomInstance, DateTime>(DomInstanceExposers.CreatedAt, comparer, (DateTime)fieldValue, (value) => ((ITrackCreatedAt)value).CreatedAt.Equals((DateTime)fieldValue));
                case nameof(Ticket.CreatedBy):
                    return new ManagedFilter<DomInstance, String>(DomInstanceExposers.CreatedBy, comparer, (String)fieldValue, (value) => ((ITrackCreatedBy)value).CreatedBy.Equals((String)fieldValue));
                default:
                    throw new NotSupportedException("This comparer option is not supported yet.");
            }
        }

        private static ManagedFilter<DomInstance, IEnumerable> TranslateFilter(DynamicListExposer<DomInstance, object> exposer, Skyline.DataMiner.Net.Messages.SLDataGateway.Comparer comparer, object value)
        {
            switch (comparer)
            {
                case Skyline.DataMiner.Net.Messages.SLDataGateway.Comparer.Equals:
                    return exposer.Equal(value);
                case Skyline.DataMiner.Net.Messages.SLDataGateway.Comparer.NotEquals:
                    return exposer.NotEqual(value);
                case Skyline.DataMiner.Net.Messages.SLDataGateway.Comparer.GT:
                    return exposer.GreaterThan(value);
                case Skyline.DataMiner.Net.Messages.SLDataGateway.Comparer.GTE:
                    return exposer.GreaterThanOrEqual(value);
                case Skyline.DataMiner.Net.Messages.SLDataGateway.Comparer.LT:
                    return exposer.LessThan(value);
                case Skyline.DataMiner.Net.Messages.SLDataGateway.Comparer.LTE:
                    return exposer.LessThanOrEqual(value);
                case Skyline.DataMiner.Net.Messages.SLDataGateway.Comparer.Contains:
                    return exposer.Contains(value);
                case Skyline.DataMiner.Net.Messages.SLDataGateway.Comparer.NotContains:
                    return exposer.NotContains(value);
                default:
                    throw new NotSupportedException("This comparer option is not supported yet");
            }
        }
    }
}