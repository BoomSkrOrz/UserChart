using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserChart
{
    public partial class frmAdd : Form
    {
        public LineItem lineItem;
        public frmAdd()
        {
            InitializeComponent();
            lineItem = new LineItem();
        }

        public frmAdd(LineItem line)
        {
            InitializeComponent();
            lineItem = line;
            tBName.Text=lineItem.Name;
            nBSize.Value=(decimal)lineItem.Size;
            cBColor.SelectColor=lineItem.LineColor;
            ckBLine.Checked=lineItem.IsShow;
            ckBValue.Checked=lineItem.DisplayValue;
        }
        private void bTnOk_Click(object sender, EventArgs e)
        {
            lineItem.Name = tBName.Text;
            lineItem.Size = (float)nBSize.Value;
            lineItem.LineColor = cBColor.SelectColor;
            lineItem.IsShow = ckBLine.Checked;
            lineItem.DisplayValue = ckBValue.Checked;
            this.DialogResult = DialogResult.OK;           
        }
    }
}
