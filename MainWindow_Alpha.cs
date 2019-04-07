using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace comparlize
{
    public partial class MainWindow_Alpha : Form
    {
        List<car> list;
        car c;
        static tools p = new tools();
        public MainWindow_Alpha()
        {
            InitializeComponent();
           
            list = new List<car>();
            //handle init
            TimeInit();
            carlistinit();
        }

        private void carlistinit()
        {
            string[] strs = p.getListofCarsinserver();
            foreach (string s in strs)
                comboBox1.Items.Add(s);
           
        }

        private void TimeInit()
        {
           c= new car("1128");
            image img = new image(p.Scan("t1"), DateTime.Now);
            c.list.Add(img);
            list.Add(c);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            c.list.Add(new image(p.Scan("t2"), DateTime.Now));
            c.dlist.Add(p.FullScani(c.list[0].img, c.list[1].img));


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            int indexi = 0,indexj=0;
            PictureBox p = (PictureBox)sender;
          
            switch (p.Name)
            {
                case "pictureBox1":
                    indexi= 0; indexj = 0;
                    break;
                case "pictureBox2":

                    indexi = 0;  indexj = 1;
                    break;
                case "pictureBox6":
                    indexi = 0; indexj = 2;
                    break;
                case "pictureBox3":
                    indexi = 0; indexj = 3;
                    break;
                case "pictureBox10":
                    indexi = 0; indexj = 4;
                    break;
                case "pictureBox9":
                    indexi = 0; indexj = 5;
                    break;
                case "pictureBox8":
                    indexi = 0; indexj = 6;
                    break;
                case "pictureBox7":
                    indexi = 0; indexj = 7;
                    break;
            }
            pictureBox12.Image = c.list[indexi].img[indexj];
            pictureBox4.Image = c.dlist[indexi].img[indexj];
            pictureBox11.Image = c.list[indexi + 1].img[indexj];

            //data set to screen
            label5.Text = c.dlist[indexi].dplist[indexj].Count.ToString();
            try
            {
                label6.Text = c.dlist[indexi].dplist[indexj].ElementAt(c.dlist[indexi].dplist[indexj].Count - 1).group.ToString();

                label8.Text =(double.Parse(label5.Text)/ double.Parse(label6.Text)).ToString();
                if(label8.Text.Length > 5)
                {
                    label8.Text = label8.Text.Substring(0, 5);
                }
            }
            catch (Exception) { }


            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox11.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox12.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            new Poc().Show();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void welcome(object sender, EventArgs e)
        {
            Thread.Sleep(2200);
            panel3.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string str="";
            folderBrowserDialog1.ShowDialog();
            str = folderBrowserDialog1.SelectedPath;
            LaunchCommandLineApp(str);
        }
        static void LaunchCommandLineApp(string str)
        {
           

            // Use ProcessStartInfo class
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.FileName = @"C:\Users\asafl\Source\Repos\Final\comparlize\dist\filter\filter.exe";
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.Arguments = str ;

            try
            {
                // Start the process with the info we specified.
                // Call WaitForExit and then the using statement will close.
                using (Process exeProcess = Process.Start(startInfo))
                {
                    exeProcess.WaitForExit();
                }
            }
            catch
            {
                // Log error.
            }
        }
    }
}
