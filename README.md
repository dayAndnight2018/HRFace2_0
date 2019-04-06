<h1>A dll for ArcSoft face-detection engine version 2.0</h1>



<p>A dll for ArcSoft face-detection engine version 2.0 based on C# Language</p>

<pre>
<h2>Engine Activation:</h2>
ResultCode result = EngineActivate.ActivateEngine(string appId, string appKey)

-- appid and appkey could be applied from the <a href="https://www.arcsoft.com.cn/">https://www.arcsoft.com.cn/</a>
-- The type of result object is an enum to identify the result
</pre>

<pre>
<h2>Engine Obtain:</h2>
IntPtr engine = EngineFactory.GetEngineInstance(
uint mode,DetectionOrientPriority orientPriority, int detectFaceScaleVal = 12)
-- engine stands for face-detection engine
-- mode could be  EngineFactory.Video(for video) or EngineFactory.Image(for image)
-- orientPriority is also an enum
-- detectFaceScaleVal is optional
</pre>

<pre>
<h2>Engine Free:</h2>
Bool result = EngineFactory.DisposeEngine()
</pre>

<pre>
<h2>Face-num Detection:</h2>
Step 1: Init the Engine

public FaceDetection(IntPtr hEngine, Bitmap image)

-- hEngine is the engine mentioned above
-- image, Bitmap image is available, and no need predealling

Step 2: Get Face-num

public int FindFaceNum()

return the number of faces
</pre>

<pre>
<h2>Age Detection:</h2>
Step 1: Init the Engine

public FaceDetection(IntPtr hEngine, Bitmap image)

-- hEngine is the engine mentioned above
-- image, Bitmap image is available, and no need predealling

Step 2: Get the age

public int GetAge()
return the age 
</pre>

<pre>
<h2>Gender Detection</h2>
Step 1: Init the Engine

public FaceDetection(IntPtr hEngine, Bitmap image)

-- hEngine is the engine mentioned above
-- image, Bitmap image is available, and no need predealling

Step 2: Get the gender

public string GetGender()

return the gender
</pre>

<pre>
<h2>Face similiar Detection</h2>
Type 1:

Step 1: Init the Engine

public FaceDetection(IntPtr hEngine, Bitmap image1, Bitmap image2)
-- hEngine is the engine mentioned above
-- image1，Bitmap image is available, and no need predealling
-- image2，Bitmap image is available, and no need predealling

Step 2: Get the similiar

public float Compare()

Type 2:

Compare the feature stored already

public float Compare(byte[] data1, byte[] data2)

-- data1 is the first face feature of size 1032 bytes
-- data1 is the second face feature of size 1032 bytes
</pre>
