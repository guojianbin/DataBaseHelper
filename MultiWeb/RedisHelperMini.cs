using ServiceStack.Redis;

namespace MultiWeb
{
    public class RedisHelperMini
    {
        /// <summary>
        /// 数据库连接
        /// </summary>
        public static RedisClient client;

        /// <summary>
        /// 初始化加载数据库
        /// </summary>
        public RedisHelperMini()
        {
            client = new RedisClient("127.0.0.1", 6379);
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="name">key</param>
        /// <returns>实体</returns>
        public T Get<T>(string name) where T : class
        {
            T result = client.Get<T>(name);
            return result;
        }

        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="val">value</param>
        /// <returns>是否成功</returns>
        public bool Set(object key, object val)
        {
            return client.Set(key.ToString(), val);
        }
    }
}