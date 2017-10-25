namespace LeaRun.Cache.Factory
{
    /// <summary>
    /// 版 本 6.1
    /// 日 期：2015.11.9 10:45
    /// 描 述：缓存工厂类
    /// </summary>
    public class CacheFactory
    {
        /// <summary>
        /// 定义通用的Repository
        /// </summary>
        /// <returns></returns>
        public static ICache Cache()
        {
            //修改为支持Redis
            string cacheType = LeaRun.Util.Config.GetValue("CacheType");
            switch (cacheType)
            {
                case "Redis":
                    return new Redis.Cache();
                    //break;
                case "WebCache":
                    return new Cache();
                    //break;
                default:
                    return new Cache();
                   // break;
            }
        }
    }
}
