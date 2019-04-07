using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace comparlize
{
    class tools
    {
        public List<car> list;
        private List<Delegate> dds
        {
            get { return dds; }
            set { dds = value; }
        }
        private List<EventArgs> args
        {
            get { return args; }
            set { args = value; }
        }
        string name
        {
            get { return name; }
            set { name = value; }
        }
        public Bitmap filter(string filterID, Bitmap imageToproces)//pure finctions
        {


            return null;
        }
        public Bitmap CompareAlgo(string algo, Bitmap img1, Bitmap img2)
        {


            return null;
        }
        public Bitmap filter(int filterID, Bitmap imageToproces)//pure finctions
        {


            return null;
        }
        public Bitmap CompareAlgo(int algo, Bitmap img1, Bitmap img2)
        {


            return null;
        }
        public tools()
        {
            list = new List<car>();
        }
        public List<Bitmap> Scan()// pure and safe
        {
            try
            {
                //open 
                List<Bitmap> images8 = new List<Bitmap>();
                DirectoryInfo d = new DirectoryInfo(@"\server");//Assuming Test is your Folder
                FileInfo[] Files = d.GetFiles("*.png"); //Getting Text files
                string str = "";
                foreach (FileInfo file in Files)
                {
                    str = str + ", " + file.Name;
                    images8.Add(new Bitmap(file.Name));
                }
                return images8;
            }
            catch (Exception) { return null; }

        }

        public string[] getListofCarsinserver()
        {
            List<string> files = new List<string>();


            string[] subdirs = Directory.GetDirectories(@"\server");

            return subdirs;
        }
        public List<Bitmap> Scan(string subServerfolder)// pure and safe
        {
            try
            {
                //open 
                List<Bitmap> images8 = new List<Bitmap>();
                DirectoryInfo d = new DirectoryInfo(@"\server\" + subServerfolder);//Assuming Test is your Folder
                FileInfo[] Files = d.GetFiles("*.jpeg"); //Getting Text files
                string str = "";
                foreach (FileInfo file in Files)
                {
                    str = str + ", " + file.Name;
                    Image openpng = Image.FromFile(file.FullName);
                    images8.Add(new Bitmap(openpng));
                }
                return images8;
            }
            catch (Exception) { return null; }

        }
        public List<Bitmap> DeltaCalculation(List<Bitmap> a, List<Bitmap> b)
        {
            try
            {
                List<Bitmap> newdeltalist = new List<Bitmap>();

                for (int i = 0; i < a.Count; i++)
                {
                    newdeltalist.Add(alg(a[i], b[i]));
                }

                return newdeltalist;
            }
            catch (Exception)
            {
                return null;
            }

        }
        public image DeltaCalculationi(List<Bitmap> a, List<Bitmap> b)
        {
            try
            {
                List<Bitmap> newdeltalist = new List<Bitmap>();
                image nl = new image(newdeltalist, DateTime.Now);
                for (int i = 0; i < a.Count; i++)
                {
                    newdeltalist.Add(alg(a[i], b[i], nl));
                }


                return nl;
            }
            catch (Exception)
            {
                return null;
            }

        }
        public Bitmap alg1(Bitmap a, Bitmap b)
        {
            return new Bitmap("");
        }
        public List<Bitmap> FullScan(List<Bitmap> a, string pathb)
        {
            return DeltaCalculation(a, Scan(pathb));
        }


        public List<Bitmap> FullScan(List<Bitmap> a, List<Bitmap> b)
        {
            return DeltaCalculation(a, b);
        }
        public image FullScani(List<Bitmap> a, List<Bitmap> b)
        {
            return DeltaCalculationi(a, b);
        }
        public List<Bitmap> FullScan(car c, int i)
        {
            return DeltaCalculation(c.list[i].img, c.list[i + 1].img);
        }
        public Bitmap alg(Bitmap bm1, Bitmap bm2)
        {
            List<dpoint> deltagroup = new List<dpoint>();
            int group = 1;

            int wid = Math.Min(bm1.Width, bm2.Width);
            int hgt = Math.Min(bm1.Height, bm2.Height);

            // Get the differences.
            int[,] diffs = new int[wid, hgt];

            int max_diff = 0;
            for (int x = 0; x < wid; x++)
            {
                for (int y = 0; y < hgt; y++)
                {
                    // Calculate the pixels' difference.
                    Color color1 = bm1.GetPixel(x, y);
                    Color color2 = bm2.GetPixel(x, y);
                    diffs[x, y] = (int)(
                        Math.Abs(color1.R - color2.R) +
                        Math.Abs(color1.G - color2.G) +
                        Math.Abs(color1.B - color2.B));
                    if (diffs[x, y] > max_diff)
                        max_diff = diffs[x, y];
                }
            }
            //max_diff = 255;
            dpoint p;
            Boolean added = false;
            // Create the difference image.
            Bitmap bm3 = new Bitmap(wid, hgt);
            for (int x = 0; x < wid; x++)
            {
                for (int y = 0; y < hgt; y++)
                {
                    if (diffs[x, y] != 0)
                    {
                        p = new dpoint();
                        p.p.X = x; p.p.Y = y;
                        p.value = diffs[x, y];
                        added = false;
                        if (deltagroup.Count == 0)
                        {
                            p.group = group;
                            deltagroup.Add(p);
                            added = true;
                        }
                        foreach (dpoint dd in deltagroup)
                        {

                            if (canGroupingEdges(p.p.X, dd.p.X) && canGroupingEdges(dd.p.Y, p.p.Y))
                            {
                                p.group = dd.group;
                                deltagroup.Add(p);
                                added = true;
                                break;
                            }
                        }
                        if (added == false)
                        {
                            p.group = ++group;
                            deltagroup.Add(p);
                        }
                    }
                    int clr = 255;

                    if (diffs[x, y] != 0)
                        clr = 255 - (int)(255.0 / max_diff * diffs[x, y]);

                    bm3.SetPixel(x, y, Color.FromArgb(clr, clr, clr));
                }
            }

            Graphics g = Graphics.FromImage(bm3);

            Font f = new Font("Times New Roman", 6);
            for (int i = 1; i <= group; ++i)
            {
                foreach (dpoint dd in deltagroup)
                {
                    if (dd.group == i)
                    {

                        g.DrawString("hit(" + dd.p.X + "," + dd.p.Y + ") v" + dd.value + "|" + dd.group, f, Brushes.Red, dd.p);
                        //listBox1.Items.Add("hit num(" + dd.group + ") in(" + dd.p.X + ", " + dd.p.Y + ") value: " + dd.value + ". ");
                        i++;
                    }
                }
            }


            // new pictureshow(bm1,bm3,bm2).Show();

            return bm3;
        }
        public Bitmap alg(Bitmap bm1, Bitmap bm2, image c)
        {
            List<dpoint> deltagroup = new List<dpoint>();

            int group = 1;

            int wid = Math.Min(bm1.Width, bm2.Width);
            int hgt = Math.Min(bm1.Height, bm2.Height);

            // Get the differences.
            int[,] diffs = new int[wid, hgt];

            int max_diff = 0;
            for (int x = 0; x < wid; x++)
            {
                for (int y = 0; y < hgt; y++)
                {
                    // Calculate the pixels' difference.
                    Color color1 = bm1.GetPixel(x, y);
                    Color color2 = bm2.GetPixel(x, y);
                    diffs[x, y] = (int)(
                        Math.Abs(color1.R - color2.R) +
                        Math.Abs(color1.G - color2.G) +
                        Math.Abs(color1.B - color2.B));
                    if (diffs[x, y] > max_diff)
                        max_diff = diffs[x, y];
                }
            }
            //max_diff = 255;
            dpoint p;
            Boolean added = false;
            // Create the difference image.
            Bitmap bm3 = new Bitmap(wid, hgt);
            for (int x = 0; x < wid; x++)
            {
                for (int y = 0; y < hgt; y++)
                {
                    if (diffs[x, y] != 0)
                    {
                        p = new dpoint();
                        p.p.X = x; p.p.Y = y;
                        p.value = diffs[x, y];
                        added = false;
                        if (deltagroup.Count == 0)
                        {
                            p.group = group;
                            deltagroup.Add(p);
                            added = true;
                        }
                        foreach (dpoint dd in deltagroup)
                        {

                            if (canGroupingEdges(p.p.X, dd.p.X) && canGroupingEdges(dd.p.Y, p.p.Y))
                            {
                                p.group = dd.group;
                                deltagroup.Add(p);
                                added = true;
                                break;
                            }
                        }
                        if (added == false)
                        {
                            p.group = ++group;
                            deltagroup.Add(p);
                        }
                    }
                    int clr = 255;

                    if (diffs[x, y] != 0)
                        clr = 255 - (int)(255.0 / max_diff * diffs[x, y]);

                    bm3.SetPixel(x, y, Color.FromArgb(clr, clr, clr));
                }
            }

            Graphics g = Graphics.FromImage(bm3);

            Font f = new Font("Times New Roman", 6);
            for (int i = 1; i <= group; ++i)
            {
                foreach (dpoint dd in deltagroup)
                {
                    if (dd.group == i)
                    {

                        g.DrawString("hit(" + dd.p.X + "," + dd.p.Y + ") v" + dd.value + "|" + dd.group, f, Brushes.Red, dd.p);
                        //listBox1.Items.Add("hit num(" + dd.group + ") in(" + dd.p.X + ", " + dd.p.Y + ") value: " + dd.value + ". ");
                        i++;
                    }
                }
            }


             new pictureshow(bm1,bm3,bm2).Show();
            c.dplist.Add(deltagroup);
            return bm3;
        }
        private bool canGroupingEdges(int t, int t2)
        {
            return Math.Abs(t - t2) <= 2;
        }
    }
}
