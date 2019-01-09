using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAF_2_0
{
    /// <summary>
    /// 引擎激活
    /// </summary>
    public class EngineActivate
    {
        /// <summary>
        /// 激活引擎
        /// </summary>
        /// <param name="appId">appid</param>
        /// <param name="appKey">appkey</param>
        /// <returns>激活结果</returns>
        public static ResultCode ActivateEngine(string appId, string appKey)
        {
            int result = ASFAPI.ASFActivation(appId, appKey);
            return (ResultCode)result;
        }
    }
}
