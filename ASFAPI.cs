using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HAF_2_0
{
    public class ASFAPI
    {
        /// <summary>
        /// 激活SDK
        /// </summary>
        /// <param name="AppId">申请到的AppId</param>
        /// <param name="SDKKey">申请到的SDK秘钥</param>
        /// <returns>激活结果</returns>
        [DllImport("libarcsoft_face_engine.dll", EntryPoint = "ASFActivation", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ASFActivation(string AppId, string SDKKey);


        /// <summary>
        /// 初始化引擎（每次使用虹软只调用一次即可）
        /// </summary>
        /// <param name="detectMode">video模式或者image模式</param>
        /// <param name="detectFaceOrientPriority">检测脸部较低的优先值</param>
        /// <param name="detectFaceScaleVal">数值化的最小人脸尺寸，视频[2,16]/图片[2,32]，推荐值16</param>
        /// <param name="detectFaceMaxNum">最大检测人脸的个数[1,50]</param>
        /// <param name="combinedMask">要用到的引擎组合</param>
        /// <param name="hEngine">初始化返回的引擎handle</param>
        /// <returns></returns>
        [DllImport("libarcsoft_face_engine.dll", EntryPoint = "ASFInitEngine", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ASFInitEngine(uint detectMode, int detectFaceOrientPriority, int detectFaceScaleVal, int detectFaceMaxNum, int combinedMask, ref IntPtr hEngine);


        /// <summary>
        /// 人脸检测
        /// </summary>
        /// <param name="hEngine">引擎handle</param>
        /// <param name="width">图片宽度4的倍数，大于0</param>
        /// <param name="height">YUYV/I420/NV21/NV12格式的图片高度为2的倍数，BGR24格式的图片高度不限制</param>
        /// <param name="format">颜色空间格式</param>
        /// <param name="imgData">图片数据</param>
        /// <param name="detectedFaces">检测到的人脸信息</param>
        /// <returns></returns>
        [DllImport("libarcsoft_face_engine.dll", EntryPoint = "ASFDetectFaces", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ASFDetectFaces(IntPtr hEngine, int width, int height, int format, byte[] data, ref MultiFaceInfo detectedFaces);
      


        /// <summary>
        /// 单人脸特征提取
        /// </summary>
        /// <param name="hEngine">引擎handle</param>
        /// <param name="width">图片宽度为4的倍数且大于0</param>
        /// <param name="height">YUYV/I420/NV21/NV12格式的图片高度为2的倍数，BGR24格式的图片高度不限制</param>
        /// <param name="format">颜色空间格式</param>
        /// <param name="imgData">图片数据</param>
        /// <param name="faceInfo">单张人脸位置和角度信息</param>
        /// <param name="feature">人脸特征</param>
        /// <returns></returns>
        [DllImport("libarcsoft_face_engine.dll", EntryPoint = "ASFFaceFeatureExtract", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ASFFaceFeatureExtract(IntPtr hEngine, int width, int height, int format, byte[] imgData, IntPtr faceInfo, ref FaceFeature feature);

        /// <summary>
        /// 人脸特征比对
        /// </summary>
        /// <param name="hEngine">引擎handle</param>
        /// <param name="feature1">待比对的人脸特征</param>
        /// <param name="feature2">待比对的人脸特征</param>
        /// <param name="confidenceLevel">比对结果，置信度数值</param>
        /// <returns></returns>
        [DllImport("libarcsoft_face_engine.dll", EntryPoint = "ASFFaceFeatureCompare", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ASFFaceFeatureCompare(IntPtr hEngine, FaceFeature feature1, FaceFeature feature2, ref float confidenceLevel);

        /// <summary>
        /// 人脸信息检测（年龄/性别/人脸3D角度）最多支持4张人脸信息检测
        /// </summary>
        /// <param name="hEngine">引擎handle</param>
        /// <param name="width">图片宽度为4的倍数且大于0</param>
        /// <param name="height">YUYV/I420/NV21/NV12格式的图片高度为2的倍数，BGR24格式的图片高度不限制</param>
        /// <param name="format">颜色空间格式</param>
        /// <param name="imgData">图片数据</param>
        /// <param name="detectedFaces">检测到的人脸信息</param>
        /// <param name="combinedMask">初始化中参数combinedMask与ASF_AGE| ASF_GENDER| ASF_FACE3DANGLE的交集的子集</param>
        /// <returns></returns>
        [DllImport("libarcsoft_face_engine.dll", EntryPoint = "ASFProcess", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ASFProcess(IntPtr hEngine, int width, int height, int format, byte[] imgData, MultiFaceInfo detectedFaces, int combinedMask);

        /// <summary>
        /// 获取年龄信息
        /// </summary>
        /// <param name="hEngine">引擎handle</param>
        /// <param name="ageInfo">检测到的年龄信息</param>
        /// <returns></returns>
        [DllImport("libarcsoft_face_engine.dll", EntryPoint = "ASFGetAge", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ASFGetAge(IntPtr hEngine, ref AgeInfo ageInfo);

        /// <summary>
        /// 获取性别信息
        /// </summary>
        /// <param name="hEngine">引擎handle</param>
        /// <param name="genderInfo">检测到的性别信息</param>
        /// <returns></returns>
        [DllImport("libarcsoft_face_engine.dll", EntryPoint = "ASFGetGender", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ASFGetGender(IntPtr hEngine, ref GenderInfo genderInfo);

        /// <summary>
        /// 获取3D角度信息
        /// </summary>
        /// <param name="hEngine">引擎handle</param>
        /// <param name="p3DAngleInfo">检测到脸部3D 角度信息</param>
        /// <returns></returns>
        [DllImport("libarcsoft_face_engine.dll", EntryPoint = "ASFGetFace3DAngle", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ASFGetFace3DAngle(IntPtr hEngine, ref ThreeDAngle p3DAngleInfo);

        /// <summary>
        /// 销毁引擎
        /// </summary>
        /// <param name="hEngine">引擎handle</param>
        /// <returns></returns>
        [DllImport("libarcsoft_face_engine.dll", EntryPoint = "ASFUninitEngine", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ASFUninitEngine(IntPtr hEngine);


    }
}
