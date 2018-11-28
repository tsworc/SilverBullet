using Microsoft.Xna.Framework;
using MknGames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilverBullet.Common
{
    public class Box
    {
        //public BoxType type = BoxType.REFLECT;
        //public float mass = 1;
        //public Vector3 velocity;
        //public Vector3 force;
        //public Vector3 originalPosition;
        //public bool ignoresBullets = false;
        private Vector3 _size;
        private Vector3 _position;
        //public Vector3 position0;
        public Color color;
        //public bool embedsBulletOnImpact = false;
        //public bool isdoor;
        //public bool isnew = true;
        //public float restitution = 0;
        public ulong id = 0;
        public static ulong instantiationCounter = 0;

        public BoundingBox boundingBox { get; private set; }

        // box properties
        public Vector3 position
        {
            get { return _position; }
            set
            {
                _position = value;
                _UpdateBoundingBox();
            }
        }
        public Vector3 size
        {
            get { return _size; }
            set
            {
                _size = value;
                _UpdateBoundingBox();
            }
        }

        //box constructor
        public Box()
        {
            id = instantiationCounter++;
        }
        public Box(Vector3 size, Vector3 position)
        {
            id = instantiationCounter++;
            this._size = size;
            this._position = position;
            //originalPosition = this._position;
            _UpdateBoundingBox();
        }
        public Box(Box copyBox)
        {
            id = instantiationCounter++;
            this._size = copyBox.size;
            this._position = copyBox.position;
            //originalPosition = this._position;
            this.color = copyBox.color;
            //this.type = copyBox.type;
            //this.ignoresBullets = copyBox.ignoresBullets;
            _UpdateBoundingBox();
        }
        void _UpdateBoundingBox()
        {
            boundingBox = GameMG.MakeBox(position, size);
        }
        public override string ToString()
        {
            return string.Format("Id: {0} | Pos: {1} | Size: {2}", id, _position, _size);
        }
        //public void GetMaterialProperties(out float friction, out float velocityCofactor, out bool positionCorrecting)
        //{
        //    positionCorrecting = true;
        //    switch (type)
        //    {
        //        case BoxType.REFLECT:
        //            velocityCofactor = 1;
        //            friction = 0;
        //            break;
        //        case BoxType.STICK:
        //            velocityCofactor = 0;
        //            friction = 1;
        //            break;
        //        case BoxType.FLATTEN:
        //            friction = 1;
        //            velocityCofactor = 1;
        //            break;
        //        case BoxType.SLIDE:
        //            velocityCofactor = 0;
        //            friction = 0;
        //            break;
        //        case BoxType.SLOW:
        //            velocityCofactor = -0.95f;
        //            friction = 0;
        //            positionCorrecting = false;
        //            break;
        //        case BoxType.DEFAULT:
        //            velocityCofactor = 0;
        //            friction = 0.03f;
        //            break;
        //        default:
        //            friction = -10;
        //            velocityCofactor = -10;
        //            break;
        //    }
        //}
    }
}
