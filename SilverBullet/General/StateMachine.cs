using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilverBullet.General
{
    public class StateMachine
    {
        public int state;
        public int previousState;
        public float stateStartTime;
        public static float totalGameTime;

        public StateMachine()
        {

        }

        //WARN: may not work as expected for the state == 0
        public float getElapsed()
        {
            return totalGameTime - stateStartTime;
        }

        public void ChangeState(int newState)
        {
            previousState = state;
            state = newState;
            stateStartTime = totalGameTime;
        }
        public void Increment()
        {
            ChangeState(state + 1);
        }
        public void DecrementState()
        {
            ChangeState(state - 1);
        }
    }
}
