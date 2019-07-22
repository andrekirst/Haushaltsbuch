using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Threading.Tasks;
using Haushaltsbuch.Library.Infrastructure.Interfaces;
using Newtonsoft.Json;
using static System.Text.Encoding;

namespace Haushaltsbuch.Library.Infrastructure.Implementations
{
    public class FileEventStore : IEventStore
    {
        private IFileSystem FileSystem { get; }

        public FileEventStore(IFileSystem fileSystem)
        {
            FileSystem = fileSystem;
        }

        public async Task<IEnumerable<Event<TAggregateId>>> ReadEventsAsync<TAggregateId>(TAggregateId id)
            where TAggregateId : IAggregateId
        {
            List<Event<TAggregateId>> ret = new List<Event<TAggregateId>>();

            string pathWithIdentifier = FileSystem.Path.Combine(path1: "Events", path2: id.Identifier);

            var files = FileSystem.Directory.GetFiles(path: pathWithIdentifier)
                .Select(selector: file => FileSystem.FileInfo.FromFileName(fileName: file))
                .Select(selector: file => new
                {
                    Order = int.Parse(s: file.Name.Replace(oldValue: "Version-", newValue: "").Replace(oldValue: ".json", newValue: "")),
                    File = file.FullName
                })
                .OrderBy(keySelector: file => file.Order);

            foreach (var file in files)
            {
                dynamic json = JsonConvert.DeserializeObject(value: FileSystem.File.ReadAllText(path: file.File));
                string typeName = json.type;
                var myEvent = json.@event;
                byte[] data = UTF8.GetBytes(JsonConvert.SerializeObject(myEvent));
                IDomainEvent<TAggregateId> resolvedEvent = Deserialize<TAggregateId>(eventType: typeName, data: data);

                ret.Add(item: new Event<TAggregateId>(
                    domainEvent: resolvedEvent,
                    eventNumber: resolvedEvent.AggregateVersion));
            }

            return ret;
        }

        public async Task<AppendResult> AppendEventAsync<TAggregateId>(IDomainEvent<TAggregateId> @event)
            where TAggregateId : IAggregateId
        {
            if (!FileSystem.Directory.Exists(path: "Events"))
            {
                FileSystem.Directory.CreateDirectory(path: "Events");
            }

            string pathWithIdentifier = FileSystem.Path.Combine(path1: "Events", path2: @event.AggregateId.Identifier);

            if (!FileSystem.Directory.Exists(path: pathWithIdentifier))
            {
                FileSystem.Directory.CreateDirectory(path: pathWithIdentifier);
            }

            var eventData = new
            {
                @event,
                type = @event.GetType().AssemblyQualifiedName,
                eventId = @event.EventId
            };

            var files = FileSystem.Directory.GetFiles(path: pathWithIdentifier)
                .Select(selector: file => FileSystem.FileInfo.FromFileName(fileName: file))
                .Select(selector: file => new
                {
                    Order = int.Parse(s: file.Name.Replace(oldValue: "Version-", newValue: "").Replace(oldValue: ".json", newValue: "")),
                    File = file.FullName
                }).ToList();

            int nextVersion = -1;
            if (files.Any())
            {
                nextVersion = files.Max(selector: file => file.Order) + 1;
            }

            string json = JsonConvert.SerializeObject(value: eventData);
            FileSystem.File.WriteAllText(path: FileSystem.Path.Combine(
                    path1: "Events",
                    path2: @event.AggregateId.Identifier,
                    path3: $"Version-{nextVersion}.json"),
                contents: json);

            return new AppendResult(nextExpectedVersion: @event.AggregateVersion + 1);
        }

        public Task<List<string>> GetEventsRawData()
        {
            throw new NotImplementedException();
        }

        private static IDomainEvent<TAggregateId> Deserialize<TAggregateId>(string eventType, byte[] data)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ContractResolver = new PrivateSetterContractResolver()
            };
            return (IDomainEvent<TAggregateId>) JsonConvert.DeserializeObject(
                value: UTF8.GetString(bytes: data),
                type: Type.GetType(typeName: eventType),
                settings: settings);
        }

        private static byte[] Serialize<TAggregateId>(IDomainEvent<TAggregateId> @event) =>
            UTF8.GetBytes(s: JsonConvert.SerializeObject(value: @event));
    }
}