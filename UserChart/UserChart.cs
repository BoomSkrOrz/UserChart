using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace UserChart
{
    public partial class UserChart : UserControl
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
        #endregion

        #region     
        public UserChart()
        {
            InitializeComponent();
            this.MouseWheel += UserChart_MouseWheel;
            dateTime = DateTime.Now;
        }

        private void UserChart_MouseWheel(object sender, MouseEventArgs e)
        {
            DateTime dt = DateTime.Now;
            if ((dt - dateTime).TotalMilliseconds < 100)
            {
                return;
            }
            if (e.X < 40)
            {
                if (e.Delta > 0)
                {
                    xySize.Multiple(1.05f, Common.eChangeType.V);
                }
                else
                {
                    xySize.Multiple(0.95f, Common.eChangeType.V);
                }
            }
            if (e.Y > Height - 70)
            {
                if (e.Delta > 0)
                {
                    xySize.Multiple(1.05f, Common.eChangeType.H);
                }
                else
                {
                    xySize.Multiple(0.95f, Common.eChangeType.H);
                }
            }
            this.Refresh();
            dateTime = DateTime.Now;
        }

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

        private void UserChart_Resize(object sender, EventArgs e)
        {
            this.Refresh();
        }

        public void ReFlush()
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
                    using (SolidBrush strBrush = new SolidBrush(Color.Black))
                    {
                        g.DrawString(chartName, new Font("宋体", 27), strBrush, (Width - 150) / 2 - 40, 0);
                        g.DrawString(xName, new Font("宋体", 15), strBrush, Width - 170, Height - 25);
                        g.DrawString(yName, new Font("宋体", 15), strBrush, 10, 15, new StringFormat(StringFormatFlags.DirectionVertical));
                        drawScale(g, strBrush, xySize);
                    }
                    g.Save();
                }
                using (Graphics g = this.CreateGraphics())
                {
                    g.Clear(this.BackColor);
                    g.DrawImage(bmp, 0, 0);
                }
            }
        }

        private void UserChart_Paint(object sender, PaintEventArgs e)
        {
            ReFlush();
        }

        public void LineChanged()
        {
            xySize = new XYMaxMin(GetMaxMinValue());
        }

        private void drawScale(Graphics g, Brush brush, XYMaxMin size)
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

        private void UserChart_Load(object sender, EventArgs e)
        {
            
        }

        private void UserChart_MouseMove(object sender, MouseEventArgs e)
        {
            if (isLeftMouseDown)
            {
                DateTime dt = DateTime.Now;
                if ((dt - dateTime).TotalMilliseconds < 100)
                {
                    return;
                }
                xySize.xMax += ((e.X - pointMouse.X) / xySize.xsize * 200);
                xySize.xMin -= ((e.X - pointMouse.X) / xySize.xsize * 200);
                xySize.yMax -= ((e.Y - pointMouse.Y) / xySize.ysize * 500);
                xySize.yMin += ((e.Y - pointMouse.Y) / xySize.ysize * 500);
                this.Refresh();
                dateTime = DateTime.Now;
            }
        }

        private void UserChart_MouseDown(object sender, MouseEventArgs e)
        {
            pointMouse.X = e.X;
            pointMouse.Y = e.Y;
            isLeftMouseDown = true;
        }

        private void UserChart_MouseUp(object sender, MouseEventArgs e)
        {
            isLeftMouseDown = false;
        }

        public LineItem AddLine()
        {
            frmAdd frm = new frmAdd();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                lines.Add(frm.lineItem);
                LineChanged();
                this.Refresh();
            }
            return frm.lineItem;
        }
        public LineItem remove(string name)
        {
            LineItem line = lines.Find(i => i.Name == name);
            lines.Remove(line);
            return line;
        }
        public LineItem editLine(string name)
        {
            LineItem line = lines.Find(i => i.Name == name);
            frmAdd frm = new frmAdd(line);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LineChanged();
                this.Refresh();
            }
            return line;
        }

        public void AddData(LineItem line, IEnumerable<float> data)
        {
            if (lines.Contains(line))
            {
                line.AddData(data);
                this.LineChanged();
                this.Refresh();
            }

        }
        public void AddData(LineItem line, PointF[] data)
        {
            if (lines.Contains(line))
            {
                line.AddData(data);
                DrawNew();
            }
        }

        public void DrawNew()
        {
            this.LineChanged();
            this.Refresh();
        }
        #endregion
    }
}
