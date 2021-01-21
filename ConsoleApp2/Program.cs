using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            float[] test = new float[1000];
            Random random = new Random();
            Parallel.For(0, test.Length, i =>
            {
                test[i] = (float)(random.NextDouble() * 1024 - 512);
            });
            using (Draw draw = new Draw())
            {
                LineItem line = new LineItem(name: "test");
                draw.AddLine(line);
                draw.AddData(line, test);
                draw.ReFlush($"C:\\{DateTime.Now:yyyyMMddHHmmss}.jpg");
            }
        }
    }

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
                else if (changeType == Common.eChangeType.V)
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


    public class LineItem
    {
        #region 值
        private string name = string.Empty;
        public string Name
        {
            set
            {
                name = value;
                PaintName?.Invoke(); ;
            }
            get => name;
        }

        private Color lineColor;
        public Color LineColor
        {
            set
            {
                lineColor = value;
                PaintLine?.Invoke(); ;
            }
            get => lineColor;
        }

        private float size = 1;
        public float Size
        {
            set
            {
                size = value;
                PaintLine?.Invoke(); ;
            }
            get => size;
        }
        private bool displayValue = false;
        public bool DisplayValue
        {
            set
            {
                displayValue = value;
                PaintLine?.Invoke(); ;
            }
            get => displayValue;
        }
        private bool isShow = true;
        public bool IsShow
        {
            set
            {
                isShow = value;
                PaintLine?.Invoke(); ;
            }
            get => isShow;
        }

        private Bitmap imageBuffer = null;
        public Bitmap ImageLine
        {
            get => imageBuffer;
            set
            {
                if (value != null)
                {
                    imageBuffer.Dispose();
                    imageBuffer = value;
                }
            }
        }
        private List<PointF> points = new List<PointF>(4096);
        public List<PointF> Points
        {
            //private set => points = value;
            get => points;
        }
        #endregion

        #region 方法
        public LineItem()
        {
            lineColor = Common.RandomColor();
        }
        public LineItem(string name) : this()
        {
            this.name = name;
        }
        public LineItem(string name, IEnumerable<float> data) : this(name)
        {
            foreach (var x in data)
            {
                points.Add(new PointF(points.Count, x));
            }
        }
        public LineItem(string name, PointF[] data) : this(name)
        {
            points.AddRange(data);
        }
        public void AddData(IEnumerable<float> data)
        {
            foreach (var x in data)
            {
                points.Add(new PointF(points.Count, x));
            }
        }

        public void AddData(PointF[] data)
        {
            points.AddRange(data);
        }
        public void GetLineImage(int Width, int Height, XYMaxMin size)
        {
            if (points.Count < 2) return;
            imageBuffer?.Dispose();
            imageBuffer = new Bitmap(width: Width, height: Height);
            using (Graphics g = Graphics.FromImage(imageBuffer))
            {
                float xsale = Width / size.xsize;
                float ysale = Height / size.ysize;
                PointF[] poin = points.ToArray();
                Parallel.For(0, poin.Length, i =>
                {
                    poin[i].X = (poin[i].X - size.xMin) * xsale;
                    poin[i].Y = Height - (poin[i].Y - size.yMin) * ysale;
                });
                using (Pen pline = new Pen(lineColor))
                {
                    g.DrawLines(pline, poin);
                }
                if (displayValue)
                {
                    using (SolidBrush strBrush = new SolidBrush(Color.Black))
                    {
                        for (int i = 0; i < poin.Length; ++i)
                        {
                            g.DrawString(points[i].Y.ToString("f2"), new Font("宋体", 7), strBrush, poin[i]);

                        }
                    }
                }
                g.Save();
            }
        }

        #endregion

        #region 事件
        public event Action PaintName;
        public event Action PaintLine;
        #endregion

    }
    public class Draw : IDisposable
    {
        #region 值
        public Color colorAxis = Color.Green;
        private int sumXScale = 20;
        private int sumYScale = 15;
        private List<LineItem> lines = new List<LineItem>(16);
        private Bitmap bLegend = null;
        private Bitmap bUnderground = null;
        private Bitmap bLines = null;
        private XYMaxMin xySize;
        private Point pointMouse = new Point(0, 0);
        private bool isLeftMouseDown = false;
        private DateTime dateTime;
        private string chartName = string.Empty;
        public string ChartName
        {
            set => chartName = value;
            get => chartName;
        }
        private string xName = string.Empty;
        public string XName
        {
            set => xName = value;
            get => xName;
        }
        private string yName = string.Empty;
        public string YName
        {
            set => yName = value;
            get => yName;
        }
        public List<LineItem> Lines
        {
            get => lines;
        }
        public int Width { set; get; } = 1920;
        public int Height { set; get; } = 1080;
        #endregion

        //背景
        public bool GetBackground(ref Bitmap bkgrnd)
        {
            try
            {
                Graphics g = Graphics.FromImage(bkgrnd);
                Pen penAxis = new Pen(colorAxis, 4);
                Pen penScale = new Pen(colorAxis, 2);
                g.DrawLine(penAxis, 0, 0, 0, bkgrnd.Height);//Y轴
                g.DrawLine(penAxis, 0, bkgrnd.Height, bkgrnd.Width, bkgrnd.Height);//x轴
                //刻度
                int sumyscale = (sumYScale > 8) ? sumYScale : 8;
                int sumxscale = (sumXScale > 10) ? sumXScale : 10;
                //y
                for (int i = 1; i < sumyscale; ++i)
                {
                    g.DrawLine(penScale, 0, bkgrnd.Height - i * (bkgrnd.Height / sumyscale), 5, bkgrnd.Height - i * (bkgrnd.Height / sumyscale));
                }
                //x
                for (int i = 1; i < sumxscale; ++i)
                {
                    g.DrawLine(penScale, i * (bkgrnd.Width / sumxscale), bkgrnd.Height, i * (bkgrnd.Width / sumxscale), bkgrnd.Height - 5);
                }

                g.Save();
                penAxis.Dispose();
                penScale.Dispose();
                g.Dispose();
                return true;
            }
            catch
            {
                return false;
            }
        }
        //图例
        public bool GetLegend(ref Bitmap legend)
        {
            try
            {
                using (Graphics g = Graphics.FromImage(legend))
                {
                    using (Pen pen = new Pen(Color.Black, 1))
                    {
                        using (SolidBrush strBrush = new SolidBrush(Color.Black))
                        {
                            int y = 10;
                            for (int i = 0; i < lines.Count; ++i)
                            {
                                using (SolidBrush colorBrush = new SolidBrush(lines[i].LineColor))
                                {
                                    Rectangle rectangle = new Rectangle(10, y, 15, 10);
                                    g.DrawRectangle(pen, rectangle);
                                    g.FillRectangle(colorBrush, rectangle);
                                    if (!string.IsNullOrWhiteSpace(lines[i].Name))
                                    {
                                        y += 20;
                                        continue;
                                    }
                                    else
                                    {
                                        if (lines[i].Name.Length == 0)
                                        {
                                            y += 20;
                                            continue;
                                        }
                                        for (int j = 0; j < Math.Ceiling(lines[i].Name.Length / 4.0); ++j)
                                        {
                                            g.DrawString(lines[i].Name.Substring(j * 4, ((lines[i].Name.Length - (j * 4)) >= 4) ? 4 : lines[i].Name.Length - (j * 4)), new Font("宋体", 9), strBrush, 30, y);
                                            y += 20;
                                        }
                                    }


                                }
                            }
                        }
                    }
                    g.Save();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool GetImageLines(ref Bitmap imageLines)
        {
            try
            {
                using (Graphics g = Graphics.FromImage(imageLines))
                {
                    using (Pen pen = new Pen(Color.Black, 1))
                    {
                        for (int i = 0; i < lines.Count; ++i)
                        {

                        }
                    }
                    g.Save();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private float[] GetMaxMinValue()
        {
            float[] value = { 0, 0, 0, 0 };//x0,x1,y0,y1
            if (lines.Count == 0)
            {
                value[0] = 0;
                value[1] = 10;
                value[2] = 0;
                value[3] = 10;
            }
            else
            {
                foreach (var line in lines)
                {
                    if (line.Points.Count < 3) continue;
                    float x0 = line.Points.Min(i => i.X);
                    float x1 = line.Points.Max(i => i.X);
                    float y0 = line.Points.Min(i => i.Y);
                    float y1 = line.Points.Max(i => i.Y);
                    value[0] = (value[0] > x0) ? x0 : value[0];
                    value[1] = (value[1] > x1) ? value[1] : x1;
                    value[2] = (value[2] > y0) ? y0 : value[2];
                    value[3] = (value[3] > y1) ? value[3] : y1;
                }
            }
            return value;
        }


        public void ReFlush(string path, ImageFormat imageFormat = null)
        {
            if (xySize == null) return;
            bLegend?.Dispose();
            bUnderground?.Dispose();
            bUnderground = new Bitmap(Width - 150, Height - 70);
            bLegend = new Bitmap(80, Height);
            bool x = GetBackground(ref bUnderground);
            bool y = GetLegend(ref bLegend);
            using (Bitmap bmp = new Bitmap(Width, Height))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.DrawImage(bUnderground, new Point(40, 30));
                    lines.ForEach(i =>
                    {
                        //i.DisplayValue = true;
                        i.GetLineImage(Width - 150, Height - 70, xySize);
                        if (i.ImageLine != null && i.IsShow)
                        {
                            g.DrawImage(i.ImageLine, new Point(40, 30));
                        }
                    });
                    g.DrawImage(bLegend, new Point(60 + bUnderground.Width, 10));
                    //图例字体颜色 自己调
                    using (SolidBrush strBrush = new SolidBrush(Color.White))
                    {
                        g.DrawString(chartName, new Font("宋体", 27), strBrush, (Width - 150) / 2 - 40, 0);
                        g.DrawString(xName, new Font("宋体", 15), strBrush, Width - 170, Height - 25);
                        g.DrawString(yName, new Font("宋体", 15), strBrush, 10, 15, new StringFormat(StringFormatFlags.DirectionVertical));
                        drawScale(Width, Height, g, strBrush, xySize);
                    }
                    g.Save();
                }
                bmp.Save(path, imageFormat ?? ImageFormat.Jpeg);
            }
        }

        public void LineChanged()
        {
            xySize = new XYMaxMin(GetMaxMinValue());
        }

        private void drawScale(int Width, int Height, Graphics g, Brush brush, XYMaxMin size)
        {
            //刻度
            int sumyscale = (sumYScale > 8) ? sumYScale : 8;
            int sumxscale = (sumXScale > 10) ? sumXScale : 10;
            //y
            for (int i = 1; i < sumyscale; ++i)
            {
                g.DrawString((size.ysize / sumyscale * i + size.yMin).ToString("f1"), new Font("宋体", 8), brush, 5, (Height - 45) - i * ((Height - 70) / sumyscale));
            }
            //x
            for (int i = 1; i < sumxscale; ++i)
            {
                g.DrawString(Math.Ceiling((size.xsize / sumxscale * i) + size.xMin).ToString("f1"), new Font("宋体", 8), brush, i * ((Width - 150) / sumxscale) + 20, Height - 35);
            }
        }
        public void AddLine(LineItem line)
        {
            lines.Add(line);
            LineChanged();
        }
        public LineItem remove(string name)
        {
            LineItem line = lines.Find(i => i.Name == name);
            lines.Remove(line);
            return line;
        }
        public void editLine(LineItem line)
        {
            LineItem value = lines.Find(i => i.Name == line.Name);
            value = line;
        }

        public void AddData(LineItem line, IEnumerable<float> data)
        {
            if (lines.Contains(line))
            {
                line.AddData(data);
                this.LineChanged();
            }

        }
        public void AddData(LineItem line, PointF[] data)
        {
            if (lines.Contains(line))
            {
                line.AddData(data);
                this.LineChanged();
            }
        }

        public void Dispose()
        {
            lines?.Clear();
            bLegend.Dispose();
            bUnderground?.Dispose();
            bLines?.Dispose();
        }
    }
}





