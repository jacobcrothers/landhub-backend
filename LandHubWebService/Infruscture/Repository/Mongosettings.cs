using MongoDB.Driver;

namespace Services.Repository
{
    public class Mongosettings
    {
        public MongoClientSettings Connection { get; internal set; }
        public string DatabaseName { get; internal set; }
    }
}