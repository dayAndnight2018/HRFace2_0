using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.InteropServices;

namespace HAF_2_0
{
    /// <summary>
    /// 人脸个数检测、年龄检测、性别检测、人脸对比
    /// 
    /// 人脸个数检测：多脸信息->提取个数
    /// 年龄检测：多脸信息检测->多脸信息分析->年龄分析
    /// 性别检测：多脸信息检测->多脸信息分析->性别分析
    /// 
    /// 人脸对比：多脸信息检测1->提取单脸信息1->分析单脸信息1
    ///                                                                                                                                      ->人脸对比
    ///人脸对比：多脸信息检测2->提取单脸信息2->分析单脸信息2
    /// </summary>
    public class FaceDetection
    {
        private IntPtr hEngine;
        private Bitmap image1;
        private Bitmap image2;
        BitmapImage bitmapImage;

        /// <summary>
        /// 人脸检测
        /// </summary>
        /// <param name="hEngine"></param>
        /// <param name="image"></param>
        public FaceDetection(IntPtr hEngine, Bitmap image)
        {
            this.hEngine = hEngine;
            this.image1 = image;
        }

        public FaceDetection(IntPtr hEngine)
        {
            this.hEngine = hEngine;
        }

        /// <summary>
        /// 人脸检测
        /// </summary>
        /// <param name="hEngine"></param>
        /// <param name="image1"></param>
        /// <param name="image2"></param>
        public FaceDetection(IntPtr hEngine, Bitmap image1, Bitmap image2)
        {
            this.hEngine = hEngine;
            this.image1 = image1;
            this.image2 = image2;
        }

