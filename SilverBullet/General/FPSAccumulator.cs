using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilverBullet.General
{
    public class FPSAccumulator
    {
        public float spf;
        public float sum;
        public float counter;
        public int iterations = 60;
        public float fps;
        public void Update(float et)
        {
            sum += et;
            counter++;
            if (counter >= iterations)
            {
                spf = sum / counter;
                fps = 1 / spf;
                counter = 0;
                sum = 0;
            }
        }
    }
}
