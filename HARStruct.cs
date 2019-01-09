using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HAF_2_0
{
    /// <summary>
    /// 单人脸信息
    /// </summary>
    public struct SingleFaceInfo
    {
        /// <summary>
        /// 人脸框
        /// </summary>
        public Mrect faceRect;

        /// <summary>
        /// 人脸角度
        /// </summary>
        public int faceOrient;

    }

    /// <summary>
    /// 多人脸信息
    /// </summary>
    public struct MultiFaceInfo
    {
        /// <summary>
        /// 人脸框数组
        /// </summary>
        public IntPtr faceRect;

        /// <summary>
        /// 人脸角度数组
        /// </summary>
        public IntPtr faceOrient;             

       
        /// <summary>
        /// 检测到的人脸个数
        /// </summary>
        public int faceNum;  
    }

    /// <summary>
    /// 年龄信息
    /// </summary>
    public struct AgeInfo
    {
        /// <summary>
        /// 年龄结果
        /// </summary>
        public IntPtr ageArray;

        /// <summary>
        /// 检测到人脸的个数
        /// </summary>
        public int num;


    }

    /// <summary>
    /// 性别信息
    /// </summary>
    public struct GenderInfo
    {
        /// <summary>
        /// 0男，1女，-1未知
        /// </summary>
        public IntPtr genderArray;

        /// <summary>
        /// 检测到的人脸的个数
        /// </summary>
        public int num;
    }


    /// <summary>
    /// 人脸特征信息
    /// </summary>
    public struct FaceFeature
    {
        /// <summary>
        /// 特征信息
        /// </summary>
        public IntPtr feature;

        /// <summary>
        /// 人脸特征的长度
        /// </summary>
        public int featureSize;


    }

    /// <summary>
    /// 3D角度信息
    /// </summary>
    public struct ThreeDAngle
    {
        /// <summary>
        /// 横滚角度
        /// </summary>
        public IntPtr roll;

        /// <summary>
        /// 偏航角度
        /// </summary>
        public IntPtr yaw;

        /// <summary>
        /// 俯仰角度
        /// </summary>
        public IntPtr pitch;

        /// <summary>
        /// 0为正常
        /// </summary>
        public IntPtr status;

        /// <summary>
        /// 检测到人脸的个数
        /// </summary>
        public int num;
    }

    /// <summary>
    /// 人脸框信息
    /// </summary>
    public struct Mrect
    {
        /// <summary>
        /// 左距离
        /// </summary>
        public int left;

        /// <summary>
        /// 上距离
        /// </summary>
        public int top;

        /// <summary>
        /// 右距离
        /// </summary>
        public int right;

        /// <summary>
        /// 下距离
        /// </summary>
        public int bottom;
    }


}
