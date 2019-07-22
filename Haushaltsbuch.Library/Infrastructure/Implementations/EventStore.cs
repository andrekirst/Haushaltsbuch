using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using EventStore.ClientAPI.Exceptions;
using EventStore.ClientAPI.SystemData;
using FluentTimeSpan;
using Haushaltsbuch.Library.Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using static System.Text.Encoding;
using Convert = System.Convert;

namespace Haushaltsbuch.Library.Infrastructure.Implementations
{
    public class EventStore : IEventStore
    {
        private IEventStoreConnection Connection { get; set; }
        private IConfiguration Configuration { get; }

        public EventStore(IEventStoreConnection connection, IConfiguration configuration)
        {
            Connection = connection;
            Configuration = configuration;
            Connection.ConnectAsync().Wait();
        }

        public async Task<IEnumerable<Event<TAggregateId>>> ReadEventsAsync<TAggregateId>(TAggregateId id)
            where TAggregateId : IAggregateId
        {
            try
            {
                List<Event<TAggregateId>> ret = new List<Event<TAggregateId>>();
                StreamEventsSlice currentSlice;
                long nextSliceStart = StreamPosition.Start;

                do
                {
                    currentSlice = await ReadStreamEventsForwardAsync(id: id, nextSliceStart: nextSliceStart);
                    if (currentSlice.Status != SliceReadStatus.Success)
                    {
                        throw new EventStoreAggregateNotFoundException(message: $"Aggregate {id.Identifier} not found");
                    }

                    nextSliceStart = currentSlice.NextEventNumber;
                    ret.AddRange(
                        collection: currentSlice.Events.Select(
                            selector: resolvedEvent => new Event<TAggregateId>(
                                domainEvent: Deserialize<TAggregateId>(
                                    eventType: resolvedEvent.Event.EventType,
                                    data: resolvedEvent.Event.Data),
                                eventNumber: resolvedEvent.Event.EventNumber)));
                } while (!currentSlice.IsEndOfStream);

                return ret;
            }
            catch (EventStoreCommunicationException e)
            {
                throw new EventStoreCommunicationException(message: $"Error while reading events for aggregate {id}", innerException: e);
            }
        }

        private async Task<StreamEventsSlice> ReadStreamEventsForwardAsync<TAggregateId>(TAggregateId id, long nextSliceStart) where TAggregateId : IAggregateId
        {
            try
            {
                return await Connection.ReadStreamEventsForwardAsync(
                        stream: id.Identifier,
                        start: nextSliceStart,
                        count: 200,
                        resolveLinkTos: false);
            }
            catch (Exception ex)
            {
                string eventStoreConnectionString = Environment.GetEnvironmentVariable(variable: "EVENTSTORE_CONNECTIONSTRING") ??
                                                    Configuration.GetSection(key: "EventStore").GetValue<string>(key: "ConnectionString");

                ConnectionSettings settings = ConnectionSettings
                    .Create()
                    .KeepReconnecting()
                    .SetQueueTimeoutTo(queueTimeout: 5.Seconds())
                    .UseConsoleLogger()
                    .Build();
                Connection = EventStoreConnection.Create(
                    uri: new Uri(uriString: eventStoreConnectionString),
                    connectionName: "Haushaltsbuch",
                    connectionSettings: settings);
                await Connection.ConnectAsync();
                return await Connection.ReadStreamEventsForwardAsync(
                    stream: id.Identifier,
                    start: nextSliceStart,
                    count: 200,
                    resolveLinkTos: false);
            }
        }

        public async Task<AppendResult> AppendEventAsync<TAggregateId>(IDomainEvent<TAggregateId> @event)
            where TAggregateId : IAggregateId
        {
            try
            {
                EventData eventData = new EventData(
                    eventId: @event.EventId,
                    type: @event.GetType().AssemblyQualifiedName,
                    isJson: true,
                    data: Serialize(@event: @event),
                    metadata: UTF8.GetBytes(s: "{}"));

                WriteResult writeResult = await WriteResult(@event: @event, eventData: eventData);

                return new AppendResult(nextExpectedVersion: writeResult.NextExpectedVersion);
            }
            catch (EventStoreConnectionException e)
            {
                throw new EventStoreCommunicationException(message: $"Error while appending event {@event.EventId} for aggregate {@event.AggregateId}", innerException: e);
            }
        }

        private async Task<WriteResult> WriteResult<TAggregateId>(IDomainEvent<TAggregateId> @event, EventData eventData) where TAggregateId : IAggregateId
        {
            try
            {
                WriteResult writeResult = await Connection.AppendToStreamAsync(
                        stream: @event.AggregateId.Identifier,
                        expectedVersion: @event.AggregateVersion == AggregateBase<TAggregateId>.NewAggregateVersion
                            ? ExpectedVersion.NoStream
                            : @event.AggregateVersion,
                        eventData);
                return writeResult;
            }
            catch (Exception ex)
            {
                string eventStoreConnectionString = Environment.GetEnvironmentVariable(variable: "EVENTSTORE_CONNECTIONSTRING") ??
                                                    Configuration.GetSection(key: "EventStore").GetValue<string>(key: "ConnectionString");

                ConnectionSettings settings = ConnectionSettings
                    .Create()
                    .KeepReconnecting()
                    .SetQueueTimeoutTo(queueTimeout: 5.Seconds())
                    .UseConsoleLogger()
                    .Build();
                Connection = EventStoreConnection.Create(
                    uri: new Uri(uriString: eventStoreConnectionString),
                    connectionName: "Haushaltsbuch",
                    connectionSettings: settings);
                
                await Connection.ConnectAsync();
                
                WriteResult writeResult = await Connection.AppendToStreamAsync(
                    stream: @event.AggregateId.Identifier,
                    expectedVersion: @event.AggregateVersion == AggregateBase<TAggregateId>.NewAggregateVersion
                        ? ExpectedVersion.NoStream
                        : @event.AggregateVersion,
                    eventData);
                return writeResult;
            }
        }

        public async Task<List<string>> GetEventsRawData()
        {
            AllEventsSlice allEventsSlice = await Connection.ReadAllEventsBackwardAsync(position: Position.End, maxCount: 2000, resolveLinkTos: false, userCredentials: new UserCredentials(username: "admin", password: "changeit"));

            return allEventsSlice.Events
                .Where(predicate: e => !e.Event.EventType.StartsWith(value: "$"))
                .Select(selector: s => Convert.ToBase64String(inArray: s.Event.Data))
                .ToList();
        }

        private static IDomainEvent<TAggregateId> Deserialize<TAggregateId>(string eventType, byte[] data)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            { ContractResolver = new PrivateSetterContractResolver() };
            return (IDomainEvent<TAggregateId>)JsonConvert.DeserializeObject(value: UTF8.GetString(bytes: data),
                type: Type.GetType(typeName: eventType), settings: settings);
        }

        private static byte[] Serialize<TAggregateId>(IDomainEvent<TAggregateId> @event) =>
            UTF8.GetBytes(s: JsonConvert.SerializeObject(value: @event));
    }
}