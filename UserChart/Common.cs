using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserChart
{
    //公共函数类
    public static class Common
    {
        public enum eChangeType
        {
            /// <summary>
            /// 水平
            /// </summary>
            H = 0,
            /// <summary>
            /// 垂直
            /// </summary>
            V = 1,
        }
        //随机颜色
        public static Color RandomColor()
        {
            long tick = DateTime.Now.Ticks;
            Random ran = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
            int R = ran.Next(255);
            int G = ran.Next(255);
            int B = ran.Next(255);
            B = (R + G > 400) ? R + G - 400 : B;//0 : 380 - R - G;
            B = (B > 255) ? 255 : B;
            return Color.FromArgb(R, G, B);
        }
    }

    public class XYMaxMin
    {
        public XYMaxMin(float xmin, float xmax, float ymin, float ymax)
        {
            this.xMin = xmin;
            this.xMax = xmax;
            this.yMin = ymin;
            this.yMax = ymax;
            xsize = xMax - xMin;
            ysize = yMax - yMin;
        }
        public XYMaxMin(float[] vs)
        {
            this.xMin = vs[0];
            this.xMax = vs[1];
            this.yMin = vs[2];
            this.yMax = vs[3];
            xsize = xMax - xMin;
            ysize = yMax - yMin;
        }
        public float xMin;
        public float xMax;
        public float yMin;
        public float yMax;
        public float xsize;
        public float ysize;

        public XYMaxMin Multiple(float mul)
        {
            if (mul > 0)
            {
                return new XYMaxMin(xMin * mul, xMax * mul, yMin * mul, yMax * mul);
            }
            else
            {
                return this;
            }
        }
        public void Multiple(float mul, Common.eChangeType changeType)
        {
            if (mul > 0)
            {
                if (changeType == Common.eChangeType.H)
                {
                    this.xMin *= mul;
                    this.xMax *= mul;
                    xsize = xMax - xMin;
                    ///return new XYMaxMin(xMin * mul, xMax * mul, yMin, yMax);
                }
                else if(changeType== Common.eChangeType.V)
                {
                    this.yMin *= mul;
                    this.yMax *= mul;
                    ysize = yMax - yMin;
                    //return new XYMaxMin(xMin , xMax , yMin* mul, yMax* mul);
                }
                else
                {
                    //return this;
                }
            }
            else
            {
                //return this;
            }
        }
    }
}
