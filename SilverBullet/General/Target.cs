using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilverBullet.Common
{
    public class Target
    {
        //public Vector3 Center
        //{
        //    get { return phy.pos; }
        //    set { phy.pos = value; }
        //}
        public Vector3 Center;
        public float Radius;
        //public bool isCube;
        public bool off;
        //public PhysicsState phy;
        public Vector3 movementTarget;

        public Target(Vector3 center, float radius)
        {
            //phy = new PhysicsState(1);
            this.Center = center;
            this.Radius = radius;
        }
    }
}
