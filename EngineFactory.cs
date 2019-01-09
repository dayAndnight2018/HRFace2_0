using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HAF_2_0
{
    /// <summary>
    /// 引擎工厂
    /// </summary>
    public class EngineFactory
    {

        private static IntPtr hEngine;

        private  const uint video = 0x00000000;
        private  const uint image = 0xFFFFFFFF;

        public static uint Video { get { return video; } }
        public static uint Image { get { return image; } }

        /// <summary>
        /// 获得引擎Handler
        /// </summary>
        /// <param name="mode">模式（图像/视频流）</param>
        /// <param name="orientPriority">检测方向的优先级</param>
        /// <param name="detectFaceScaleVal">数值化的最小人脸尺寸</param>
        /// <returns></returns>
        public static IntPtr GetEngineInstance(uint mode, DetectionOrientPriority orientPriority, int detectFaceScaleVal = 12)
        {

            hEngine =  IntPtr.Zero;
            try
            {
                ResultCode result = (ResultCode)ASFAPI.ASFInitEngine(mode, (int)orientPriority, detectFaceScaleVal, 50, (int)(EngineMode.人脸检测|EngineMode.人脸识别|EngineMode.性别识别|EngineMode.年龄识别|EngineMode.角度识别), ref  hEngine);
                if (result == ResultCode.成功)
                {
                    return hEngine;
                }
                else
                {
                    throw new Exception(result.ToString());
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
    
        /// <summary>
        /// 释放引擎Handler
        /// </summary>
        /// <returns></returns>
        public static bool DisposeEngine()
        {
            if (hEngine != IntPtr.Zero)
            {
                try
                {
                    ResultCode result = (ResultCode)ASFAPI.ASFUninitEngine(hEngine);
                    if (result == ResultCode.成功)
                    {
                        return true;
                    }
                    else
                    {
                        throw new Exception(result.ToString());
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }            
            return true;
        }
    }
}
