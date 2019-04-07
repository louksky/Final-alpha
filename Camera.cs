using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace comparlize
{
    class Camera
    {
        private string name
        {
            get { return name; }
            set { name = value; }
        }
        private string model
        {
            get { return model; }
            set { model = value; }
        }
        private string con
        {
            get { return con; }
            set { con = value; }
        }
        private List<string> sensors
        {
            get { return sensors; }
            set { sensors = value; }
        }
        public Camera(List<string> sensors_, string name_, string con_, string model)
        {
            this.model = model;
            this.name = name_;
            this.con = con_;
            sensors = sensors_;


        }
    }
}
