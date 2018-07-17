using Akka.Actor;
using Akka.Configuration;
using Akka.Persistence.MongoDb;
using MongoDB.Bson.Serialization;


namespace TicketTrader.Orders.Domain
{
    public static class DomainActorSystemBuilder
    {
        public static DomainActorSystem Build(string actorSystemName, string journalConnectionString, string snapshotStoreConnectionString)
        {
            var configStr = $@"
akka {{
    persistence {{
        publish-plugin-commands = on
        journal {{
            plugin = ""akka.persistence.journal.mongodb""
            mongodb {{
                class = ""Akka.Persistence.MongoDb.Journal.MongoDbJournal, Akka.Persistence.MongoDb""
                connection-string = ""{journalConnectionString}""
                collection = ""EventJournal""
                metadata-collection = ""Metadata""
                auto-initialize = on
            }}
        }}
        snapshot-store {{
	        mongodb {{
		        class = ""Akka.Persistence.MongoDb.Snapshot.MongoDbSnapshotStore, Akka.Persistence.MongoDb""
		        connection-string = ""{snapshotStoreConnectionString}""
		        auto-initialize = on
		        collection = ""SnapshotStore""
	        }}
        }}
    }}
}}
";
            var configuration = ConfigurationFactory.ParseString(configStr);
            var actorSystem = ActorSystem.Create(actorSystemName, configuration);
            MongoDbPersistence.Get(actorSystem);

            return new DomainActorSystem(actorSystemName, actorSystem);
        }
    }
}
