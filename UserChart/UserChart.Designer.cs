namespace UserChart
{
    partial class UserChart
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // UserChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.MinimumSize = new System.Drawing.Size(160, 80);
            this.Name = "UserChart";
            this.Size = new System.Drawing.Size(516, 264);
            this.Load += new System.EventHandler(this.UserChart_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.UserChart_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.UserChart_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.UserChart_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.UserChart_MouseUp);
            this.Resize += new System.EventHandler(this.UserChart_Resize);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
