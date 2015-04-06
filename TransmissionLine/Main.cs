using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Smith_Chart.Model;

namespace TransmissionLine
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void transmissionLine1_Load(object sender, EventArgs e)
        {

        }

        private int time = 0;
        private double Reflect = 0;
        private double move = Math.PI/10;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (transmissionLine1.Limit < 350)
                transmissionLine1.Limit += move;
            else if (Reflect < 350)
                Reflect += move;
            if (transmissionLine1.Limit > 350) transmissionLine1.Limit = 350;
            if (Reflect > 350) Reflect = 350;
            for (var i = 0; i < 350 && i <= transmissionLine1.Limit; i++)
                transmissionLine1.points[i] = Program.In[time / 500D * Program.In.Time, i, (350 - i) <= Reflect];
            time++;

            transmissionLine1.Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            time = 0;
            Reflect = 0;
            SyncToIn();
            move = Program.In.W * Program.In.Time / 20 / Math.PI;
            transmissionLine1.Limit = 0;
        }

        void SyncToIn()
        {
            double temp_d;
            Program.In.A1 = Convert.ToDouble(nuda1.Value);
            Program.In.A2 = Convert.ToDouble(nuda2.Value);
            Program.In.Zl.Real = (double.TryParse(tbzlr.Text, out temp_d) ? temp_d : 0);
            Program.In.Zl.Imaginary = (double.TryParse(tbzli.Text, out temp_d) ? temp_d : 0);
            Program.In.W = (double.TryParse(tbw.Text, out temp_d) ? temp_d : 1);
            Program.In.Time = Convert.ToInt32(nudtime.Value);
        }

        private void transmissionLine1_MouseClick(object sender, MouseEventArgs e)
        {
            if (timer1.Enabled == false) return;
            var x = e.X;
            var index = (int)((x - transmissionLine1.width_percent * 5) / transmissionLine1.width_percent / 0.08);
            if (index < 0 || index >= 350) return;
            transmissionLine1.SelectedIndex = index;
            tbz.Text = index.ToString();
            double z0 = Program.In.Z0, b = Program.In.B, z = index * In.DB;
            Complex zl = Program.In.Zl;
            Complex zin = z0 * ((zl + Complex.J * z0 * Math.Tan(b * z)) / (z0 + Complex.J * zl * Math.Tan(b * z)));
            Complex eta = (zin - z0) / (zin + z0);
            tbeta.Text = eta.ToString("f2");
            tbr.Text = zin.ToString("f2");
        }
    }
}
