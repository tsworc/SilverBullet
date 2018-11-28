using Microsoft.Xna.Framework;
using MknGames;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilverBullet.Common
{
    public class Gun
    {
        public Vector3 pos;
        public Matrix rot = Matrix.Identity;
        public Vector3 size = new Vector3(.05f, .08f, 0.55f) * 2;
        //public Bullet[] bullets;
        //public Queue<Bullet> bullets;
        public List<Bullet> bullets;
        public int bulleti;
        public float bulletSpeed = 100;
        float _bulletSize;
        float _bulletLifespan;
        public bool bulletAffectedByGravity;
        public bool off = false;
        public bool isFullAuto = true;
        public bool requireTriggerUp;
        public bool isTriggerDown = false;
        //gonna need manual fire delay
        public float automaticFireDelayS = 0.15f;
        public float fireElapsed;
        public int bulletsPerShot = 1;
        int _startingBullets = 6;
        public float spreadConeAngle = MathHelper.ToRadians(3);
        public string filename = "";
        public float deathElapsed;
        public float deathDuration;
        public bool canRespawn;
        public bool fireAutomatically;

        public Gun(float bulletSize = 0.05f, int bulletCount = 12, bool hasGravity = true, float lifeSpan = 3)
        {
            bulleti = 0;
            this._bulletSize = bulletSize;
            this._startingBullets = bulletCount;
            bulletAffectedByGravity = hasGravity;
            _bulletLifespan = lifeSpan;
            bullets = new List<Bullet>();
            for (int i = 0; i < _startingBullets; ++i)
            {
                AddBullet();
            }
        }


        public float BulletSize
        {
            get { return _bulletSize; }
            set
            {
                _bulletSize = value;
                for (int i = 0; i < bullets.Count; ++i)
                {
                    bullets[i].size = _bulletSize;
                }
            }
        }

        public float BulletLifespan
        {
            get { return _bulletLifespan; }
            set
            {
                _bulletLifespan = value;
                for (int i = 0; i < bullets.Count; ++i)
                {
                    bullets[i].lifeSpan = _bulletLifespan;
                }
            }
        }

        //public void ClearBullets(List<Bullet> allbullets)
        //{
        //    for (int i = 0; i < bullets.Count; ++i)
        //    {
        //        allbullets.Remove(bullets[i]);
        //    }
        //}

        // gun init
        //public void AddBullets(List<Bullet> allbullets)
        //{
        //    //bullets = new Bullet[bulletCount];
        //    //bullets = new List<Bullet>();
        //    //for (int b = 0; b < bullets.Count; ++b)
        //    for(int i = 0; i < startingBullets; ++i)
        //    {
        //        GunAddBullet(allbullets);
        //    }
        //}

        public void Shutdown()
        {
            if (!off)
            {
                deathElapsed = 0;
                off = true;
            }
            else
            {
                Debugger.Break();
            }
        }

        //helper gun
        public Vector3 MakeForward()
        {
            return Vector3.Transform(Vector3.Forward, rot);
        }

        public bool TriggerDown(PhysicsState bodyState)
        {
            isTriggerDown = true;
            if (!requireTriggerUp && !off)
            {
                if (!isFullAuto)
                {
                    requireTriggerUp = true;
                }
                //TODO: solve the issue of elapsed = elapsed % delay being helpful sometimes and detrimental others
                //either fire manually or fire automatically after a delay
                if (!isFullAuto || fireElapsed >= automaticFireDelayS)
                {
                    return Shoot(bodyState.vel);
                }
            }
            return false;
        }
        //gun shoot
        public bool Shoot(Vector3 additionalVelocity = default(Vector3))
        {
            int checks = 0;
            int shots = 0;
            float spreadAngle = spreadConeAngle / 2;
            float bulletPatternStepAngle = 0;
            if (bulletsPerShot > 1)
            {
                bulletPatternStepAngle = MathHelper.TwoPi / (bulletsPerShot - 1);
            }
            //while (checks < bullets.Length && shots < bulletsPerShot)
            while (checks < bullets.Count && shots < bulletsPerShot)
            {
                Bullet b = bullets[bulleti++];
                //if (bulleti >= bullets.Length)
                if (bulleti >= bullets.Count)
                {
                    bulleti = 0;
                }
                if (b.off)
                {
                    //Vector2 s = GraphicsDevice.Viewport.Bounds.Size.ToVector2();
                    //Vector2 c = s / 2;
                    //    Func<float> rands = () => { return game1.randf(-spread, spread); };
                    //    Vector2 S = new Vector2(rands(), rands());
                    //    Ray r = camera.ScreenToRay(c + (s / 2 * S), GraphicsDevice.Viewport);
                    //b.p.pos = r.Position;
                    //b.p.vel = r.Direction * (10 + Vector3.Dot(bodyState.vel, r.Direction));
                    //b.p.pos = camera.Position3D;
                    //Vector3 d = camera.ScreenToWorld(c, 1, GraphicsDevice.Viewport);
                    //Vector3 v = d - b.p.pos;
                    //v.Normalize();
                    //b.p.vel = forward * (10 + Vector3.Dot(bodyState.vel, forward));
                    //b.p.vel = v * (10 + Vector3.Dot(bodyState.vel, v));
                    Vector3 gunf = Vector3.Transform(Vector3.Forward, rot);
                    b.phy.pos = pos + gunf * (size.Z / 2 + b.size / 2);
                    Vector3 bulletDir = gunf;
                    if (bulletsPerShot > 0 && shots > 0)
                    {
                        Vector3 spreadDir = new Vector3(
                            -(float)Math.Sin(spreadAngle),
                            0,
                            -(float)Math.Cos(spreadAngle));
                        float spreadDirRotation = bulletPatternStepAngle * (float)shots;
                        Vector3 spreadDirT = Vector3.Transform(spreadDir,
                            Matrix.CreateRotationZ(spreadDirRotation) * rot);

                        bulletDir = spreadDirT;
                        //bulletDir.Normalize();
                        //b.phy.pos += spreadDirT * b.size * 5;
                    }
                    //b.p.pos = gunpos;
                    //b.phy.vel = bulletDir * (bulletSpeed + Vector3.Dot(additionalVelocity, gunf));
                    //b.phy.vel = bulletDir * bulletSpeed + additionalVelocity;
                    b.phy.vel = bulletDir * bulletSpeed;
                    b.skipNextAdvance = true;
                    //b.direction = Vector3.Normalize(b.phy.vel);
                    //guneuler.X += MathHelper.Pi / 16.0f;
                    //recoile.X += MathHelper.Pi / 32.0f;
                    b.off = false;
                    b.t = 0;
                    fireElapsed = 0;
                    shots++;
                    //return true;
                }
                checks++;
            }
            if (shots > 0)
                return true;
            return false;
        }
        public void TriggerUp()
        {
            isTriggerDown = false;
            requireTriggerUp = false;
        }
        public bool getEmpty()
        {
            //for(int i = 0; i < bullets.Length;++i)
            for (int i = 0; i < bullets.Count; ++i)
            {
                if (bullets[i].off)
                    return false;
            }
            return true;
        }
        //gun update
        public void Update(float et)
        {
            if (canRespawn && off)
            {
                if (deathElapsed > deathDuration)
                    off = false;
                else
                    deathElapsed += et;
            }
            fireElapsed += et;
            if (fireAutomatically)
            {
                TriggerDown(new PhysicsState(1));
                TriggerUp();
            }
            Vector3 forward = Vector3.Transform(Vector3.Forward, rot);
            Vector3 up = Vector3.Transform(Vector3.Up, rot);
            Vector3 right = Vector3.Cross(forward, up);
            int offCount = 0;
            //for (int b = 0; b < bullets.Length; ++b)
            for (int b = 0; b < bullets.Count; ++b)
            {
                if (bullets[b].off)
                {
                    int z = offCount / bulletsPerShot;
                    int x = offCount % bulletsPerShot;
                    //Vector3 offset = gua * (size.Y/2) + gfa * (float)b * 0.1f;
                    float bSize = bullets[b].size;
                    float radius = bSize / 2;
                    Vector3 offset = forward * (this.size.Z / 2 - (float)z * bSize)
                        + right * (bullets[b].size * x) / 3
                        - right * (bullets[b].size * (float)bulletsPerShot / 2 - radius) / 3;
                    //Vector3 offset = gu / 10 + gf * (float)b * 0.1f;
                    //offset = Vector3.Transform(offset, gunrotlocal);
                    bullets[b].phy.pos = pos + offset;
                    bullets[b].phy.vel = Vector3.Zero;
                    bullets[b].phy.force = Vector3.Zero;
                    bullets[b].affectedByGravity = bulletAffectedByGravity;
                    offCount++;
                }
            }
        }
        // gun add bullet
        public Bullet AddBullet()//List<Bullet> allbullets)
        {
            Bullet b = new Bullet();
            b.size = _bulletSize;
            b.phy.mass = GameMG.getVolumeSphere(_bulletSize / 2) * 1000;
            b.affectedByGravity = bulletAffectedByGravity;
            b.lifeSpan = _bulletLifespan;
            bullets.Add(b);
            //if (allbullets != null)
            //    allbullets.Add(b);
            return b;
        }
        public void AddBullet(List<Bullet> bulletRegistrationList)
        {
            bulletRegistrationList.Add(AddBullet());
        }

        //gun to string
        public override string ToString()
        {
            return filename;
        }
    }
}
