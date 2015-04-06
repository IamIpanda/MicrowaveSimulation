namespace Smith_Chart
{
    partial class Form1
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.smithChart1 = new Smith_Chart.SmithChart();
            this.SuspendLayout();
            // 
            // smithChart1
            // 
            this.smithChart1.Dock = System.Windows.Forms.DockStyle.Left;
            this.smithChart1.Location = new System.Drawing.Point(0, 0);
            this.smithChart1.Name = "smithChart1";
            this.smithChart1.Size = new System.Drawing.Size(786, 640);
            this.smithChart1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(912, 640);
            this.Controls.Add(this.smithChart1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private SmithChart smithChart1;
    }
}

