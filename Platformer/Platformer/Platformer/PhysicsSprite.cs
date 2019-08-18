using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Platformer
{
    public class PhysicsSprite : CollisionSprite
    {
        protected float gX, gY = 0.1f, aX, aY, vX, vY;
        public static Boolean inAir = true;
        public static Boolean isTouchingbool = false;

        enum MotionModels { Static, Kinematic, Dynamic };

        MotionModels MotionModel = MotionModels.Dynamic;

        public float Gx
        {
            get { return gX; }
            set { gX = value; }
        }

        public float Gy
        {
            get { return gY; }
            set { gY = value; }
        }

        public float Ax
        {
            get { return aX; }
            set { aX = value; }
        }

        public float Ay
        {
            get { return aY; }
            set { aY = value; }
        }

        public float Vx
        {
            get { return vX; }
            set { vX = value; }
        }

        public float Vy
        {
            get { return vY; }
            set { vY = value; }
        }

        public PhysicsSprite(Image image) : base(image)
        {

        }

        public PhysicsSprite(Image image, int x, int y) : base(image)
        {
            X = x;
            Y = y;
        }

        public void setMotionModel(int type)
        {
            if (type == 0) MotionModel = MotionModels.Static;
            if (type == 1) MotionModel = MotionModels.Kinematic;
            if (type == 2) MotionModel = MotionModels.Dynamic;
        }

        public override void act()
        {
            /*
            List<CollisionSprite> list = getCollisions();
            foreach (CollisionSprite s in list)
            {
                if (s.GetType() == typeof(Goal)) isTouchingbool = true;
            }*/
            if (MotionModel == MotionModels.Dynamic)
            {
                Vx += Ax + Gx;
                Vy += Ay + Gy;
            }
            if (MotionModel != MotionModels.Static)
            {
                X += Vx;
                if (getCollisions().Count > 0)
                {
                    X -= Vx;
                }
            }
            if (MotionModel != MotionModels.Static)
            {
                Y += Vy;
                if (getCollisions().Count > 0)
                {
                    inAir = false;
                    Y -= Vy;
                    Vy = 0;
                }
            }

        }

        public override void paint(Graphics g)
        {
            base.paint(g);
        }
    }
}