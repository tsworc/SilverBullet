using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MknGames;
using MknGames.FPSWahtever;
using MknGames.Split_Screen_Dungeon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilverBullet.General
{
    public class Player
    {
        public Vector2 gv; //player aim center
        public Matrix untransformedProjection;
        ////public float jetfuel = 50;
        ////public float jetfuelrechargerate = 5;
        ////public float playerFriction = 1;
        ////public float gunpowder = 
        public CameraState playerCam = new CameraState();
        public PhysicsState bodyState = new PhysicsState(1);
        public Vector3 playerSpawnPoint;
        public Vector3 playerSpawnEuler;
        //public bool wasInContact;
        public bool is3rdPerson = false;
        public float height = 3;
        public float dropValue;
        public Vector3 drop;
        //float turnElapsed = 0;
        //float turnCap = 0.5f;
        public int sphereBodyCount = 6;
        public int bodyc;
        public float moveSpeed;
        int stance = 0;
        int stance0 = 0;
        bool stanceChanged = false;
        bool stancekeyheld = false;
        public FPSInput localInput0;
        public FPSInput localInput;
        public int myGunLimit = 1;
        public Vector3 returnLoc;
        public float groundJumpSpeed = 0;
        public float jumpSpeed = 0;
        public float playerFriction = 0;
        public float playerFrictionStopped = 0;
        public float playerFrictionAir = 0;
        public float playerFrictionOverride = 0;
        public float playerInputAccelRate = 0;
        public float playerInputDecelRate = 0;
        public float playerMoveInputElapsed = 0;
        public float playerTerminalVelocity = 0;
        public float playerWalkBoost = 0;
        public float playerRunBoost = 0;
        public float playerFlyBoost = 0;
        public float playerGunForwardOffset = 0;
        public bool playerRequestFrictionOverride = false;
        public bool playerLeftHandy = false;
        public bool playerCanSwapToEmpty = false;
        public const int playerHolsterOcto = 0;
        public const int playerHolsterAdventurer = 1;
        public int playerGunHolsterFormation = playerHolsterOcto;

        float crawlSpeed = 200;
        float crouchSpeed = 500;
        float standSpeed = 1000;

        public Player()
        {
            height = 3;
            dropValue = height / (float)sphereBodyCount;
            drop = Vector3.Down * dropValue;
            bodyc = 6;
        }

        public void UpdateStanceInput(GameMG game1, bool controlDown, bool shiftDown)
        {
            Action<int, float, int> changeStance = (int spherecount, float speed, int stancelabel) =>
            {
                int delta = spherecount - bodyc;
                bodyc += delta;
                moveSpeed = speed;
                //move up or down for each sphere
                bodyState.pos += (Vector3.Up * (height / 6 * delta)) / 2;
                stance0 = stance;
                stance = stancelabel;
                stanceChanged = true;
            };
            if (game1.kclick(Keys.C) && !controlDown)
            {
                if (stance == 0)//standing
                {
                    //crouch
                    changeStance(sphereBodyCount - 2, crouchSpeed, 1);
                }
                if (stance == 2)
                {
                    //stand
                    changeStance(sphereBodyCount - 2, crouchSpeed, 1);
                }
            }
            if (game1.kheld(Keys.C) && !controlDown)
            {
                if (stance == 0 || stance == 1)
                {
                    if (!stanceChanged || stance0 == 0) //allow stand>crouch>prone but not prone>stand>prone
                    {
                        changeStance(sphereBodyCount - 4, crawlSpeed, 2);
                    }
                }
                stancekeyheld = true;
            }
            if (game1.krelease(Keys.C) && !controlDown)
            {
                if (stance == 1 && !stancekeyheld && !stanceChanged)
                {
                    changeStance(sphereBodyCount, standSpeed, 0);
                }
                stancekeyheld = false;
                stanceChanged = false;
            }
        }
    }
}
