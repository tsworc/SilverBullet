using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilverBullet.Common
{
    public class SmallFPSEditState
    {
        public List<Box> boxes = new List<Box>();
        public Box hoverbox0;
        public Box hoverbox;
        public int hoverTarget = -1;
        public Vector3 hoverboxContact;
        public bool active;
        public bool occludeTargets = true;
        public Vector3 hoverboxNormal;
        public bool saveNeeded = false;
        public int target = -1;
    }
}
