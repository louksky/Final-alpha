using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;

using System.Windows.Forms;
namespace comparlize
{
    class image
    {
        public List<Bitmap> img;
        public List<List<dpoint>> dplist;
        public DateTime date;
        public int id;
        public int nid, pid;
        public int did;
        public image(List<Bitmap> image, DateTime date)
        {
            if (image != null)
                this.img = image;
            else
                img = new List<Bitmap>();
            this.date = date;
            dplist = new List<List<dpoint>>();
        }
    }
}
