using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChislMethod
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            chart1.Visible = false;
        }
        delegate Massive RigthPart(double x, Massive y);
        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            chart1.Visible = true;
            var y = new Massive(2);
            var x0 = Convert.ToDouble(textBox1.Text);
            y[0] = Convert.ToDouble(textBox2.Text);
            y[1] = Convert.ToDouble(textBox3.Text);
            var h = Convert.ToDouble(textBox4.Text);
            var xn = Convert.ToDouble(textBox5.Text);
            if (comboBox1.SelectedIndex == 0)
            {
                Eiler(x0, xn, h, y, FProizvd);

            }
            else if (comboBox1.SelectedIndex == 1)
            {
                ModifyEiler(x0, xn, h, y, FProizvd);
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                RungeKutta(x0, xn, h, y, FProizvd);
            }
            else if (comboBox1.SelectedIndex == 3)
            {
                Adam(x0, xn, h, y, FProizvd);
            }



        }
        private void Eiler(double x0, double xn, double h, Massive y, RigthPart f)
        {
            chart1.Series.Add("original");
            chart1.Series.Add("Производная");
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            var dy = new Massive(2);
            for (double x = x0; x < xn; x = x + h)
            {
                dy = f(x, y);
                y = y + (dy * h);
                chart1.Series[0].Points.AddXY(x, y[0]);
                chart1.Series[1].Points.AddXY(x, y[1]);
            }

        }
        private void ModifyEiler(double x0, double xn, double h, Massive y, RigthPart f)
        {
            chart1.Series.Add("original");
            chart1.Series.Add("Производная");
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            var dy = new Massive(2);
            for (double x = x0; x < xn; x = x + h)
            {
                dy = f(x, y);
                y = y + (dy + f(x + h, y + dy * h)) * h / 2.0;
                chart1.Series[0].Points.AddXY(x, y[0]);
                chart1.Series[1].Points.AddXY(x, y[1]);
            }

        }
        private void RungeKutta(double x0, double xn, double h, Massive y, RigthPart f)
        {
            chart1.Series.Add("original");
            chart1.Series.Add("Производная");
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            var k1 = new Massive(2);
            var k2 = new Massive(2);
            var k3 = new Massive(2);
            var k4 = new Massive(2);
            for (double x = x0; x < xn; x = x + h)
            {
                k1 = f(x, y);
                k2 = f(x + h * 0.5, y + k1 * h * 0.5);
                k3 = f(x + h * 0.5, y + k2 * h * 0.5);
                k4 = f(x + h, y + k3 * h);
                y = y + (k1 + k2 * 2 + k3 * 2 + k4) * h / 6.0;

                chart1.Series[0].Points.AddXY(x, y[0]);
                chart1.Series[1].Points.AddXY(x, y[1]);
            }
        }
        //странный результат 
         private void Adam(double x0, double xn, double h, Massive y, RigthPart f)
         {
             chart1.Series.Add("original");
             chart1.Series.Add("Производная");
             chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
             chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

             var y1 = new Massive(2);
             var y2 = new Massive(2);
             var y3 = new Massive(2);
             var y4 = new Massive(2);
             var y5 = new Massive(2);
             for (double x = x0; x < xn; x = x + h)
             {

                 y1 = y + (f(x, y) * h);
                 y2 = y1 + ((f(x, y1) * 3 / 2) - (f(x, y) * 0.5)) * h;
                 y3 = y2 + ((f(x, y2) * 23 / 12) - (f(x, y1) * 4 / 3) + (f(x, y) * 5 / 12)) * h;
                 y4 = y3 + ((f(x, y3) * 55 / 24) - (f(x, y2) * 59 / 24) + (f(x, y1) * 37 / 24) - (f(x, y) * 3 / 8)) * h;
                 y5 = y4 + ((f(x, y4) * 1901 / 720) - (f(x, y3) * 1387 / 360) + (f(x, y2) * 109 / 30) - (f(x, y1) * 637 / 360) + (f(x, y) * 251 / 720)) * h;

                 chart1.Series[0].Points.AddXY(x, y5[0]);
                 chart1.Series[1].Points.AddXY(x, y5[1]);
             }
         }

        private Massive FProizvd(double x, Massive y)
        {
            var dy = new Massive(2);
            dy[0] = y[1];
            dy[1] = -x * x * y[1] - 2*y[0] - x;
            return dy;
        }





        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {


        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!double.TryParse(textBox1.Text, out double a))
            {
                textBox1.Text = "1";
            }
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!(Char.IsDigit(e.KeyChar)) && !((e.KeyChar == '-')) && !((e.KeyChar == ',')))
            {
                e.Handled = true;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (!double.TryParse(textBox3.Text, out double a))
            {
                textBox3.Text = "1";
            }
        }
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!(Char.IsDigit(e.KeyChar)) && !((e.KeyChar == '-')) && !((e.KeyChar == ',')))
            {
                e.Handled = true;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (!double.TryParse(textBox2.Text, out double a))
            {
                textBox2.Text = "1";
            }
        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!(Char.IsDigit(e.KeyChar)) && !((e.KeyChar == '-')) && !((e.KeyChar == ',')))
            {
                e.Handled = true;
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (!double.TryParse(textBox4.Text, out double a))
            {
                textBox4.Text = "1";
            }
        }
        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!(Char.IsDigit(e.KeyChar)) && !((e.KeyChar == '-')) && !((e.KeyChar == ',')))
            {
                e.Handled = true;
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (!double.TryParse(textBox5.Text, out double a))
            {
                textBox5.Text = "1";
            }
        }
        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!(Char.IsDigit(e.KeyChar)) && !((e.KeyChar == '-')) && !((e.KeyChar == ',')))
            {
                e.Handled = true;
            }
        }
    }
}
