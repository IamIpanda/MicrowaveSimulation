using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Smith_Chart
{
    public partial class SmithChart : UserControl
    {
        public SmithChart()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.Selectable, false);
            Help.Draw.Target = this;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Help.Draw.G = e.Graphics;
            Help.Draw.RedrawAll();
        }
    }
}
