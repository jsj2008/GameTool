using System.Drawing;
using System.IO;
using System.Net;

namespace PublicUtilities
{
    public class HttpVerifyCodeParser
    {
        public void ParseBitmap(Bitmap sourceBitmap)
        {
            for (int x = 0; x < sourceBitmap.Width; x++)
            {
                for (int y = 0; y < sourceBitmap.Height; y++)
                {
                    this.GetGrayBitmap(sourceBitmap, x, y);
                    this.GetBlackWhiteBitmap(sourceBitmap, x, y);
                }
            }
        }

        private void GetGrayBitmap(Bitmap sourceBitmap, int x, int y)
        {
            //转换灰度的算法
            Color c = sourceBitmap.GetPixel(x, y);
            int luma = (int)(c.R * 0.3 + c.G * 0.59 + c.B * 0.11);
            sourceBitmap.SetPixel(x, y, Color.FromArgb(luma, luma, luma));
        }

        private void GetBlackWhiteBitmap(Bitmap sourceBitmap, int x, int y)
        {
            //转换黑白的算法
            int critical_value = 185;
            Color c = sourceBitmap.GetPixel(x, y);
            if (c.R >= critical_value)
                //sourceBitmap.SetPixel(x, y, Color.FromArgb(0, 0, 0));
                sourceBitmap.SetPixel(x, y, Color.FromArgb(255, 255, 255));
            else
                //sourceBitmap.SetPixel(x, y, Color.FromArgb(255, 255, 255));
                sourceBitmap.SetPixel(x, y, Color.FromArgb(0, 0, 0));
        }


        private void GetFigureBitmap(Bitmap sourceBitmap)
        {
            bool myColumn = false;
            bool charStart = false;
            int charNum = 0;
            int[] widthStartX = new int[sourceBitmap.Width];
            int[] widthEndX = new int[sourceBitmap.Width];

            for (int x = 0; x < sourceBitmap.Width; x++)
            {
                myColumn = true;
                for (int y = 0; y < sourceBitmap.Height; y++)
                {
                    Color c = sourceBitmap.GetPixel(x, y);
                    //第一次出现黑点
                    if (c.R == 0 && !charStart)
                    {
                        widthStartX[charNum] = x;
                        charStart = true;
                        break;
                    }
                    //后续出现黑点
                    if (c.R == 0 && charStart)
                    {
                        myColumn = false;
                        break;
                    }
                }

                //如果当列没有黑点并且前面出现过黑点还没结束
                if (myColumn && charStart && widthStartX[charNum] < x)
                {
                    widthEndX[charNum] = x - 1;
                    charStart = false;
                    charNum++;
                }
                //如果开始出现黑点了,并且最后一列也有黑点
                if (charStart && !myColumn && x == (sourceBitmap.Width - 1))
                {
                    widthEndX[charNum] = x;
                    charStart = false;
                    charNum++;
                }
            }
        }
    }


    public class HttpWebClientHelper
    {
        public void LoadWeb(string url)
        {
            WebClient webClient = new WebClient();
            Stream stream = webClient.OpenRead(url);
            
        }
    }
}
