using Domains.DBModels;

using MongoDB.Driver;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly IMongoLandHubDBContext _mongoContext;
        private readonly IMongoCollection<TEntity> _dbCollection;

        public BaseRepository(IMongoLandHubDBContext context)
        {
            _mongoContext = context;
            _dbCollection = _mongoContext.GetCollection<TEntity>($"{typeof(TEntity).Name}");
        }



        public async Task<TEntity> Get(string id)
        {
            var filter = Builders<TEntity>.Filter.Eq(x => x.Id, id);
            return await _dbCollection.FindAsync(filter).Result.FirstOrDefaultAsync();

        }


        public async Task<IEnumerable<TEntity>> Get()
        {
            var all = await _dbCollection.FindAsync(Builders<TEntity>.Filter.Empty);
            return await all.ToListAsync();
        }

        public async Task Create(TEntity obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(typeof(TEntity).Name + " object is null");
            }
            obj.Id = Guid.NewGuid().ToString();
            await _dbCollection.InsertOneAsync(obj);
        }

        public void Update(TEntity obj)
        {
            _ = _dbCollection.ReplaceOneAsync(Builders<TEntity>.Filter.Eq("_id", obj.ToString()), obj);
        }

        public void Delete(string id)
        {
            //ex. 5dc1039a1521eaa36835e541

            var filter = Builders<TEntity>.Filter.Eq(x => x.Id, id);


            _dbCollection.DeleteOneAsync(filter);

        }
    }
}
