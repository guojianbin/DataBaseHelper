using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultiWeb
{
    public class MongoDBHelperMini<T> where T : class, new()
    {
        /// <summary>
        /// 数据库连接
        /// </summary>
        private static MongoClient client;

        /// <summary>
        /// 指定数据库
        /// </summary>
        private static IMongoDatabase database;

        /// <summary>
        /// 初始化的时候，加载数据库连接和指定数据库
        /// </summary>
        /// <param name="connectionString">数据库连接字符串，例如 mongodb://127.0.0.1:27017</param>
        /// <param name="databaseName">指定的数据库名称</param>
        public MongoDBHelperMini(string connectionString, string databaseName)
        {
            client = new MongoClient(connectionString);
            database = client.GetDatabase(databaseName);
        }

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns>结果集</returns>
        public async Task<List<BsonDocument>> GetList(string tableName)
        {
            var collection = database.GetCollection<BsonDocument>(tableName);
            var doc = await collection.Find(new BsonDocument()).ToListAsync();
            return doc;
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="model">实体</param>
        /// <param name="tableName">表名</param>
        /// <returns>无</returns>
        public async Task Add(T model, string tableName)
        {
            var collection = database.GetCollection<T>(tableName);
            await collection.InsertOneAsync(model);
        }

        /// <summary>
        /// 更改数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns>无</returns>
        public async Task Update(string tableName)
        {
            var collection = database.GetCollection<T>(tableName);
            var filter = Builders<T>.Filter.Eq("Id", 1);
            var update = Builders<T>.Update.Set("Name", 110);

            await collection.UpdateOneAsync(filter, update);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns>无</returns>
        public async Task Delete(string tableName)
        {
            var collection = database.GetCollection<T>(tableName);
            var filter = Builders<T>.Filter.Eq("Id", 1);
            await collection.DeleteOneAsync(filter);
        }
    }
}