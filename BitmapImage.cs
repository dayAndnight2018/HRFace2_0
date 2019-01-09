using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HAF_2_0
{
    /// <summary>
    /// 图像信息类
    /// </summary>
    public class BitmapImage
    {

        private Bitmap image;

        /// <summary>
        /// 宽度
        /// </summary>
        public int Width{get;set;}

        /// <summary>
        /// 高度
        /// </summary>
        public int Height{get;set;}

        /// <summary>
        /// 格式（默认513）
        /// </summary>
        public int Format { get; set; }

        /// <summary>
        /// 图像数据
        /// </summary>
        public byte[] ImageData { get; set; }

        /// <summary>
        /// 使用Bitmap构造
        /// </summary>
        /// <param name="image"></param>
        public BitmapImage(Bitmap image)
        {
            this.image = image;
        }

        /// <summary>
        /// 图像数据构造
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="imageData"></param>
        public BitmapImage(int width, int height, byte[] imageData)
        {
            this.Width = width;
            this.Height = height;
            this.Format = 0x201;
            this.ImageData = imageData;
        }

        /// <summary>
        /// 解析图片的信息
        /// </summary>
        public void  ParseImage()
        {
            if (image != null)
            {
                int tempwidth = 0;

                if (image.Width % 4 != 0)
                {
                    tempwidth = image.Width / 4 * 4;
                }
                else
                {
                    tempwidth = image.Width;
                }

                BitmapData data = image.LockBits(new Rectangle(0, 0, tempwidth, image.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

                IntPtr ptr = data.Scan0;
                int soureBitArrayLength = data.Height * Math.Abs(data.Stride);

                byte[] sourceBitArray = new byte[soureBitArrayLength];

                //将bitmap中的内容拷贝到ptr_bgr数组中
                Marshal.Copy(ptr, sourceBitArray, 0, soureBitArrayLength);

                //Marshal.FreeHGlobal(ptr);

                Width = data.Width;
                Height = data.Height;
                Format = 0x201;
                int pitch = Math.Abs(data.Stride);

                int line = Width * 3;
                int bgr_len = line * Height;

                ImageData = new byte[bgr_len];
                for (int i = 0; i < Height; ++i)
                {
                    Array.Copy(sourceBitArray, i * pitch, ImageData, i * line, line);
                }
                image.UnlockBits(data);
            }

        }

    }
}
