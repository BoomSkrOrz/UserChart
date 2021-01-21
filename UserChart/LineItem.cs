using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserChart
{
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
                        for(int i=0;i<poin.Length;++i)
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
}
