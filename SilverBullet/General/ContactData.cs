using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilverBullet.General
{
    public class ContactData
    {
        public Vector3 contact;
        public Vector3 norm;
        public float pen;
        //public float restitution = 0;
        public override string ToString()
        {
            return string.Format("Contact: {2}, Norm: {0}, Pen: {1}",
                norm, pen, contact);
        }
    }
}
