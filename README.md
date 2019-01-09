HRFace2_0
虹软开发2.0封装
本类库用于虹软人脸识别库C#语言封装使用
激活：
ResultCode result = EngineActivate.ActivateEngine(string appId, string appKey)

--appid和appkey在官网获取 -- result是一个枚举的状态码

获取引擎：
IntPtr engine = EngineFactory.GetEngineInstance(
uint mode,DetectionOrientPriority orientPriority, int detectFaceScaleVal = 12)
--engine是引擎
--mode可以根据EngineFactory.Video或者EngineFactory.Image设置是图像还是视频，目前只支持图像。
-- orientPriority是枚举
-- detectFaceScaleVal可以不填

释放引擎：
Bool result = EngineFactory.DisposeEngine()
人脸个数检测：
1.初始化人脸检测器：

public FaceDetection(IntPtr hEngine, Bitmap image)

-- hEngine就是获取的引擎
--image，bitmap格式的图片，不需要提前处理图片大小，内部有处理操作
2.获取人脸数量

public int FindFaceNum()

返回人脸数量
人脸年龄检测：
1.初始化人脸检测器：

public FaceDetection(IntPtr hEngine, Bitmap image)

-- hEngine就是获取的引擎
--image，bitmap格式的图片，不需要提前处理图片大小，内部有处理操作
2.获取人脸年龄

public int GetAge()
返回人脸年龄
人脸性别检测：
1.初始化人脸检测器：

public FaceDetection(IntPtr hEngine, Bitmap image)

-- hEngine就是获取的引擎
--image，bitmap格式的图片，不需要提前处理图片大小，内部有处理操作
2.获取人脸性别

public string GetGender()

返回人脸性别
人脸相似度对比：
方式一：

1.初始化人脸检测器：

public FaceDetection(IntPtr hEngine, Bitmap image1, Bitmap image2)
-- hEngine就是获取的引擎
--image1，bitmap格式的图片，不需要提前处理图片大小，内部有处理操作
--image2，bitmap格式的图片，不需要提前处理图片大小，内部有处理操作
2.返回相似度

public float Compare()
方式二：

返回相似度(直接对比)

public float Compare(byte[] data1, byte[] data2)

--data1是人脸图像数据，大小1032
--data2是人脸图像数据，大小1032
