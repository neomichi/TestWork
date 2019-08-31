using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using Raskadrovka.Models;

namespace Raskadrovka.Code
{
    public static class Helper
    {

        /// <summary>
        /// получаем список с добавленными картинками 
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ImgFile> GetImgList()
        {
            var path = HostingEnvironment.MapPath(Constant.ImageWebpath);
            var dir = new DirectoryInfo(path);

            var imgList=new List<ImgFile>();

            if (dir.Exists)
            {
                imgList = dir.GetFiles().Select(x => new ImgFile
                { 
                    Name =string.Format("{0}/{1}",Constant.ImageWebpath, x.Name),
                    W = Image.FromFile(x.FullName).Width,
                    H = Image.FromFile(x.FullName).Height
                }).ToList();
            }
            return imgList;
        }


        /// <summary>
        /// вычесление коафицентов методом гаусса-жордана
        /// </summary>
        /// <param name="imgList">список картинок</param>
        /// <param name="w">ширина</param>
        /// <returns></returns>
        public static float[] GausJordan(ImgFile[] imgList,int w)
        {

            var n = imgList.Count();
            var coaf = new float[n, n];
            var res = new float[n];
            for (var i = 0; i < n; i++)
            {
                coaf[0, i] = imgList[i].W;
                if (i == 0) res[i] = w;
                else res[i] = 0;
            }
            for (var i = 0; i < n - 1; i++)
            {
                coaf[i + 1, 0] = imgList[0].H;
                coaf[i + 1, i + 1] = -imgList[i + 1].H;
            }
            var a = coaf;
            var b = res;

            for (var i = 0; i < n - 1; i++)
            {
                for (var j = i + 1; j < n; j++)
                {
                    var s = a[j, i] / a[i, i];
                    for (var k = i; k < n; k++)
                    {
                        a[j, k] -= a[i, k] * s;
                    }
                    b[j] -= b[i] * s;
                }
            }

            for (var i = n - 1; i >= 0; i--)
            {
                b[i] /= a[i, i];
                a[i, i] /= a[i, i];
                for (var j = i - 1; j >= 0; j--)
                {
                    var s = a[j, i] / a[i, i];
                    a[j, i] -= s;
                    b[j] -= b[i] * s;
                }
            }
            return Enumerable.Range(0, n).Select(i => b[i] / a[i, i]).ToArray();
        }
       
        
        
        
        
        /// <summary>
        /// изменить размер и сохранить с новым именем в jpg
        /// </summary>
        /// <param name="image"></param>
        /// <param name="width">новое width</param>
        /// <param name="height">новый height </param>
        /// <returns></returns>
        public static void ResizeImage(Image image, int width, int height,string path)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
                
                destImage.Save(path, ImageFormat.Jpeg);
            }
            
        }


        

    }
}