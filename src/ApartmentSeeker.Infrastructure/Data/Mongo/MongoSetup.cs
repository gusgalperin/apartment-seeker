using ApartmentScrapper.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;

namespace ApartmentScrapper.Infrastructure.Data.Mongo
{
    public static class MongoSetup
    {
        public static void OnStartup()
        {
            BsonSerializer.RegisterSerializer(new DecimalSerializer(BsonType.Decimal128));
            BsonSerializer.RegisterSerializer(new NullableSerializer<decimal>(
                new DecimalSerializer(BsonType.Decimal128)));
            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.DateTime));
            ConventionRegistry.Register("DocumentConventions",
                new ConventionPack {
                    new IgnoreExtraElementsConvention(true),
                    new EnumRepresentationConvention(BsonType.String),
                    new CamelCaseElementNameConvention(),
                },
                _ => true);

            BsonClassMap.RegisterClassMap<Apartment>(cm =>
            {
                cm.AutoMap();
            });
        }
    }
}