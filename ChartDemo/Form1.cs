using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserChart;

namespace ChartDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LineItem line = userChart1.AddLine();
            comboBox1.Items.Add(line.Name);
            comboBox1.SelectedIndex = comboBox1.Items.Count - 1;
            float[] test = new float[10000];
            Random random = new Random();
            Parallel.For(0, test.Length, i =>
            {
                test[i] = (float)(random.NextDouble() * 1024 - 512);
            });
            userChart1.AddData(line, test);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            userChart1.editLine(comboBox1.Text);
            userChart1.DrawNew();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            userChart1.remove(comboBox1.Text);
            comboBox1.Items.RemoveAt(comboBox1.SelectedIndex);
            userChart1.DrawNew();
        }
    }
}
