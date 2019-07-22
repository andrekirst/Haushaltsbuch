using System;
using Haushaltsbuch.Library.Infrastructure.Extensions;
using MongoDB.Driver;

namespace Haushaltsbuch.Library.Domain.Queries
{
    public class UebersichtQueries : IUebersichtQueries
    {
        private IMongoDatabase MongoDatabase { get; }

        public UebersichtQueries(IMongoDatabase mongoDatabase)
        {
            MongoDatabase = mongoDatabase;
        }

        public Uebersicht AktuellerMonat(string haushaltsbuchId)
        {
            throw new System.NotImplementedException();
        }

        public Uebersicht FuerMonat(string haushaltsbuchId, int monat, int jahr)
        {
            throw new System.NotImplementedException();
        }

        public AktuellerKassenbestand AktuellerKassenbestand(string haushaltsbuchId)
        {
            IMongoCollection<ReadModel.Haushaltsbuch> collection = GetCollection<ReadModel.Haushaltsbuch>();
            double kassenbestand = collection
                .Find(filter: x => x.Id == haushaltsbuchId)
                .First().Kassenbestand;
            return new AktuellerKassenbestand(kassenbestand: kassenbestand);
        }

        public KategorieAusgabe KategorieAusgabe(string haushaltsbuchId, string kategorie)
        {
            IMongoCollection<ReadModel.HaushaltsbuchAuszahlung> collection = GetCollection<ReadModel.HaushaltsbuchAuszahlung>();
            var x = collection
                .Find(filter: x =>
                    x.HaushaltsbuchId == haushaltsbuchId
                    && x.Auszahlungsdatum >= DateTime.Today.FirstDayOfMonth()
                    && x.Auszahlungsdatum <= DateTime.Today.LastDayOfMonth())
                .ToCursor();

            throw new NotImplementedException();
        }

        private IMongoCollection<T> GetCollection<T>() =>
            MongoDatabase.GetCollection<T>(name: typeof(T).Name);
    }
}