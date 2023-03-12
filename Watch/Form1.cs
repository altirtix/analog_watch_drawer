using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Watch
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Graphics g = panel1.CreateGraphics();
                g.Clear(Color.White);

                Pen pen = new Pen(Color.Black, 2);
                SolidBrush brush = new SolidBrush(Color.Black);

                int hour = Convert.ToInt32(comboBox1.SelectedItem);
                int minute = Convert.ToInt32(comboBox2.SelectedItem);
                int second = Convert.ToInt32(comboBox3.SelectedItem);

                double hourAngle = (hour * 30 - 90) * 3.14 / 180;
                double minuteAngle = (minute * 6 - 90) * 3.14 / 180;
                double secondAngle = (second * 6 - 90) * 3.14 / 180;

                int lenghtHour = 30;
                int lenghtMinute = 60;
                int lenghtSecond = 90;

                float x1 = panel1.Width / 2;
                float y1 = panel1.Height / 2;
                float x2 = Convert.ToSingle((panel1.Width / 2) * Math.Cos(hourAngle) * lenghtHour);
                float y2 = Convert.ToSingle((panel1.Height / 2) * Math.Sin(hourAngle) * lenghtHour);
                float x3 = panel1.Width / 2;
                float y3 = panel1.Height / 2;
                float x4 = Convert.ToSingle((panel1.Width / 2) * Math.Cos(minuteAngle) * lenghtMinute);
                float y4 = Convert.ToSingle((panel1.Height / 2) * Math.Sin(minuteAngle) * lenghtMinute);
                float x5 = panel2.Width / 2;
                float y5 = panel2.Height / 2;
                float x6 = Convert.ToSingle((panel2.Width / 2) * Math.Cos(secondAngle) * lenghtSecond);
                float y6 = Convert.ToSingle((panel2.Height / 2) * Math.Sin(secondAngle) * lenghtSecond);

                Pen penHour = new Pen(Color.Black, 3);
                g.DrawLine(penHour, x1, y1, x2, y2);
                
                Pen penMinute = new Pen(Color.Black, 1);
                g.DrawLine(penMinute, x3, y3, x4, y4);

                Pen penSecond = new Pen(Color.Red, 1);
                g.DrawLine(penSecond, x5, y5, x6, y6);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Something went wrong!" + "\r\n" + ex.ToString(),
                    "Message",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Graphics g = panel1.CreateGraphics();
            g.Clear(Color.White);
            
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();

            comboBox1.Items.AddRange(Enumerable.Range(1, 12).Cast<object>().ToArray());
            comboBox2.Items.AddRange(Enumerable.Range(0, 60).Cast<object>().ToArray());
            comboBox3.Items.AddRange(Enumerable.Range(0, 60).Cast<object>().ToArray());

            comboBox1.SelectedIndex = 11;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;

            button1_Click(sender, e);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Graphics g = panel1.CreateGraphics();
            g.Clear(Color.White);

            comboBox1.Items.Clear();
            comboBox2.Items.Clear();

            comboBox1.Items.AddRange(Enumerable.Range(0, 24).Cast<object>().ToArray());
            comboBox2.Items.AddRange(Enumerable.Range(0, 60).Cast<object>().ToArray());
            comboBox3.Items.AddRange(Enumerable.Range(0, 60).Cast<object>().ToArray());

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;


            button1_Click(sender, e);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            radioButton1_CheckedChanged(sender, e);
            disableControls(this.Controls);
            osToolStripMenuItem.Text = SystemInfo.getOS();
            lANIPToolStripMenuItem.Text = SystemInfo.getLANIP();
            wANIPToolStripMenuItem.Text = SystemInfo.getWANIP();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            string filename = openFileDialog.FileName;
            string fileText = System.IO.File.ReadAllText(filename);
            //textBox1.Text = fileText;
            MessageBox.Show("File is opened!");
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            string filename = saveFileDialog.FileName;
            System.IO.File.WriteAllText(filename, Convert.ToString(DateTime.Now));
            MessageBox.Show("File is saved!");
        }

        public void clearControls(Control.ControlCollection ctrlCollection)
        {
            foreach (Control ctrl in ctrlCollection)
            {
                if (ctrl is TextBoxBase)
                {
                    ctrl.Text = String.Empty;
                }
                else
                {
                    clearControls(ctrl.Controls);
                }
            }
        }

        public void enableControls(Control.ControlCollection ctrlCollection)
        {
            foreach (Control ctrl in ctrlCollection)
            {
                if (ctrl is TextBoxBase)
                {
                    ctrl.Enabled = true;
                }
                else if (ctrl is ButtonBase)
                {
                    ctrl.Enabled = true;
                }
                else if (ctrl is ComboBox)
                {
                    ctrl.Enabled = true;
                }
                else
                {
                    enableControls(ctrl.Controls);
                }
            }
        }

        public void disableControls(Control.ControlCollection ctrlCollection)
        {
            foreach (Control ctrl in ctrlCollection)
            {
                if (ctrl is TextBoxBase)
                {
                    ctrl.Enabled = false;
                }
                else if (ctrl is ButtonBase)
                {
                    ctrl.Enabled = false;
                }
                else if (ctrl is ComboBox)
                {
                    ctrl.Enabled = false;
                }
                else
                {
                    disableControls(ctrl.Controls);
                }
            }
        }

        private void unlockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            enableControls(this.Controls);
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clearControls(this.Controls);
        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "Analog Watch Drawer\r\n"
                        + "Artur Zhadan\r\n"
                        + "2020",
                        "Message",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            dateToolStripMenuItem.Text = SystemInfo.getDate();
            timeToolStripMenuItem.Text = SystemInfo.getTime();
            stopwatchToolStripMenuItem.Text = SystemInfo.getStopwatch();

            Graphics g = panel2.CreateGraphics();
            g.Clear(Color.White);

            Pen pen = new Pen(Color.Black, 2);
            SolidBrush brush = new SolidBrush(Color.Black);

            int hour = Convert.ToInt32(DateTime.Now.Hour);
            int minute = Convert.ToInt32(DateTime.Now.Minute);
            int second = Convert.ToInt32(DateTime.Now.Second);

            double hourAngle = (hour * 30 - 90) * 3.14 / 180;
            double minuteAngle = (minute * 6 - 90) * 3.14 / 180;
            double secondAngle = (second * 6 - 90) * 3.14 / 180;

            int lenghtHour = 30;
            int lenghtMinute = 60;
            int lenghtSecond = 90;

            float x1 = panel2.Width / 2;
            float y1 = panel2.Height / 2;
            float x2 = Convert.ToSingle((panel2.Width / 2) * Math.Cos(hourAngle) * lenghtHour);
            float y2 = Convert.ToSingle((panel2.Height / 2) * Math.Sin(hourAngle) * lenghtHour);
            float x3 = panel2.Width / 2;
            float y3 = panel2.Height / 2;
            float x4 = Convert.ToSingle((panel2.Width / 2) * Math.Cos(minuteAngle) * lenghtMinute);
            float y4 = Convert.ToSingle((panel2.Height / 2) * Math.Sin(minuteAngle) * lenghtMinute);
            float x5 = panel2.Width / 2;
            float y5 = panel2.Height / 2;
            float x6 = Convert.ToSingle((panel2.Width / 2) * Math.Cos(secondAngle) * lenghtSecond);
            float y6 = Convert.ToSingle((panel2.Height / 2) * Math.Sin(secondAngle) * lenghtSecond);

            Pen penHour = new Pen(Color.Black, 3);
            g.DrawLine(penHour, x1, y1, x2, y2);

            Pen penMinute = new Pen(Color.Black, 1);
            g.DrawLine(penMinute, x3, y3, x4, y4);

            Pen penSecond = new Pen(Color.Red, 1);
            g.DrawLine(penSecond, x5, y5, x6, y6);
        }
    }
}
