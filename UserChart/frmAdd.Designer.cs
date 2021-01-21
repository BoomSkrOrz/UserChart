namespace UserChart
{
    partial class frmAdd
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tBName = new System.Windows.Forms.TextBox();
            this.nBSize = new System.Windows.Forms.NumericUpDown();
            this.ckBValue = new System.Windows.Forms.CheckBox();
            this.ckBLine = new System.Windows.Forms.CheckBox();
            this.bTnOk = new System.Windows.Forms.Button();
            this.cBColor = new User_ColorComboBox_Library1.ColorComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.nBSize)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "名称：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "线条宽度：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "颜色：";
            // 
            // tBName
            // 
            this.tBName.Location = new System.Drawing.Point(71, 4);
            this.tBName.Name = "tBName";
            this.tBName.Size = new System.Drawing.Size(100, 21);
            this.tBName.TabIndex = 1;
            // 
            // nBSize
            // 
            this.nBSize.Location = new System.Drawing.Point(71, 31);
            this.nBSize.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nBSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nBSize.Name = "nBSize";
            this.nBSize.Size = new System.Drawing.Size(100, 21);
            this.nBSize.TabIndex = 2;
            this.nBSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // ckBValue
            // 
            this.ckBValue.AutoSize = true;
            this.ckBValue.Location = new System.Drawing.Point(111, 89);
            this.ckBValue.Name = "ckBValue";
            this.ckBValue.Size = new System.Drawing.Size(60, 16);
            this.ckBValue.TabIndex = 4;
            this.ckBValue.Text = "显示值";
            this.ckBValue.UseVisualStyleBackColor = true;
            // 
            // ckBLine
            // 
            this.ckBLine.AutoSize = true;
            this.ckBLine.Checked = true;
            this.ckBLine.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckBLine.Location = new System.Drawing.Point(14, 89);
            this.ckBLine.Name = "ckBLine";
            this.ckBLine.Size = new System.Drawing.Size(72, 16);
            this.ckBLine.TabIndex = 4;
            this.ckBLine.Text = "显示线条";
            this.ckBLine.UseVisualStyleBackColor = true;
            // 
            // bTnOk
            // 
            this.bTnOk.Location = new System.Drawing.Point(96, 111);
            this.bTnOk.Name = "bTnOk";
            this.bTnOk.Size = new System.Drawing.Size(75, 23);
            this.bTnOk.TabIndex = 5;
            this.bTnOk.Text = "确定";
            this.bTnOk.UseVisualStyleBackColor = true;
            this.bTnOk.Click += new System.EventHandler(this.bTnOk_Click);
            // 
            // cBColor
            // 
            this.cBColor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cBColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBColor.FormattingEnabled = true;
            this.cBColor.Items.AddRange(new object[] {
            "ActiveBorder",
            "ActiveCaption",
            "ActiveCaptionText",
            "AppWorkspace",
            "Control",
            "ControlDark",
            "ControlDarkDark",
            "ControlLight",
            "ControlLightLight",
            "ControlText",
            "Desktop",
            "GrayText",
            "Highlight",
            "HighlightText",
            "HotTrack",
            "InactiveBorder",
            "InactiveCaption",
            "InactiveCaptionText",
            "Info",
            "InfoText",
            "Menu",
            "MenuText",
            "ScrollBar",
            "Window",
            "WindowFrame",
            "WindowText",
            "Transparent",
            "AliceBlue",
            "AntiqueWhite",
            "Aqua",
            "Aquamarine",
            "Azure",
            "Beige",
            "Bisque",
            "Black",
            "BlanchedAlmond",
            "Blue",
            "BlueViolet",
            "Brown",
            "BurlyWood",
            "CadetBlue",
            "Chartreuse",
            "Chocolate",
            "Coral",
            "CornflowerBlue",
            "Cornsilk",
            "Crimson",
            "Cyan",
            "DarkBlue",
            "DarkCyan",
            "DarkGoldenrod",
            "DarkGray",
            "DarkGreen",
            "DarkKhaki",
            "DarkMagenta",
            "DarkOliveGreen",
            "DarkOrange",
            "DarkOrchid",
            "DarkRed",
            "DarkSalmon",
            "DarkSeaGreen",
            "DarkSlateBlue",
            "DarkSlateGray",
            "DarkTurquoise",
            "DarkViolet",
            "DeepPink",
            "DeepSkyBlue",
            "DimGray",
            "DodgerBlue",
            "Firebrick",
            "FloralWhite",
            "ForestGreen",
            "Fuchsia",
            "Gainsboro",
            "GhostWhite",
            "Gold",
            "Goldenrod",
            "Gray",
            "Green",
            "GreenYellow",
            "Honeydew",
            "HotPink",
            "IndianRed",
            "Indigo",
            "Ivory",
            "Khaki",
            "Lavender",
            "LavenderBlush",
            "LawnGreen",
            "LemonChiffon",
            "LightBlue",
            "LightCoral",
            "LightCyan",
            "LightGoldenrodYellow",
            "LightGray",
            "LightGreen",
            "LightPink",
            "LightSalmon",
            "LightSeaGreen",
            "LightSkyBlue",
            "LightSlateGray",
            "LightSteelBlue",
            "LightYellow",
            "Lime",
            "LimeGreen",
            "Linen",
            "Magenta",
            "Maroon",
            "MediumAquamarine",
            "MediumBlue",
            "MediumOrchid",
            "MediumPurple",
            "MediumSeaGreen",
            "MediumSlateBlue",
            "MediumSpringGreen",
            "MediumTurquoise",
            "MediumVioletRed",
            "MidnightBlue",
            "MintCream",
            "MistyRose",
            "Moccasin",
            "NavajoWhite",
            "Navy",
            "OldLace",
            "Olive",
            "OliveDrab",
            "Orange",
            "OrangeRed",
            "Orchid",
            "PaleGoldenrod",
            "PaleGreen",
            "PaleTurquoise",
            "PaleVioletRed",
            "PapayaWhip",
            "PeachPuff",
            "Peru",
            "Pink",
            "Plum",
            "PowderBlue",
            "Purple",
            "Red",
            "RosyBrown",
            "RoyalBlue",
            "SaddleBrown",
            "Salmon",
            "SandyBrown",
            "SeaGreen",
            "SeaShell",
            "Sienna",
            "Silver",
            "SkyBlue",
            "SlateBlue",
            "SlateGray",
            "Snow",
            "SpringGreen",
            "SteelBlue",
            "Tan",
            "Teal",
            "Thistle",
            "Tomato",
            "Turquoise",
            "Violet",
            "Wheat",
            "White",
            "WhiteSmoke",
            "Yellow",
            "YellowGreen",
            "ButtonFace",
            "ButtonHighlight",
            "ButtonShadow",
            "GradientActiveCaption",
            "GradientInactiveCaption",
            "MenuBar",
            "MenuHighlight"});
            this.cBColor.Location = new System.Drawing.Point(71, 61);
            this.cBColor.Name = "cBColor";
            this.cBColor.SelectColor = System.Drawing.Color.Red;
            this.cBColor.SelectColorName = "Red";
            this.cBColor.Size = new System.Drawing.Size(100, 22);
            this.cBColor.TabIndex = 6;
            // 
            // frmAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(180, 140);
            this.Controls.Add(this.cBColor);
            this.Controls.Add(this.bTnOk);
            this.Controls.Add(this.ckBLine);
            this.Controls.Add(this.ckBValue);
            this.Controls.Add(this.nBSize);
            this.Controls.Add(this.tBName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximumSize = new System.Drawing.Size(196, 179);
            this.MinimumSize = new System.Drawing.Size(196, 179);
            this.Name = "frmAdd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "添加";
            ((System.ComponentModel.ISupportInitialize)(this.nBSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tBName;
        private System.Windows.Forms.NumericUpDown nBSize;
        private System.Windows.Forms.CheckBox ckBValue;
        private System.Windows.Forms.CheckBox ckBLine;
        private System.Windows.Forms.Button bTnOk;
        private User_ColorComboBox_Library1.ColorComboBox cBColor;
    }
}