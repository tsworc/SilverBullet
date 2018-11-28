using MknGames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilverBullet.Common
{
    public class Bullet
    {
        /// <summary>
        /// physics state
        /// </summary>
        public PhysicsState phy;
        /// <summary>
        /// is inactive
        /// </summary>
        public bool off;
        public bool affectedByGravity;
        //public bool inContactOld;
        //public bool inContact;
        /// <summary>
        /// time spent active
        /// </summary>
        public float t;
        public float lifeSpan = 3;
        public bool skipNextAdvance;
        public float size = 0.05f;
        //public Vector3 direction;
        //public Vector3 incomingPosition;
        //public Vector3 incomingVelocity;
        public static ulong instantiationCounter = 0;
        public ulong id;

        public Bullet()
        {
            id = instantiationCounter++;
            phy = new PhysicsState(1);
            off = true;
            t = 0;
            affectedByGravity = true;
        }

        public bool isTooFast(float et)
        {
            float limit = size;// * 4;
            float deltasq = (phy.vel * et).LengthSquared();
            return deltasq >= limit * limit;
        }
        public bool isSolid(float et)
        {
            return off || !isTooFast(et);
        }
        public bool isRay(float et)
        {
            return !off && isTooFast(et);
        }

        public override string ToString()
        {
            return string.Format("Off: {0}, Adv: {1}", off, skipNextAdvance);
        }
    }
}