        /// <summary>
        /// 获取人脸检测的结果
        /// </summary>
        /// <returns></returns>
        public MultiFaceInfo DetectFaces()
        {
            bitmapImage = new BitmapImage(image1);
            bitmapImage.ParseImage();

            MultiFaceInfo faceInfo = new MultiFaceInfo();
            try
            {
                ResultCode result = (ResultCode)ASFAPI.ASFDetectFaces(hEngine, bitmapImage.Width, bitmapImage.Height, bitmapImage.Format, bitmapImage.ImageData, ref faceInfo);
                if (result == ResultCode.成功)
                {
                    return faceInfo;
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
        /// 获取人脸检测结果
        /// </summary>
        /// <param name="image"></param>
        /// <param name="bitmapImage"></param>
        /// <returns></returns>
        public MultiFaceInfo DetectFaces(Bitmap image, out BitmapImage bitmapImage)
        {
            bitmapImage = new BitmapImage(image);
            bitmapImage.ParseImage();
            MultiFaceInfo faceInfo = new MultiFaceInfo();
            try
            {
                ResultCode result = (ResultCode)ASFAPI.ASFDetectFaces(hEngine, bitmapImage.Width, bitmapImage.Height, bitmapImage.Format, bitmapImage.ImageData, ref faceInfo);
                if (result == ResultCode.成功)
                {
                    return faceInfo;
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
        /// 获取人脸的数量
        /// </summary>
        /// <returns></returns>
        public int FindFaceNum()
        {

            MultiFaceInfo faceInfo = DetectFaces();
            //如果底层返回的数据并没有相框及角度信息，默认没有检测到人脸
            if (faceInfo.faceOrient == IntPtr.Zero || faceInfo.faceRect == IntPtr.Zero)
            {
                return 0;
            }

            return faceInfo.faceNum;

        }

        /// <summary>
        /// 获取年龄信息
        /// </summary>
        /// <returns></returns>
        public int GetAge()
        {
            var faceinfo = DetectFaces();
            if (faceinfo.faceOrient == IntPtr.Zero || faceinfo.faceRect == IntPtr.Zero)
            {
                return 0;
            }

            var processResult =ASFAPI.ASFProcess(hEngine, bitmapImage.Width, bitmapImage.Height, bitmapImage.Format, bitmapImage.ImageData, faceinfo, (int)(EngineMode.年龄识别));
            if (processResult != (int)ResultCode.成功)
            {
                return 0;
            }

            AgeInfo age = new AgeInfo();
            var ageResult = ASFAPI.ASFGetAge(hEngine, ref age);
            if (ageResult != (int)ResultCode.成功)
            {
                return 0;
            }

            int[] ageArray = new int[age.num];
            Marshal.Copy(age.ageArray, ageArray, 0, ageArray.Length);
            return ageArray[0];
        }

        /// <summary>
        /// 获取性别信息
        /// </summary>
        /// <returns></returns>
        public string GetGender()
        {
            var faceinfo = DetectFaces();
            if (faceinfo.faceOrient == IntPtr.Zero || faceinfo.faceRect == IntPtr.Zero)
            {
                return "未知";
            }

            var processResult = ASFAPI.ASFProcess(hEngine, bitmapImage.Width, bitmapImage.Height, bitmapImage.Format, bitmapImage.ImageData, faceinfo, (int)(EngineMode.性别识别));
            if (processResult != (int)ResultCode.成功)
            {
                return "未知";
            }

            GenderInfo gender = new GenderInfo();
            var genderResult = ASFAPI.ASFGetGender(hEngine, ref gender);
            if (genderResult != (int)ResultCode.成功||gender.num <=0)
            {
                return "未知";
            }

            int[] genderArray = new int[gender.num];
            Marshal.Copy(gender.genderArray, genderArray, 0, genderArray.Length);
            switch (genderArray[0])
            {
                case 0:
                    return "男";
                case 1:
                    return "女";
                default:
                    return "未知";
            }
        }

        /// <summary>
        /// 获取人脸特征
        /// </summary>
        /// <returns></returns>
        public byte[] getFaceFeature(Bitmap image)
        {
            BitmapImage bitmapImage = null;
            var multiFaceInfo = DetectFaces(image, out bitmapImage);
            if (multiFaceInfo.faceNum == 0 || multiFaceInfo.faceOrient == IntPtr.Zero || multiFaceInfo.faceRect == IntPtr.Zero )
            {
                return null;
            }

            SingleFaceInfo info = new SingleFaceInfo();
            Mrect mrect = new Mrect();
            int[] orientArray = new int[multiFaceInfo.faceNum];
            Marshal.Copy(multiFaceInfo.faceOrient, orientArray, 0, orientArray.Length);
            info.faceOrient = orientArray[0];

            byte[] byteArray = new byte[4 * 4];
            Marshal.Copy(multiFaceInfo.faceRect, byteArray, 0, byteArray.Length);
            int size = Marshal.SizeOf(mrect);
            IntPtr buffer = Marshal.AllocHGlobal(size);

            try
            {
                Marshal.Copy(byteArray, 0, buffer, size);
                mrect = (Mrect)Marshal.PtrToStructure(buffer, typeof(Mrect));
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
            info.faceRect = mrect;

            FaceFeature feature = new FaceFeature();
            IntPtr ptr1 = Marshal.AllocHGlobal(Marshal.SizeOf(info));
            Marshal.StructureToPtr(info, ptr1, false);
            var result = ASFAPI.ASFFaceFeatureExtract(hEngine, bitmapImage.Width, bitmapImage.Height, bitmapImage.Format, bitmapImage.ImageData, ptr1, ref feature);
            Marshal.FreeHGlobal(ptr1);
            byte[] data = null;
            if (result == 0 && feature.featureSize > 0 && feature.feature != IntPtr.Zero)
            {
                data = new byte[1032];
                Marshal.Copy(feature.feature, data, 0, 1032);
                return data;
            }
            else
            {
                return null;
            }
            
        }



        /// <summary>
        /// 人脸对比
        /// </summary>
        /// <returns></returns>
        public float Compare()
        {
            try
            {
                byte[] data1 = getFaceFeature(image1);
                byte[] data2 = getFaceFeature(image2);
                FaceFeature feature1 = new FaceFeature();
                feature1.feature = Marshal.AllocHGlobal(data1.Length);
                Marshal.Copy(data1, 0, feature1.feature, data1.Length);
                feature1.featureSize = data1.Length;
                IntPtr pLocalFeature1 = Marshal.AllocHGlobal(Marshal.SizeOf(feature1));
                Marshal.StructureToPtr(feature1, pLocalFeature1, false);

                FaceFeature feature2 = new FaceFeature();
                feature2.feature = Marshal.AllocHGlobal(data2.Length);
                Marshal.Copy(data2, 0, feature2.feature, data2.Length);
                feature2.featureSize = data2.Length;
                IntPtr pLocalFeature2 = Marshal.AllocHGlobal(Marshal.SizeOf(feature2));
                Marshal.StructureToPtr(feature2, pLocalFeature2, false);
                float similar = 0.0f;

                ASFAPI.ASFFaceFeatureCompare(hEngine, pLocalFeature1, pLocalFeature2, out similar);
                return similar;
            }
            catch (Exception ex)
            {
                return 0.0f;
            }
            
        }

        /// <summary>
        /// 人脸比对
        /// </summary>
        /// <param name="data1">第一张人脸数据</param>
        /// <param name="data2">第二张人脸数据</param>
        /// <returns></returns>
        public float Compare(byte[] data1, byte[] data2)
        {
            try
            {
                FaceFeature feature1 = new FaceFeature();
                feature1.feature = Marshal.AllocHGlobal(data1.Length);
                Marshal.Copy(data1, 0, feature1.feature, data1.Length);
                feature1.featureSize = data1.Length;
                IntPtr pLocalFeature1 = Marshal.AllocHGlobal(Marshal.SizeOf(feature1));
                Marshal.StructureToPtr(feature1, pLocalFeature1, false);

                FaceFeature feature2 = new FaceFeature();
                feature2.feature = Marshal.AllocHGlobal(data2.Length);
                Marshal.Copy(data2, 0, feature2.feature, data2.Length);
                feature2.featureSize = data2.Length;
                IntPtr pLocalFeature2 = Marshal.AllocHGlobal(Marshal.SizeOf(feature2));
                Marshal.StructureToPtr(feature2, pLocalFeature2, false);
                float similar = 0.0f;

                ASFAPI.ASFFaceFeatureCompare(hEngine, pLocalFeature1, pLocalFeature2, out similar);
                return similar;
            }
            catch (Exception ex)
            {
                return 0.0f;
            }
        }
        /// <summary>
        /// 释放资源
        /// </summary>
        public void DisposeResource()
        {
            if (hEngine != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(hEngine);
            }

        }
    }
}
